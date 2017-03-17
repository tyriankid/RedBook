using System.Web;
using Taotaole.Common;
using YH.Utility;

namespace RedBook.internalapi
{
    /// <summary>
    /// taotaolepay 的摘要说明
    /// </summary>
    public class taotaolepay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string action = context.Request["action"];
            switch (action)
            {
                case "userpayment":             //用户付款
                    userpayment(context);
                    break;
                case "userpaymenttuan":         //用户验证
                    userpaymenttuan(context);
                    break;
                case "userpaymentsubmit":       //用户提交付款
                    userpaymentsubmit(context);
                    break;
                case "userpaymenttuansubmit":   //用户提交团购付款
                    userpaymenttuansubmit(context);
                    break;
                case "userpaymentQuan":
                    userpaymentQuan(context);
                    break;
                case "userpaymentQuanSubmit": // 用户提交直接购买付款
                    userpaymentQuanSubmit(context);
                    break;
            }
        }
        /// <summary>
        /// 用户付款
        /// </summary>
        private void userpayment(HttpContext context)
        {
            //设置参数
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string shopid = context.Request["shopid"];
            string quantity = context.Request["quantity"];
            string redpaper = context.Request["redpaper"];    
            string ip = NetworkHelper.GetBuyIP();
            //输出JSON
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrpay", string.Format("action=userpayment&sign={0}&shopid={1}&quantity={2}&uid={3}&ip={4}"
                , sign, shopid, quantity, uid, ip));
            context.Response.Write(result);
        }

        /// <summary>
        /// 用户团购付款
        /// </summary>
        private void userpaymenttuan(HttpContext context)
        {
            //设置参数
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();

            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1948");
            //sign = SecurityHelper.GetSign(uid);

            string businessId = context.Request["businessId"];
            string typeid = context.Request["typeid"];

            //输出JSON
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrpay"
                , string.Format("action=userpaymenttuan&sign={0}&uid={1}&typeid={2}&businessId={3}"
                , sign, uid, typeid, businessId));
            context.Response.Write(result);
        }

        /// <summary>
        /// 用户提交付款
        /// </summary>
        private void userpaymentsubmit(HttpContext context)
        {
            //设置参数
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string shopid = context.Request["shopid"];
            string productid = context.Request["productid"];
            string quantity = context.Request["quantity"];
            string redpaper = context.Request["redpaper"];
            string APIsubmitcode = context.Request["APIsubmitcode"];
            string paytype = context.Request["paytype"];
            string ip = context.Request["ip"];
            if (string.IsNullOrEmpty(ip)) ip = NetworkHelper.GetBuyIP();
           /* Globals.DebugLogger("用户提交付款:" + string.Format("action=userpaymentsubmit&sign={0}&shopid={1}&productid={2}&quantity={3}&redpaper={4}&APIsubmitcode={5}&paytype={6}&uid={7}&ip={8}"
                , sign, shopid, productid, quantity, redpaper, APIsubmitcode, paytype, uid, ip));*/
            //输出JSON
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrpay"
                , string.Format("action=userpaymentsubmit&sign={0}&shopid={1}&productid={2}&quantity={3}&redpaper={4}&APIsubmitcode={5}&paytype={6}&uid={7}&ip={8}"
                , sign, shopid, productid, quantity, redpaper, APIsubmitcode, paytype, uid, ip));
            context.Response.Write(result);
        }

        /// <summary>
        /// 用户直购余额验证
        /// </summary>
        private void userpaymentQuan(HttpContext context)
        {
            //设置参数
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string quanid = context.Request["quanid"];
            string ip = NetworkHelper.GetBuyIP();

            //输出JSON
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrpay", string.Format("action=userpaymentQuan&sign={0}&quanid={1}&uid={2}&ip={3}"
                , sign, quanid, uid, ip));
            context.Response.Write(result);
        }

        /// <summary>
        /// 用户提交直购付款
        /// </summary>
        private void userpaymentQuanSubmit(HttpContext context)
        {
            //设置参数
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string quanid = context.Request["quanid"];
            string APIsubmitcode = context.Request["APIsubmitcode"];
            string paytype = context.Request["paytype"];
            string redpaper = context.Request["redpaper"];
            string ip = context.Request["ip"];
            if (string.IsNullOrEmpty(ip)) ip = NetworkHelper.GetBuyIP();
            /* Globals.DebugLogger("用户提交付款:" + string.Format("action=userpaymentsubmit&sign={0}&shopid={1}&productid={2}&quantity={3}&redpaper={4}&APIsubmitcode={5}&paytype={6}&uid={7}&ip={8}"
                 , sign, shopid, productid, quantity, redpaper, APIsubmitcode, paytype, uid, ip));*/
            //输出JSON
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrpay"
                , string.Format("action=userpaymentQuanSubmit&sign={0}&quanid={1}&APIsubmitcode={2}&paytype={3}&uid={4}&redpaper={5}&ip={6}"
                , sign, quanid, APIsubmitcode, paytype, uid,redpaper, ip));
            context.Response.Write(result);
        }


        /// <summary>
        /// 用户提交付款
        /// </summary>
        private void userpaymenttuansubmit(HttpContext context)
        {
            //设置参数
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            string businessId = context.Request["businessId"];
            string typeid = context.Request["typeid"];
            string tuanorderid = context.Request["tuanorderid"];
            
            //输出JSON
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrpay"
                , string.Format("action=userpaymenttuansubmit&sign={0}&uid={1}&typeid={2}&businessId={3}&tuanorderid={4}"
                , sign, uid, typeid, businessId, tuanorderid));
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