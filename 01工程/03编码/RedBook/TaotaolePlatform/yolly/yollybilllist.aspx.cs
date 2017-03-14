using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
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


namespace RedBookPlatform.yolly
{
    public partial class yollybilllist : Base
    {

        private static string yollyid = "65548187";//永乐账号
        private static string yollykey = "D39316FEC4984ABE97ED554D749EBE86";//永乐账号唯一key
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPage();
            }
        }
        /// <summary>
        /// 绑定系统账单信息
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            go_yolly_orderinfoQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("status!=-2");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (username Like '%{0}%' or serialid like '%{0}%' or usenum like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            if (!string.IsNullOrEmpty(query.Type))
            {
                builder.AppendFormat("and (usetype='{0}' )", DataHelper.CleanSearchString(query.Type));
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
            string from = "(select a.*,m.username from go_yolly_orderinfo_api a left join go_yolly_orderinfo y on a.serialid=y.serialid left join go_orders o on y.orderId=o.orderId  left join go_member m on o.uid=m.uid) T ";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "serialid");
            this.rptRechargerecord.DataSource = managers.Data;
            this.rptRechargerecord.DataBind();
            this.dropType.SelectedValue = query.Type;
            this.tboxKeyword.Text = query.keyword;

            this.pager1.TotalRecords = managers.TotalRecords;
            DataTable res = CustomsBusiness.GetListData(from, builder.ToString(), "SUM(convert(money,money)) as money");
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
        private go_yolly_orderinfoQuery GetQuery()
        {
            go_yolly_orderinfoQuery query = new go_yolly_orderinfoQuery();
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

            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["EndTime"]))
            {
                query.EndTime = base.Server.UrlDecode(this.Page.Request.QueryString["EndTime"]);
            }
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;

            query.SortBy = "t.usetime";
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }


        //同步流水号
        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            try
            {



                //查询对账表所有流水号，拼接成条件
                string where = "'0'";
                DataTable dtyollyapi = go_yolly_orderinfo_apiBusiness.GetListData(null, "serialid", null, 0, DbServers.DbServerName.LatestDB);
                Globals.DebugLogger(dtyollyapi.Rows.Count.ToString());
                for (int p = 0; p < dtyollyapi.Rows.Count; p++)
                {
                    if (p == 0)
                    {
                        where += ",'" + dtyollyapi.Rows[p]["serialid"].ToString() + "',";
                    }
                    else if (p == dtyollyapi.Rows.Count - 1)
                    {
                        where += "'" + dtyollyapi.Rows[p]["serialid"].ToString() + "'";
                    }
                    else
                    {
                        where += "'" + dtyollyapi.Rows[p]["serialid"].ToString() + "',";
                    }
                }
                #region *********************************实例化新表，并添加新的列，与数据库列名一样 *********************************
                DataTable dtnew = new DataTable();
                dtnew.Columns.Add("serialid", typeof(string));
                dtnew.Columns.Add("orderId", typeof(string));
                dtnew.Columns.Add("money", typeof(string));
                dtnew.Columns.Add("usenum", typeof(string));
                dtnew.Columns.Add("usetime", typeof(string));
                dtnew.Columns.Add("usetype", typeof(string));
                dtnew.Columns.Add("status", typeof(string));
                dtnew.Columns.Add("context", typeof(string));
                #endregion
                //未同步的数据
                string strxml = null;
                //解析XML文件并查询所需要的数据
                XmlDocument doc = new XmlDocument();
                DataTable dtsystem = go_yolly_orderinfoBusiness.GetListData("serialid not in (" + where + ") and status='1'", "*", "usetime asc", 0, YH.Utility.DbServers.DbServerName.LatestDB);
                for (int i = 0; i < dtsystem.Rows.Count; i++)
                {

                    switch (dtsystem.Rows[i]["usetype"].ToString())
                    {
                        case "1"://话费查询
                            strxml = yollyinterface.telephoneyolly(dtsystem.Rows[i]["serialid"].ToString(), "1");
                            doc.LoadXml(strxml);
                            XmlNodeList xxList = doc.GetElementsByTagName("YOLLY");
                            //循环xml数据，得到自己想要的值
                            foreach (XmlNode xxNode in xxList)  //Node 是每一个<CL>...</CL>体  
                            {
                                XmlNode RSPCODE = xxNode.SelectNodes("RESPONSE/RSPCODE").Item(0);
                                if (RSPCODE.InnerText == "0000")
                                {
                                    #region *********************************新表添加新的行，与数据库同步，并把永乐数据写到内存表 *********************************
                                    dtnew.Rows.Add();
                                    dtnew.Rows[i]["serialid"] = xxNode.SelectNodes("RESPONSE/FLOWNUMBERTHIRD").Item(0).InnerText;
                                    dtnew.Rows[i]["orderId"] = "";
                                    dtnew.Rows[i]["money"] = xxNode.SelectNodes("RESPONSE/MONEY").Item(0).InnerText;
                                    dtnew.Rows[i]["usenum"] = xxNode.SelectNodes("RESPONSE/MOBILE").Item(0).InnerText;
                                    dtnew.Rows[i]["usetime"] = DateTime.Now;
                                    dtnew.Rows[i]["usetype"] = "1";
                                    dtnew.Rows[i]["status"] = xxNode.SelectNodes("RESPONSE/RSPSTATUS").Item(0).InnerText;
                                    dtnew.Rows[i]["context"] = xxNode.SelectNodes("RESPONSE/RSPDESC").Item(0).InnerText;
                                    #endregion
                                }
                            }
                            break;
                        case "2"://支付宝
                        case "4"://QQ币
                            Globals.DebugLogger(dtsystem.Rows[i]["serialid"].ToString() + ",");
                            strxml = yollyinterface.telephoneyolly(dtsystem.Rows[i]["serialid"].ToString(), "2");
                            doc.LoadXml(strxml);
                            XmlNodeList xxLists = doc.GetElementsByTagName("YOLLY");
                            //循环xml数据，得到自己想要的值
                            foreach (XmlNode xxNode in xxLists)  //Node 是每一个<CL>...</CL>体  
                            {
                                XmlNode RSPCODE = xxNode.SelectNodes("RESPONSE/RSPCODE").Item(0);
                                if (RSPCODE.InnerText == "0000")
                                {
                                    #region *********************************新表添加新的行，与数据库同步，并把永乐数据写到内存表 *********************************
                                    dtnew.Rows.Add();
                                    dtnew.Rows[i]["serialid"] = xxNode.SelectNodes("RESPONSE/FLOWNUMBERTHIRD").Item(0).InnerText;
                                    dtnew.Rows[i]["orderId"] = "";
                                    dtnew.Rows[i]["money"] = xxNode.SelectNodes("RESPONSE/NUM").Item(0).InnerText;
                                    dtnew.Rows[i]["usenum"] = xxNode.SelectNodes("RESPONSE/ACCOUNT").Item(0).InnerText;
                                    dtnew.Rows[i]["usetime"] = DateTime.Now;
                                    dtnew.Rows[i]["usetype"] = xxNode.SelectNodes("RESPONSE/PRODUCTNAME").Item(0).InnerText.ToString() == "支付宝账户充值 " ? "2" : "4";
                                    dtnew.Rows[i]["status"] = xxNode.SelectNodes("RESPONSE/RSPSTATUS").Item(0).InnerText;
                                    dtnew.Rows[i]["context"] = xxNode.SelectNodes("RESPONSE/RSPDESC").Item(0).InnerText;
                                    #endregion
                                }
                            }
                            Globals.DebugLogger(dtnew.Rows.ToString());
                            Globals.DebugLogger(strxml);
                            break;
                    }

                }
                //导入到数据库
                if (CustomsBusiness.CommitDataTable(dtnew, "select * from go_yolly_orderinfo_api", DbServers.DbServerName.LatestDB))
                {
                    int i = dtnew.Rows.Count;
                    DataTable dt = go_configsBusiness.GetListData("ctype='go_yolly_orderinfo_api' and ccode is not null", "*", null, 1, DbServers.DbServerName.LatestDB);
                    go_configsEntity configsEntity;
                    //更新configs表，计数表
                    if (dt.Rows.Count != 0)
                    {
                        configsEntity = go_configsBusiness.LoadEntity(Convert.ToInt32(dt.Rows[0]["cid"]));
                        configsEntity.Ctime = DateTime.Now;
                        configsEntity.Ccode = dtnew.Rows[i - 1]["serialid"].ToString();
                        go_configsBusiness.SaveEntity(configsEntity, false);
                    }
                    else
                    {
                        configsEntity = new go_configsEntity
                        {
                            Ctype = "go_yolly_orderinfo_api",
                            Ctime = DateTime.Now,
                            Ccode = dtnew.Rows[i - 1]["serialid"].ToString()
                        };
                        go_configsBusiness.SaveEntity(configsEntity, true);
                    }
                }
            }

            catch
            {

            }

        }
    }
}