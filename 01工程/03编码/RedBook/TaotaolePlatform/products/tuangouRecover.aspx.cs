﻿using System;
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
    public partial class tuangouRecover :Base
    {
        public string display = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindPage();
                //载入分类下拉框
                DataTable categoryRows = go_categoryBusiness.GetListData("model=1", "name,cateid");
                dropType.DataSource = categoryRows;
                dropType.DataTextField = "name";
                dropType.DataValueField = "cateid";
                dropType.DataBind();
                dropType.Items.Insert(0, new ListItem("全部", ""));

                int code = getcode();
                if (code != 3)
                {
                    display = "display:none";
                }


                string action = Request["action"];
                switch (action)
                {
                    case "goRemove":
                        goRemove(Convert.ToInt32(Request["id"]), Request["name"]);
                        break;
                    case "goReturn":
                        goReturn(Convert.ToInt32(Request["id"]), Request["name"]);
                        break;
                        
                   
                }
            }
        }
         /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            go_tuangouQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            string cateName = "";
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (title Like '%{0}%' )", DataHelper.CleanSearchString(query.keyword));
            }
            //类型筛选
            if (!string.IsNullOrEmpty(query.Type))
            {
              cateName= string.Format(" and cateid='{0}'", query.Type);
            }
            //调用通用分页方法绑定
            string from = "(select gy.*,gp.typeid,gp.money,gp.renqi,gp.pos,gc.name as categoryname,GP.thumb from go_tuan GY inner join go_products GP on GY.productid= GP.productid left join go_category GC on GP.categoryid = gc.cateid  where is_delete=1 " + cateName + ") t";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "tId");
            this.rptTuangou.DataSource = managers.Data;
            this.rptTuangou.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
            this.dropType.SelectedValue = query.Type;
            this.selMultiple.Value = this.Page.Request.QueryString["multiple"];
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
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Type"]))
            {
                query.Type = base.Server.UrlDecode(this.Page.Request.QueryString["Type"]);
            }
            query.SortBy = "sort";
            query.SortOrder = SortAction.Asc;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["multiple"]))
            {
                //公共筛选
                    switch (this.Page.Request.QueryString["multiple"])
                    {
                        case "0":
                            query.SortBy = "sort";
                           query.SortOrder = SortAction.Asc;
                            break;
                        case "1":
                            query.SortBy = "sort";
                            query.SortOrder = SortAction.Desc;
                            break;
                }
            }
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
            queryStrings.Add("Type", this.dropType.SelectedValue);
            queryStrings.Add("multiple", this.selMultiple.Value);
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "tId");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }
        //彻底删除商品
      
        public  void goRemove(int id,string name)
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
         
            go_tuanBusiness.Del(id);
          
            base.addAdminLog(operationAction.Delete, null, "彻底删除了团购商品" + name );

            builder.Append("{\"success\":true,\"msg\":\"删除成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End(); 

            
        }
        //商品还原
     
        public  void goReturn(int id,string name)
        {

            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            go_tuanEntity tuangouProduct = go_tuanBusiness.LoadEntity(id);
            tuangouProduct.Is_delete=0;
            go_tuanBusiness.UpdateEntity(tuangouProduct);

            base.addAdminLog(operationAction.Update, null, "还原了团购商品" + name);

            builder.Append("{\"success\":true,\"msg\":\"还原成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End(); 



        }
    }
}