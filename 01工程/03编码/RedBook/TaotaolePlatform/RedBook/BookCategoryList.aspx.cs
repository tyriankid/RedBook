using System;
using System.Collections.Specialized;
using System.Text;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;

namespace RedBookPlatform.RedBook
{
    public partial class BookCategoryList : Base
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
                        goDelete(new Guid(Request["id"]), Request["name"]);
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
            RB_Book_CategoryQuery query = GetQuery();
            StringBuilder bd = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.Keyword))
            {
                bd.AppendFormat(" AND (category like '%{0}%' or description like '%{0}%')", DataHelper.CleanSearchString(query.Keyword.Trim(',')));
            }
            //调用通用分页方法绑定
            DbQueryResult category = DataHelper.GetPageData(query, "RB_Book_Category", bd, "Id");
            this.rptAdmin.DataSource = category.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.Keyword;
            this.pager1.TotalRecords = category.TotalRecords;
        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private RB_Book_CategoryQuery GetQuery()
        {
            RB_Book_CategoryQuery query = new RB_Book_CategoryQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.Keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            query.SortBy = "SortBaseNum";
            query.SortOrder = SortAction.Asc;
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
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBaseNum", SortAction.Asc.ToString());
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }

     
        public void goDelete(Guid cateid,string name)
        {
            RB_Book_CategoryBusiness.Del(cateid);

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