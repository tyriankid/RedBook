using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Common;
using YH.Utility;

namespace RedBook.internalapi
{
    /// <summary>
    /// taotaoleorder 的摘要说明
    /// </summary>
    public class taotaoleorder : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "mylist": //我的云购记录
                    mylist(context);
                    break;
                case "mywinninglist": //我的中奖记录
                    mywinninglist(context);
                    break;
                case "mysharelist": //我的晒单记录
                    mysharelist(context);
                    break;
                case "partakewin":  //参与详情
                    partakewin(context);
                    break;
                case "otheroneselfpartake"://别人参与记录
                    otheroneselfpartake(context);
                    break;
                case "otherfpartakewin"://别人获奖记录
                    otherfpartakewin(context);
                    break;
                case "othersharelist": //别人晒单记录
                    othersharelist(context);
                    break;
                case "winningremind":   //中奖提醒
                    winningremind(context);
                    break;
                case "ZhiOrderList": //直购订单
                    ZhiOrderList(context);
                    break;
            }

        }
        /// <summary>
        /// 我的云购纪录
        /// </summary>
        /// <param name="context"></param>
        private void mylist(HttpContext context)
        {
            //签名的验证
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);

            string p = context.Request["p"];
            string typeid = context.Request["typeid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrorder", string.Format("action=mylist&sign={0}&p={1}&typeid={2}&uid={3}", sign, p, typeid, uid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 我的中奖纪录
        /// </summary>
        /// <param name="context"></param>
        private void mywinninglist(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrorder", string.Format("action=mywinninglist&sign={0}&p={1}&uid={2}", sign, p,uid));
            context.Response.Write(result);
        }
       /// <summary>
       /// 我的晒单记录
       /// </summary>
       /// <param name="context"></param>
        private void mysharelist(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrorder", string.Format("action=mysharelist&sign={0}&p={1}&uid={2}", sign, p,uid));
            context.Response.Write(result);

        }
        /// <summary>
        /// 我的直购订单
        /// </summary>
        /// <param name="context"></param>
        private void ZhiOrderList(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrorder", string.Format("action=ZhiOrderList&sign={0}&uid={1}", sign, uid));
            context.Response.Write(result);

        }
        /// <summary>
        /// 参与详情
        /// </summary>
        /// <param name="context"></param>
        private void partakewin(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "4");
            //sign = SecurityHelper.GetSign(uid);
            string shopid = context.Request["shopid"];
            string otherid = context.Request["otherid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrorder", string.Format("action=partakewin&shopid={0}&otherid={1}&sign={2}&uid={3}", shopid, otherid, sign, uid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 自己参与记录
        /// </summary>
        /// <param name="context"></param>
        private void oneselfpartake(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrorder", string.Format("action=oneselfpartake&sign={0}&p={1}&uid={2}", sign, p,uid));
            context.Response.Write(result);
        }
       /// <summary>
       /// 自己获奖记录
       /// </summary>
       /// <param name="context"></param>
        private void oneselfpartakewin(HttpContext context)
        {
            string p = context.Request["p"];
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrorder", string.Format("action=oneselfpartakewin&sign={0}&p={1}&uid={2}", sign, p,uid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 别人参与详情
        /// </summary>
        /// <param name="context"></param>
        private void otheroneselfpartake(HttpContext context)
        {
            string typeid = context.Request["typeid"];
            string uid = context.Request["uid"];
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrorder", string.Format("action=otheroneselfpartake&uid={0}&p={1}&typeid={2}", uid, p, typeid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 别人获奖记录
        /// </summary>
        private void otherfpartakewin(HttpContext context)
        {
            string uid = context.Request["uid"];
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrorder", string.Format("action=otherfpartakewin&uid={0}&p={1}", uid, p));
            context.Response.Write(result);
            
        }
        /// <summary>
        /// 别人晒单记录
        /// </summary>
        /// <param name="context"></param>
        private void othersharelist(HttpContext context)
        {
            string uid = context.Request["uid"];
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrorder", string.Format("action=othersharelist&uid={0}&p={1}", uid, p));
            context.Response.Write(result);

        }

        /// <summary>
        /// 中奖提醒
        /// </summary>
        private void winningremind(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrorder", string.Format("action=winningremind&sign={0}&uid={1}", sign, uid));
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