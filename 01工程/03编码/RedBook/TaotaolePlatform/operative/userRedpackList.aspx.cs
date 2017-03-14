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

namespace RedBookPlatform.operative
{
    public partial class userRedpackList : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
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
            go_userredpackQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (codetitle Like '%{0}%' or order_id like '%{0}%' or uid like '%{0}%' or amount like '%{0}%' or discount like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            if (!string.IsNullOrEmpty(query.starttime))
            {
                builder.AppendFormat(" AND addtime>'{0}'", DataHelper.CleanSearchString(query.starttime));
            }
            if (!string.IsNullOrEmpty(query.endtime))
            {
                builder.AppendFormat(" AND addtime<'{0}'", DataHelper.CleanSearchString(query.endtime));
            }
            if (!string.IsNullOrEmpty(query.state))
            {
                builder.AppendFormat(" AND state='{0}'", DataHelper.CleanSearchString(query.state));
            }
            //调用通用分页方法绑定
            DbQueryResult managers = DataHelper.GetPageData(query, "go_activity_code", builder, "id");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.tboxStarttime.Text = query.starttime;
            this.tboxEndtime.Text = query.endtime;
            this.dropState.SelectedValue = query.state;
            this.pager1.TotalRecords = managers.TotalRecords;
            string sumdis = "0";
            string sumsave = "0";
            string sumdo = "0";
            string sumover = "0";
            string sumisuse = "0";
            if (!string.IsNullOrEmpty(query.state))
            {

                DataTable dtRedpackinfo = go_activity_codeBusiness.GetListData(builder.ToString(), "SUM(discount) as sumdiscount");
               
                sumdis = (dtRedpackinfo.Rows[0]["sumdiscount"]).ToString();
                if (query.state == "0")
                {
                     sumsave = sumdis;
                }
                if (query.state == "1")
                {
                     sumdo = sumdis;
                }
                if (query.state == "2")
                {
                     sumisuse = sumdis;
                }
                if (query.state == "3")
                {
                     sumover = sumdis;
                }
            }
            else {
                DataTable dtRedpacklist = go_activity_codeBusiness.GetListData(builder.ToString(), "discount,state");
                sumdis = dtRedpacklist.Compute("Sum(discount)","discount>0").ToString();
                sumsave = dtRedpacklist.Compute("Sum(discount)", " state=0").ToString();
                sumdo = dtRedpacklist.Compute("Sum(discount)", " state=1").ToString();
                sumover = dtRedpacklist.Compute("Sum(discount)", " state=3").ToString();
                sumisuse = dtRedpacklist.Compute("Sum(discount)", " state=2").ToString();
            }
            this.labSumdiscount.Text = sumdis;
            this.labsave.Text = sumsave;
            this.labdo.Text = sumdo;
            this.labisuse.Text = sumisuse;
            this.labover.Text = sumover;
        }
        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_userredpackQuery GetQuery()
        {
            go_userredpackQuery query = new go_userredpackQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["starttime"]))
            {
                query.starttime = base.Server.UrlDecode(this.Page.Request.QueryString["starttime"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["endtime"]))
            {
                query.endtime = base.Server.UrlDecode(this.Page.Request.QueryString["endtime"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["state"]))
            {
                query.state = base.Server.UrlDecode(this.Page.Request.QueryString["state"]);
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
            string t = this.tboxEndtime.Text;
            NameValueCollection queryStrings = new NameValueCollection();
            queryStrings.Add("keyword", this.tboxKeyword.Text.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "id");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            queryStrings.Add("starttime", this.tboxStarttime.Text.Trim());
            queryStrings.Add("endtime", this.tboxEndtime.Text.Trim());
            queryStrings.Add("state", this.dropState.SelectedValue.Trim());
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }
    }
}