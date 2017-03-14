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

namespace RedBookPlatform.CardRecharge
{
    public partial class CardRechargeList : Base
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
            go_cardRechargeQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");


            string where = "";
            //账号
            if (!string.IsNullOrEmpty(query.code))
            {
                where += string.Format(" and a.code like '%{0}%'", query.code);
                this.txtcode.Text = query.code;
                
            }

            if (!string.IsNullOrEmpty(query.orderId))
            {
                where += string.Format(" and a.orderId like '%{0}%'", query.orderId);
                this.txtorderId.Text = query.orderId;
                
            }
            
            if (query.money!=0)
            {
                where += string.Format(" and a.money = {0}", query.money);
                this.txtmoney.Text =query.money.ToString();
            } 
            if (!string.IsNullOrEmpty(query.username))
            {
                where += string.Format(" and b.username like '%{0}%'", query.username);
                this.txtusername.Text = query.username;
                
            }
            if (query.isrepeat!=-1)
            {
                where += string.Format(" and a.isrepeat ={0}", query.isrepeat);
               
            }
            this.ddlisrepeat.SelectedValue = query.isrepeat.ToString();
              
            if (query.usetype != -1)
            {
                where += string.Format(" and a.usetype ={0}", query.usetype);
              
            }
            this.ddlusetype.SelectedValue = query.usetype.ToString();



            string from = "(select a.*,b.username  from go_card_recharge a  left join go_member b on a.uid=b.uid  where 1=1 " + where + ") T ";

            //调用通用分页方法绑定
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "id");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
           
            this.pager1.TotalRecords = managers.TotalRecords;
           

        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_cardRechargeQuery GetQuery()
        {
            go_cardRechargeQuery query = new go_cardRechargeQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["code"]))
            {
                query.code = this.Page.Request.QueryString["code"];
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["orderId"]))
            {
                query.orderId = this.Page.Request.QueryString["orderId"];
            }
            


            decimal i=0;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["money"]) && decimal.TryParse(this.Page.Request.QueryString["money"],out i))
            {
                query.money = Convert.ToDecimal(this.Page.Request.QueryString["money"]);
            }
            else
            {
                query.money = 0;
            }

            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["username"]))
            {
                query.username = this.Page.Request.QueryString["username"];
            }

            if (string.IsNullOrEmpty(this.Page.Request.QueryString["isrepeat"]))
            {
                query.isrepeat = -1;
            }
            else
            { 
              query.isrepeat = Convert.ToInt32( this.Page.Request.QueryString["isrepeat"]);
            }

            if (string.IsNullOrEmpty(this.Page.Request.QueryString["usetype"]))
            {
                query.usetype = -1;
            }
            else
            {
                query.usetype = Convert.ToInt32(this.Page.Request.QueryString["usetype"]);

            }
           
             
           

            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortBy"]))
            {
                query.SortBy = this.Page.Request.QueryString["SortBy"];
            }
            query.SortOrder = SortAction.Desc;
            return query;
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="isSearch">是否查询(重置起始页码)</param>
        private void ReLoad(bool isSearch)
        {
            NameValueCollection queryStrings = new NameValueCollection();
          
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "id");
            queryStrings.Add("SortOrder", SortAction.Desc.ToString());
            queryStrings.Add("code", this.txtcode.Text.Trim());
            queryStrings.Add("money", this.txtmoney.Text.Trim());
            queryStrings.Add("username", this.txtusername.Text.Trim());
            queryStrings.Add("isrepeat", this.ddlisrepeat.SelectedValue);
            queryStrings.Add("usetype", this.ddlusetype.SelectedValue);
            queryStrings.Add("orderId", this.txtorderId.Text.Trim());
            base.ReloadPage(queryStrings);
        }

        /// <summary>
        /// 使用用途。1、充话费 2、充支付宝 3、充爽乐币
        /// </summary>
        /// <param name="usetype"></param>
        /// <returns></returns>
        protected string getuseType(string usetype)
        {
            string  getuseType="";
            if (usetype == "1")
            {
                getuseType = "充话费";
            }
            else if (usetype == "2")
            {
                getuseType = "充支付宝";
            }
            else if (usetype == "3")
            {
                getuseType = "充爽乐币";
            }

            return getuseType;
        }

        [System.Web.Services.WebMethod]
        public static string goDelete(string id)
        {
                   string []len=  id.Split(',');
                   for (int i = 0; i < len.Length; i++)
            {
                if (len[i] != "")
                {
                    go_card_rechargeBusiness.Del(Convert.ToInt32(len[i]));
                }
            }
          
            return "删除成功";
        }
    }
}