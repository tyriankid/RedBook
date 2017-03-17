using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;


namespace RedBook.internalapi
{
    /// <summary>
    /// taotaoletuan 的摘要说明
    /// </summary>
    public class taotaoletuan : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "tuanlist": //团购列表
                    tuanlist(context);
                    break;
                case "tuandetail": //团购列表
                    tuandetail(context);
                    break;
                case "tuanjoinlist": //参团列表
                    tuanjoinlist(context);
                    break;
                case "tuanjoinlistdetail": //参团明细
                    tuanjoinlistdetail(context);
                    break;
                case "tuanmylist":
                    tuanmylist(context);//我的团信息
                    break;
                case "tuanorderinfo"://订单详细
                    tuanorderinfo(context);
                    break;
                case "tuanwinnerinfo"://团中奖用户信息
                    tuanwinnerinfo(context);
                    break;
                case "tuanlike":
                    tuanlike(context);
                    break;
                case "tuankeep":
                    tuankeep(context);
                    break;
                case "tuankeeplist":
                    tuankeeplist(context);
                    break;
            }
        }

        private void tuanlist(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();

            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1948");
            //sign = SecurityHelper.GetSign(uid);

            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrtuan", string.Format("action=tuanlist&sign={0}&uid={1}&p={2}", sign, uid, p));
            context.Response.Write(result);
        }

        private void tuandetail(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string tid = context.Request["tid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrtuan", string.Format("action=tuandetail&tid={2}&sign={0}&uid={1}", sign, uid, tid));
            context.Response.Write(result);
        }

        private void tuanjoinlist(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string tid = context.Request["tid"];
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrtuan", string.Format("action=tuanjoinlist&sign={0}&uid={1}&tid={2}&p={3}", sign, uid, tid, p));
            context.Response.Write(result);
        }

        private void tuanjoinlistdetail(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string tid = context.Request["tid"];
            string tlistid = context.Request["tuanlistId"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrtuan", string.Format("action=tuanjoinlistdetail&sign={0}&uid={1}&tid={2}&tlistid={3}", sign, uid, tid, tlistid));
            context.Response.Write(result);
        }

        private void tuanmylist(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();

            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string p = context.Request["p"];
            string status = context.Request["status"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrtuan", string.Format("action=tuanmylist&sign={0}&uid={1}&status={2}&p={3}", sign, uid, status, p));
            context.Response.Write(result);
        }


        private void tuanorderinfo(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string orderid = context.Request["orderid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrtuan", string.Format("action=tuanorderinfo&sign={0}&uid={1}&orderid={2}", sign, uid, orderid));
            context.Response.Write(result);
        }
        private void tuanwinnerinfo(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string tid = context.Request["tid"];
            string type = context.Request["type"];
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrtuan", string.Format("action=tuanwinnerinfo&tid={0}&type={1}&p={2}&sign={3}&uid={4}", tid, type, p, sign, uid));
            context.Response.Write(result);
        }
        private void tuanlike(HttpContext context)
        {
            string tid = context.Request["tid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrtuan", string.Format("action=tuanlike&tid={0}", tid));
            context.Response.Write(result);
        }
        private void tuankeep(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string tid = context.Request["tid"];
            string type = context.Request["type"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrtuan", string.Format("action=tuankeep&tid={0}&type={1}&sign={2}&uid={3}", tid, type, sign, uid));
            context.Response.Write(result);
        }
        //获取我的收藏列表信息
        ///ilerrtuan?action=tuankeeplist&p=1
        private void tuankeeplist(HttpContext context)
        {
            //验证签名
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();

            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1948");
            //sign = SecurityHelper.GetSign(uid);

            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrtuan", string.Format("action=tuankeeplist&sign={0}&uid={1}&p={2}", sign, uid, p));
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