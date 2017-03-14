using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;
using System.Threading;
using Taotaole.Bll;
using System.Web.UI.HtmlControls;

namespace RedBookPlatform.channel
{
    public partial class distributorRecodes : Base
    {
        private int uid;

        protected string allmoney;//总佣金
        protected string allrechargemoney; //总充值金额
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = "btnSearch";
            uid = Convert.ToInt32(Request.QueryString["uid"]);
            if (!IsPostBack)
            {
                BindPage();
                string action = Request["action"];
                string adidlist = Request.Form["adidlist"]; //更改主键
                string names = Request.Form["names"]; //所选用户名字
                switch (action)
                {
                    case "settlement":
                        Settlement(adidlist, names);
                        break;

                }
                if (Request.QueryString["distributor"] != null)
                {
                    this.btnSettlement.Visible = false;
                    this.checkall.Visible = false;
                }
            }
        }


        public void Settlement(string adidlist, string names)
        {

            //更改提现状态
            string[] adidArr = adidlist.Split(',');
            for (int i = 0; i < adidArr.Length; i++)
            {
                if (adidArr[i] != "" && adidArr[i] != "all")
                {

                    go_channel_recodesEntity classify = go_channel_recodesBusiness.LoadEntity(Convert.ToInt32(adidArr[i]));
                    if (classify != null)
                    {

                        classify.State = 1; //更改为已结算
                        classify.Settlementtime = DateTime.Now;
                        go_channel_recodesBusiness.SaveEntity(classify, false);

                    }
                }
            }

            base.addAdminLog(operationAction.Review, null, "批量结算了" + names.TrimEnd(','));
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
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
            StringBuilder builder;
            go_channelrecodesQuery query = GetQuery();
            if (uid == 0)
            {
                builder = new StringBuilder("1=1");

            }
            else
            { builder = new StringBuilder("uid=" + uid + ""); }

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
                if (Request.QueryString["distributor"] != null)//渠道商佣金明细
                {
                    builder = new StringBuilder("uid=" + this.CurrLoginChannelUser.Uid + "");
                }
                else
                {
                    builder = new StringBuilder("parentid=" + this.CurrLoginChannelUser.Uid + "");
                }
            }
            if (query.state != -1)
            {
                builder.AppendFormat(string.Format(" And state={0}", query.state));
            }
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat("and(realname  like '%{0}%' or  username like '%{0}%' or money like '%{0}%' or rechargemoney like '%{0}%' or time like '%{0}%' )", DataHelper.CleanSearchString(query.keyword));
            }

            //调用通用分页方法绑定
            string swr = " And parentid<>0";
            string from = "(Select c.realname,c.parentid,R.* ,m.username from go_channel_user C Inner Join  go_channel_recodes R on C.uid=R.uid inner join go_member m on m.uid=r.cid {0})T";
            from = string.Format(from, swr);
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "rid");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.txtTimeStart.Value = query.starttime;
            this.txtTimeEnd.Value = query.endtime;
            this.ddlExchange.SelectedValue = query.state.ToString();
            this.pager1.TotalRecords = managers.TotalRecords;


            //if (Request.QueryString["agent"] != null)
            //{
            //    if (Request.QueryString["distributor"] != null)//渠道商佣金明细
            //    {
            //        allmoney = Taotaole.Bll.go_channel_recodesBusiness.SelectAllMoney("<>" , builder.ToString());   //汇总佣金
            //        allrechargemoney = Taotaole.Bll.go_channel_recodesBusiness.SelectAllrechargemoney("<>", builder.ToString());  //汇总充值金额
            //    }
            //    else //服务商下面的渠道商佣金明细
            //    {
            //        allmoney = Taotaole.Bll.go_channel_recodesBusiness.SelectAllMoney("<>", builder.ToString());   //汇总佣金
            //        allrechargemoney = Taotaole.Bll.go_channel_recodesBusiness.SelectAllrechargemoney("<>", builder.ToString());  //汇总充值金额
            //    }
            //}
            //else  //全部渠道商的佣金明细
            //{
            allmoney = Taotaole.Bll.go_channel_recodesBusiness.SelectAllMoney(from, builder.ToString());   //汇总佣金
            allrechargemoney = Taotaole.Bll.go_channel_recodesBusiness.SelectAllrechargemoney(from, builder.ToString());  //汇总充值金额
            //   }
            if (allrechargemoney == "")
            {
                allrechargemoney = "0";
            }
            if (allmoney == "")
            {
                allmoney = "0";
            }
            allrechargemoney = String.Format("{0:F}", Convert.ToDecimal(allrechargemoney)); //保留两位小数
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
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["state"]))
            {
                query.state = Convert.ToInt32(base.Server.UrlDecode(this.Page.Request.QueryString["state"]));
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
                query.state = -1;
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
            queryStrings.Add("state", this.ddlExchange.SelectedValue.Trim());
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

            if (Request.QueryString["distributor"] != null)
            {
                queryStrings.Add("distributor", "true");
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

        protected void rptAdmin_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (Request.QueryString["distributor"] != null)
            {
                HtmlTableCell td = e.Item.FindControl("checkall2") as HtmlTableCell;
                td.Visible = false;
            }

        }
    }
}