using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace YH.Weixin.Pay
{
    public class AppRefund
    {
        /// <summary>
        /// * 申请退款完整业务流程逻辑
        /// </summary>
        /// <param name="transaction_id">微信订单号（优先使用）</param>
        /// <param name="out_trade_no">商户订单号</param>
        /// <param name="total_fee">订单总金额</param>
        /// <param name="refund_fee">退款金额</param>
        /// <returns>退款结果</returns>
        public static bool Run(string transaction_id, string out_trade_no, int total_fee, int refund_fee, bool isWeixinJS)
        {
            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(transaction_id))//微信订单号存在的条件下，则已微信订单号为准
            {
                data.SetValue("transaction_id", transaction_id);
            }
            else//微信订单号不存在，才根据商户订单号去退款
            {
                data.SetValue("out_trade_no", out_trade_no);
            }

            data.SetValue("total_fee", total_fee);//订单总金额
            data.SetValue("refund_fee", refund_fee);//退款金额
            data.SetValue("out_refund_no", AppWxPayApi.GenerateOutTradeNo());//随机生成商户退款单号

            //如果是微信退款,调用爽乐购微信端(贝格互动)的公众号内容,否则调用appWx(爽乐购)的公众号内容
            data.SetValue("op_user_id", AppWxPayConfig.MCHID);//操作员，默认为商户号

            WxPayData result = AppWxPayApi.Refund(data, isWeixinJS);//提交退款申请给API，接收返回数据

            string xml = result.ToPrintStr();
            int i = xml.IndexOf("result_code");
            if (i > -1)
            {
                if (xml.Substring(i + 12, 7).ToUpper() == "SUCCESS")
                {
                    return true;
                }
            }
            Taotaole.Common.Globals.DebugLogger("Refund.Run:" + xml);
            return false;// result.ToPrintStr();
        }
    }
}