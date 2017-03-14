using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;
using YH.Utility;

namespace RedBookPlatform.channel
{
    public partial class ChangePassword :Base
    {
        public string UserName;
        protected void Page_Load(object sender, EventArgs e)
        {
            //验证页面权限
            if (!this.IsPageRight("city,channel"))
            {
                Response.Redirect("/Resources/403.html");
                return;
            }
            if (!IsPostBack)
            {
                int uid = this.CurrLoginChannelUser.Uid;
                UserName = this.CurrLoginChannelUser.Username;
                string action = Request["action"];
                switch (action)
                {
                    case "ResettingPass":
                        ResetPass(uid);
                        break;
                }
            }

        }
        /// <summary>
        /// 更改管理员密码
        /// </summary>
        /// <param name="uid"></param>
        protected void ResetPass(int uid)
        {

            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            string userpass = Request.Form["Oldpass"];
            string okpass = SecurityHelper.GetMd5To32(userpass).ToLower();
            go_channel_userEntity userEntity = go_channel_userBusiness.LoadEntity(uid);
            if (okpass == userEntity.Userpass)
            {
                string newpass = Request.Form["NewPass"];
                string oknewpass = SecurityHelper.GetMd5To32(newpass).ToLower();
                userEntity.Userpass = oknewpass;
                go_channel_userBusiness.SaveEntity(userEntity, false);
                builder.Append("{\"success\":true,\"msg\":\"更新成功！\"}");
            }
            else
            {
                builder.Append("{\"success\":false,\"msg\":\"原始密码错误！\"}");
            }
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();
        }
    }
}