using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taotaole.API
{
    /// <summary>
    /// wx 的摘要说明
    /// </summary>
    public class wx : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            /*System.Web.HttpRequest request = context.Request;
            string weixinToken = SettingsManager.GetMasterSettings(false).WeixinToken;
            string signature = request["signature"];
            string nonce = request["nonce"];
            string timestamp = request["timestamp"];
            string s = request["echostr"];
            if (request.HttpMethod == "GET")
            {
                if (CheckSignature.Check(signature, timestamp, nonce, weixinToken))
                {
                    context.Response.Write(s);
                }
                else
                {
                    context.Response.Write("");
                }
                context.Response.End();
            }
            else
            {
                try
                {
                    CustomMsgHandler handler = new CustomMsgHandler(request.InputStream);
                    handler.Execute();
                    context.Response.Write(handler.ResponseDocument);
                }
                catch (System.Exception exception)
                {
                    System.IO.StreamWriter writer = System.IO.File.AppendText(context.Server.MapPath("error.txt"));
                    writer.WriteLine(exception.Message);
                    writer.WriteLine(exception.StackTrace);
                    writer.WriteLine(System.DateTime.Now);
                    writer.Flush();
                    writer.Close();
                }
            }*/
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