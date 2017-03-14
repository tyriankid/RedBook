using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;

namespace RedBook
{
    public partial class cart_payment : System.Web.UI.Page
    {
        public ShopOrders.BusinessType businessType;
        #region 一元购需要属性
        public int yid;
        public int quantity;
        public int paymoney;//总需支付
        public int redpackNum;//红包数量
        public int leftMoney;//用户余额
        public string sign;
        public string uid;
        #endregion

        #region 团购需要属性
        public int tid;
        public string tuanType;
        #endregion

        #region 直购需要属性
        public int quanId;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try{
                string yidStr = Request.QueryString["yid"];

                if (string.IsNullOrEmpty(yidStr) || !PageValidate.IsNumberPlus(yidStr)) yid = 0;
                else
                {
                    businessType = ShopOrders.BusinessType.yuan;//支付页面为一元购使用
                    yid = Convert.ToInt32(yidStr);
                    quantity = Convert.ToInt32(Request.QueryString["quantity"]);
                }

                string tidStr = Request.QueryString["tid"];
                if (string.IsNullOrEmpty(tidStr) || !PageValidate.IsNumberPlus(tidStr)) tid = 0;
                else
                {
                    businessType = ShopOrders.BusinessType.tuan;;//支付页面为团购使用
                    tid = Convert.ToInt32(Request.QueryString["tidStr"]);
                    tuanType = Request.QueryString["tuantype"];
                }

                string quanIdStr = Request.QueryString["quanid"];
                if (string.IsNullOrEmpty(quanIdStr) || !PageValidate.IsNumberPlus(quanIdStr)) quanId = 0;
                else
                {
                    businessType = ShopOrders.BusinessType.quan;//支付页面为直购使用
                    quanId = Convert.ToInt32(quanIdStr);
                }
                
            }
            catch{
                //如果参数错误跳转到错误页面
            }
            if (!this.IsPostBack)
            {
                switch (businessType)
                {
                    case ShopOrders.BusinessType.yuan:
                        initYuan();
                        break;
                    case ShopOrders.BusinessType.tuan:
                        initTuan();
                        break;
                    case ShopOrders.BusinessType.quan:
                        initQuan();
                        break;
                }
                
            }
            
        }

        /// <summary>
        /// 载入一元购支付等信息
        /// </summary>
        protected void initYuan()
        {
            if (Globals.GetCurrentMemberUserId() == 0) Response.Redirect("/user.login.aspx");
            //首先调用用户支付接口
            sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            uid = Globals.GetCurrentMemberUserIdSign();
            /*
            //测试固定号
            uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1908");
            sign = SecurityHelper.GetSign(uid);
             */ 
            string postUserPay = string.Format("action=userpayment&shopid={0}&quantity={1}&sign={2}&uid={3}", yid, quantity, sign,uid);
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrpay", postUserPay);
            

            JObject payInfo = JsonConvert.DeserializeObject(result) as JObject;
            switch (payInfo["state"].ToString())
            {
                case "0"://验证通过,获取红包列表,获取总支付金额
                    rptRedpack.DataSource = payInfo["data"];
                    rptRedpack.DataBind();
                    paymoney = Convert.ToInt32(Convert.ToDecimal(payInfo["paymoney"].ToString()));
                    leftMoney = Convert.ToInt32(Convert.ToDecimal(payInfo["money"].ToString()));
                    if (payInfo["data"].Type == JTokenType.Array) redpackNum = ((JArray)payInfo["data"]).Count;
                    break;

                case "4"://跳转到新一期
                    Response.Write(string.Format("<script>if (confirm('当前商品已经开奖，是否跳转到最新一期？')) location.href='{0}' </script>","/index.item.aspx?shopid=" + payInfo["newshopid"]));
                    break;
            }

        }

        /// <summary>
        /// 载入直购支付信息
        /// </summary>
        protected void initQuan()
        {
            if (Globals.GetCurrentMemberUserId() == 0) Response.Redirect("/user.login.aspx");
            //首先调用用户支付接口
            sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign());
            uid = Globals.GetCurrentMemberUserIdSign();

            string postUserPay = string.Format("action=userpaymentQuan&quanid={0}&sign={1}&uid={2}", quanId, sign, uid);
            string result = new WebUtils().DoPost(Globals.API_Domain + "ilerrpay", postUserPay);
            JObject payInfo = JsonConvert.DeserializeObject(result) as JObject;
            switch (payInfo["state"].ToString())
            {
                case "0"://验证通过,获取总支付金额
                    rptRedpack.DataSource = payInfo["data"];
                    rptRedpack.DataBind();
                    paymoney = Convert.ToInt32(Convert.ToDecimal(payInfo["paymoney"].ToString()));
                    leftMoney = Convert.ToInt32(Convert.ToDecimal(payInfo["leftmoney"].ToString()));
                    if (payInfo["data"].Type == JTokenType.Array) redpackNum = ((JArray)payInfo["data"]).Count;
                    break;

                default:
                    
                    break;
            }
        }

        /// <summary>
        /// 载入团购支付信息
        /// </summary>
        protected void initTuan()
        {

        }


    }
}