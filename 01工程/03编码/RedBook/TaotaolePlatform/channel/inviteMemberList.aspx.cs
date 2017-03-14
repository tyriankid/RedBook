using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;

namespace RedBookPlatform.channel
{
    public partial class inviteMemberList : Base
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
            StringBuilder builder;
            go_memberQuery query = GetQuery();
            builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.starttime))
            {
                builder.AppendFormat(string.Format(" And time>='{0}'", query.starttime));
            }
            if (!string.IsNullOrEmpty(query.endtime))
            {
                builder.AppendFormat(string.Format(" And time<='{0}'", query.endtime));
            }
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat("and(username  like '%{0}%' or  mobile like '%{0}%' )", DataHelper.CleanSearchString(query.keyword));
            }

            //调用通用分页方法绑定
            string from = "(select uid, username,time,mobile,servicechannelid from go_member where servicechannelid={0})T";
            from = string.Format(from,this.CurrLoginChannelUser.Uid);
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "uid");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.txtTimeStart.Value = query.starttime;
            this.txtTimeEnd.Value = query.endtime;
            this.pager1.TotalRecords = managers.TotalRecords;
        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_memberQuery GetQuery()
        {
            go_memberQuery query = new go_memberQuery();
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
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortBy"]))
            {
                query.SortBy = this.Page.Request.QueryString["SortBy"];
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortOrder"]))
            {
                query.SortOrder = SortAction.Desc;
            }
            //设置界面默认值
            if (this.Page.Request.QueryString["SortOrder"] == null)
            {
                int MaxDays = Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                query.starttime = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + "1";
                query.endtime = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + MaxDays;
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
            queryStrings.Add("starttime", this.txtTimeStart.Value.Trim());
            queryStrings.Add("endtime", this.txtTimeEnd.Value.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "time");
            queryStrings.Add("SortOrder", SortAction.Desc.ToString());
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }
    }
}