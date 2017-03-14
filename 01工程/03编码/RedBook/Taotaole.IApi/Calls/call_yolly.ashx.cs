using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Taotaole.Common;
using Taotaole.Model;
using Taotaole.Bll;

namespace Taotaole.IApi.Calls
{
    /// <summary>
    /// call_yolly 的摘要说明
    /// </summary>
    public class call_yolly : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request["xmlMsg"] == null || context.Request["xmlMsg"] == "")
            {
                context.Response.Write("失败");
                Globals.DebugLogger("失败");
            }
            else
            {
                Globals.DebugLogger(context.Request["xmlMsg"]);
                string strxmlMsg = HttpUtility.UrlDecode(HttpUtility.UrlDecode(context.Request["xmlMsg"], UTF8Encoding.UTF8), UTF8Encoding.UTF8);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strxmlMsg);
                string state = "";
                string content = "";
                string serialid = "";
                XmlNodeList xxList = doc.GetElementsByTagName("YOLLY");
                foreach (XmlNode xxNode in xxList)  //Node 是每一个<CL>...</CL>体  
                {
                    XmlNode id = xxNode.SelectNodes("YOLLYFLOW").Item(0);
                    XmlNode node1 = xxNode.SelectNodes("RESPONSE/RSPCODE").Item(0);
                    XmlNode node2 = xxNode.SelectNodes("RESPONSE/RSPDESC").Item(0);
                    if (node1 != null)
                    {
                        state = node1.InnerText;
                    }
                    if (node2 != null)
                    {
                        content = node2.InnerText;
                    }
                    if (id != null)
                    {
                        serialid = id.InnerText;
                    }
                }
                Globals.DebugLogger(state + "," + content + "," + serialid);
                go_yolly_orderinfoEntity yolly_orderinfoEntity = go_yolly_orderinfoBusiness.LoadEntity(serialid);
                if (state == "0000")//充值成功
                {
                    yolly_orderinfoEntity.Issynchro = 0;
                    yolly_orderinfoEntity.Status = 1;
                    yolly_orderinfoEntity.Context = content;
                    yolly_orderinfoEntity.Paytype = "永乐充值";
                }
                else
                {
                    yolly_orderinfoEntity.Context = content;
                }
                go_yolly_orderinfoBusiness.SaveEntity(yolly_orderinfoEntity, false, YH.Utility.DbServers.DbServerName.LatestDB);

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