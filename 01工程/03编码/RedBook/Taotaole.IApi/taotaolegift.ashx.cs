using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Cache;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;
using YH.Weixin.MP.Messages;
using YH.Weixin.Pay;

namespace Taotaole.IApi
{
    /// <summary>
    /// 积分商城相关
    /// </summary>
    public class taotaolegift : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "application/json";
                string action = context.Request["action"];
                switch (action)
                {
                    case "getScoreNums":         //获取积分场区间
                        getScoreNums(context);
                        break;
                    case "getGiftList": //获取待开奖商品
                        getGiftList(context);
                        break;
                    case "goGiftDraw": //跑马灯,大转盘抽奖
                        goGiftDraw(context);
                        break;
                    case "goGiftGua": //刮刮乐抽奖
                        goGiftGua(context);
                        break;
                    case "getScoreDetail"://积分使用详情
                        getScoreDetail(context);
                        break;
                    case "giftWinDetail"://奖品详情
                        giftWinDetail(context);
                        break;
                    case "giftGameIsOpen"://游戏是否开启
                        giftGameIsOpen(context);
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }
        /// <summary>
        /// 游戏是否开启
        /// </summary>
        /// <param name="context"></param>
        private void giftGameIsOpen(HttpContext context)
        {
            string Score = context.Request["Score"];
            string gameType = context.Request["gameType"];
            if (string.IsNullOrEmpty(Score) || string.IsNullOrEmpty(gameType))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            bool isOpen = go_giftsBusiness.isGameOpen(Score, gameType);
            string result = string.Format("{{\"isOpen\":\"{0}\"}}", isOpen);
            context.Response.Write(result);

        }
        /// <summary>
        /// 刮刮乐抽奖
        /// </summary>
        private void goGiftGua(HttpContext context)
        {
            #region 参数验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            //场次
            string giftid = context.Request["giftid"];
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(giftid) || !PageValidate.IsNumberPlus(giftid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            go_memberEntity currentMember = go_memberBusiness.LoadEntity(Convert.ToInt32(uid));
            go_giftsEntity gift = go_giftsBusiness.LoadEntity(int.Parse(giftid));
            //如果传过来的场次比自己当前的积分要大,返回非法操作
            if (currentMember.Score < gift.Scope)
            {
                context.Response.Write("{\"state\":3}"); //积分不足
            }
            #endregion

            //计算区间


            string orderid = ShopOrders.GenerateOrderID(ShopOrders.BusinessType.gift);
            int[] resultArr = go_giftsBusiness.goGiftGua(orderid, currentMember.Uid, gift.GiftId);

            string result = string.Format("{{\"state\":{0},\"isWon\":{1},\"orderId\":\"{2}\"}}", resultArr[0], resultArr[1], resultArr[1] != -1 ? orderid : "");
            context.Response.Write(result);
        }

        /// <summary>
        /// 开始抽奖
        /// </summary>
        private void goGiftDraw(HttpContext context)
        {
            #region 参数验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            //场次
            string maxScore = context.Request["maxScore"];
            string gameType = context.Request["gameType"];
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(maxScore) || string.IsNullOrEmpty(gameType) || !PageValidate.IsNumberPlus(maxScore))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            go_memberEntity currentMember = go_memberBusiness.LoadEntity(Convert.ToInt32(uid));
            //如果传过来的场次比自己当前的积分要大,返回非法操作
            if (currentMember.Score < Convert.ToInt32(maxScore))
            {
                context.Response.Write("{\"state\":3}"); //积分不足
            }
            #endregion

            //计算区间

            //根据用户当前的积分获取该区间内的最大值和最小值
            DataTable dt = go_giftNumsBusiness.GetListData();
            int min = 0; int max = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["nums"]) <= Convert.ToInt32(maxScore))
                {
                    max = Convert.ToInt32(dt.Rows[i]["nums"]);
                    if (i >= 1) min = Convert.ToInt32(dt.Rows[i - 1]["nums"]);
                    continue;
                }
            }

            string orderid = ShopOrders.GenerateOrderID(ShopOrders.BusinessType.gift);
            int[] resultArr = go_giftsBusiness.goGiftDraw(orderid, currentMember.Uid, gameType, min, max);

            string result = string.Format("{{\"state\":{0},\"winGiftId\":{1},\"orderId\":\"{2}\"}}", resultArr[0], resultArr[1], resultArr[1] != -1 ? orderid : "");
            context.Response.Write(result);
        }
        /// <summary>
        /// 获取积分使用详情
        /// </summary>
        /// <param name="context"></param>
        private void getScoreDetail(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            string str = "";
            string result = "";
            string strScore = "";
            string strTime = "";
            string strRange = "";
            string strtype = "";
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            DataTable dtScoreDetail = go_scoreDetailBusiness.GetListData("uid=" + uid + "","*","usetime desc");
            for (int i = 0; i < dtScoreDetail.Rows.Count; i++)
            {
                strScore = dtScoreDetail.Rows[i]["money"].ToString().TrimEnd();    //金额
                strTime = dtScoreDetail.Rows[i]["usetime"].ToString().TrimEnd();//时间
                strRange = dtScoreDetail.Rows[i]["use_range"].ToString().TrimEnd();//范围
                strtype = dtScoreDetail.Rows[i]["type"].ToString().TrimEnd();//类型0为消费  1为收入
                str += "{" + string.Format("\"score\":{0},\"tiem\":\"{1}\",\"range\":\"{2}\",\"type\":{3}", strScore, strTime, strRange, strtype) + "}" + ",";
            }
            str = str.TrimEnd(',');
            result = "[" + str + "]";
            context.Response.Write(result);

        }
        /// <summary>
        /// 积分抽奖中奖列表
        /// </summary>
        /// <param name="context"></param>
        private void giftWinDetail(HttpContext context)
        {
            string time = "";
            string title = "";
            string score = "";
            string status = "";
            string str = "";
            string iswon = "";
            string result = "";
            string thumb = "";
            string orderId = "";
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            DataTable dtWinDetail = go_giftWinDetailBusiness.selectData("WD.uid=" + uid + "","addtime desc");
            for (int i = 0; i < dtWinDetail.Rows.Count; i++)
            {
                time = dtWinDetail.Rows[i]["addtime"].ToString().TrimEnd(); //时间
                title = dtWinDetail.Rows[i]["title"].ToString().TrimEnd(); //奖品ID
                score = dtWinDetail.Rows[i]["costScore"].ToString().TrimEnd();  //积分场次 
                status = dtWinDetail.Rows[i]["status"].ToString().TrimEnd();//奖品状态
                iswon = dtWinDetail.Rows[i]["isWon"].ToString().TrimEnd();//是否中奖
                thumb = dtWinDetail.Rows[i]["thumb"].ToString().TrimEnd();
                orderId = dtWinDetail.Rows[i]["orderId"].ToString().TrimEnd();

                str += "{" + string.Format("\"time\":\"{0}\",\r\"title\":\"{1}\",\r\"score\":{2},\r\"status\":\"{3}\",\r\"isWon\":{4},\r\"thumb\":\"{5}\",\r\"orderId\":\"{6}\",\r\"imgroot\":\"{7}\"", time, title, score, status, iswon, thumb, orderId,Globals.G_UPLOAD_PATH) + "}" + ",";
            }
            result = "[" + str.TrimEnd(',') + "]";
            context.Response.Write(result);


        }
        /// <summary>
        /// 获取积分场区间
        /// </summary>
        private void getScoreNums(HttpContext context)
        {
            DataTable dt = go_giftNumsBusiness.GetListData();
            string imageurls = "";
            string nums = "";
            string result = "";
            //输出JSON
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                imageurls += dt.Rows[i]["imageurl"].ToString() + "|";
                nums += dt.Rows[i]["nums"].ToString() + "|";
            }
            result = string.Format("{{\"nums\":\"{0}\",\r\"imageurl\":\"{1}\",\r\"imgroot\":\"{2}\"}}", nums.TrimEnd('|').ToString(), imageurls.TrimEnd('|'), Globals.G_UPLOAD_PATH);
            context.Response.Write(result);
        }

        /// <summary>
        /// 获取待开奖商品
        /// </summary>
        private void getGiftList(HttpContext context)
        {
            #region 参数验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            //场次
            string maxScore = context.Request["maxScore"];
            string gameType = context.Request["gameType"];
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(maxScore) || string.IsNullOrEmpty(gameType) || !PageValidate.IsNumberPlus(maxScore))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            go_memberEntity currentMember = go_memberBusiness.LoadEntity(Convert.ToInt32(uid));
            /*
            //如果传过来的场次比自己当前的积分要大,返回非法操作
            if (currentMember.Score < Convert.ToInt32(maxScore))
            {
                context.Response.Write("{\"state\":3}"); //积分不足
                return;
            }
             */
            #endregion
            //根据用户当前的积分获取该区间内的最大值和最小值
            DataTable dt = go_giftNumsBusiness.GetListData();

            int min = 0; int max = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["nums"]) <= Convert.ToInt32(maxScore))
                {
                    max = Convert.ToInt32(dt.Rows[i]["nums"]);
                    if (i >= 1) min = Convert.ToInt32(dt.Rows[i - 1]["nums"]);
                    continue;
                }
            }

            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;//待开奖商品数
            int currPage = 1;
            string from = "(select gg.*,gp.thumb from go_gifts gg left join go_products gp on gg.productid = gp.productid";
            string where = string.Format("where scope > {0} and scope <= {1}  and gg.stock>0 and charindex('{2}',giftsuse)>0 ) t ", min, max, gameType);
            string orderby = " addtime desc";
            //获取该用户符合的奖品列表
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）


            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }

        private void WriteLog(string log)
        {
            System.IO.StreamWriter writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("~/wx_gift.txt"));

            writer.WriteLine(System.DateTime.Now);

            writer.WriteLine(log);

            writer.Flush();

            writer.Close();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

}