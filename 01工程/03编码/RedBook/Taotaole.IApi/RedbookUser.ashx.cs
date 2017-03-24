using System;
using System.Web;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;

namespace Taotaole.IApi
{
    /// <summary>
    /// RedbookUser 的摘要说明
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
        /// <param name=""></param>
        private void goFabulous(HttpContext context)
        {
            #region 参数验证和获取
            //验证参数合法性
            string businessid = context.Request["businessid"];
            string uid = context.Request["uid"];
            if (string.IsNullOrEmpty(businessid) || string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            #endregion

            string result = string.Empty;


            //本身的处理,处理对应的业务
            UserDynamic ud = new UserDynamic()
            {
                Id = Guid.NewGuid(),
                UserId = go_memberBusiness.GetCurrentMember().Uid, 
                AddTime = DateTime.Now,
                BusinessId = businessid,
                DynamicType = Enum.GetName(typeof(UserDynamic.dynamicType), UserDynamic.dynamicType.Fabulous),
                BusinessType = Enum.GetName(typeof(Fabulous.fabulousType), Fabulous.fabulousType.Book),
            };
            ud.Execute();

            /*
            RB_User_FabulousEntity fbEntity = new RB_User_FabulousEntity()
            {
                Id = Guid.NewGuid(),
                UserId = Globals.GetCurrentMemberUserId(),
                AddTime = DateTime.Now,
                BusinessId = new Guid(businessid),
                BusinessType = Enum.GetName(typeof(UserDynamic.fabulousType),UserDynamic.fabulousType.Book),
            };
            Fabulous.goFabulous(fbEntity);
            */

            context.Response.Write(result);
        }

        /// <summary>
        /// 对文章进行评论
        /// 该接口需要使用防刷接口处理
        /// </summary>
        /// <param name=""></param>
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