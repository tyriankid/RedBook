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

namespace RedBookPlatform
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string action = Request["action"];
                switch (action)
                {
                    case "login":
                        adminlogin();
                        break;
                }
            }
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("TTL_Admin");
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {

            }
            else
            {

                //this.Page.Response.Redirect(Globals.ApplicationPath + "/index.aspx", true);
                string url = Globals.ApplicationPath + "/index.aspx";
                Response.Write(" <script>parent.window.location.href= '" + url + "' </script> ");

            }

        }
        public void adminlogin()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            string username = Request.Form["username"];
            string userpass = Request.Form["userpass"];
            string jzmm = Request.Form["jzmm"];
            string okpass = SecurityHelper.GetMd5To32(userpass);
            DataTable dt = go_adminBusiness.GetListData("username='" + username + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["userpass"].ToString() == okpass)
                {
                    if (jzmm == "1")
                    {
                        //设置cookie
                        System.Web.HttpCookie cookie = new System.Web.HttpCookie("TTL_Admin")
                        {
                            Value = SecurityHelper.Encrypt("uid", dt.Rows[0]["uid"].ToString()),
                            Expires = System.DateTime.Now.AddDays(7.0),//Expires = System.DateTime.Now.AddDays(1.0) 
                        };
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                        Globals.menujson = dt.Rows[0]["menujson"].ToString(); //获取权限字段

                    }
                    if (jzmm == "2")
                    {
                        System.Web.HttpCookie cookie = new System.Web.HttpCookie("TTL_Admin")
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

            addlog(username, builder.ToString());


            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();
        }

        private void addlog(string name,string msg)
        {
            go_adminlogsEntity entity = new go_adminlogsEntity();
            entity.Username = name;
            entity.Loginip = NetworkHelper.GetRequestIp();
            entity.Actiontime = DateTime.Now;
            entity.Actionform = Page.Title;
            entity.Action = operationAction.Login;
            entity.Infobefore = "";
            DataTable dtmsg = DataHelper.JsonToDataTable("["+msg+"]");
            dtmsg.Rows[0]["msg"].ToString();
            entity.Infoafter = dtmsg.Rows[0]["msg"].ToString(); ;
            go_adminlogsBusiness.SaveEntity(entity, true);
        }
    }
}