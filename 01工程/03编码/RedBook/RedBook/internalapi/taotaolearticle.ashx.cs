using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Common;
using YH.Utility;

namespace RedBook.internalapi
{
    /// <summary>
    /// taotaolearticle 的摘要说明
    /// </summary>
    public class taotaolearticle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "article": //文章列表
                    articleListInfo(context);
                    break;
                case "articleDetail":          //文章详情
                    articleDetail(context);
                    break;
                case "slides": //幻灯片
                    slidesList(context);
                    break;
                case "slidesDetail": //幻灯片详情
                    slidesDetail(context);
                    break;
                case "ispass": //判断用户参与网吧活动的资格
                    ispass(context);
                    break;
                case "exchange": //兑换接口
                    exchange(context);
                    break;

            }
        }

         /// <summary>
        /// 幻灯片详情
        /// </summary>
        /// <param name="context"></param>
        private void slidesDetail(HttpContext context)
        {
            string slideid = context.Request["slideid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrarticle", string.Format("action=slidesDetail&slideid={0}", slideid));
            context.Response.Write(result);
        }



        /// <summary>
        /// 幻灯片
        /// </summary>
        /// <param name="context"></param>
        private void slidesList(HttpContext context)
        {
            string slidetype = context.Request["slidetype"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrarticle", string.Format("action=slides&slidetype={0}", slidetype));
            context.Response.Write(result);
        }

         /// <summary>
        /// 文章详情
        /// </summary>
        /// <param name="context"></param>
        private void articleDetail(HttpContext context)
        {
            string id = context.Request["id"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrarticle", string.Format("action=articleDetail&id={0}", id));
            context.Response.Write(result);
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="context"></param>
        private void articleListInfo(HttpContext context)
        {
            string cateid = context.Request["cateid"];
            string p = context.Request["p"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrarticle", string.Format("action=articleListInfo&cateid={0}&p={1}", cateid,p));
            context.Response.Write(result);
        }

        /// <summary>
        /// 判断用户参与网吧活动的资格
        /// </summary>
        private void ispass(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrarticle", string.Format("action=ispass&sign={0}&uid={1}", sign, uid));
            context.Response.Write(result);
        }

        /// <summary>
        /// 兑换接口
        /// </summary>
        private void exchange(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrarticle", string.Format("action=exchange&sign={0}&uid={1}", sign, uid));
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