using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Cache;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;
using YH.Weixin.MP;
using YH.Weixin.Pay;

namespace RedBook.WxPay
{
    /// <summary>
    /// 公共支付：WeixinAppId、WeixinAppSecret
    /// </summary>
    public partial class wx_SubmitV2 : System.Web.UI.Page
    {
        public string pay_json = "";
        #region 一元购需要参数
        //一元购支付成功后待提交的参数,如果支付成功,将这些参数取出来,用前台js调用相应的接口完成生成业务订单等业务流程
        public string sign;
        public string shopid;
        public string quantity;
        public string redpaper;
        public string APIsubmitcode;
        public string productid;
        public string otype = "0";
        #endregion
        #region 团购需要参数
        public string tuanType;
        public string businessid;
        public string tuanorderid;//团购充值订单号
        #endregion

        public ShopOrders.BusinessType businessType;
        private go_memberEntity currentMember;

        public string ConvertPayJson(PayRequestInfo req)
        {
            string packageValue = "{";
            packageValue = packageValue + " \"appId\": \"" + req.appId + "\", ";
            packageValue = packageValue + " \"timeStamp\": \"" + req.timeStamp + "\", ";
            packageValue = packageValue + " \"nonceStr\": \"" + req.nonceStr + "\", ";
            packageValue = packageValue + " \"package\": \"" + req.package + "\", ";
            packageValue += " \"signType\": \"MD5\", ";
            return packageValue + " \"paySign\": \"" + req.paySign + "\"}";
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                //解码用户
                if (!string.IsNullOrEmpty(Request.QueryString["uid"])   && !string.IsNullOrEmpty(Request.QueryString["sign"]))
                {
                    string uid = Globals.ValidateSign(Request["sign"], Request["uid"]);
                    if (string.IsNullOrEmpty(uid))
                    {
                        Response.Write("网络忙，请稍后！");
                        Response.End();
                        return;
                    }
                    HttpCookie cookie = new HttpCookie(Globals.TaotaoleMemberKey);
                    cookie.Value = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, uid.ToString());
                    cookie.Expires = DateTime.Now.AddYears(10);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    System.Threading.Thread.Sleep(10);
                    currentMember = go_memberBusiness.LoadEntity(int.Parse(uid));
                }
                else
                {
                    return;
                }

                if (currentMember == null)
                    currentMember = go_memberBusiness.GetCurrentMember();

                //获取支付OPENID
                if (!string.IsNullOrEmpty(Globals.GetMasterSettings().Pay_Domain) && string.IsNullOrEmpty(currentMember.Band))
                {
                    //获取当前请求页地址、微信授权属性、微信授权码
                    string currentRequestUrl = HttpContext.Current.Request.Url.ToString();
                    currentRequestUrl = System.Text.RegularExpressions.Regex.Replace(currentRequestUrl, "[\f\n\r\t\v]", "");
                    WeiXinOAuthAttribute oAuth2Attr = Attribute.GetCustomAttribute(this.GetType(), typeof(WeiXinOAuthAttribute)) as WeiXinOAuthAttribute;
                    string code = this.Page.Request.QueryString["code"];

                    //如果未到微信网关授权，先授权获取code
                    if (string.IsNullOrEmpty(code))
                    {
                        string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid="
                            + Globals.GetMasterSettings().Pay_WeixinAppId + "&redirect_uri=" + Globals.UrlEncode(currentRequestUrl)
                            + "&response_type=code&scope=snsapi_base&state=123"
                            + "#wechat_redirect";
                        try
                        {
                            //触发微信返回code码         
                            this.Page.Response.Redirect(url);//Redirect函数会抛出ThreadAbortException异常，不用处理这个异常
                        }
                        catch (System.Threading.ThreadAbortException ex)
                        {
                        }
                        return;
                    }

                    //从微信网关授权回来，获取微信用户信息
                    if (!string.IsNullOrEmpty(code))
                    {
                        string responseResult = this.GetResponseResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid="
                            + Globals.GetMasterSettings().Pay_WeixinAppId + "&secret=" + Globals.GetMasterSettings().Pay_WeixinAppSecret 
                            + "&code=" + code + "&grant_type=authorization_code");

                        //获取到用户信息，判断该用户是否已存在本系统数据库
                        JObject obj2 = JsonConvert.DeserializeObject(responseResult) as JObject;
                        currentMember.Band = obj2["openid"].ToString();//微信用户ＯＰＥＮＩＤ
                        go_memberBusiness.UpdateBand(currentMember);
                    }
                }

                //获取传过来的业务类型是否能转换成订单业务类型的枚举,以验证调用充值的合法性
                businessType = (ShopOrders.BusinessType)Enum.Parse(typeof(ShopOrders.BusinessType), base.Request.QueryString.Get("businesstype"));
                switch (businessType)
                {
                    case ShopOrders.BusinessType.add:
                        addGo();
                        break;
                    case ShopOrders.BusinessType.yuan:
                        yuanGo();
                        break;
                    case ShopOrders.BusinessType.tuan:
                        tuanGo();
                        break;
                }


            }
            catch (Exception ex)
            {
                //跳转到错误页面
            }
        }


        /// <summary>
        /// 获取支付的OPENID
        /// </summary>
        private string getPayOpenID()
        {
            string strOpenid = (currentMember == null) ? string.Empty : currentMember.Wxid;
            if (currentMember != null && !string.IsNullOrEmpty(Globals.GetMasterSettings().Pay_Domain))
            {
                strOpenid = currentMember.Band;
            }
            return strOpenid;
        }

        /// <summary>
        /// 充值业务
        /// </summary>
        protected void addGo()
        {
            if (!CacheHelper.IsOK("addGo_" + currentMember.Uid.ToString()))
            {
                Globals.DebugLogger("频繁调用接口【addGo】_" + currentMember.Uid.ToString(), "API_Log.txt");
                return;
            }

            string moneyStr = base.Request.QueryString.Get("money");
            if (string.IsNullOrEmpty(moneyStr)) Response.Redirect("errPage.aspx");
            decimal money = Convert.ToDecimal(moneyStr);
            //首先插入用户充值记录表
            string codeid = ShopOrders.GenerateOrderID(ShopOrders.BusinessType.add);//用户充值订单ID
            string itemDescription = "用户充值";
            PackageInfo package = new PackageInfo
            {
                Body = itemDescription,
                NotifyUrl = string.Format("http://{0}/WxPay/wx_Pay.aspx", base.Request.Url.Host),
                OutTradeNo = codeid,
                TotalFee = (int)(money * 100m)
            };

            go_member_addmoney_recordEntity addmoneyEntity = new go_member_addmoney_recordEntity()
            {
                Uid = currentMember.Uid,
                Code = codeid,
                Money = money,
                Pay_type = "微信支付",
                Status = "未付款",
                Time = DateTime.Now,
                Buy_type = 0,
                Memo = "add"
            };
            go_member_addmoney_recordBusiness.SaveEntity(addmoneyEntity, true);

            if (package.TotalFee < 1m)
            {
                package.TotalFee = 1m;
            }
            string openId = "";

            if (currentMember != null)
            {
                openId = getPayOpenID();// currentMember.Wxid;
            }
            package.OpenId = openId;
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            PayRequestInfo req = new PayClient(masterSettings.Pay_WeixinAppId, masterSettings.Pay_WeixinAppSecret, masterSettings.WeixinPartnerID, masterSettings.WeixinPartnerKey, masterSettings.WeixinPaySignKey).BuildPayRequest(package);
            this.pay_json = this.ConvertPayJson(req);
            CacheHelper.DelCache("addGo_" + currentMember.Uid.ToString());
        }

        /// <summary>
        /// 一元购业务
        /// </summary>
        protected void yuanGo()
        {
            if (!CacheHelper.IsOK("yuanGo_" + currentMember.Uid.ToString()))
            {
                Globals.DebugLogger("频繁调用接口【yuanGo】_" + currentMember.Uid.ToString(), "API_Log.txt");
                return;
            }

            //接收的参数
            sign = SecurityHelper.GetSign(Globals.GetCurrentMemberUserIdSign()); //base.Request.QueryString.Get("sign");
            shopid = base.Request.QueryString.Get("shopid");
            if (string.IsNullOrEmpty(shopid)) shopid = "0";
            productid = base.Request.QueryString.Get("productid");
            if (string.IsNullOrEmpty(productid)) productid = "0";
            quantity = base.Request.QueryString.Get("quantity");
            otype = base.Request.QueryString.Get("otype");
            if (string.IsNullOrEmpty(otype)) otype = "0";
            redpaper = string.IsNullOrEmpty(base.Request.QueryString.Get("redpaper")) ? "0" : base.Request.QueryString.Get("redpaper");//可以为空
            APIsubmitcode = string.IsNullOrEmpty(base.Request.QueryString.Get("APIsubmitcode")) ? "0" : base.Request.QueryString.Get("APIsubmitcode");//可以为空
            string moneyStr = base.Request.QueryString.Get("money");

            if (string.IsNullOrEmpty(moneyStr) || string.IsNullOrEmpty(shopid) || string.IsNullOrEmpty(quantity)) Response.Redirect("errPage.aspx");

            decimal money = Convert.ToDecimal(moneyStr);
            string memo = "yuan";
            //首先插入用户充值记录表
            string codeid = ShopOrders.GenerateOrderID(ShopOrders.BusinessType.add);//用户充值订单ID
            string itemDescription = "用户充值";
            PackageInfo package = new PackageInfo
            {
                Body = itemDescription,
                NotifyUrl = string.Format("http://{0}/WxPay/wx_Pay.aspx", base.Request.Url.Host),
                OutTradeNo = codeid,
                TotalFee = (int)(money * 100m)
            };

            go_member_addmoney_recordEntity addmoneyEntity = new go_member_addmoney_recordEntity()
            {
                Uid = currentMember.Uid,
                Code = codeid,
                Money = money,
                Pay_type = "微信支付",
                Status = "未付款",
                Time = DateTime.Now,
                Buy_type = 0,
                Tuanid = int.Parse(otype),
                Memo = memo,
            };
            //现将充值记录写入数据库,成功支付后在wx_pay.aspx.cs里取出相应的数据(uid等)并做后续的处理
            if (!go_member_addmoney_recordBusiness.SaveEntity(addmoneyEntity, true))
            {
                return;
            }
            if (package.TotalFee < 1m)
            {
                package.TotalFee = 1m;
            }
            string openId = "";

            if (currentMember != null)
            {
                openId = getPayOpenID();// currentMember.Wxid;
            }
            package.OpenId = openId;
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            PayRequestInfo req = new PayClient(masterSettings.Pay_WeixinAppId, masterSettings.Pay_WeixinAppSecret, masterSettings.WeixinPartnerID, masterSettings.WeixinPartnerKey, masterSettings.WeixinPaySignKey).BuildPayRequest(package);
            this.pay_json = this.ConvertPayJson(req);
            CacheHelper.DelCache("yuanGo_" + currentMember.Uid.ToString());
        }

        protected void tuanGo()
        {
            if (!CacheHelper.IsOK("tuanGo_" + currentMember.Uid.ToString()))
            {
                Globals.DebugLogger("频繁调用接口【tuanGo】_" + currentMember.Uid.ToString(), "API_Log.txt");
                return;
            }

            string moneyStr = base.Request.QueryString.Get("money");

            businessid = base.Request.QueryString.Get("shopid");

            tuanType = base.Request.QueryString.Get("tuanType");

            if (string.IsNullOrEmpty(moneyStr) || string.IsNullOrEmpty(businessid) || string.IsNullOrEmpty(tuanType)) Response.Redirect("errPage.aspx");

            tuanType = tuanType == "openTuan" ? "1" : "2";//参团方式(1团长开团、2成员参团)

            decimal money = Convert.ToDecimal(moneyStr);
            //首先插入用户充值记录表
            tuanorderid = ShopOrders.GenerateOrderID(ShopOrders.BusinessType.add);//用户充值订单ID
            string itemDescription = "团购支付";
            PackageInfo package = new PackageInfo
            {
                Body = itemDescription,
                NotifyUrl = string.Format("http://{0}/WxPay/wx_Pay.aspx", base.Request.Url.Host),
                OutTradeNo = tuanorderid,
                TotalFee = (int)(money * 100m)
            };

            //保存充值记录
            go_member_addmoney_recordEntity addmoneyEntity = new go_member_addmoney_recordEntity()
            {
                Uid = currentMember.Uid,
                Code = tuanorderid,
                Money = money,
                Pay_type = "微信支付",
                Status = "未付款",
                Time = DateTime.Now,
                Score = 1,
                Buy_type = 0,
                Memo = "tuan"
            };
            go_member_addmoney_recordBusiness.SaveEntity(addmoneyEntity, true);

            if (package.TotalFee < 1m)
            {
                package.TotalFee = 1m;
            }
            string openId = "";

            if (currentMember != null)
            {
                openId = getPayOpenID();// currentMember.Wxid;
            }
            package.OpenId = openId;
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            PayRequestInfo req = new PayClient(masterSettings.Pay_WeixinAppId, masterSettings.Pay_WeixinAppSecret, masterSettings.WeixinPartnerID, masterSettings.WeixinPartnerKey, masterSettings.WeixinPaySignKey).BuildPayRequest(package);
            this.pay_json = this.ConvertPayJson(req);
            CacheHelper.DelCache("tuanGo_" + currentMember.Uid.ToString());
        }


        private string GetResponseResult(string url)
        {
            using (HttpWebResponse response = (HttpWebResponse)WebRequest.Create(url).GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}