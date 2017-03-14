using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;

namespace TaotaolePlatform
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string action = Request["action"];
                switch (action)
                {
                    case "login":
                        channeluserLogin();
                        break;
                }
            }
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("ChannelUser");
          
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {

            }
            else
            {
                string uid = SecurityHelper.Decrypt("uid", HttpContext.Current.Request.Cookies.Get("ChannelUser").Value);
                go_channel_userEntity classify = go_channel_userBusiness.LoadEntity(int.Parse(uid));
                if (classify.Parentid == 0)
                    this.Page.Response.Redirect(Globals.ApplicationPath + "/HomePage.aspx", true);
                else
                    this.Page.Response.Redirect(Globals.ApplicationPath + "/Channel.aspx", true);

            }

        }
        public void channeluserLogin()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            string username = Request.Form["username"];
            string userpass = Request.Form["userpass"];
            string jzmm = Request.Form["jzmm"];
            string okpass = SecurityHelper.GetMd5To32(userpass).ToLower();
            DataTable dt = go_channel_userBusiness.GetListData("username='" + username + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["userpass"].ToString() == okpass)
                {
                    if (jzmm == "1")
                    {
                        //设置cookie
                        System.Web.HttpCookie cookie = new System.Web.HttpCookie("ChannelUser")
                        {
                            Value = SecurityHelper.Encrypt("uid", dt.Rows[0]["uid"].ToString()),
                            Expires = System.DateTime.Now.AddDays(7.0),//Expires = System.DateTime.Now.AddDays(1.0) 
                        };
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                    if (jzmm == "2")
                    {
                        System.Web.HttpCookie cookie = new System.Web.HttpCookie("ChannelUser")
                        {
                            Value = SecurityHelper.Encrypt("uid", dt.Rows[0]["uid"].ToString())
                        };
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                    }

                    builder.Append("{\"success\":true,\"msg\":\"登录成功！\"}");
                }
                else
                {
                    builder.Append("{\"success\":false,\"msg\":\"密码错误！\"}");
                }
            }
            else
            {
                builder.Append("{\"success\":false,\"msg\":\"无效用户信息！\"}");
            }
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();
        }
    }
}