using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Taotaole.Common;
using YH.Utility;

namespace RedBook.internalapi
{
    /// <summary>
    /// taotaoleshareorder 的摘要说明
    /// </summary>
    public class taotaoleshareorder : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "productaffirm": //商品确认
                    productaffirm(context);
                    break;
                case "giftConfirm"://积分商城奖品确认
                    giftConfirm(context);
                    break;
                case "quanConfirm":
                    quanConfirm(context);
                    break;
                case "affirminfo": //用户确认商品信息
                    affirminfo(context);
                    break;
                case "confirmshouhuo": //确认收货
                    confirmshouhuo(context);
                    break;
            }
        }
        /// <summary>
        /// 商品确认
        /// 参数 sign 用户签名 orderid 订单ID 
        /// 返回状态 state 0获取信息成功 2未知的签名参数 3订单ID错误 4获取订单信息失败
        /// </summary>
        /// <param name="context"></param>
        private void productaffirm(HttpContext context)
        {
            //签名的验证
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string orderid = context.Request["orderid"];
            string shopid = context.Request["shopid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshareorder", string.Format("action=productaffirm&sign={0}&orderid={1}&uid={2}&shopid={3}", sign, orderid, uid, shopid));
            context.Response.Write(result);

        }

        /// <summary>
        /// 积分商城奖品确认
        /// 参数 sign 用户签名 orderid 订单ID 
        /// 返回状态 state 0获取信息成功 2未知的签名参数 3订单ID错误 4获取订单信息失败
        /// </summary>
        /// <param name="context"></param>
        private void giftConfirm(HttpContext context)
        {
            //签名的验证
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string orderid = context.Request["orderid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshareorder", string.Format("action=giftConfirm&sign={0}&orderid={1}&uid={2}", sign, orderid, uid));
            context.Response.Write(result);

        }

        /// <summary>
        /// 直购商品确认
        /// 参数 sign 用户签名 orderid 订单ID 
        /// 返回状态 state 0获取信息成功 2未知的签名参数 3订单ID错误 4获取订单信息失败
        /// </summary>
        /// <param name="context"></param>
        private void quanConfirm(HttpContext context)
        {
            //签名的验证
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string orderid = context.Request["orderid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshareorder", string.Format("action=quanConfirm&sign={0}&orderid={1}&uid={2}", sign, orderid, uid));
            context.Response.Write(result);
        }

        /// <summary>
        /// 用户确认商品信息
        /// 参数 sign 用户签名 orderid订单ID info用户填写的信息 mobile用户手机号
        /// 返回值 state 0卡密发送成功  2未知的签名参数 3订单ID错误 4用户信息有误 5手机号有误 （6获取订单信息有误 7商品库存不足，请稍后再试，或者联系客服宝宝）  10信息已提交 状态6、7针对发送卡密的状态  10针对实物、游戏，更新状态
        /// </summary>
        /// <param name="context"></param>
        private void affirminfo(HttpContext context)
        {
            //签名的验证
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string orderid = context.Request["orderid"];
            string info = context.Request["info"];
            string mobile = context.Request["mobile"];
            string kahao = context.Request["kahao"];
            string kami = context.Request["kami"];
            string usetype = context.Request["usetype"];
            string usenum = context.Request["usenum"];
            string usename = context.Request["usename"];
            string code = context.Request["code"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshareorder", string.Format("action=affirminfo&sign={0}&orderid={1}&uid={2}&info={3}&mobile={4}&kahao={5}&kami={6}&usetype={7}&usenum={8}&usename={9}&code={10}", sign, orderid, uid, info, mobile, kahao, kami, usetype, usenum, usename, code));
            context.Response.Write(result);

        }
        /// <summary>
        /// 确认收货
        /// </summary>
        private void confirmshouhuo(HttpContext context)
        {
            string sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            string uid = Globals.GetCurrentMemberUserIdSign();
            //uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1940");
            //sign = SecurityHelper.GetSign(uid);
            string orderid = context.Request["orderid"];
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrshareorder", string.Format("action=confirmshouhuo&sign={0}&orderid={1}&uid={2}", sign, orderid, uid));
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