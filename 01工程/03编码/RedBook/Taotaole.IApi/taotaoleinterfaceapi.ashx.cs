using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Common;

namespace Taotaole.IApi
{
    /// <summary>
    /// taotaoleinterfaceapi 的摘要说明
    /// </summary>
    public class taotaoleinterfaceapi : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            switch (action)
            {
                case "selectyolly": //查询并同步永乐账单
                    selectyolly(context);
                    break;
                case "yollyrecharge": //后台网吧结算充值支付宝
                    yollyrecharge(context);
                    break;
            }
        }
        //通过流水号查询
        public void selectyolly(HttpContext context)
        {
            string serialid = context.Request["serialid"];
            string type = context.Request["type"];
            if (string.IsNullOrEmpty(serialid))
            {
                context.Response.Write("流水号错误");
            }
            if (string.IsNullOrEmpty(type))
            {
                context.Response.Write("类型错误");
            }
           string strxml= yollyinterface.telephoneyolly(serialid, type);
           context.Response.Write(strxml);
        }


        //后台网吧结算充值支付宝
        public void yollyrecharge(HttpContext context)
        {
            string now = context.Request["timenow"];
            string type = context.Request["type"];
            string money = context.Request["money"];
            string usenum = context.Request["usenum"];
            string Aserialid = context.Request["Aserialid"];
            string usename = context.Request["usename"];
            if (string.IsNullOrEmpty(now))
            {
                context.Response.Write("流水号错误");
            }
            if (string.IsNullOrEmpty(type))
            {
                context.Response.Write("类型错误");
            }
            if (string.IsNullOrEmpty(money))
            {
                context.Response.Write("流水号错误");
            }
            if (string.IsNullOrEmpty(usenum))
            {
                context.Response.Write("类型错误");
            }
            if (string.IsNullOrEmpty(Aserialid))
            {
                context.Response.Write("流水号错误");
            }
            if (string.IsNullOrEmpty(usename))
            {
                context.Response.Write("类型错误");
            }
            Globals.DebugLogger(now + "__" + usenum + "__" + money + "__" + Aserialid + "__" + type + "__" + usename);
            //顺序：时间—充值账号—金额—流水号—充值类型（1Q币，2支付宝）—用户姓名
            object[] Apost = new object[] { now, usenum, money, Aserialid, type, usename };
            string strxml = yollyinterface.APIyolly(Apost, yollyinterface.usetype.Alipay);
            Globals.DebugLogger(strxml);
            context.Response.Write(strxml);
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