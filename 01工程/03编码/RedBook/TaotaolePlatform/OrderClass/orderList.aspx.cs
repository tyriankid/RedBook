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
using Taotaole.Business;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;
using System.IO;

namespace RedBookPlatform.OrderClass
{
    public partial class orderList :Base
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
            go_orderQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat("and (username Like '%{0}%' or title like '%{0}%' or  orderId like '%{0}%' )", DataHelper.CleanSearchString(query.keyword));
            }
            if (!string.IsNullOrEmpty(query.Type))
            {

                string type = DataHelper.CleanSearchString(query.Type);
                if (query.Type == "yuan")
                {
                    builder.AppendFormat("and (typeids<>9 )", type);
                }
                else if (query.Type == "baidu")
                {
                    builder.AppendFormat("and (typeids=9)", type);
                }
                else
                {
                    builder.AppendFormat("and (ordertype='{0}' )", type);
                }


            }
            if (!string.IsNullOrEmpty(query.iswon))
            {
                builder.AppendFormat("and (iswon='{0}' )", DataHelper.CleanSearchString(query.iswon));
            }
            if (!string.IsNullOrEmpty(query.State))
            {

                builder.AppendFormat("and (status='{0}')", DataHelper.CleanSearchString(query.State));
            }
            if (!string.IsNullOrEmpty(query.StarTime))
            {
                builder.AppendFormat(" and time>'{0}'", DataHelper.CleanSearchString(query.StarTime));
            }
            if (!string.IsNullOrEmpty(query.EndTime))
            {
                builder.AppendFormat(" and time <'{0}'", DataHelper.CleanSearchString(query.EndTime));
            }
            //调用通用分页方法绑定
            string from = "(Select GM.typeid as typeids,t.qishu,PD.thumb,pd.typeid,(case when AC.discount is null then 'no' else 'ok' end)as shiyong,AC.discount,T.title,GM.username,PD.productid,O.* From go_orders O left join go_activity_code  AC on O.orderId=AC.order_id inner Join go_member GM on o.uid=Gm.uid Inner Join  (Select ordertype='yuan',yId as businessid,title,productid ,qishu From go_yiyuan union all Select ordertype='tuan',tl.tuanlistId as businessid,t.title,t.productid ,'0' as qishu From go_tuan_listinfo tl Inner Join go_tuan t on tl.tuanId=t.tId  )T ON O.ordertype=T.ordertype And O.businessId=T.businessid  Inner Join go_products PD  on  PD.productid=t.productid )t ";
            DbQueryResult orders = DataHelper.GetPageData(query, from, builder, "orderId");
            this.rptAdmin.DataSource = orders.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.dropType.SelectedValue = query.Type;
            this.dropState.SelectedValue = query.State;
            this.iswon.SelectedValue = query.iswon;
            this.pager1.TotalRecords = orders.TotalRecords;
            this.inpStarTime.Value = query.StarTime;
            this.inpEndTime.Value = query.EndTime;

            DataTable all = CustomsBusiness.GetListData(from, builder.ToString(), "SUM(money) as Allmoney,sum(quantity)as Allquantity,sum(discount)as discount", "", 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            if (all.Rows.Count > 0)
            {
                if (all.Rows[0]["Allmoney"].ToString() != "")
                {
                    allMoney.InnerText = Convert.ToDouble(all.Rows[0]["Allmoney"].ToString()).ToString("0.00");
                    allQuantity.InnerText = all.Rows[0]["Allquantity"].ToString();
                    hbmoney.InnerText =all.Rows[0]["discount"].ToString()!=""? Convert.ToDouble(all.Rows[0]["discount"].ToString()).ToString("0.00"):"0";
                    shmoney.InnerText = (Convert.ToDecimal(allMoney.InnerText) - Convert.ToDecimal(hbmoney.InnerText)).ToString();
                }
                else
                {
                    allMoney.InnerText = "0";
                    allQuantity.InnerText = "0";
                    hbmoney.InnerText = "0";
                    shmoney.InnerText = "0";
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
            query.SortBy = "time";
            query.SortOrder = SortAction.Desc;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Type"]))
            {
                query.Type = base.Server.UrlDecode(this.Page.Request.QueryString["Type"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["State"]))
            {
                query.State = base.Server.UrlDecode(this.Page.Request.QueryString["State"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["iswon"]))
            {
                query.iswon = base.Server.UrlDecode(this.Page.Request.QueryString["iswon"]);
            }
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["StarTime"]))
            {
                query.StarTime = base.Server.UrlDecode(this.Page.Request.QueryString["StarTime"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["EndTime"]))
            {
                query.EndTime = base.Server.UrlDecode(this.Page.Request.QueryString["EndTime"]);
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
            queryStrings.Add("Type", this.dropType.SelectedValue);
            queryStrings.Add("State", this.dropState.SelectedValue);
            queryStrings.Add("iswon", this.iswon.SelectedValue);
            queryStrings.Add("StarTime", this.inpStarTime.Value.Trim());
            queryStrings.Add("EndTime", this.inpEndTime.Value.Trim());
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
     

        /// <summary>
        /// 导出指定日期内的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Export_Click(object sender, EventArgs e)
        {
            string StarTime = this.inpStarTime.Value.Trim();//开始时间
            string EndTime = this.inpEndTime.Value.Trim();//结束时间
            string where = "1=1";
            if (this.tboxKeyword.Text.Trim() != "")
            {
                where +="and username like '"+ this.tboxKeyword.Text.Trim()+"'";
                where += "or title like '" + this.tboxKeyword.Text.Trim() + "'";
                where += "or orderId like '" + this.tboxKeyword.Text.Trim() + "'";
            }
            if (this.dropType.SelectedValue != "")
            {
                where += " and o.ordertype='" + this.dropType.SelectedValue+"'";
            }
            if (this.dropState.SelectedValue != "")
            {
                where += " and o.status='" + this.dropState.SelectedValue + "'";
            }
            if (this.iswon.SelectedValue != "")
            {
                where += " and o.iswon=' " + this.iswon.SelectedValue + "'";
            }
            double du = 9.40;
            int h = Convert.ToInt32(du);
            //需要导出的数据
            DataTable dt = CustomsBusiness.GetListData("go_orders o left join go_activity_code a on  o.orderId=a.order_id join go_member m on o.uid=m.uid  join go_yiyuan y on o.businessId=y.yId  join go_products p on y.productid=p.productid", where + " and o.time>'" + StarTime + "' and o.time<'" + EndTime + "'", "m.username,p.title,o.orderId,y.qishu,o.quantity,o.money,(case when a.discount is not null then '已使用' else '未使用' end)as shiyong,a.discount,CONVERT(varchar,o.time,121) as time ,o.status,(case when o.ordertype ='yuan' then '一元购' else '团购' end)as ordertype,(case when o.iswon =0 then '未中奖' else '中奖' end)as iswon", "o.time desc", 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
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
            dt.Columns["iswon"].ColumnName = "是否中奖";
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Resources\\orderxls\\oederlist\\" + time + "orderlist.xls";
            ExcelDBClass exls = new ExcelDBClass(path, false);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt.Copy());
            ds.Tables[0].TableName = "OrderInfo";
            exls.ImportToExcel(ds);
            exls.Dispose();
            //将文件从服务器上下载到本地
            FileInfo fileInfo = new FileInfo(path );//文件路径如：E:/11/22  
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;   filename=" + Server.UrlEncode(fileInfo.Name.ToString()));//文件名称  
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());//文件长度  
            Response.ContentType = "application/octet-stream";//获取或设置HTTP类型  
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.WriteFile(path);//将文件内容作为文件块直接写入HTTP响应输出流 
        }
    }
}