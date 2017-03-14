using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Common;
using YH.Utility;

namespace RedBook.internalapi
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
                case "usergathermoney"://充值记录汇总
                    usergathermoney(context);
                    break;
                case "zengPointDetail"://夺宝币使用/获取详情记录
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
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrbuy", string.Format("action=zengPointDetail&sign={0}&uid={1}", sign, uid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 我的消费记录
        /// </summary>
        /// <param name="context"></param>
        private void myconsume(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrbuy", string.Format("action=myconsume&sign={0}&p={1}&uid={2}", sign, p, uid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 我的充值记录
        /// </summary>
        /// <param name="context"></param>
        private void mycharge(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrbuy", string.Format("action=mycharge&sign={0}&p={1}&uid={2}", sign, p, uid));
            context.Response.Write(result);
        }

        private void listinfo(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrbuy", string.Format("action=listinfo&sign={0}&p={1}&uid={2}", sign, p, uid));
            context.Response.Write(result);
        }

        private void listwill(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrbuy", string.Format("action=listwill&sign={0}&uid={1}", sign, uid));
            context.Response.Write(result);
        }

        /// <summary>
        /// 计算中奖人(揭晓中更改为已揭晓)
        /// </summary>
        private void refreshLottery(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string yid = context.Request["yid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrbuy", string.Format("action=refreshLottery&yid={0}&sign={1}&uid={2}", yid, sign, uid));
            context.Response.Write(result);
        }
        /// <summary>
        /// 充值记录汇总
        /// </summary>
        /// <param name="context"></param>
        private void usergathermoney(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrbuy", string.Format("action=usergathermoney&sign={0}&uid={1}", sign, uid));
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