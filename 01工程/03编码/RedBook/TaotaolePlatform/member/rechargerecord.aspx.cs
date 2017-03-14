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
using YH.Utility;

namespace RedBookPlatform.member
{
    public partial class rechargerecord : Base
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
            go_adminQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (uid Like '%{0}%' or username like '%{0}%' or code like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            if (!string.IsNullOrEmpty(query.status))
            {
                builder.AppendFormat(" AND status='{0}'", DataHelper.CleanSearchString(query.status));
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
            string from = "(select CR.money as chanmoney,pricemoney,obligatemoney,ADM.money as giftMoney,a.*,b.username  from  ( select*from go_member_addmoney_record  where  pay_type!='系统赠送') a  left join go_member b on a.uid=b.uid  left  join  (select orderid,money as  pricemoney from  go_moneyPoolDetail  where moneyType='总部奖池') PRI  on a.code=PRI.orderid  left  join (select orderid,money as  obligatemoney  from go_moneyPoolDetail  where  moneyType='总部预留') OBI  on  a.code=OBI.orderid left  join  go_channel_recodes   CR   on  a.code=CR.orderId left  join  (select*from go_member_addmoney_record where pay_type='系统赠送')  ADM   on  ADM.code=a.code) T ";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "id");
            this.rptRechargerecord.DataSource = managers.Data;
            this.rptRechargerecord.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.dropStatus.SelectedValue = query.status;
            this.pager1.TotalRecords = managers.TotalRecords;
            this.inpStarTime.Value = query.StarTime;
            this.inpEndTime.Value = query.EndTime;

           // DataTable res = go_member_addmoney_recordBusiness.GetListData(builder.ToString(), "SUM(money) as money");
            DataTable res = CustomsBusiness.GetListData(from, builder.ToString(), "SUM(money) as money,SUM(chanmoney) as chanmoney,SUM(pricemoney) as pricemoney,SUM(obligatemoney) as obligatemoney,SUM(giftMoney) as giftMoney");
                this.labSum.Text =(res.Rows[0]["money"]).ToString()==""?"0":Convert.ToDouble((res.Rows[0]["money"]).ToString()).ToString("0.00");
                this.libYu.Text = (res.Rows[0]["obligatemoney"]).ToString() == "" ? "0" : Convert.ToDouble((res.Rows[0]["obligatemoney"]).ToString()).ToString("0.00");
                this.LabJiang.Text = (res.Rows[0]["pricemoney"]).ToString() == "" ? "0" : Convert.ToDouble((res.Rows[0]["pricemoney"]).ToString()).ToString("0.00");
                this.LabYong.Text = (res.Rows[0]["chanmoney"]).ToString() == "" ? "0" : Convert.ToDouble((res.Rows[0]["chanmoney"]).ToString()).ToString("0.00");
                this.LabGiftMoney.Text = (res.Rows[0]["giftMoney"]).ToString() == "" ? "0" : Convert.ToDouble((res.Rows[0]["giftMoney"]).ToString()).ToString("0.00");
            
        }
        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_adminQuery GetQuery()
        {
            go_adminQuery query = new go_adminQuery();
            disPlay.Visible = false;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            
            }
            query.status = "已付款";
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["status"]))
            {
                query.status = base.Server.UrlDecode(this.Page.Request.QueryString["status"]);
                if (this.Page.Request.QueryString["status"]== "未付款")
                {
                    disPlay.Visible = true;
                }
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
            query.SortBy = "id";
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
            queryStrings.Add("StarTime", this.inpStarTime.Value.Trim());
            queryStrings.Add("EndTime", this.inpEndTime.Value.Trim());

            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "uid");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            queryStrings.Add("status", this.dropStatus.SelectedValue.Trim());
           
            base.ReloadPage(queryStrings);
        }
        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }
        [System.Web.Services.WebMethod]
        public static string oncDelete(string Id)
        {
            if (Id != "")
            {   
                string [] Ids=Id.TrimEnd(',').Split(',');
                for( int i=0;i<Ids.Length;i++)
                {
                    go_member_addmoney_recordBusiness.Del(int.Parse(Ids.GetValue(i).ToString()));
                }
            }
            return "删除成功！";
            
        }
    }
}