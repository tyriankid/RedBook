using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using YH.Utility;

namespace RedBookPlatform
{
    public partial class Channel : Base
    {
        public static int uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            uid = this.CurrLoginChannelUser.Uid;
            object oState = go_channel_userBusiness.GetScalar(string.Format("uid = '{0}'", uid), "frozenstate");
            if (oState != null && oState.ToString() =="1")
            {
                Response.Write("<script>alert('对不起！该账号已被冻结')</script>");
                this.Page.Response.Redirect(Globals.ApplicationPath + "/agent.aspx", true);
            }
            if (Request.QueryString["out"] != null)
            {
                WebCache.Remove("ChannelUser_" + HttpContext.Current.Request.Cookies.Get("ChannelUser").Value);
                System.Web.HttpCookie cookies = new System.Web.HttpCookie("ChannelUser")
                {
                    Expires = System.DateTime.Now
                };
                System.Web.HttpContext.Current.Response.Cookies.Add(cookies);
                base.Response.Redirect("agent.aspx", true);
            }
        }
    }
}