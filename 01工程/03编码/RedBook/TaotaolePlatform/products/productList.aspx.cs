using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;
using YH.Utility;

namespace RedBookPlatform.products
{
    public partial class productList : Base
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


            }
        }

        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            go_productsQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (title Like '%{0}%' or title2 like '%{0}%' or description like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            if (!string.IsNullOrEmpty(query.State))
            {
                switch (query.State)
                {
                    case "推荐":
                        builder.AppendFormat(" AND pos=1");
                        break;
                    case "人气":
                        builder.AppendFormat(" AND renqi=1");
                        break;
                    case "爆款":
                        builder.AppendFormat(" AND baokuan=1");
                        break;
                    case "特价":
                        builder.AppendFormat(" AND tejia=1");
                        break;
                    case "优惠":
                        builder.AppendFormat(" AND youhui=1");
                        break;
                    case "新手":
                        builder.AppendFormat(" AND newhand=1");
                        break;
                }
                
            }
            //调用通用分页方法绑定
            DbQueryResult managers = DataHelper.GetPageData(query, "go_products", builder, "productid");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
            this.dropState.SelectedValue = query.State;
            this.selTime.Value = this.Page.Request.QueryString["Time"];
        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_productsQuery GetQuery()
        {
            go_productsQuery query = new go_productsQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["State"]))
            {
                query.State = base.Server.UrlDecode(this.Page.Request.QueryString["State"]);
            }
            query.SortBy = "time";
            query.SortOrder = SortAction.Desc;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Time"]))
            {
                switch (this.Page.Request.QueryString["Time"])
                {
                    case "0":
                        query.SortBy = "time";
                        query.SortOrder = SortAction.Asc;
                        break;
                    case "1":
                        query.SortBy = "time";
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
            queryStrings.Add("State", this.dropState.SelectedValue);
            queryStrings.Add("Time", this.selTime.Value.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "time");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }

        [System.Web.Services.WebMethod]
        public static string goDelete(int productId)
        {
            string where = "Where productid=" + productId;
            string from = "go_yiyuan";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            if (count > 0)
            {
                //在一元购中使用该商品无法删除
                return "在一元购中使用过该商品无法删除，请先删除该商品的一元购信息"; ;
            }

            where = "Where productid=" + productId;
            from = "go_tuan";
            count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            if (count > 0)
            {
                //在团购中使用该商品无法删除
                return "在团购中使用过该商品无法删除，请先删除该商品的团购信息"; ;
            }

            where = "Where productid=" + productId;
            from = "go_gifts";
            count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            if (count > 0)
            {
                //在积分商城中使用该商品无法删除
                return "在积分商城中使用过该商品无法删除，请先删除该商品的积分商城信息"; ;
            }

            where = "Where productid=" + productId;
            from = "go_zhigou";
            count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            if (count > 0)
            {
                //在直购商城中使用该商品无法删除
                return "在直购商城中使用过该商品无法删除，请先删除该商品的直购商城信息"; ;
            }
            go_productsBusiness.Del(productId);
            return "删除成功";
        }
    }
}