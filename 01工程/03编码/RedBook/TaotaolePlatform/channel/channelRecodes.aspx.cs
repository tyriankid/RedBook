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
    public partial class channelRecodes : Base
    {
        private int uid;
        public int applyUid;
        public string allmoney;   //佣金
        public string allrechargemoney; //总充值金额
        public string nowithdrawcash; //服务商登录时显示的可体现金额
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = "btnSearch";
            uid = Convert.ToInt32(Request.QueryString["uid"]);
            //if (HttpContext.Current.Request.Cookies.Get("ChannelUser") != null)
            //    applyUid = this.CurrLoginChannelUser.Uid;
            //else
            //    applyUid = this.CurrLoginAdmin.Uid;
            if (!IsPostBack)
            {

                if (Request.QueryString["agent"] != null)    //服务代理商进来
                {

                    applyUid = this.CurrLoginChannelUser.Uid;
                    nowithdrawcash = Taotaole.Bll.go_channel_userBusiness.GetScalar("uid=" + this.CurrLoginChannelUser.Uid, "nowithdrawcash", null).ToString();
                    if (!string.IsNullOrEmpty(nowithdrawcash))
                    {
                        nowithdrawcash = String.Format("{0:F}", Convert.ToDecimal(nowithdrawcash)); //保留两位小数
                        nowithdrawcash = "，未提现金额<span style='color: red'>" + nowithdrawcash + "</span>元</span>";
                    }
                    else
                    {
                        nowithdrawcash = "，未提现金额<span style='color: red'>0</span>元</span>";
                    }

                }
                else  //admin进来
                {

                    btntx.Visible = false;
                }
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
            go_channelrecodesQuery query = GetQuery();
            if (uid == 0)
            {
                builder = new StringBuilder("parentid=" + 0);

                this.ApplyCash.Visible = false;
            }
            else
            {
                builder = new StringBuilder("uid=" + uid + "");
                this.ApplyCash.Visible = false;
            }
            if (!string.IsNullOrEmpty(query.starttime))
            {
                builder.AppendFormat(string.Format(" And time>='{0}'", query.starttime));
            }
            if (!string.IsNullOrEmpty(query.endtime))
            {

                builder.AppendFormat(string.Format(" And time<='{0}'", query.endtime + " 23:59:59"));
            }
            if (Request.QueryString["agent"] != null)
            {
                builder = new StringBuilder("uid=" + this.CurrLoginChannelUser.Uid + "");
                this.ApplyCash.Visible = true;
            }
            //if (Request.QueryString["channel"] != null)
            //{
            //    builder = new StringBuilder("uid=" + this.CurrLoginChannelUser.Uid + "");
            //    this.ApplyCash.Visible = false;
            //}
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat("And(realname like '%{0}%'  or username like '%{0}%' or money like '%{0}%' or rechargemoney like '%{0}%' )", DataHelper.CleanSearchString(query.keyword));
            }


            //调用通用分页方法绑定
            string from = "(select c.realname,c.parentid,r.* ,m.username from go_channel_user c inner join  go_channel_recodes r on c.uid=r.uid inner join go_member m on m.uid=r.cid )T";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "rid");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.txtTimeStart.Value = query.starttime;
            this.txtTimeEnd.Value = query.endtime;
            this.pager1.TotalRecords = managers.TotalRecords;






            allmoney = Taotaole.Bll.go_channel_recodesBusiness.SelectAllMoney(from, builder.ToString());   //汇总佣金
            allrechargemoney = Taotaole.Bll.go_channel_recodesBusiness.SelectAllrechargemoney(from, builder.ToString());   //汇总充值金额
            if (allrechargemoney == "")
            {
                allrechargemoney = "0";
            }
            allrechargemoney = String.Format("{0:F}", Convert.ToDecimal(allrechargemoney)); //保留两位小数
            if (allmoney == "")
            {
                allmoney = "0";
            }

        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_channelrecodesQuery GetQuery()
        {
            go_channelrecodesQuery query = new go_channelrecodesQuery();
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


            if (Request.QueryString["agent"] != null)
            {
                queryStrings.Add("agent", "true");
            }
            if (Request.QueryString["uid"] != null)
            {
                queryStrings.Add("uid", Request.QueryString["uid"]);
            }
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }
    }
}