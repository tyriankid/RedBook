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
using System.Data;
using System.Threading;

namespace RedBookPlatform.channel
{
    public partial class agentCashout : Base
    {
        public string Agent;
        public string Allcashoutmoney;//总提现金额
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

             
                Agent = Request.QueryString["agent"];
                BindPage();
                string action = Request["action"];
                switch (action)
                {
                    case "adopt":
                        AdoptApply();
                        break;
                }
            }

        }


        /// <summary>
        /// 通过服务商申请提现
        /// </summary>
        public void AdoptApply()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            int rid = Convert.ToInt32(Request.Form["rid"]);
            string ustate = Request.Form["ustate"];
            go_channel_cash_outEntity classify = go_channel_cash_outBusiness.LoadEntity(rid);
            classify.Auditstatus = Convert.ToInt32(ustate);
            go_channel_cash_outBusiness.SaveEntity(classify, false);
            builder.Append("{\"success\":true,\"msg\":\"操作成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();


        }

        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            go_channelcashoutQuery query = GetQuery();
            StringBuilder builder;
            if (Request.QueryString["agent"] != null)
                builder = new StringBuilder("uid=" + this.CurrLoginChannelUser.Uid + "");
            else
            {

                builder = new StringBuilder("1=1");
                if (!string.IsNullOrEmpty(query.starttime))
                {
                    builder.AppendFormat(string.Format(" And reviewtime>='{0}'", query.starttime));
                }
                if (!string.IsNullOrEmpty(query.endtime))
                {
                    builder.AppendFormat(string.Format(" And reviewtime<='{0}'", query.endtime));
                }

            }

            if (!string.IsNullOrEmpty(query.auditstatus))
            {
                builder.AppendFormat(" AND (auditstatus ={0})", DataHelper.CleanSearchString(query.auditstatus));
            }

            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (username Like '%{0}%' or bankname like '%{0}%' or branch like '%{0}%' or linkphone like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }

            //调用通用分页方法绑定
            string from = "(select c.rid,c.username,c.bankname,c.branch,c.money,c.reviewtime,c.banknumber,c.linkphone,c.auditstatus,u.realname,u.uid from go_channel_cash_out c inner join go_channel_user u on c.uid=u.uid)T";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "rid");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.txtTimeStart.Value = query.starttime;
            this.txtTimeEnd.Value = query.endtime;
            this.pager1.TotalRecords = managers.TotalRecords;
            this.ddlauditstatus.SelectedValue = query.auditstatus;

             Allcashoutmoney = Taotaole.Bll.go_channel_cash_outBusiness.SelectAllcashoutmoney(from, builder.ToString());

            if (string.IsNullOrEmpty(Allcashoutmoney))
            {
                Allcashoutmoney = "0";
            }
            Allcashoutmoney = String.Format("{0:F}", Convert.ToDecimal(Allcashoutmoney)); //保留两位小数



        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_channelcashoutQuery GetQuery()
        {
            go_channelcashoutQuery query = new go_channelcashoutQuery();
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

            if (!string.IsNullOrEmpty(Request.QueryString["auditstatus"]))
            {
                query.auditstatus = base.Server.UrlDecode(this.Page.Request.QueryString["auditstatus"]);
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
            queryStrings.Add("SortBy", "money");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            queryStrings.Add("auditstatus", this.ddlauditstatus.SelectedValue);
            queryStrings.Add("agent","true");
            
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }

        protected string getauditstatus(string auditstatus)
        {
            string status = "";
            if (auditstatus == "0")
            {
                status = " <span style='color:black'>未发放</span> ";

            }
            else if (auditstatus == "1")
            {
                status = " <span style='color:green'>已发放</span> ";
            }
            else if (auditstatus == "2")
            {
                status = " <span style='color:red'>未通过</span> ";
            }
            return status;
        }


        protected string modifystatus(string auditstatus, string rid, string username)
        {
            string status = "";
            if (auditstatus == "0")
            {
                status = @"<span class='" + rid + "' style='padding-left: 5px'><a class='btn' onclick=\"AdoptApply('" + rid + "','" + username + "','1')\" style='color:green'  >通过</a> |  <a class='btn'   onclick=\"AdoptApply('" + rid + "','" + username + "','2')\" style='color:red'  >不通过</a>   </span>";
            }

            return status;
        }


    }
}