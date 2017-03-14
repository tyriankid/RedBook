using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;

namespace YH.Weixin.MP
{
    /// <summary>
    /// 微信授权登录,继承页面会自动调用微信登录
    /// </summary>
    public class VWeiXinOAuth : Page
    {
        static readonly bool isWxLogger = true; //微信日志开关

        protected override void OnInit(EventArgs e)
        {
            VWeiXinOAuthInit();
            base.OnInit(e);
        }


        private void VWeiXinOAuthInit()
        {
                //当前用户已存在并登录
                go_memberEntity currentMember = go_memberBusiness.GetCurrentMember();
                if (currentMember != null) return;
                //是否启用微信登录
                SiteSettings masterSettings = Globals.GetMasterSettings();
                if (!masterSettings.IsOpenWeixin) return;

                //获取当前请求页地址、微信授权属性、微信授权码
                string currentRequestUrl = HttpContext.Current.Request.Url.ToString();
                currentRequestUrl = System.Text.RegularExpressions.Regex.Replace(currentRequestUrl, "[\f\n\r\t\v]", "");
                WeiXinOAuthAttribute oAuth2Attr = Attribute.GetCustomAttribute(this.GetType(), typeof(WeiXinOAuthAttribute)) as WeiXinOAuthAttribute;
                string code = this.Page.Request.QueryString["code"];
                //如果未到微信网关授权，先授权获取code
                if (string.IsNullOrEmpty(code))
                {
                    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid="
                        + masterSettings.WeixinAppId + "&redirect_uri=" + Globals.UrlEncode(currentRequestUrl) + "&response_type=code&scope=snsapi_userinfo&state=STATE"
                        + "#wechat_redirect";
                    this.Page.Response.Redirect(url, true);
                    return;
                }
                //从微信网关授权回来，获取微信用户信息
                if (!string.IsNullOrEmpty(code))
                {
                    string responseResult = this.GetResponseResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid="
                        + masterSettings.WeixinAppId + "&secret=" + masterSettings.WeixinAppSecret + "&code=" + code + "&grant_type=authorization_code");

                    //获取用户信息失败，跳转至首页
                    if (!responseResult.Contains("access_token"))
                    {
                        this.Page.Response.Redirect(Globals.ApplicationPath + "/index.aspx");
                        return;
                    }
                    //获取到用户信息，判断该用户是否已存在本系统数据库
                    JObject obj2 = JsonConvert.DeserializeObject(responseResult) as JObject;
                    string openId = obj2["openid"].ToString();//微信用户ＯＰＥＮＩＤ
                    string unionId = (obj2["unionid"] == null) ? "" : obj2["unionid"].ToString(); //微信用户UNIONID
                    string where = string.IsNullOrEmpty(unionId) ? string.Format("wxid='{0}'", openId) : string.Format("unionid='{0}' or wxid='{1}'", unionId, openId);
                    IList<go_memberEntity> listmemberEntity = go_memberBusiness.GetListEntity(where);
                    if (listmemberEntity.Count > 0)
                    {
                        //用户已存在，更新APP用户的WXID
                        if (string.IsNullOrEmpty(listmemberEntity[0].Wxid))
                        {
                            listmemberEntity[0].Wxid = openId;//修改微信Openid
                            go_memberBusiness.SaveEntity(listmemberEntity[0], false);
                        }
                        if (string.IsNullOrEmpty(listmemberEntity[0].Unionid))
                        {
                            listmemberEntity[0].Unionid = unionId;//修改UNIONID
                            go_memberBusiness.SaveEntity(listmemberEntity[0], false);
                        }
                        SetCurrUserCookie(listmemberEntity[0].Uid);
                    }
                    else
                    {
                        string wxUserInfoStr = this.GetResponseResult("https://api.weixin.qq.com/sns/userinfo?access_token=" + obj2["access_token"].ToString() + "&openid=" + obj2["openid"].ToString() + "&lang=zh_CN");
                        if (!wxUserInfoStr.Contains("nickname"))
                        {
                            this.Page.Response.Redirect(Globals.ApplicationPath + "/index.aspx");//获取用户昵称失败，跳转至首页
                            return;
                        }
                        JObject wxUserInfo = JsonConvert.DeserializeObject(wxUserInfoStr) as JObject;
                        string usercity = string.IsNullOrEmpty(wxUserInfo["city"].ToString()) ? "" : wxUserInfo["city"].ToString().Replace("市", "");
                        string strunionid = (wxUserInfo["unionid"] == null) ? "" : wxUserInfo["unionid"].ToString();
                        if (this.SkipWinxinOpenId(Globals.UrlDecode(wxUserInfo["nickname"].ToString()), wxUserInfo["openid"].ToString(), string.IsNullOrEmpty(wxUserInfo["headimgurl"].ToString()) ? "" : wxUserInfo["headimgurl"].ToString(), Page.Request["state"], strunionid, usercity))
                        {
                            listmemberEntity = go_memberBusiness.GetListEntity(string.Format("wxid='{0}'", wxUserInfo["openid"].ToString()));
                            SetCurrUserCookie(listmemberEntity[0].Uid);
                            this.Page.Response.Redirect(currentRequestUrl);
                        }
                        else
                        {
                            this.Page.Response.Redirect(Globals.ApplicationPath + "/index.aspx");
                        }
                    }
                }

            

        }

        /// <summary>
        /// 设置当前登录用户COOKIE
        /// </summary>
        private void SetCurrUserCookie(int uid)
        {
            HttpCookie cookie = new HttpCookie(Globals.TaotaoleMemberKey);
            cookie.Value = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, uid.ToString());
            cookie.Expires = DateTime.Now.AddYears(10);
            HttpContext.Current.Response.Cookies.Add(cookie);
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

        bool SkipWinxinOpenId(string userName, string openId, string headimgurl, string state, string unionid, string usercity)
        {
            try
            {

                WxLogger("开始创建用户");
                bool flag = false;

                int channel_type = 0;//推广人类型(0渠道商 1城市服务商)
                go_memberEntity member = new go_memberEntity();
                member.Username = userName;
                member.Wxid = openId;
                member.Typeid = 0;//微信注册
                member.Time = DateTime.Now;
                member.Email = "@localhost.com";
                member.Password = SecurityHelper.GetMd5To32(Globals.GetGenerateId());
                member.Headimg = headimgurl;//用户头像
                member.Unionid = unionid;
                //member.Money=200000;//余额

                string Uid = Request.QueryString["agentid"];
                if (!string.IsNullOrEmpty(Uid) && !string.IsNullOrEmpty(SecurityHelper.Decrypt(Globals.TaotaolePlatformKey, Uid)))
                {
                    //更改渠道商商
                    int DecryptUid = Convert.ToInt32(SecurityHelper.Decrypt(Globals.TaotaolePlatformKey, Uid));
                    go_channel_userEntity classify = go_channel_userBusiness.LoadEntity(DecryptUid);
                    if (classify != null)
                    {
                        member.Servicecityid = classify.Parentid;
                        member.Servicechannelid = DecryptUid;
                    }
                }
                else if (usercity != "")
                {
                    //更改城市服务商
                    object oServicecityid = go_channel_userBusiness.GetScalar(string.Format("parentid=0 And cityid='{0}'", usercity));
                    if (oServicecityid != null)
                    {
                        member.Servicecityid = int.Parse(oServicecityid.ToString());
                        channel_type = 1;
                        Uid = oServicecityid.ToString();
                    }

                }
                WxLogger("开始保存");
                //保存用户到数据库
                if (go_memberBusiness.SaveEntity(member, true))
                {
                    WxLogger("保存成功");
                    go_channel_userBusiness.AddUsercount((string.IsNullOrEmpty(Uid) ? 0 : int.Parse(Uid)), channel_type);
                    flag = true;
                }
                return flag;
            }
            catch (Exception ex)
            {
                WxLogger(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 微信日志
        /// </summary>
        /// <param name="log"></param>
        void WxLogger(string log)
        {
            if (!isWxLogger) return;
            string logFile = Page.Request.MapPath("~/wx_login.txt");
            File.AppendAllText(logFile, string.Format("{0}:{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), log));
        }


    }
}
