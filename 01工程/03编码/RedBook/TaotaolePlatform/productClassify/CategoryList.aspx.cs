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

namespace RedBookPlatform.productClassify
{
    public partial class productCategory : Base
    {
        public string actions;
        public static string wz;
        public string display = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                 wz = Request["action"];
                if (wz == "wzlist")
                {
                    actions = "addwzfl";
                    BindPage(2);
                }
                if (wz == null)
                {
                    actions = "addspfl";
                    BindPage(1);
                }

                int code = getcode();
                if (code != 3)
                {
                    display = "display:none";
                }


                string action = Request["action"];
                switch (action)
                {
                    case "goDelete":
                        goDelete(Convert.ToInt32(Request["id"]), Request["name"]);
                        break;



                }



            }

        }

        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage(int Model)
        {
            //获得用户输入的条件
            go_categoryQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1  and Model='"+Model+"'");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (name like '%{0}%' or catdir like '%{0}%')", DataHelper.CleanSearchString(query.keyword.Trim(',')));
            }
            //调用通用分页方法绑定
            DbQueryResult category = DataHelper.GetPageData(query, "go_category", builder, "orders");
            this.rptAdmin.DataSource = category.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.selMultiple.Value = query.model;
            this.pager1.TotalRecords = category.TotalRecords;
            this.selMultiple.Value = this.Page.Request.QueryString["model"];
        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_categoryQuery GetQuery()
        {
            go_categoryQuery query = new go_categoryQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            query.SortBy = "orders";
            query.SortOrder = SortAction.Asc;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["model"]))
            {
                switch (this.Page.Request.QueryString["model"])
                {
                    case "1":
                        query.SortBy = "orders";
                        query.SortOrder = SortAction.Asc;
                        break;
                    case "2":
                        query.SortBy = "orders";
                        query.SortOrder = SortAction.Desc;
                        break;
                }
            }
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
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
            queryStrings.Add("model", selMultiple.Value.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }

     
        public void goDelete(int cateid,string name)
        {
            go_categoryBusiness.Del(cateid);

            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            base.addAdminLog(operationAction.Delete, null, "删除分类" + name);
            builder.Append("{\"success\":true,\"msg\":\"删除成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End(); 
        }
    
    }
}