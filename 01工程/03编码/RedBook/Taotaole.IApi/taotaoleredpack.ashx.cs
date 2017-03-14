using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Taotaole.IApi
{
    /// <summary>
    /// taotaoleredpack 的摘要说明
    /// </summary>
    public class taotaoleredpack : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string action = context.Request["action"];
            switch (action)
            {
                case "redpacklist":        //获取可支付红包列表
                    getUserRedpackList(context);
                    break;
                case "redpackinfo":        //获取用户红包列表
                    getUserRedpackInfo(context);
                    break;
                case "redpackcount":        //获取用户红包列表
                    getDoRedpackCount(context);
                    break;
                case "activityqualification":        //领取活动资格
                    getActivityQualification(context);
                    break;
                case "pullsentredpack":        //拉新促活活动发包
                    pullSentRedpack(context);
                    break;
                case "staysentredpack":        //留存活动发包
                    staySentRedpack(context);
                    break;
                case "valactivityqualification":        //判断活动资格接口
                    ValActivityQualification(context);
                    break;
                case "valactivitydate":        //判断当前时间是否在活动时间内
                    ValActivityDate(context); 
                    break;
            }
        }
        //获取支付时可使用红包列表
        //uid 用户id ,money 消费金额
        //获取传参 /ilerrredpack?action=redpacklist&sign=MW_B5ECA031A5D9F85F2972E648AA8FA648&money=1000&uid=2BD59896A291D2F9
        private void getUserRedpackList(HttpContext context)
        {
            
            //string uid = context.Request["uid"]; 
            #region 参数的验证和获取

            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }

            string money = context.Request["money"];
            if (string.IsNullOrEmpty(money) || !PageValidate.IsNumberPlus(money))
            {
                context.Response.Write("{\"state\":3}");    //缺少参数
                return;
            }


            #endregion

            DataTable redlist = go_activity_codeBusiness.getUserRedpack(uid, money);
            string result = string.Format("{{\"state\":{0},\r\"data\":", (redlist.Rows.Count > 0) ? 0 : 1);
            result += JsonConvert.SerializeObject(redlist, Newtonsoft.Json.Formatting.Indented)+"}";

            context.Response.Write(result);
        }
        //获取传参 /ilerrredpack?action=redpackinfo&sign=MW_B5ECA031A5D9F85F2972E648AA8FA648&uid=2BD59896A291D2F9&typeid=1&p=1
        //获取用户中心红包分类列表信息
        private void getUserRedpackInfo(HttpContext context)
        {
            //获取传参 /ilerrredpack?action=redpackinfo&sign=MW_B5ECA031A5D9F85F2972E648AA8FA648&uid=2BD59896A291D2F9&typeid=1&p=1
            #region 参数的验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string typeid = context.Request["typeid"];
            if (string.IsNullOrEmpty(typeid) || !PageValidate.IsNumberPlus(typeid)) typeid = "1";
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            #endregion
           go_activity_codeBusiness.ValRedpackState(uid);
            string where = "Where 1=1 AND uid=" + uid;
           //string where = "Where 1=1";
            string wheredo = "Where 1=1 AND uid=" + uid; ;
            string wheresave = "Where 1=1 AND uid=" + uid; ;
            string orderby = "";
            switch (typeid)
            {
                case "1":        //可用红包列表
                    where += " and state=1";
                    wheresave += " and state=0";
                    orderby = "senttime asc";
                    break;
                case "2":        //待发红包列表
                    where += " and state=0";
                    wheredo += " and state=1";
                    orderby = "senttime asc";
                    break;
                case "3":        //过期和已使用
                    where += " and (state=2 or state=3)";
                    wheresave += " and state=0";
                    wheredo += " and state=1";
                    orderby = "sorttime desc";
                    break;

            }
            //获取总条数、当前页数据、总页码
            int pagesize = 10;
            int docount = 0;
            int savecount = 0;
            int currPage = int.Parse(p);
            string from = "(select *,sorttime = case [state] when 0 then senttime when 1 then overtime when 2 then usetime when 3 then senttime else null end from go_activity_code) a";
            string fromcount = "go_activity_code";
            int count = CustomsBusiness.GetDataCount(fromcount, where, DbServers.DbServerName.LatestDB);
            DataTable redlist = CustomsBusiness.GetPageData(from, orderby, "*"
                , currPage, pagesize, where, DbServers.DbServerName.LatestDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
            if (typeid == "1")
            {
                 docount = count;
                 savecount = CustomsBusiness.GetDataCount(from, wheresave, DbServers.DbServerName.LatestDB);
            }
            else if (typeid == "2")
            {
                 savecount = count;
                 docount = CustomsBusiness.GetDataCount(from, wheredo, DbServers.DbServerName.LatestDB);
            }
            else {
                 savecount = CustomsBusiness.GetDataCount(from, wheresave, DbServers.DbServerName.LatestDB); ;
                 docount = CustomsBusiness.GetDataCount(from, wheredo, DbServers.DbServerName.LatestDB);

            }
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"docount\":{4},\r\"savecount\":{5},\r\"data\":"
              , (redlist.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, docount, savecount);
            result += JsonConvert.SerializeObject(redlist, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }
       

        //获取传参 /ilerrredpack?action=redpackinfo&sign=MW_B5ECA031A5D9F85F2972E648AA8FA648&uid=2BD59896A291D2F9
        //获取用户可用红包数量
        public void getDoRedpackCount(HttpContext context)
        {
            
            #region 参数的验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            #endregion
           go_activity_codeBusiness.ValRedpackState(uid);
           int count = go_activity_codeBusiness.getRedpackDoNum(uid);
            string result = string.Format("{{\"state\":{0},\r\"count\":{1}"
              , 0, count);
            result += "}";
            context.Response.Write(result);
        }
        //领取活动资格
        //uid 用户id aid 活动id
        public void getActivityQualification(HttpContext context)
        {
            //获取传参 /ilerrredpack?action=activityqualification&sign=MW_B5ECA031A5D9F85F2972E648AA8FA648&uid=2BD59896A291D2F9&aid=17
            #region 参数的验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            string aid = context.Request["aid"];
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            #endregion

            int res = go_activity_userBusiness.Qualification(uid, aid);
            context.Response.Write("{\"state\":" + res + "}");
            return;
        }
        //领取活动资格
        //aid 活动id
        public void ValActivityDate(HttpContext context)
        {
            //获取传参 /ilerrredpack?action=valactivitydate&aid=17
            #region 参数的验证和获取
            string aid = context.Request["aid"];
            #endregion

            bool res = go_activityBusiness.valActivityDate(aid);
            if (res)
            {
                context.Response.Write("{\"state\":" + 0 + "}");//在活动时间范围内
            }
            else {
                context.Response.Write("{\"state\":" + 1 + "}");//不在活动时间范围内
            }
            return;
        }
        //验证活动资格
        //uid 用户id aid 活动id
        public void ValActivityQualification(HttpContext context)
        {
            //获取传参 /ilerrredpack?action=valactivityqualification&sign=MW_B5ECA031A5D9F85F2972E648AA8FA648&uid=2BD59896A291D2F9&aid=17
            #region 参数的验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            string aid = context.Request["aid"];
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            #endregion

            bool res = go_activity_userBusiness.valQualification(uid, aid);
            if (res)
            {
                if (go_activity_userBusiness.valQualificationCount(uid, aid))
                {
                    context.Response.Write("{\"state\":" + 1 + "}");//已有资格
                }
                else {
                    context.Response.Write("{\"state\":" + 2 + "}");//已有资格并达到参与次数上限
                }
            }
            else
            {
                context.Response.Write("{\"state\":" + 0 + "}");//无资格
            }
            return;
        }
        //拉新活动发包方法
        //获取传参 /ilerrredpack?action=pullsentredpack&sign=MW_B5ECA031A5D9F85F2972E648AA8FA648&uid=2BD59896A291D2F9&aid=17
        //uid 用户id aid 活动id
        public void pullSentRedpack(HttpContext context)
        {
            #region 参数的验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            string aid = context.Request["aid"];
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            #endregion
            int res = go_activity_codeBusiness.sentUserRedpack(uid, aid);
           context.Response.Write("{\"state\":" + res + "}");    
                return;
        }
        //留存活动发包接口
        //获取传参 /ilerrredpack?action=staysentredpack&sign=MW_B5ECA031A5D9F85F2972E648AA8FA648&uid=2BD59896A291D2F9&bid=1
        //uid 用户id bid 业务id
        public void staySentRedpack(HttpContext context)
        {
            #region 参数的验证和获取
            //验证签名
            string bid = context.Request["bid"];
            if (string.IsNullOrEmpty(bid))
            {
                context.Response.Write("{\"state\":1}");    //未获得业务ID
                return;
            }
            //验证签名
            string sign = Globals.ValidateSign(context.Request["sign"],context.Request["uid"]);
            if (string.IsNullOrEmpty(sign))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            #endregion
            go_activity_codeBusiness.sentBidRedpack(bid);
            //买爆款不中包赔活动发包end
            context.Response.Write("{\"state\":0}");    //执行完毕
            return;
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