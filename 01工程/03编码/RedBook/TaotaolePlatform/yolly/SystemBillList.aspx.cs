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
using System.IO;
using System.Net;
using System.Xml;

namespace RedBookPlatform.yolly
{
    public partial class SystemBillList : Base
    {
        private static string yollyid = "65548187";//永乐账号
        private static string yollykey = "D39316FEC4984ABE97ED554D749EBE86";//永乐账号唯一key
        public int code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPage();
                string action = Request["action"];
                switch (action)
                {
                    case "update"://线下打款，线上更新状态
                        updateState();
                        break;
                }

                 code = getcode();
              
            }
        }
        /// <summary>
        /// 绑定系统账单信息
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            go_yolly_orderinfo_apiQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("status!=-2");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (username Like '%{0}%' or orderId like '%{0}%' or serialid like '%{0}%' or usenum like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            if (!string.IsNullOrEmpty(query.State))
            {
                builder.AppendFormat("and (status='{0}')", DataHelper.CleanSearchString(query.State));
            }
            if (!string.IsNullOrEmpty(query.Type))
            {
                builder.AppendFormat("and (usetype='{0}' )", DataHelper.CleanSearchString(query.Type));
            }
            if (!string.IsNullOrEmpty(query.paytype))
            {
                builder.AppendFormat("and (paytype='{0}' )", DataHelper.CleanSearchString(query.paytype));
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
            string from = "(select y.*, (case when m.username is null  	then u.realname 	ELSE m.username	end) as username  from go_yolly_orderinfo y left join go_orders o on y.orderId=o.orderId left join go_member m on o.uid=m.uid left join (Select orderid,cuid From go_channel_activitydetail Group by orderid,cuid) as a on y.orderId = a.orderid left join go_channel_user as u on a.cuid = u.uid ) T ";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "serialid");
            this.rptRechargerecord.DataSource = managers.Data;
            this.rptRechargerecord.DataBind();
            this.dropType.SelectedValue = query.Type;
            this.tboxKeyword.Text = query.keyword;
            this.dropState.SelectedValue = query.State;
            this.pager1.TotalRecords = managers.TotalRecords;
            this.paytype.SelectedValue = query.paytype;
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

            DataTable dttime = go_configsBusiness.GetListData("ctype='SystemYolly'", "*", null, 1, DbServers.DbServerName.LatestDB);
            if (dttime.Rows.Count == 0)
            {
                this.time.Text = "暂未同步";
            }
            else
            {
                this.time.Text = dttime.Rows[0]["ctime"].ToString();
            }
        }
        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_yolly_orderinfo_apiQuery GetQuery()
        {
            go_yolly_orderinfo_apiQuery query = new go_yolly_orderinfo_apiQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["StarTime"]))
            {
                query.StarTime = base.Server.UrlDecode(this.Page.Request.QueryString["StarTime"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Type"]))
            {
                query.Type = base.Server.UrlDecode(this.Page.Request.QueryString["Type"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["paytype"]))
            {
                query.paytype = base.Server.UrlDecode(this.Page.Request.QueryString["paytype"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["State"]))
            {
                query.State = base.Server.UrlDecode(this.Page.Request.QueryString["State"]);
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
            queryStrings.Add("Type", this.dropType.SelectedValue);
            queryStrings.Add("paytype", this.paytype.SelectedValue);
            queryStrings.Add("keyword", this.tboxKeyword.Text.Trim());
            queryStrings.Add("StarTime", this.inpStarTime.Value.Trim());
            queryStrings.Add("State", this.dropState.SelectedValue);
            queryStrings.Add("EndTime", this.inpEndTime.Value.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "uid");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            base.ReloadPage(queryStrings);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }

        protected void tongbuyolly_Click(object sender, EventArgs e)
        {
            DataTable dttongbu = go_yolly_orderinfoBusiness.GetListData("status='0' and issynchro!='1' and usetime<'" + DateTime.Now.AddHours(-4) + "'", "*", "usetime desc", 0, DbServers.DbServerName.LatestDB);
            string strxml = null;
            //解析XML文件并查询所需要的数据
            XmlDocument doc = new XmlDocument();
            if (dttongbu.Rows.Count != 0)
            {

                //获取需要同步的流水号并拼接成条件
                for (int p = 0; p < dttongbu.Rows.Count; p++)
                {
                    go_yolly_orderinfoEntity orderinfoEntity = go_yolly_orderinfoBusiness.LoadEntity(dttongbu.Rows[p]["serialid"].ToString());
                    switch (dttongbu.Rows[p]["usetype"].ToString())
                    {

                        case "1"://话费查询
                            strxml = new WebUtils().DoPost(Globals.API_Domain + "taotaoleinterfaceapi.ashx?", string.Format("action=selectyolly&serialid={0}&type={1}", dttongbu.Rows[p]["serialid"].ToString(), "1"));
                            doc.LoadXml(strxml);
                            XmlNodeList xxList = doc.GetElementsByTagName("YOLLY");
                            //循环xml数据，得到自己想要的值
                            foreach (XmlNode xxNode in xxList)  //Node 是每一个<CL>...</CL>体  
                            {
                                XmlNode RSPCODE = xxNode.SelectNodes("RESPONSE/RSPCODE").Item(0);
                                if (RSPCODE.InnerText == "0000")
                                {
                                    int zt = Convert.ToInt32(xxNode.SelectNodes("RESPONSE/RSPSTATUS").Item(0).InnerText);
                                    orderinfoEntity.Status = zt == 2 ? 1 : 0;
                                    orderinfoEntity.Issynchro = 1;
                                    switch (zt)
                                    {
                                        case 1:
                                            orderinfoEntity.Context = "充值处理中" + "(操作用户：" + this.CurrLoginAdmin.Username + "，操作时间：" + DateTime.Now + ")";
                                            break;
                                        case 2:
                                            orderinfoEntity.Context = "充值成功" + "(操作用户：" + this.CurrLoginAdmin.Username + "，操作时间：" + DateTime.Now + ")";
                                            break;
                                        case 3:
                                            orderinfoEntity.Context = "充值失败" + "(操作用户：" + this.CurrLoginAdmin.Username + "，操作时间：" + DateTime.Now + ")";
                                            break;
                                        case 4:
                                            orderinfoEntity.Context = "冲正处理中" + "(操作用户：" + this.CurrLoginAdmin.Username + "，操作时间：" + DateTime.Now + ")";
                                            break;
                                        case 5:
                                            orderinfoEntity.Context = "冲正成功" + "(操作用户：" + this.CurrLoginAdmin.Username + "，操作时间：" + DateTime.Now + ")";
                                            break;

                                    }
                                    go_yolly_orderinfoBusiness.SaveEntity(orderinfoEntity, false);
                                }
                                //orderinfoEntity.Context=
                            }
                            break;
                        case "2"://支付宝
                        case "4"://QQ币
                            strxml = new WebUtils().DoPost(Globals.API_Domain + "taotaoleinterfaceapi.ashx?", string.Format("action=selectyolly&serialid={0}&type={1}", dttongbu.Rows[p]["serialid"].ToString(), "2"));
                            doc.LoadXml(strxml);
                            XmlNodeList xxLists = doc.GetElementsByTagName("YOLLY");
                            //循环xml数据，得到自己想要的值
                            foreach (XmlNode xxNode in xxLists)  //Node 是每一个<CL>...</CL>体  
                            {
                                XmlNode RSPCODE = xxNode.SelectNodes("RESPONSE/RSPCODE").Item(0);
                                if (RSPCODE.InnerText == "0000")
                                {
                                    int zt = Convert.ToInt32(xxNode.SelectNodes("RESPONSE/RSPSTATUS").Item(0).InnerText);
                                    orderinfoEntity.Status = zt == 2 ? 1 : 0;
                                    orderinfoEntity.Issynchro = 1;
                                    switch (zt)
                                    {
                                        case 1:
                                            orderinfoEntity.Context = "充值处理中" + "(操作用户：" + this.CurrLoginAdmin.Username + "，操作时间：" + DateTime.Now + ")";
                                            break;
                                        case 2:
                                            orderinfoEntity.Context = "充值成功" + "(操作用户：" + this.CurrLoginAdmin.Username + "，操作时间：" + DateTime.Now + ")";
                                            break;
                                        case 3:
                                            orderinfoEntity.Context = "充值失败" + "(操作用户：" + this.CurrLoginAdmin.Username + "，操作时间：" + DateTime.Now + ")";
                                            break;
                                        case 4:
                                            orderinfoEntity.Context = "冲正处理中" + "(操作用户：" + this.CurrLoginAdmin.Username + "，操作时间：" + DateTime.Now + ")";
                                            break;
                                        case 5:
                                            orderinfoEntity.Context = "冲正成功" + "(操作用户：" + this.CurrLoginAdmin.Username + "，操作时间：" + DateTime.Now + ")";
                                            break;

                                    }
                                    go_yolly_orderinfoBusiness.SaveEntity(orderinfoEntity, false);
                                }
                            }
                            break;
                    }
                }
                //判断技术表是否有数据,更新计数表数据
                go_configsEntity configsEntity = null;
                DataTable dttime = go_configsBusiness.GetListData("ctype='SystemYolly'", "*", null, 1, DbServers.DbServerName.LatestDB);
                if (dttime.Rows.Count == 0)
                {
                    configsEntity = new go_configsEntity
                    {
                        Ctype = "SystemYolly",
                        Ctime = DateTime.Now
                    };
                    go_configsBusiness.SaveEntity(configsEntity, true);
                }
                else
                {
                    configsEntity = go_configsBusiness.LoadEntity(Convert.ToInt32(dttime.Rows[0]["cid"]), DbServers.DbServerName.LatestDB);
                    configsEntity.Ctime = DateTime.Now;
                    go_configsBusiness.SaveEntity(configsEntity, false);
                }
                ReLoad(true);
            }
            else
            {
                Response.Write("<script>alert('没有需要同步的数据！')</script>");
            }

        }
        /// <summary>
        /// 更新状态
        /// </summary>
        private void updateState()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            string serialid = Request.Form["serialid"];
            go_yolly_orderinfoEntity orderinfoEntity = go_yolly_orderinfoBusiness.LoadEntity(serialid);
            orderinfoEntity.Status = 1;
            orderinfoEntity.Issynchro = 2;
            orderinfoEntity.Context = "充值成功(操作用户：" + this.CurrLoginAdmin.Username + "，操作时间：" + DateTime.Now + ")";
            orderinfoEntity.Paytype = "线下充值";
            go_yolly_orderinfoBusiness.SaveEntity(orderinfoEntity, false);
            builder.Append("{\"success\":true,\"msg\":\"状态已更新！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();
        }

    }
}