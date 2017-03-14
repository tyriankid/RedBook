using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Xml;
using Taotaole.Common;
using Taotaole.Model;
using YH.Weixin.MP.Messages;

namespace RedBookPlatform.API
{
    /// <summary>
    /// wx 的摘要说明
    /// </summary>
    public class wx : IHttpHandler
    {

        private bool CheckSignature(HttpContext context)
        {
            string signature = context.Request.QueryString["signature"].ToString();
            string timestamp = context.Request.QueryString["timestamp"].ToString();
            string nonce = context.Request.QueryString["nonce"].ToString();
            string[] ArrTmp = { "8B5AADD5A0AFD888", timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序  
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            /*string echoStr = context.Request.QueryString["echoStr"].ToString();
            if (CheckSignature(context))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    context.Response.Write(echoStr);
                    context.Response.End();
                }
            } */


            
            System.Web.HttpRequest request = context.Request;
            try
            {
                CustomMsgHandler handler = new CustomMsgHandler(request.InputStream);
                handler.Execute();
                context.Response.Write(handler.ResponseDocument);
                WriteLog(handler.ResponseDocument);
                

            }
            catch (System.Exception exception)
            {
                WriteLog(exception.Message);
            }
            
        }

        private void WriteLog(string log)
        {
            System.IO.StreamWriter writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("~/wx_login.txt"));
            writer.WriteLine(System.DateTime.Now);
            writer.WriteLine(log);
            writer.Flush();
            writer.Close();
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