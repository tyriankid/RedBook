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

namespace RedBookPlatform.OrderClass
{
    public partial class orderNotOut :Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPage();
            }
        }
        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            go_orderQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat("and (username Like '%{0}%' or title like '%{0}%'or orderId like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            string from = ""; 
            if (!string.IsNullOrEmpty(query.Type))
            {
                
                string type = DataHelper.CleanSearchString(query.Type);
                if (query.Type == "yuan")
                {
                    from = "(select P.typeid,M.username,P.title,P.thumb,O.*from go_orders O left join go_member M on O.uid=M.uid left join  go_yiyuan Y on O.businessId=y.yId left join go_products P on Y.productid=P.productid  where iswon=1 and O.status='待发货' and ordertype='yuan')t";
                }
                if(query.Type=="quan")
                {
                    from = "(select P.typeid,M.username,P.title,P.thumb,O.*from go_orders O left join go_member M on O.uid=M.uid left join  go_zhigou Z on O.businessId=Z.QuanId left join go_products P on Z.productid=P.productid  where O.status='待发货' and ordertype='quan')t";
                }
                if (query.Type == "gift")
                {
                    from = "(select P.typeid,M.username,P.title,P.thumb,O.*from go_orders O left join go_member M on O.uid=M.uid left join  go_gifts G on O.businessId=G.giftId left join go_products P on G.productid=P.productid  where O.status='待发货' and ordertype='gift')t";
                }
                //else if (query.Type == "baidu")
                //{
                //    builder.AppendFormat("and (mtypeid=9)", type);
                //}
                //else
                //{
                //    builder.AppendFormat("and (ordertype='{0}' )", type);
                //}
            }
            if (!string.IsNullOrEmpty(query.State))
            {
                builder.AppendFormat(" and typeid in ({0})", DataHelper.CleanSearchString(query.State));
            }
            DbQueryResult orders = DataHelper.GetPageData(query, from, builder, "orderId");
            this.rptAdmin.DataSource = orders.Data;
            this.rptAdmin.DataBind();
            this.pager1.TotalRecords = orders.TotalRecords;
            this.tboxKeyword.Text = query.keyword;
            this.dropType.SelectedValue = query.Type;
            this.dropState.SelectedValue = query.State;
        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_orderQuery GetQuery()
        {
            go_orderQuery query = new go_orderQuery();
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            query.State = "0,2";
            query.Type = "yuan";
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Type"]))
            {
                query.Type = base.Server.UrlDecode(this.Page.Request.QueryString["Type"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["State"]))
            {
                query.State = base.Server.UrlDecode(this.Page.Request.QueryString["State"]);
            }
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
            queryStrings.Add("Type", this.dropType.SelectedValue);
            queryStrings.Add("State", this.dropState.SelectedValue);
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
        public static string ockMoney(string status, string orderId)
        {
            go_ordersEntity orderEntity = go_ordersBusiness.LoadEntity(orderId);
            if (orderEntity != null)
            {
                orderEntity.Status = status;
                go_ordersBusiness.SaveEntity(orderEntity, false);
            }
            return "";
        }
    }
}