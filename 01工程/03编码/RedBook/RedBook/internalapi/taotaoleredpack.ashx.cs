using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Common;
using YH.Utility;

namespace RedBook.internalapi
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
                case "redpacklist":        //获取支付红包列表
                    getUserRedpackList(context);
                    break;
                case "redpackinfo":        //获取用户中心红包信息
                    getUserRedpackInfo(context);
                    break;
                case "redpackcount":        //获取用户红包数量
                    getDoRedpackCount(context);
                    break;
                case "activityqualification":        //领取活动资格
                    getActivityQualification(context);
                    break;
                case "pullsentredpack":        //拉新促活活动发包 17 23 25 31
                    pullSentRedpack(context);
                    break;
                case "staysentredpack":        //留存活动发包 18 19 20
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
        private void getUserRedpackList(HttpContext context)
        {
            //获取传参
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string money = context.Request["money"];

            //调用爽乐购通用API
            string url = Globals.API_Domain + "ilerrredpack?action=redpacklist" + string.Format("&sign={0}&money={1}&uid={2}", sign, money, uid);
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrredpack?action=redpacklist", string.Format("&sign={0}&money={1}&uid={2}", sign, money, uid));
            context.Response.Write(result);
        }

        private void getUserRedpackInfo(HttpContext context)
        {
            //获取传参
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string typeid = context.Request["typeid"];
            if (string.IsNullOrEmpty(typeid) || !PageValidate.IsNumberPlus(typeid)) typeid = "1";
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrredpack?action=redpackinfo", string.Format("&sign={0}&uid={1}&typeid={2}&p={3}", sign, uid, typeid, p));
            context.Response.Write(result);
        }

        private void getDoRedpackCount(HttpContext context)
        {
            //获取传参
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();

            //调用爽乐购通用API
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrredpack?action=redpackcount", string.Format("&sign={0}&uid={1}", sign, uid));
            context.Response.Write(result);
        }

        private void getActivityQualification(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string aid = context.Request["aid"];

            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrredpack?action=activityqualification", string.Format("&sign={0}&uid={1}&aid={2}", sign, uid, aid));
            context.Response.Write(result);
        }
        private void ValActivityQualification(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string aid = context.Request["aid"];

            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrredpack?action=valactivityqualification", string.Format("&sign={0}&uid={1}&aid={2}", sign, uid, aid));
            context.Response.Write(result);
        }
        private void pullSentRedpack(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string aid = context.Request["aid"];

            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrredpack?action=pullsentredpack", string.Format("&sign={0}&uid={1}&aid={2}", sign, uid, aid));
            context.Response.Write(result);
        }
        private void staySentRedpack(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string shopid = context.Request["shopid"];

            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrredpack?action=staysentredpack", string.Format("&sign={0}&uid={1}&shopid={2}", sign, uid, shopid));
            context.Response.Write(result);
        }
        //领取活动资格
        //aid 活动id
        public void ValActivityDate(HttpContext context)
        {
            //获取传参 /ilerrredpack?action=valactivitydate&aid=17
            #region 参数的验证和获取
            string aid = context.Request["aid"];
            #endregion

            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrredpack?action=valactivitydate", string.Format("&aid={0}",aid));
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