using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;
using YH.Utility;

namespace RedBookPlatform.admin
{
    public partial class adminlist : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindPage();
                string action = Request["action"];
                switch (action)
                {
                    case "delete":
                        deleteUser();
                        break;
                    case  "updatepass":
                        updateadminpass();
                        break;
                    case "FrozenAdmin":
                        FrozenAdmin();
                        break;
                }
                  
               
            }
        }


        /// <summary>
        ///管理员账号冻结或解冻
        /// </summary>
        public void FrozenAdmin()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            int uid = Convert.ToInt32(Request.Form["uid"]);
            string ustate = Request.Form["state"];

            go_adminEntity admin = go_adminBusiness.LoadEntity(uid);
            admin.State = Convert.ToInt32(ustate == "1" ? "0" : "1");
            go_adminBusiness.SaveEntity(admin, false);
            builder.Append("{\"success\":true,\"msg\":\"操作成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();

        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        public void deleteUser()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            int uid = Convert.ToInt32(Request.Form["uid"]);
            go_adminBusiness.Del(uid);
            string name = Request.Form["name"].ToString();
            base.addAdminLog(operationAction.Delete, null, "删除了名为" + name + "的管理员"); 


            builder.Append("{\"success\":true,\"msg\":\"删除成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End(); 
        }
        /// <summary>
        /// 重置管理员密码
        /// </summary>
        public void updateadminpass()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            int uid = Convert.ToInt32(Request.Form["uid"]);
            go_adminEntity Entity = go_adminBusiness.LoadEntity(uid);
            Entity.Userpass = SecurityHelper.GetMd5To32("123456");
            go_adminBusiness.SaveEntity(Entity, false);
            builder.Append("{\"success\":true,\"msg\":\"重置密码成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End(); 
        }

        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            go_adminQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (username Like '%{0}%' or useremail like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }

            //调用通用分页方法绑定
            DbQueryResult managers = DataHelper.GetPageData(query, "go_admin", builder, "uid");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_adminQuery GetQuery()
        {
            go_adminQuery query = new go_adminQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortBy"]))
            {
                query.SortBy = this.Page.Request.QueryString["SortBy"];
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortOrder"]))
            {
                query.SortOrder = SortAction.Desc;
            }
            return query;
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="isSearch">是否查询(重置起始页码)</param>
        private void ReLoad(bool isSearch)
        {
            NameValueCollection queryStrings = new NameValueCollection();
            queryStrings.Add("keyword", this.tboxKeyword.Text.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "uid");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }



    }
}