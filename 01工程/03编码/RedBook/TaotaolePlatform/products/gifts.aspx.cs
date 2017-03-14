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

namespace RedBookPlatform.products
{
    public partial class gifts :Base
    {   

        public string display = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                BindPage();
                //载入分类下拉框
                int code = getcode();
                if (code != 3)
                {
                    display = "display:none";
                }


                string action = Request["action"];
                switch (action)
                {
                    case "goDelete":
                        goDelete(Convert.ToInt32(Request["giftsId"]), Request["name"]);
                        break;
                }
            }

        }
        private void BindPage()
        {
            //获得用户输入的条件
            go_tuangouQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (title Like '%{0}%' )", DataHelper.CleanSearchString(query.keyword));
            }
            //调用通用分页方法绑定
            string from = "(select p.thumb,g.*from go_gifts g  left  join  go_products p on  g.productId=p.productId) t";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "giftId");
            this.rptTuangou.DataSource = managers.Data;
            this.rptTuangou.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
        }
        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_tuangouQuery GetQuery()
        {
            go_tuangouQuery query = new go_tuangouQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            query.SortBy = "addtime";
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
            queryStrings.Add("SortBy", "giftId");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            base.ReloadPage(queryStrings);
        }
        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }
        //删除
        public void goDelete(int giftsId, string name)
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();

            go_giftsBusiness.Del(giftsId);
          
            base.addAdminLog(operationAction.Delete, null, "彻底删除了团购商品" + name);

            builder.Append("{\"success\":true,\"msg\":\"删除成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End(); 

        }
    }
}