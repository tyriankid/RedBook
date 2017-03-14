using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
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
    public partial class brandList :Base
    {
        public string display = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindPage();
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
        private void BindPage()
        {
            //获得用户输入的条件
            go_brandQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (name like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            //调用通用分页方法绑定
            DbQueryResult category = DataHelper.GetPageData(query, "go_brand", builder, "id");
            this.rptAdmin.DataSource = category.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = category.TotalRecords;
            this.selOrder.Value = query.order;
            this.selOrder.Value = this.Page.Request.QueryString["order"];
        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_brandQuery GetQuery()
        {
            go_brandQuery query = new go_brandQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            query.SortBy = "[order]";
            query.SortOrder = SortAction.Asc;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["order"]))
            {
                switch (this.Page.Request.QueryString["order"])
                {
                    case "1":
                        query.SortBy = "[order]";
                        query.SortOrder = SortAction.Asc;
                        break;
                    case "2":
                        query.SortBy = "[order]";
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
            queryStrings.Add("order", this.selOrder.Value.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }

   
        public void goDelete(int id,string name)
        {
            go_brandBusiness.Del(id);

            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();

            base.addAdminLog(operationAction.Delete, null, "删除品牌" + name);

            builder.Append("{\"success\":true,\"msg\":\"删除成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End(); 

           
        }
        protected void rptAdmin_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Literal litClass = (Literal)e.Item.FindControl("litClass");
                Literal litCateId = (Literal)e.Item.FindControl("litCateId");
                go_categoryEntity categoryRows = go_categoryBusiness.LoadEntity(int.Parse(litCateId.Text));
                if (categoryRows!=null)
                {
                    litClass.Text = categoryRows.Name;
                }
            }
        }
    }
}