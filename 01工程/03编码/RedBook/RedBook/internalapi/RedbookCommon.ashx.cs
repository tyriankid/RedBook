using System.Web;
using Taotaole.Common;
using YH.Utility;

namespace RedBook.internalapi
{
    /// <summary>
    /// RedbookCommon 的摘要说明
    /// </summary>
    public class RedbookCommon : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "getBookList":
                    getBookList(context);
                    break;
                case "getBookContent":
                    getBookContent(context);
                    break;
            }


        }


        /// <summary>
        /// 获取文章详情
        /// </summary>
        private void getBookContent(HttpContext context)
        {
            //获取传参
            string bookid = context.Request["bookid"];
            if (string.IsNullOrEmpty(bookid)) bookid = "";
            
            string result = new WebUtils().DoPost(Globals.API_Domain + "rdbook", string.Format("action=getBookContent&bookid={0}", bookid));
            context.Response.Write(result);
        }


        /// <summary>
        /// 获取首页导购文章列表
        /// </summary>
        /// <param name="context"></param>
        private void getBookList(HttpContext context)
        {
            //获取传参
            string categoryid = context.Request["categoryid"];
            if (string.IsNullOrEmpty(categoryid) || !PageValidate.IsNumberPlus(categoryid)) categoryid = "";
            string order = context.Request["order"];
            if (string.IsNullOrEmpty(order)) order = "0";
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            string pagesize = context.Request["pagesize"];
            if (string.IsNullOrEmpty(pagesize) || !PageValidate.IsNumberPlus(pagesize)) pagesize = "1";
            
            string result = new WebUtils().DoPost(Globals.API_Domain + "rdbook", string.Format("action=getBookList&categoryId={0}&order={1}&p={2}&pagesize={3}", categoryid, order, p, pagesize));
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