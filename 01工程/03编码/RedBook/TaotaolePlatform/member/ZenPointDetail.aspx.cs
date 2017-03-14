using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Business;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;

namespace RedBookPlatform.member
{
    public partial class ZenPointDetail : Base 
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
            go_zengDetailQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1 = 1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (ZenId Like '%{0}%' or username like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            if (!string.IsNullOrEmpty(query.StarTime))
            {
                builder.AppendFormat(" and usetime>'{0}'", DataHelper.CleanSearchString(query.StarTime));
            }
            if (!string.IsNullOrEmpty(query.EndTime))
            {
                builder.AppendFormat(" and usetime <'{0}'", DataHelper.CleanSearchString(query.EndTime));
            }
            //调用通用分页方法绑定
            string from = "(select m.username,ZP.*from  go_zenPointDetail  ZP  left  join  go_member  m on  zp.uid=m.uid) T ";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "ZenId");
            this.rptRechargerecord.DataSource = managers.Data;
            this.rptRechargerecord.DataBind();

            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
            DataTable res = CustomsBusiness.GetListData(from, builder.ToString(), "SUM(money) as money");
            if ((res.Rows[0]["money"]).ToString() != "")
            {
                this.labSum.Text = Convert.ToDouble((res.Rows[0]["money"]).ToString()).ToString("0.00");
            }
            else
            {

                this.labSum.Text = "0";
            }
            this.inpStarTime.Value = query.StarTime;
            this.inpEndTime.Value = query.EndTime;
        }
        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_zengDetailQuery GetQuery()
        {
            go_zengDetailQuery query = new go_zengDetailQuery();
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
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;

            query.SortBy = "usetime";
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
            base.ReloadPage(queryStrings);
        }
        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }
    }
}