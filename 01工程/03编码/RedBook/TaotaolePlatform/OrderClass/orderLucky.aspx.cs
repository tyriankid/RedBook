using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;

namespace RedBookPlatform.OrderClass
{
    public partial class orderLucky :Base
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
            go_orderQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat("and (username Like '%{0}%' or title like '%{0}%'or orderId like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            if (!string.IsNullOrEmpty(query.Type))
            {
               // builder.AppendFormat("and (ordertype='{0}' )", DataHelper.CleanSearchString(query.Type));

                string type = DataHelper.CleanSearchString(query.Type);
                if (query.Type == "yuan")
                {
                    builder.AppendFormat("  and  ordertype='yuan'", type);
                }
                if (query.Type == "gift")
                {
                    builder.AppendFormat(" and ordertype='gift'", type);
                }
                //else
                //{
                //    builder.AppendFormat("and (ordertype='{0}' )", type);
                //}

            }
            if (!string.IsNullOrEmpty(query.State))
            {
                builder.AppendFormat("and (status='{0}')", DataHelper.CleanSearchString(query.State));
            }
            if (!string.IsNullOrEmpty(query.StarTime))
            {
                builder.AppendFormat(" and q_end_time>'{0}'", DataHelper.CleanSearchString(query.StarTime));
            }
            if (!string.IsNullOrEmpty(query.EndTime))
            {
                builder.AppendFormat(" and q_end_time <'{0}'", DataHelper.CleanSearchString(query.EndTime));
            }
            string from = "(select m.typeid as mtypeid ,m.username ,o.quantity,g.title,o.orderId,o.money,a.discount,o.ordertype,p.typeid,o.order_address_info,o.status,o.iswon,o.time,c.usetype,(case when a.discount is not null then 'ok' else 'no' end)as shiyong from  go_orders o join go_member m on o.uid=m.uid left join go_activity_code a on a.order_id=o.orderId join go_gifts g on o.businessId=g.giftid join go_products p on g.productid =p.productid left join go_card_recharge c on o.orderId=c.orderId  where o.iswon=1) t";
            DbQueryResult orders = DataHelper.GetPageData(query, from, builder, "orderId");
            this.rptAdmin.DataSource = orders.Data;
            this.rptAdmin.DataBind();
            this.pager1.TotalRecords = orders.TotalRecords;
            this.tboxKeyword.Text = query.keyword;
            this.dropType.SelectedValue = query.Type;
            this.dropState.SelectedValue = query.State;
            this.inpStarTime.Value = query.StarTime;
            this.inpEndTime.Value = query.EndTime;

            DataTable all = CustomsBusiness.GetListData(from, builder.ToString(), "SUM(money) as Allmoney,sum(quantity)as Allquantity","",0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            if (all.Rows.Count > 0)
            {
                if (all.Rows[0]["Allmoney"].ToString() != "")
                {
                    allMoney.InnerText = Convert.ToDouble(all.Rows[0]["Allmoney"].ToString()).ToString("0.00");
                    allQuantity.InnerText = all.Rows[0]["Allquantity"].ToString();
                }
                else
                {
                    allMoney.InnerText = "0";
                    allQuantity.InnerText = "0";
                }
            }

        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_orderQuery GetQuery()
        {
            go_orderQuery query = new go_orderQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Type"]))
            {
                query.Type = base.Server.UrlDecode(this.Page.Request.QueryString["Type"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["State"]))
            {
                query.State = base.Server.UrlDecode(this.Page.Request.QueryString["State"]);
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
            query.SortBy = "time";
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
            queryStrings.Add("Type", this.dropType.SelectedValue);
            queryStrings.Add("State", this.dropState.SelectedValue);
            queryStrings.Add("StarTime", this.inpStarTime.Value.Trim());
            queryStrings.Add("EndTime", this.inpEndTime.Value.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }

            base.ReloadPage(queryStrings);
        }
        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }

        protected void Export_Click(object sender, EventArgs e)
        {
            string StarTime = this.inpStarTime.Value.Trim();//开始时间
            string EndTime = this.inpEndTime.Value.Trim();//结束时间
            string where = "1=1";
            if (this.tboxKeyword.Text.Trim() != "")
            {
                where += "and username like '" + this.tboxKeyword.Text.Trim() + "'";
                where += "or title like '" + this.tboxKeyword.Text.Trim() + "'";
                where += "or orderId like '" + this.tboxKeyword.Text.Trim() + "'";
            }
            if (this.dropType.SelectedValue != "")
            {
               
                //if (this.dropType.SelectedValue == "yuan")
                //{
                //    where += " and m.typeid<>9";
                //}
                //else if (this.dropType.SelectedValue == "baidu")
                //{
                //    where += " and m.typeid=9";
                //}
                //else
                //{
                    where += " and o.ordertype='" + this.dropType.SelectedValue + "'";
              //  }



            }
            if (this.dropState.SelectedValue != "")
            {
                where += " and o.status='" + this.dropState.SelectedValue + "'";
            }
            DataTable dt = CustomsBusiness.GetListData("go_orders o join go_member m on o.uid=m.uid and o.iswon=1 left join go_activity_code a on a.order_id=o.orderId join go_yiyuan y on o.businessId=y.yId join go_products p on y.productid =p.productid left join go_card_recharge c on o.orderId=c.orderId ", where + " and o.time>'" + StarTime + "' and o.time<'" + EndTime + "'", "m.username ,o.quantity,p.title,o.orderId,y.qishu,o.money,(case when o.ordertype ='yuan' then '一元购' else '团购' end)as ordertype,o.status,CONVERT(varchar,o.time,121) as time,CONVERT(varchar,y.q_end_time,121) as q_end_time,(case when a.discount is not null then '已使用' else '未使用' end)as shiyong,(case when a.discount !=0 then a.discount else '0' end)as discount", "o.time desc", 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            dt.Columns["orderId"].ColumnName = "订单ID";
            dt.Columns["time"].ColumnName = "订单时间";
            dt.Columns["qishu"].ColumnName = "期数";
            dt.Columns["quantity"].ColumnName = "购买次数";
            dt.Columns["money"].ColumnName = "订单金额";
            dt.Columns["discount"].ColumnName = "红包金额";
            dt.Columns["username"].ColumnName = "用户名";
            dt.Columns["status"].ColumnName = "订单状态";
            dt.Columns["ordertype"].ColumnName = "订单来源";
            dt.Columns["title"].ColumnName = "商品信息";
            dt.Columns["shiyong"].ColumnName = "是否使用红包";
            dt.Columns["q_end_time"].ColumnName = "揭晓时间";
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Resources\\orderxls\\oederlist\\" + time + "orderlucky.xls";
            ExcelDBClass exls = new ExcelDBClass(path, false);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt.Copy());
            ds.Tables[0].TableName = "orderlucky";
            exls.ImportToExcel(ds);
            exls.Dispose();
            //将文件从服务器上下载到本地
            FileInfo fileInfo = new FileInfo(path);//文件路径如：E:/11/22  
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;   filename=" + Server.UrlEncode(fileInfo.Name.ToString()));//文件名称  
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());//文件长度  
            Response.ContentType = "application/octet-stream";//获取或设置HTTP类型  
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.WriteFile(path);//将文件内容作为文件块直接写入HTTP响应输出流 


        }
    }
}