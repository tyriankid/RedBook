using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;
using YH.Utility;
using System.Data;
using System.Text;
using Taotaole.Common;
namespace RedBookPlatform.admin
{
    public partial class addadmin : Base
    {
        bool isSave = true;
        public string active = null;
        public string Showusername = null;
        public string Showuseremail = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            //判断添加管理员还是编辑管理员，前台显示不同
            if (Request.QueryString["uid"] == null)
            {
                active = "添加管理员";
            }
            else
            {
                isSave = false;
                active = "编辑管理员";
                BindAdmin();
            }
            switch (action)
            {
                case "hold":
                    AddUserOrUpUser();
                    break;
            }
        }
        /// <summary>
        /// 编辑管理员绑定数据
        /// </summary>
        public void BindAdmin()
        {
            go_adminEntity entity = go_adminBusiness.LoadEntity(Convert.ToInt32(Request.QueryString["uid"]));
            Showusername = entity.Username;
            Showuseremail = entity.Useremail;
        }
        /// <summary>
        /// 更新或添加管理员操作
        /// </summary>
        public void AddUserOrUpUser()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            //获取 ajax post提交过来的用户信息
            string username = Request.Form["username"];
            string userpass = Request.Form["userpass"];
            string okpass = SecurityHelper.GetMd5To32(userpass);
            string useremail = Request.Form["useremail"];

            //添加管理员时，判断用户名是否存在
            if (isSave)
            {
                DataTable dt = go_adminBusiness.GetListData("username='" + username + "'");
                if (dt.Rows.Count > 0)
                {
                    builder.Append("{\"success\":false,\"msg\":\"用户名已存在！\"}");
                    Response.Clear();
                    Response.ContentType = "text/plain";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.Write(builder.ToString());
                    Response.End();
                }


                //判断是做更新管理员信息还是添加管理员
                go_adminEntity entity = (isSave) ? new go_adminEntity() : go_adminBusiness.LoadEntity(Convert.ToInt32(Request.QueryString["uid"]));
                entity.Username = username;
                entity.Useremail = useremail;
                if (isSave)
                    entity.Userpass = okpass;
                entity.Loginip = NetworkHelper.GetRequestIp();
                entity.Addtime = DateTime.Now;
                entity.Logintime = DateTime.Now;
                go_adminBusiness.SaveEntity(entity, isSave);
                builder.Append("{\"success\":true,\"msg\":\"添加成功！\"}");
                base.addAdminLog(operationAction.Add, null, "增加了一个管理员" + username);  //添加操作日志


            }
            else
            {
                go_adminEntity entity = new go_adminEntity();
                entity.Uid = Convert.ToInt32(Request.QueryString["uid"]);
                entity.Username = username;
                entity.Useremail = useremail;
                entity.Userpass = okpass;
                entity.Loginip = NetworkHelper.GetRequestIp();
                entity.Addtime = DateTime.Now;
                entity.Logintime = DateTime.Now;
                go_adminBusiness.SaveEntity(entity, isSave);
                base.addAdminLog(operationAction.Update, null, "修改了管理员" + username);  //添加操作日志
                builder.Append("{\"success\":false,\"msg\":\"更改成功！\"}");


            }




            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();
        }
    }
}