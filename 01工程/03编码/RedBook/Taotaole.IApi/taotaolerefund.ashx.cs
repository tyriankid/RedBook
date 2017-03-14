using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taotaole.IApi
{

    /// <summary>
    /// 退款接口
    /// </summary>
    public class taotaolerefund : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "orderRefund": //对订单退款
                    //orderRefund(context);
                    break;
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