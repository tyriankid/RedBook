using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;

namespace RedBookPlatform.admin
{
    public partial class ResettingPass : Base
    {
        public string AdminName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int uid = this.CurrLoginAdmin.Uid;
                AdminName = this.CurrLoginAdmin.Username;
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
            string okpass = SecurityHelper.GetMd5To32(userpass);
            go_adminEntity adminEntity = go_adminBusiness.LoadEntity(uid);
            if (okpass == adminEntity.Userpass)
            {
                string newpass = Request.Form["NewPass"];
                string oknewpass = SecurityHelper.GetMd5To32(newpass);
                adminEntity.Userpass = oknewpass;
                if (go_adminBusiness.SaveEntity(adminEntity, false,DbServers.DbServerName.LatestDB))
                {
                    builder.Append("{\"success\":true,\"msg\":\"更新成功！\"}");
                }
                else
                {
                    builder.Append("{\"success\":false,\"msg\":\"更新失败！\"}");
                }
                
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