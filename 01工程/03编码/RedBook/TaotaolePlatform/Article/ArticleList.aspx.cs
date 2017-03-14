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

namespace RedBookPlatform.Article
{
    public partial class ArticleList : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindPage();
                //载入分类下拉框
                DataTable categoryRows = go_categoryBusiness.GetListData("model=2", "name,cateid");
                ddlCategory.DataSource = categoryRows;
                ddlCategory.DataTextField = "name";
                ddlCategory.DataValueField = "cateid";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("全部", ""));
            }
        }


        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
           


            //获得用户输入的条件
            go_articleQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (title Like '%{0}%' )", DataHelper.CleanSearchString(query.keyword));
            }
          
            string cateName = "";
            //类型筛选
            if (!string.IsNullOrEmpty(query.categoryid))
            {
                cateName = string.Format(" and a.cateid='{0}'", query.categoryid);
            }


            string from = "(select a.ordersn,a.thumb, a.id, a.title,a.author ,a.hit,a.posttime,b.name  from go_article a  left join go_category b on a.cateid=b.cateid  where 1=1 " + cateName + ") T ";

            //调用通用分页方法绑定
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "id");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
            this.selMultiple.Value = this.Page.Request.QueryString["multiple"];
            this.ddlCategory.SelectedValue = query.categoryid;


        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_articleQuery GetQuery()
        {
            go_articleQuery query = new go_articleQuery();
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
            query.SortOrder = SortAction.Desc;

           



            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["multiple"]))
            {
                //公共筛选
                switch (this.Page.Request.QueryString["multiple"])
                {
                    case "0":
                        query.SortBy = "posttime";
                        query.SortOrder = SortAction.Desc;
                        break;
                    case "1":
                        query.SortBy = "posttime";
                        query.SortOrder = SortAction.Asc;
                        break;
                }
            }
             if (!string.IsNullOrEmpty(this.Page.Request.QueryString["type"]))
            {
                query.categoryid = this.Page.Request.QueryString["type"].ToString();
            }
            
            return query;
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
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
            queryStrings.Add("SortBy", "posttime");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            queryStrings.Add("multiple", this.selMultiple.Value);
            queryStrings.Add("type", this.ddlCategory.SelectedValue);
            base.ReloadPage(queryStrings);
        }

        [System.Web.Services.WebMethod]
        public static string goDelete(int id)
        {
            go_articleBusiness.Del(id);
            return "删除成功";
        }

        //更新排序号
        [System.Web.Services.WebMethod]
        public static string UpOrder(string yid, string order)
        {
            string[] yids = yid.TrimEnd(',').Split(',');
            string[] orders = order.TrimEnd(',').Split(',');

            if (yids.Count() > 0 && orders.Count() > 0)
            {
                for (int i = 0; i < yids.Count(); i++)
                {
                    go_articleEntity articleEntity = go_articleBusiness.LoadEntity(int.Parse(yids.GetValue(i).ToString()));
                  
                    //1回收站
                    articleEntity.Ordersn = int.Parse(orders.GetValue(i).ToString());
                    go_articleBusiness.UpdateEntity(articleEntity);
                }
            }
            return "更新排序成功";
        }
    }
}