using System.Web;

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

            }
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