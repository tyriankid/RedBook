using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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