using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.Pay
{
    public class WxPayException : Exception
    {
        public WxPayException(string msg)
            : base(msg)
        {

        }
    }
}
