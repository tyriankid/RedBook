using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;
using YH.Utility;

namespace RedBookPlatform.channel
{
    public partial class activityDetailList : Base
    {
        public string sumMoney;
        public int uid;
        public int exchange;
        public string allusercount = "0";
        public string hadexchange = "0";  //已结算总金额
        public string unexchange = "0";//未结算总金额
        protected void Page_Load(object sender, EventArgs e)
        {
            uid = Convert.ToInt32(Request.QueryString["uid"]);
            exchange = Convert.ToInt32(Request.QueryString["exchange"]);
            //验证页面权限
            //if (!this.IsPageRight("admin,channel"))
            //{
            //    Response.Redirect("/Resources/403.html");
            //    return;
            //}
            this.Form.DefaultButton = "btnSearch";
            if (!IsPostBack)
            {

                //                                if (Request.QueryString["channel"] != null)
                //                                {
                //                                    int cuid = this.CurrLoginChannelUser.Uid;
                //                                    allusercount = CustomsBusiness.GetScalar(string.Format("go_channel_activitydetail a inner join go_channel_user b on a.cuid=b.uid where cuid={0}", "", "count(*)", ""),cuid)));
                //                                    DataTable dthadexchange = CustomsBusiness.GetListData(@"go_channel_activitydetail a inner join go_channel_user b on a.cuid=b.uid  group by exchange
                //                                having  exchange=2 ", "", " exchange,SUM( b.settlementprice) as  allmoney", null);
                //                                    if (dthadexchange != null && dthadexchange.Rows.Count > 0 && dthadexchange.Rows[0]["allmoney"].ToString() != "")
                //                                    {
                //                                        hadexchange = String.Format("{0:F}", Convert.ToDecimal(dthadexchange.Rows[0]["allmoney"].ToString()));
                //                                    }

                //                                    DataTable dtunexchange = CustomsBusiness.GetListData(@"go_channel_activitydetail a inner join go_channel_user b on a.cuid=b.uid  group by exchange
                //                                having  exchange=1 ", "", " exchange,SUM( b.settlementprice) as  allmoney", null);
                //                                    if (dtunexchange != null && dtunexchange.Rows.Count > 0 && dtunexchange.Rows[0]["allmoney"].ToString() != "")
                //                                    {
                //                                        unexchange = String.Format("{0:F}", Convert.ToDecimal(dtunexchange.Rows[0]["allmoney"].ToString()));
                //                                    }
                //                                }
                string adidlist = Request.Form["adidlist"]; //更改主键
                string names = Request.Form["names"]; //所选用户名字
                
                string action = Request["action"];
                switch (action)
                {
                    case "submit":
                        SubmitData(adidlist);
                        break;

                }
                BindPage();
                if (Request.QueryString["channel"] == null)
                    this.btnapplycash.Visible = false;
                //else
                //    this.btnBatchSettlement.Visible = false;
            }
        }


        public void SubmitData(string adidlist)
        {
            StringBuilder builder = new StringBuilder();
            //生成订单号
           string orderid= ShopOrders.GenerateOrderID(ShopOrders.BusinessType.wangba);
            go_channel_activitydetailEntity classify ;
            DateTime dtnow = DateTime.Now;
            //更改提现状态
            string[] adidArr = adidlist.Split(',');
            for (int i = 0; i < adidArr.Length; i++)
            {
                if (adidArr[i] != "" && adidArr[i] != "all")
                {
                    classify = go_channel_activitydetailBusiness.LoadEntity(Convert.ToInt32(adidArr[i]));
                    if (classify != null && classify.Exchange == 0)
                    {
                        classify.Orderid = orderid;
                        classify.Applytime = dtnow;
                        go_channel_activitydetailBusiness.SaveEntity(classify, false);

                    }
                }
            }
            //获取总订单的数量
            DataTable dt= go_channel_activitydetailBusiness.GetListData("orderid ='" + orderid + "'", "*", null, 0);
            //获取网吧订单单价
            go_channel_userEntity userEntity = go_channel_userBusiness.LoadEntity(Convert.ToInt32(dt.Rows[0]["cuid"]), YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            //订单总金额
            int money = dt.Rows.Count * Convert.ToInt32(userEntity.Settlementprice);
            //提现支付宝账号
            string usenum = userEntity.Gatheringaccount;
            //支付宝用户名
            string usename = userEntity.Contacts;
            string liuid = yollyinterface.GenerateYollyID(yollyinterface.usetype.Wangba);
            go_yolly_orderinfoEntity orderinfoEntity;
            orderinfoEntity = new go_yolly_orderinfoEntity
            {
                Serialid = liuid,
                OrderId = orderid,
                Money = money,
                Usenum = usenum,
                Usetime = dtnow,
                Usetype = 2,
                Status = 3,
                Context = "申请已提交"
            };
            bool C = go_yolly_orderinfoBusiness.SaveEntity(orderinfoEntity, true, DbServers.DbServerName.LatestDB);
            orderinfoEntity = go_yolly_orderinfoBusiness.LoadEntity(liuid, DbServers.DbServerName.LatestDB);
            if (!C && orderinfoEntity.Status != 3 && orderinfoEntity.Context != "申请已提交")
            {
                Response.ContentType = "text/json";
                builder.Append("{\"success\":false,\"msg\":\"操作失败！\"}");
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write(builder.ToString());
                Response.End();
            }
            string strxml = new WebUtils().DoPost(Globals.API_Domain + "taotaoleinterfaceapi.ashx?", string.Format("action=yollyrecharge&timenow={0}&type={1}&money={2}&usenum={3}&Aserialid={4}&usename={5}", dtnow, "2", money, usenum, liuid, usename));
            DataTable dtactivity = go_channel_activitydetailBusiness.GetListData("orderid='" + orderid + "'", "*", null, 0);
            for (int p = 0; p < dtactivity.Rows.Count; p++)
            {
                dtactivity.Rows[p]["Exchange"] = "1";
                dtactivity.Rows[p]["settlementtime"] = dtnow;
            }
            bool b= CustomsBusiness.CommitDataTable(dtactivity, "select * from go_channel_activitydetail", DbServers.DbServerName.LatestDB);
            if (!b)
            {
                Globals.DebugLogger("批量提交失败");
            }
            else
            {
                Globals.DebugLogger("批量提交成功");
            }
            if (strxml == null)
            {
                Response.ContentType = "text/json";
                builder.Append("{\"success\":false,\"msg\":\"操作失败！\"}");
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write(builder.ToString());
                Response.End();
            }
            string Astatus = strxml.Split('|')[0].ToString();//返回状态
            string Astrtext = strxml.Split('|')[1].ToString();//返回内容
            string Astrstatus = Astatus == "0000" ? "0" : "0";
            orderinfoEntity.Status = Convert.ToInt32(Astrstatus);
            orderinfoEntity.Context = Astrtext;
            if (!go_yolly_orderinfoBusiness.SaveEntity(orderinfoEntity, false, DbServers.DbServerName.LatestDB))
            {
                Response.ContentType = "text/json";
                builder.Append("{\"success\":false,\"msg\":\"操作失败！\"}");
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write(builder.ToString());
                Response.End();
            }
            Response.ContentType = "text/json";
            builder.Append("{\"success\":true,\"msg\":\"操作成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();
        }

        public void BatchSettlement(string adidlist,string names)
        {

            //更改提现状态
            string[] adidArr = adidlist.Split(',');
            for (int i = 0; i < adidArr.Length; i++)
            {
                if (adidArr[i] != "" && adidArr[i] != "all")
                {
                    go_channel_activitydetailEntity classify = go_channel_activitydetailBusiness.LoadEntity(Convert.ToInt32(adidArr[i]));
                    if (classify != null)
                    {
                        classify.Exchange = 2; //更改为已结算
                        classify.Settlementtime = DateTime.Now;
                        go_channel_activitydetailBusiness.SaveEntity(classify, false);

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
        /// 状态
        /// </summary>
        /// <param name="exchange"></param>
        /// <returns></returns>
        public string getExchange(int exchange)
        {
            string getuseStatus = "";
            if (exchange == 0)
            {
                getuseStatus = "<span style='color:gray'>未兑换</span>";
            }
            else if (exchange == 1)
            {
                getuseStatus = "<span style='color:red'>已兑换未结算</span>";
            }
            else if (exchange == 2)
            {
                getuseStatus = "<span style='color:green'>已结算</span>";
            }
            else if (exchange == 3)
            {
                getuseStatus = "<span style='color:blue'>申请结算中</span>";
            }
            return getuseStatus;
        }

        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            StringBuilder builder;
            go_channel_activitydetailQuery query = GetQuery();
            if (uid == 0)
            {
                builder = new StringBuilder("1=1");
            }
            else
            {
                if (exchange != 0)
                    builder = new StringBuilder(string.Format("cuid={0} and exchange={1}", uid, exchange));
                else
                    builder = new StringBuilder(string.Format("cuid={0}", uid));
            }
            if (!string.IsNullOrEmpty(query.starttime))
            {
                builder.AppendFormat(string.Format(" And createtime>='{0}'", query.starttime));
            }
            if (!string.IsNullOrEmpty(query.endtime))
            {
                builder.AppendFormat(string.Format(" And createtime<='{0}'", query.endtime + " 23:59:59"));
            }
            if (Request.QueryString["channel"] != null)
            {
                builder.AppendFormat(string.Format("and cuid={0}", this.CurrLoginChannelUser.Uid));
            }
            if (Request.QueryString["balance"] == "0")
            {
                builder.AppendFormat(string.Format("and exchange=0"));
                this.PlExchange.Visible = false;
                this.spSettlement.Visible = false;
            }
            if (Request.QueryString["balance"] == "1")
            {
                builder.AppendFormat(string.Format("and exchange=1"));
                this.btnapplycash.Visible = false;
                this.PlExchange.Visible = false;
                this.spUnSettlement.Visible = false;
            }
            if (query.exchange != -1)
            {
                //if (query.exchange == 11)

                //    builder.AppendFormat(string.Format(" And exchange in(0,1,3)"));

                //else
                builder.AppendFormat(string.Format(" And exchange={0}", query.exchange));
            }

            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat("and(username  like '%{0}%' or  realname like '%{0}%' )", DataHelper.CleanSearchString(query.keyword));
            }

            //调用通用分页方法绑定
            string from = string.Format(@"(select d.*,u.realname,m.username,a.atitle from go_channel_activitydetail d
                                          left join go_channel_user u on u.uid=d.cuid left join go_member m on m.uid=d.uid left join go_channel_activity a on a.aid=d.aid )T");
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "createtime");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.txtTimeStart.Value = query.starttime;
            this.txtTimeEnd.Value = query.endtime;
            this.ddlExchange.SelectedValue = query.exchange.ToString();
            this.pager1.TotalRecords = managers.TotalRecords;

            allusercount = managers.TotalRecords.ToString();


            DataTable all = CustomsBusiness.GetListData(from, builder.ToString(), "*", "", 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            if (all.Rows.Count > 0)
            {
                DataRow[] dt2 = all.Select("exchange=1");
                DataRow[] dt3 = all.Select("exchange=0");
                hadexchange = (dt2.Length * 3).ToString();
                unexchange = (dt3.Length * 3).ToString();


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
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["starttime"]))
            {
                query.starttime = base.Server.UrlDecode(this.Page.Request.QueryString["starttime"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["endtime"]))
            {
                query.endtime = base.Server.UrlDecode(this.Page.Request.QueryString["endtime"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["exchange"]))
            {
                query.exchange = Convert.ToInt32(base.Server.UrlDecode(this.Page.Request.QueryString["exchange"]));
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
                query.exchange = -1;
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
            queryStrings.Add("exchange", this.ddlExchange.SelectedValue.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "createtime");
            queryStrings.Add("SortOrder", SortAction.Desc.ToString());

            if (Request.QueryString["channel"] != null)
            {
                queryStrings.Add("channel", "true");
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

        /// <summary>
        /// 导出指定日期内的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Export_Click(object sender, EventArgs e)
        {
            string StarTime = this.txtTimeStart.Value.Trim();//开始时间
            string EndTime = this.txtTimeEnd.Value.Trim() + " 23:59:59";//结束时间
            string where = "  createtime>'" + StarTime + "' and createtime<'" + EndTime + "'";
            if (this.tboxKeyword.Text.Trim() != "")
            {
                where += " and u.username like '" + this.tboxKeyword.Text.Trim() + "'";
                where += "or u.realname like '" + this.tboxKeyword.Text.Trim() + "'";
            }

            if (Request.QueryString["channel"] != null)
            {
                where += " and cuid = " + this.CurrLoginChannelUser.Uid + "";
            }
            else
            {

            }
            //需要导出的数据
            string TableName = string.Format(@"(select rank()  over (order by adid desc) as no,d.mantime,d.settlementtime,d.createtime, (case exchange when '0' then '未兑换'  when '1' then '已兑换未结算' when '2' then '已结算' when '3' then '申请结算中' else '其他'end) as exchange,
                                                u.realname,m.username,a.atitle from go_channel_activitydetail d left join go_channel_user u on u.uid=d.cuid 
                                                left join go_member m on m.uid=d.uid left join go_channel_activity a on a.aid=d.aid where {0})T", where);
            DataTable dt = CustomsBusiness.GetListData(string.Format(TableName,
                           "*",  "createtime desc", 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB));
            dt.Columns["no"].ColumnName = "序号";
            dt.Columns["realname"].ColumnName = "所属渠道商";
            dt.Columns["username"].ColumnName = "参与人";
            dt.Columns["atitle"].ColumnName = "活动名称";
            dt.Columns["createtime"].ColumnName = "参与时间";
            dt.Columns["mantime"].ColumnName = "兑换时间";
            dt.Columns["settlementtime"].ColumnName = "结算时间";
            dt.Columns["exchange"].ColumnName = "状态";
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Resources\\channelxls" + time + "channelDeteial.xls";
            ExcelDBClass exls = new ExcelDBClass(path, false);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt.Copy());
            ds.Tables[0].TableName = "channeldetail";
            exls.ImportToExcel(ds);
            exls.Dispose();
            //将文件从服务器上下载到本地
            FileInfo fileInfo = new FileInfo(path);//文件路径
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;   filename=" + Server.UrlEncode(fileInfo.Name.ToString()));//文件名称  
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());//文件长度  
            Response.ContentType = "application/octet-stream";//获取或设置HTTP类型  
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.WriteFile(path);//将文件内容作为文件块直接写入HTTP响应输出流 

        }
        //protected void rptAdmin_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Label lb_no = (Label)e.Item.FindControl("no");
        //        lb_no.Text = (1 + e.Item.ItemIndex).ToString();
        //    }
        //}
    }
}