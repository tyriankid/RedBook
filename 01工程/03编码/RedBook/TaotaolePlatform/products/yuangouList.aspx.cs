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
using System.Windows.Forms;

namespace RedBookPlatform.products
{   
    public partial class yuangouList : Base
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
                    case "goDelete":
                        goDelete(Convert.ToInt32(Request["id"]), Request["name"]);
                        break;
                    


                }
            }
        }
        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            go_yuangouQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            string cateName = "";
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (title Like '%{0}%' )", DataHelper.CleanSearchString(query.keyword));
            }
            //状态筛选
            if(!string.IsNullOrEmpty(query.State))
            {
                switch (query.State)
                {
                    case "进行中":
                        builder.AppendFormat(" AND (shengyurenshu>0)");
                        break;
                    case "已揭晓":
                        builder.AppendFormat(" AND (q_uid!='')");
                        break;
                    case "期数已满":
                        builder.AppendFormat(" AND (qishu=maxqishu)");
                        break;
                }
            }
            //类型筛选
            if (!string.IsNullOrEmpty(query.Type))
            {
                cateName = string.Format(" and cateid='{0}'", query.Type);
            }
            //调用通用分页方法绑定
            string from = "(select gy.*,gp.typeid,gp.money,gp.renqi,gp.pos,gc.name as categoryname,GP.thumb from go_yiyuan GY inner join go_products GP on GY.productid= GP.productid left join go_category GC on GP.categoryid = gc.cateid  where recover=0 " + cateName + ") t";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "yid");
            this.rptYiyuan.DataSource = managers.Data;
            this.rptYiyuan.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
            this.dropState.SelectedValue = query.State;
            this.dropType.SelectedValue = query.Type;
            this.selMultiple.Value = this.Page.Request.QueryString["multiple"];
        }


        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_yuangouQuery GetQuery()
        {
            go_yuangouQuery query = new go_yuangouQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["State"]))
            {
                query.State = base.Server.UrlDecode(this.Page.Request.QueryString["State"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Type"]))
            {
                query.Type = base.Server.UrlDecode(this.Page.Request.QueryString["Type"]);
            }
            query.SortBy = "orders";
            query.SortOrder = SortAction.Asc;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["multiple"]))
            {
                //公共筛选
                switch (this.Page.Request.QueryString["multiple"])
                {
                    case "0":
                        query.SortBy = "orders";
                        query.SortOrder = SortAction.Asc;

                        break;
                    case "1":
                        query.SortBy = "orders";
                        query.SortOrder = SortAction.Desc;

                        break;
                    case "2":
                        query.SortBy = "time";
                        query.SortOrder = SortAction.Asc;
                        break;
                    case "3":
                        query.SortBy = "time";
                        query.SortOrder = SortAction.Desc;
                        break;
                    case "4":
                        query.SortBy = "shengyurenshu";
                        query.SortOrder = SortAction.Asc;
                        break;
                    case "5":
                        query.SortBy = "shengyurenshu";
                        query.SortOrder = SortAction.Desc;
                        break;
                    case "6":
                        query.SortBy = "yunjiage";
                        query.SortOrder = SortAction.Asc;
                        break;
                    case "7":
                        query.SortBy = "yunjiage";
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
            queryStrings.Add("State", this.dropState.SelectedValue);
            queryStrings.Add("Type", this.dropType.SelectedValue);
            queryStrings.Add("multiple", this.selMultiple.Value);
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "yid");
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }

     
        public void goDelete(int yid,string name)
        {
            go_yiyuanEntity yiyuanProduct = go_yiyuanBusiness.LoadEntity(yid);
            //1回收站
            yiyuanProduct.recover = 1;
            go_yiyuanBusiness.UpdateEntity(yiyuanProduct);

            go_configsEntity ec = go_configsBusiness.LoadEntity(3);
            ec.Ctime = DateTime.Now;
            go_configsBusiness.SaveEntity(ec, false);

            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            base.addAdminLog(operationAction.Update, null, "将一元购商品" + name+"放入回收站");
            builder.Append("{\"success\":true,\"msg\":\"回收成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End(); 
         

           
        }

        //更新排序号
        [System.Web.Services.WebMethod]
        public static string UpOrder(string yid,string  order)
        {
            string[] yids = yid.TrimEnd(',').Split(',');
            string[] orders = order.TrimEnd(',').Split(',');

            if (yids.Count() > 0 && orders.Count()>0)
            {
                for (int i = 0; i < yids.Count(); i++)
                {
                    go_yiyuanEntity yiyuanProduct = go_yiyuanBusiness.LoadEntity(int.Parse(yids.GetValue(i).ToString()));
                    //1回收站
                    yiyuanProduct.Orders = int.Parse(orders.GetValue(i).ToString());
                    go_yiyuanBusiness.UpdateEntity(yiyuanProduct);
                }
            }
            go_configsEntity ec = go_configsBusiness.LoadEntity(3);
            ec.Ctime = DateTime.Now;
            go_configsBusiness.SaveEntity(ec,false);
            return "更新排序成功";
        }
    }
}