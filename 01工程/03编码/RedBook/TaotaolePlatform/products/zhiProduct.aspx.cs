using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;

namespace RedBookPlatform.products
{
    public partial class zhiProduct :Base
    {
        public string display = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindPage();
                int code = getcode();
                if (code != 3)
                {
                    display = "display:none";
                }


            }
        }
        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            go_zhigouQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND title Like '%{0}%'", DataHelper.CleanSearchString(query.keyword));
            }
            string from = "(select  PD.typeid,PD.thumb,PD.money,Z.*from go_zhigou Z   left join  go_products   PD  on PD.productid=Z.productid)t";
            //调用通用分页方法绑定
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "QuanId");
            this.rptZhiGou.DataSource = managers.Data;
            this.rptZhiGou.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_zhigouQuery GetQuery()
        {
            go_zhigouQuery query = new go_zhigouQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            query.SortBy = "AddTime";
            query.SortOrder = SortAction.Desc;
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
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
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "time");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }
        //删除
        [System.Web.Services.WebMethod]
        public static string deleteQuan(int id, string name)
        {
            go_zhigouBusiness.Del(id);
            return "彻底删除了直购商品" + name;

        }
    }
}