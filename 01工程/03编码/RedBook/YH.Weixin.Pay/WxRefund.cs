using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;

namespace YH.Weixin.Pay
{
    public class WxRefund
    {
        public static bool refund(string orderid)
        {
            /*
info.appId = this.m_payAccount.AppId;

info.timeStamp = Utils.GetCurrentTimeSeconds().ToString();

info.package = this.BuildPackage(package);//生成prepay_id

info.nonceStr = Utils.CreateNoncestr();



PayDictionary parameters = new PayDictionary();

parameters.Add("appId", info.appId);

parameters.Add("timeStamp", info.timeStamp);

parameters.Add("package", info.package);

parameters.Add("nonceStr", info.nonceStr);

parameters.Add("signType", info.signType);



info.paySign = SignHelper.SignPay(parameters, this.m_payAccount.PartnerKey);
 */
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";

            go_ordersEntity orderinfo = go_ordersBusiness.LoadEntity(orderid);
            if (orderinfo == null) return false;

            //生成签名
            IDictionary<string,string> parameters = new Dictionary<string,string>();
            parameters.Add("appId", masterSettings.WeixinAppId);

            parameters.Add("timeStamp", Utils.GetCurrentTimeSeconds().ToString());

            return true;
            
            /*
            parameters.Add("package", info.package);

            parameters.Add("nonceStr", info.nonceStr);

            parameters.Add("signType", info.signType);
             */ 

            //生成xml

            //提交接口获取状态
        }
    }
}
