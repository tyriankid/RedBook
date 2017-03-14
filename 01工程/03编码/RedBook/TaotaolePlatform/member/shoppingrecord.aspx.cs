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
using YH.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Taotaole.Business;

namespace RedBookPlatform.member
{
    public partial class shoppingrecord : Base
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
            go_adminQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("type=-1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (uid Like '%{0}%' or username like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            if (!string.IsNullOrEmpty(query.StarTime))
            {
                builder.AppendFormat(" and time>'{0}'", DataHelper.CleanSearchString(query.StarTime));
            }
            if (!string.IsNullOrEmpty(query.EndTime))
            {
                builder.AppendFormat(" and time <'{0}'", DataHelper.CleanSearchString(query.EndTime));
            }
            if (!string.IsNullOrEmpty(query.type))
            {

                string type = DataHelper.CleanSearchString(query.type);
                if (query.type == "weixin")
                {
                    builder.AppendFormat("and (typeid<>9 )", type);
                }
                else if (query.type == "baidu")
                {
                    builder.AppendFormat("and (typeid=9)", type);
                }

            }


           

            //调用通用分页方法绑定
            string from = "(select a.*,b.username,b.typeid  from go_member_account a  left join go_member b on a.uid=b.uid ) T ";

            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "id");
            this.rptRechargerecord.DataSource = managers.Data;
            this.rptRechargerecord.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
            DataTable res = CustomsBusiness.GetListData(from, builder.ToString(), "SUM(money) as money");
            if ((res.Rows[0]["money"]).ToString() != "")
            {
                this.labSum.Text = Convert.ToDouble((res.Rows[0]["money"]).ToString()).ToString("0.00");
            }
            else {

                this.labSum.Text = "0";
            }
            this.inpStarTime.Value = query.StarTime;
            this.inpEndTime.Value = query.EndTime;
            this.dropType.SelectedValue = query.type;
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
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["StarTime"]))
            {
                query.StarTime = base.Server.UrlDecode(this.Page.Request.QueryString["StarTime"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["EndTime"]))
            {
                query.EndTime = base.Server.UrlDecode(this.Page.Request.QueryString["EndTime"]);
            }

            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["type"]))
            {
                query.type = base.Server.UrlDecode(this.Page.Request.QueryString["type"]);
            }


            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;

            query.SortBy = "id";
            query.SortOrder = SortAction.Desc;
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
            queryStrings.Add("StarTime", this.inpStarTime.Value.Trim());
            queryStrings.Add("EndTime", this.inpEndTime.Value.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "uid");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            queryStrings.Add("type", this.dropType.SelectedValue);
            
            base.ReloadPage(queryStrings);
        }
        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }
    }
}