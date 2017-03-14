using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Common;
using YH.Utility;

namespace Taotaole.IApi
{
    /// <summary>
    /// taotaolebuy 的摘要说明
    /// </summary>
    public class taotaolebuy : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "myconsume": //我的消费记录
                    myconsume(context);
                    break;
                case "mycharge": //我的充值记录
                    mycharge(context);
                    break;
                case "listinfo":    //获取最新已揭晓列表
                    listinfo(context);
                    break;
                case "listwill":    //获取马上揭晓列表
                    listwill(context);
                    break;
                case "refreshLottery":
                    refreshLottery(context);
                    break;
                case  "usergathermoney"://充值记录汇总
                    usergathermoney(context);
                    break;

                case  "zengPointDetail"://夺宝币使用/获取详情记录
                    zengPointDetail(context);
                    break;
            }
        }
        /// <summary>
        /// 我的夺宝币使用详情
        /// </summary>
        /// <param name="context"></param>
        private void zengPointDetail(HttpContext context)
        {
            //验证签名
            //string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            string uid = "1";
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string result = "";
            string where = "Z.uid=" + uid + "";
            DataTable dt = CustomsBusiness.GetListData("go_zenPointDetail  Z  left  join   go_member   M  on  Z.uid=M.uid", where, "M.zenPoint,Z.*", "", 0, DbServers.DbServerName.LatestDB);
              result= JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            context.Response.Write(result);
        }
        /// <summary>
        /// 我的消费记录
        /// </summary>
        /// <param name="context"></param>
        private void myconsume(HttpContext context)
        {
            #region 参数验证和获取
            //验证签名
           //string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            string uid = "1";
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            //页码
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            #endregion
            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = "(select content,CONVERT(varchar,time,121) as time,money from go_member_account";
            string where = string.Format("where uid={0}) t", uid);
            string orderby = " time desc";

            //获取用户充值列表及页码
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）


            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }

        /// <summary>
        /// 我的充值记录
        /// </summary>
        /// <param name="context"></param>
        private void mycharge(HttpContext context)
        {
            #region 参数验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            //页码
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            #endregion
            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = "(select pay_type,CONVERT(varchar,time,121) as time,money from go_member_addmoney_record";
            string where = string.Format("where uid={0} and status='已付款' and score in( '0','2')) t ", uid);
            string orderby = " time desc";

            //获取用户充值列表及页码
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）


            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }

        private void listinfo(HttpContext context)
        {
            //接收传参
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";

            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = @"(Select y.yId,y.productid,p.operation,y.title,qishu,p.thumb,CONVERT(varchar(16),y.q_end_time,121) as q_end_time,y.q_end_time as q_end_timeO,y.q_uid,m.username,m.headimg,m.user_ip,y.q_code,O.quantity ,y.q_showtime
                From go_yiyuan Y Inner Join go_member M on y.q_uid=m.uid 
                Inner Join (Select uid,businessId,SUM(quantity) as quantity From go_orders Where ordertype='yuan' Group by uid,businessId) O on Y.q_uid=O.uid And Y.yId=O.businessId
                Inner Join go_products P on Y.productid=P.productid) t";
            string where = " Where q_uid is not null and (q_showtime is null or q_showtime = 0 )";
            string orderby = " q_end_timeO desc";

            //获取用户充值列表及页码
            int count = CustomsBusiness.GetDataCount(from, where);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）

            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }

        private void listwill(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);

            //获取数量,一般用于首页
            string num = context.Request["num"];
            if (string.IsNullOrEmpty(num) || !PageValidate.IsNumberPlus(num)) num ="5";
            //获取yid,一般用于详情页
            string yid = context.Request["yid"];
            if (string.IsNullOrEmpty(yid) || !PageValidate.IsNumberPlus(yid)) yid = "0";    //参数错误;

            //如果当前用户登录，查看当前用户是否有中奖
            if (uid != "")
            {
                string from_my = " go_orders O Inner Join go_yiyuan Y ON O.ordertype='yuan' And O.businessId=Y.yId ";
                string where_my = "Where q_uid is not null And q_showtime=1 And q_uid=" + uid;
                object objCount_my = CustomsBusiness.GetDataCount(from_my, where_my, DbServers.DbServerName.LatestDB);
                if (int.Parse(objCount_my.ToString()) == 0) uid = "";
            }

            //查询列表
            string from = @"go_yiyuan Y 
                        Inner Join go_member M on y.q_uid=m.uid
                        Inner Join (Select uid,businessId,SUM(quantity) as quantity From go_orders Where ordertype='yuan' Group by uid,businessId) O on Y.q_uid=O.uid And Y.yId=O.businessId
                        Inner Join go_products P on Y.productid=P.productid";
            string where = "q_uid is not null And q_showtime=1";
            if (!string.IsNullOrEmpty(uid)) where += " And q_uid=" + uid;
            if (yid != "0") where += " And yid=" + yid;
            string selectFields = "y.yId,y.productid,y.title,qishu,p.thumb,CONVERT(varchar,y.q_end_time,121) as q_end_time";
            string orderby = "y.q_end_time Desc";
            DataTable dtData = CustomsBusiness.GetListData(from, where, selectFields, orderby,Convert.ToInt32(num));
            dtData.Columns.Add("timeSpan",typeof(long));    //增加一列毫秒差

            foreach (DataRow dr in dtData.Rows)
            {
                DateTime dt_q_end_time = DateTime.Parse(dr["q_end_time"].ToString());
                long timeSpan = dt_q_end_time.Ticks / 10000-DateTime.Now.Ticks / 10000;
                dr["timeSpan"] = (timeSpan <= 0) ? 0 : timeSpan;
            }

            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"second\":{2},\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, Globals.G_UPLOAD_PATH, ShopOrders.Q_end_time_second);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }

        //计算中奖人(揭晓中更改为已揭晓)
        private void refreshLottery(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);

            try
            {
                //接收传参
                string yidStr = context.Request["yid"];
                int yid = Convert.ToInt32(yidStr);
                go_yiyuanBusiness.refreshLotteryShow(yid);

                string from = @"(Select CONVERT(varchar(16),y.q_end_time,121) as q_end_time,y.title,y.yid,y.qishu,y.q_uid,y.q_code,m.username,O.quantity 
                                            From go_yiyuan Y Inner Join go_member M on y.q_uid=m.uid 
                                            Inner Join (Select uid,businessId,SUM(quantity) as quantity From go_orders Where ordertype='yuan' Group by uid,businessId) O on Y.q_uid=O.uid And Y.yId=O.businessId) t";
                string where = " t.yid = "+yid;
                DataTable dtData = CustomsBusiness.GetListData(from, where);

                //输出JSON
                string result = string.Format("{{\"state\":{0},\"ismy\":{1},\r\"data\":"
                    , (dtData.Rows.Count > 0) ? 0 : 1, (dtData.Rows.Count > 0 && dtData.Rows[0]["q_uid"].ToString() == uid) ? 1 : 0);
                result += dtData.Rows.Count > 0 ? JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented) : "0";
                result += "}";

                context.Response.Write(result);
            }
            catch(Exception ex)
            {
                context.Response.Write("{\"state\":\""+ex.Message+"\"}");    //参数错误
            }
            
        }
        private void usergathermoney(HttpContext context)
        {
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            //uid = "12";
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            DataTable dt = go_member_addmoney_recordBusiness.GetListData("score='0' and status='已付款' and uid='"+uid+"'", "SUM(convert(int,money)) as money", DbServers.DbServerName.LatestDB);
            int money = 0;
            if (dt.Rows.Count == 0)
            {
                money = 0;
            }
            else
            {
                money = Convert.ToInt32(dt.Rows[0]["money"]);
            }
            //输出JSON
            string result = string.Format("{{\"state\":{0},\"money\":{1}"
                , (dt.Rows.Count > 0) ? 0 : 1, money);
            result += "}";
            context.Response.Write(result);
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