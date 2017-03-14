using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;

namespace RedBookPlatform.Slides
{
    public partial class SlidesMarket : Base
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


            SlidesWechatQuery query = new SlidesWechatQuery();
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
            query.SortBy = "slideid";
            query.SortOrder = SortAction.Desc;
            if (selMultiple.SelectedValue == "2")
            {
                query.SortOrder = SortAction.Asc;
            }
            StringBuilder builder = new StringBuilder("1=1");
            builder.AppendFormat(" AND (slidetype = {0} )", 3);
            string from = " go_slides";
            //调用通用分页方法绑定
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "slideid");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.pager1.TotalRecords = managers.TotalRecords;

        }

        [System.Web.Services.WebMethod]
        public static string goDelete(int id)
        {
            go_slidesBusiness.Del(id);
            return "删除成功";
        }

        //更新排序号
        [System.Web.Services.WebMethod]
        public static string UpOrder(string yid, string order)
        {
            string[] yids = yid.TrimEnd(',').Split(',');
            string[] orders = order.TrimEnd(',').Split(',');

            if (yids.Count() > 0 && orders.Count() > 0)
            {
                for (int i = 0; i < yids.Count(); i++)
                {
                    go_slidesEntity articleEntity = go_slidesBusiness.LoadEntity(int.Parse(yids.GetValue(i).ToString()));

                    //1回收站
                    articleEntity.Slideorder = int.Parse(orders.GetValue(i).ToString());
                    go_slidesBusiness.UpdateEntity(articleEntity);
                }
            }
            return "更新排序成功";
        }

        protected void selMultiple_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selMultiple.SelectedValue == "0")
            {
                return;
            }
            BindPage();
        }
    }
}