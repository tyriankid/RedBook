using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Common;
using YH.Utility;

namespace RedBook.internalapi
{
    /// <summary>
    /// RedbookUserHandler 的摘要说明
    /// </summary>
    public class RedbookUser : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "goFabulous":
                    goFabulous(context);
                    break;
                case "ReviewBook":
                    ReviewBook(context);
                    break;
            }
        }

        /// <summary>
        /// 点赞
        /// 该接口需要使用防刷接口处理
        /// </summary>
        private void goFabulous(HttpContext context)
        {
            //获取传参
            string businessId = context.Request["id"];
            if (string.IsNullOrEmpty(businessId)) businessId = "";
            string type = context.Request["type"];
            if (string.IsNullOrEmpty(type)) type = "0"; //默认为关注
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();

            string result = new WebUtils().DoPost(Globals.API_Domain + "rdbookuser", string.Format("action=goFabulous&businessId={0}&type={1}&sign={2}&uid={3}", businessId, type,sign,uid));
            context.Response.Write(result);
        }

        /// <summary>
        /// 对文章进行评论
        /// 该接口需要使用防刷接口处理
        /// </summary>
        private void ReviewBook(HttpContext context)
        {
            
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