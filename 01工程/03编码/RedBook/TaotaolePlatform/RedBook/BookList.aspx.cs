using System;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;

namespace RedBookPlatform.RedBook
{
    public partial class BookList : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindPage();
                //载入分类下拉框
                DataTable categoryRows = RB_Book_CategoryBusiness.GetListData("1=1", "CategoryName,Id");
                ddlCategory.DataSource = categoryRows;
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "Id";
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
            RB_BookQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.Keyword))
            {
                builder.AppendFormat(" AND (title Like '%{0}%' or SubTitle Like '%{0}%')  ", DataHelper.CleanSearchString(query.Keyword));
            }

            string cateName = "";
            //类型筛选
            if (!string.IsNullOrEmpty(query.CategoryId))
            {
                cateName = string.Format(" and rb.categoryid='{0}'", query.CategoryId);
            }
            string from = "(Select rb.id,rb.MainTitle,rb.backImgUrl,rb.FabulousCount,rb.FavoriteCount,rb.WatchCount,rb.SubTitle,rb.UserId,rb.UserName,rb.AddTime,rb.CategoryId,rb.SortBaseNum,rb.[Status],rb.ProductIds,rb.Memo,rbc.CategoryName From RB_Book rb left join RB_Book_Category rbc on rb.CategoryId = rbc.Id  where 1=1 " + cateName + ") T ";

            //调用通用分页方法绑定
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "id");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.Keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
            this.selMultiple.Value = this.Page.Request.QueryString["multiple"];
            this.ddlCategory.SelectedValue = query.CategoryId;


        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private RB_BookQuery GetQuery()
        {
            RB_BookQuery query = new RB_BookQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.Keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortBaseNum"]))
            {
                query.SortBy = this.Page.Request.QueryString["SortBaseNum"];
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
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["CategoryId"]))
            {
                query.CategoryId = this.Page.Request.QueryString["CategoryId"].ToString();
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
            queryStrings.Add("CategoryId", this.ddlCategory.SelectedValue);
            base.ReloadPage(queryStrings);
        }

        [System.Web.Services.WebMethod]
        public static string goDelete(Guid id)
        {
            RB_BookBusiness.Del(id);
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
                    RB_BookEntity bookEntity = RB_BookBusiness.LoadEntity(new Guid(yids.GetValue(i).ToString()));
                    
                    bookEntity.SortBaseNum = int.Parse(orders.GetValue(i).ToString());
                    RB_BookBusiness.SaveEntity(bookEntity,false);
                }
            }
            return "更新排序成功";
        }
    }
}