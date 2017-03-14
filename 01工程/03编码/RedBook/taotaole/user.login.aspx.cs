using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Common;
using YH.Utility;

namespace RedBook
{
    public partial class user_login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string key = Globals.GetMasterSettings(false).WX_Template_00;
                string uid = Request.QueryString[key];
                if (!string.IsNullOrEmpty(uid) && PageValidate.IsNumberPlus(uid))
                {
                    HttpCookie cookie = new HttpCookie(Globals.TaotaoleMemberKey);
                    cookie.Value = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, uid);
                    cookie.Expires = DateTime.Now.AddYears(10);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    Response.Redirect("index.aspx");
                }
                else
                {
                    System.Web.HttpCookie cookies = new System.Web.HttpCookie(Globals.TaotaoleMemberKey)
                    {
                        Expires = System.DateTime.Now
                    };
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookies);
                    //Response.Redirect("index.aspx");
                }
            }
        }
    }
}