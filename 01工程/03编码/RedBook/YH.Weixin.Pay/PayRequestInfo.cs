using System;
using System.Collections.Generic;
using System.Text;

namespace YH.Weixin.Pay
{
    public class PayRequestInfo
    {
        public string appId { get; set; }

        public string nonceStr { get; set; }

        public string package { get; set; }

        public string paySign { get; set; }

        public string signType
        {
            get
            {
                return "MD5";
            }
        }

        public string timeStamp { get; set; }
    }
}
