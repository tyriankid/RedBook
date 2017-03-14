using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;
using YH.Utility;

namespace RedBookPlatform.channel
{
    public partial class activityList : Base
    {
        public string allusercount = "0";
        public string exchange2 = "0";  //已结算总金额
        public string allmoney = ""; //总金额
        public string unsettledmoney = ""; //未结算金额
        public string uid;
        public object oAccount;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = "btnSearch";
            if (!IsPostBack)
            {

                string action = Request["action"];
                string unumber = Request.Form["unumber"]; //充值账号
                string summoney = Request.Form["summoney"];//充值金额
                int cuid = Convert.ToInt32(Request.Form["cuid"]);//渠道商编号
                switch (action)
                {
                    case "submit":
                        SubmitData(cuid);
                        break;

                }
                BindPage();
            }
        }
        public void SubmitData(int cuid)
        {

            go_channel_userEntity classify = go_channel_userBusiness.LoadEntity(cuid);

            string state = classify.Frozenstate.ToString();
            if (state == "1")
            {
                StringBuilder builder1 = new StringBuilder();
                builder1.Append("{\"success\":false,\"msg\":\"该账号已被冻结！\"}");
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write(builder1.ToString());
                Response.End();
                return;
            }
            //string usetype = "12";
            //string Aserialid = yollyinterface.GenerateYollyID(yollyinterface.usetype.Alipay);
            //DateTime Anow = DateTime.Now;
            //object[] Apost = new object[] { Anow.ToString("yyyyMMddHHmmssss"), unumber, summoney, Aserialid, "2" };
            //string Aret = yollyinterface.APIyolly(Apost, yollyinterface.usetype.Alipay);
            //if (Aret != null)
            //{
            //    string status = Aret.Split('|')[0].ToString();//返回状态
            //    string strtext = Aret.Split('|')[1].ToString();//返回内容
            //    if (status == "0000")
            //    {
            //        //保存打款记录
            //        go_yolly_orderinfoEntity orderinfoEntity = new go_yolly_orderinfoEntity
            //        {
            //            Serialid = Aserialid,
            //            OrderId = "",
            //            Money = Convert.ToDecimal(summoney),
            //            Usenum = unumber,
            //            Usetime = Anow,
            //            Usetype = int.Parse(usetype),
            //            Status = 12,
            //            Context = strtext
            //        };
            //        go_yolly_orderinfoBusiness.SaveEntity(orderinfoEntity, true, DbServers.DbServerName.LatestDB);
            //更改状态
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            string orderid = ShopOrders.GenerateOrderID(ShopOrders.BusinessType.wangba);
            go_channel_activitydetailBusiness.UpdateExchange(cuid,orderid);//更改对应渠道商的订单号

            base.addAdminLog(operationAction.Review, null, "对" + classify .Realname+ "结算");


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
            go_channel_activitydetailQuery query = GetQuery();
            builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat("and(contacts  like '%{0}%' or  realname like '%{0}%' or usermobile like '%{0}%' )", DataHelper.CleanSearchString(query.keyword));
            }
            if (query.unexchangenumber != -1)
            {
                builder.AppendFormat(string.Format(" And unexchangenumber>0", query.unexchangenumber));
            }

            //调用通用分页方法绑定
            string from = string.Format(@"(select u.realname,u.contacts,u.usermobile,u.settlementprice,u.gatheringaccount, d.cuid,
                            count(d.cuid)as totalnumber,
                            count(case when d.exchange=1 then 'cuid' end )as hadexchangenumber,
                            count(case when d.exchange=0 then 'cuid' end )as unexchangenumber
                            from go_channel_user u inner join go_channel_activitydetail d on u.uid=d.cuid
                            group by d.cuid, u.realname,u.gatheringaccount,u.contacts,u.usermobile,u.settlementprice)T");
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "cuid");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.ddlMoney.SelectedValue = query.unexchangenumber.ToString();
            this.pager1.TotalRecords = managers.TotalRecords;


            string where = "";
            if (builder.ToString() != "")
            {
                where = " and " + builder.ToString();
            }


            DataTable all = CustomsBusiness.GetListData(from, builder.ToString(), "SUM(totalnumber) as allusercount,sum(unexchangenumber) as unexchangenumber ,sum(hadexchangenumber)as hadexchangenumber", "", 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            if (all.Rows.Count > 0)
            {
                if (all.Rows[0]["allusercount"].ToString() == "")
                    allmoney = "";
                else
                    allmoney = (int.Parse(all.Rows[0]["allusercount"].ToString()) * 3).ToString();

                if (all.Rows[0]["unexchangenumber"].ToString() == "")
                    unsettledmoney = "";
                else
                    unsettledmoney = (int.Parse(all.Rows[0]["unexchangenumber"].ToString()) * 3).ToString();
                if (all.Rows[0]["allusercount"].ToString() == "")
                    allusercount = "";
                else
                    allusercount = all.Rows[0]["allusercount"].ToString();
                if (all.Rows[0]["hadexchangenumber"].ToString() == "")
                {
                    exchange2 = "0";
                }
                else
                { exchange2 = (int.Parse(all.Rows[0]["hadexchangenumber"].ToString()) * 3).ToString(); }
            }




        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_channel_activitydetailQuery GetQuery()
        {
            go_channel_activitydetailQuery query = new go_channel_activitydetailQuery();

            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["unexchangenumber"]))
            {
                query.unexchangenumber = Convert.ToInt32(base.Server.UrlDecode(this.Page.Request.QueryString["unexchangenumber"]));
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
                query.unexchangenumber = -1;
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
            queryStrings.Add("unexchangenumber", this.ddlMoney.SelectedValue.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "totalnumber");
            queryStrings.Add("SortOrder", SortAction.Desc.ToString());
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }


        /// <summary>
        /// 导出指定日期内的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Export_Click(object sender, EventArgs e)
        {
            //string StarTime = this.inpStarTime.Value.Trim();//开始时间
            //string EndTime = this.inpEndTime.Value.Trim();//结束时间
            string where = "1=1";
            if (this.tboxKeyword.Text.Trim() != "")
            {
                where += "and contacts like '" + this.tboxKeyword.Text.Trim() + "'";
                where += "or realname like '" + this.tboxKeyword.Text.Trim() + "'";
            }
            //需要导出的数据
            string TableName = string.Format(@"(select u.realname,u.contacts,u.usermobile,u.settlementprice,u.gatheringaccount, d.cuid,
                            count(d.cuid)as totalnumber,
                            count(d.cuid)*settlementprice as totalprice,
                            count(case when d.exchange=3 then 'cuid' end )*settlementprice as unmoney,
                            count(case when d.exchange=2 then 'cuid' end )*settlementprice as hadmoney
                            from go_channel_user u inner join go_channel_activitydetail d on u.uid=d.cuid
                            group by d.cuid, u.realname,u.gatheringaccount,u.contacts,u.usermobile,u.settlementprice  )T");
            DataTable dt = CustomsBusiness.GetListData(string.Format(TableName,
                             "u.realname", 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB));
            System.Collections.Generic.IList<string> fields = new System.Collections.Generic.List<string>();
            System.Collections.Generic.IList<string> list = new System.Collections.Generic.List<string>();
            fields.Add("realname");
            list.Add("渠道商名称");
            fields.Add("contacts");
            list.Add("联系人");
            fields.Add("usermobile");
            list.Add("联系方式");
            fields.Add("gatheringaccount");
            list.Add("支付宝账号");
            fields.Add("totalnumber");
            list.Add("参与总人数");
            fields.Add("totalprice");
            list.Add("总金额");
            fields.Add("hadmoney");
            list.Add("已结算金额");
            fields.Add("unmoney");
            list.Add("申请结算金额");
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            foreach (string str in list)
            {
                builder.Append(str + ",");
                if (str == list[list.Count - 1])
                {
                    builder = builder.Remove(builder.Length - 1, 1);
                    builder.Append("\r\n");
                }
            }
            foreach (System.Data.DataRow row in dt.Rows)
            {
                foreach (string str2 in fields)
                {
                    builder.Append(row[str2]).Append(",");
                    if (str2 == fields[list.Count - 1])
                    {

                        builder = builder.Remove(builder.Length - 1, 1);
                        builder.Append("\r\n");
                    }
                }
            }
            this.Page.Response.Clear();
            this.Page.Response.Buffer = false;
            this.Page.Response.Charset = "GB2312";
            this.Page.Response.AppendHeader("Content-Disposition", "attachment;filename=网吧渠道商统计.csv");
            this.Page.Response.ContentType = "application/octet-stream";
            this.Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            this.Page.EnableViewState = false;
            this.Page.Response.Write(builder.ToString());
            this.Page.Response.End();

        }
    }
}