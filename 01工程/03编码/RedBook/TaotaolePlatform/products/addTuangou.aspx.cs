using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;

namespace RedBookPlatform.products
{
    public partial class addTuangou : Base
    {
        public int tid;
        protected void Page_Load(object sender, EventArgs e)
        {
            tid = Convert.ToInt32(Request.QueryString["tId"]);
            if (!this.IsPostBack)
            {
                Zuidashu.Value = "5000";
                init();
            }
        }
        protected void init()
        {
            //载入分类下拉框
            DataTable categoryRows = go_categoryBusiness.GetListData("model=1", "name,cateid");
            ddlCategory.DataSource = categoryRows;
            ddlCategory.DataTextField = "name";
            ddlCategory.DataValueField = "cateid";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("请选择分类", "0"));
            ddlCategory_SelectedIndexChanged(null, null);

            if (tid > 0)
            {
                load();
            }
        }

        protected void load()
        {
            DataTable dtLisTin=go_tuan_listinfoBusiness.GetListData("tuanId=" + tid + "");
            if (dtLisTin.Rows.Count>0)
            {
                Renshu.Disabled = true;
                Renshu.Style.Value = "background-color:#EEE0E5";
                Danjia.Disabled = true;
                Danjia.Style.Value = "background-color:#EEE0E5";
                Zuidashu.Disabled=true;
                Zuidashu.Style.Value = "background-color:#EEE0E5";
                reachtuan_num.Disabled = true;
                reachtuan_num.Style.Value = "background-color:#EEE0E5";
                prize_num.Disabled = true;
                prize_num.Style.Value = "background-color:#EEE0E5";
                Kaishi.Disabled = true;
                Kaishi.Style.Value = "background-color:#EEE0E5";
                Jieshu.Disabled = true;
                Jieshu.Style.Value = "background-color:#EEE0E5";
                inpTime.Disabled = true;
                inpTime.Style.Value = "background-color:#EEE0E5";
            }
            go_tuanEntity tuangouProduct = go_tuanBusiness.LoadEntity(tid);
            title.Value = tuangouProduct.Title;
            Danjia.Value = tuangouProduct.Per_price.ToString("F2");
            Renshu.Value = tuangouProduct.Total_num.ToString();
            tdSort.Value = tuangouProduct.Sort.ToString();
            Zuidashu.Value = tuangouProduct.Max_sell.ToString();
            reachtuan_num.Value = tuangouProduct.Reachtuan_num.ToString();//开奖规则
            prize_num.Value = tuangouProduct.Prize_num.ToString();//开奖规则
            if (Convert.ToDateTime(tuangouProduct.Start_time.ToString()) < DateTime.Now)
            {
                Kaishi.Disabled = true;
                Kaishi.Style.Value = "background-color:#EEE0E5";
            }
            Kaishi.Value = tuangouProduct.Start_time.ToString();
            Jieshu.Value = tuangouProduct.End_time.ToString();
            hidChange.Value = tid.ToString();
            inpTime.Value = (Convert.ToDateTime(tuangouProduct.Deadline) == DateTime.MinValue) ? "" : tuangouProduct.Deadline.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            go_tuanEntity tuangouProduct = new go_tuanEntity();
            //添加模式
            if (tid <= 0)
            {
                int Sorts = 0;
                if (tdSort.Value != "")
                {
                    Sorts = int.Parse(tdSort.Value);
                }

                tuangouProduct = new go_tuanEntity()
                {
                    Productid = Convert.ToInt32(ddlProducts.SelectedValue),
                    Per_price = Convert.ToDecimal(Danjia.Value),
                    Total_num=Convert.ToInt32(Renshu.Value),
                    Max_sell=Convert.ToInt32(Zuidashu.Value),
                    Reachtuan_num = Convert.ToInt32(reachtuan_num.Value),
                    Prize_num = Convert.ToInt32(prize_num.Value),
                    Start_time = Convert.ToDateTime(Kaishi.Value),
                    End_time = Convert.ToDateTime(Jieshu.Value),
                    Time=DateTime.Now,
                    Title = title.Value,
                    Sort= Sorts,
                    Is_delete = 0,
                    Deadline = (inpTime.Value.Trim()=="") ? DateTime.MinValue : Convert.ToDateTime(inpTime.Value)
                };
                go_tuanBusiness.SaveEntity(tuangouProduct, true);

                base.addAdminLog(operationAction.Add, null, "添加了团购商品" + title.Value);
            }
            else//编辑模式
            {
                    tuangouProduct = go_tuanBusiness.LoadEntity(tid);
                    tuangouProduct.Per_price = Convert.ToDecimal(Danjia.Value);
                    tuangouProduct.Total_num=Convert.ToInt32(Renshu.Value);
                    tuangouProduct.Max_sell=Convert.ToInt32(Zuidashu.Value);
                    tuangouProduct.Reachtuan_num = Convert.ToInt32(reachtuan_num.Value);//开奖规则
                    tuangouProduct.Prize_num = Convert.ToInt32(prize_num.Value);//开奖规则
                    tuangouProduct.Start_time = Convert.ToDateTime(Kaishi.Value);
                    tuangouProduct.End_time = Convert.ToDateTime(Jieshu.Value);
                    tuangouProduct.Title = title.Value;
                    tuangouProduct.Sort = int.Parse(tdSort.Value);
                    go_tuanBusiness.SaveEntity(tuangouProduct, false);
                    //go_tuanBusiness.UpdateEntity(tuangouProduct);
            }
           
                //新增or修改成功后跳转到列表页面
           Response.Redirect("tuangouList.aspx");
        }

         /// <summary>
        /// 绑定商品品牌下拉框
        /// </summary>
        private void BindBrand()
        {
            ddBrand.Items.Clear();
            if (ddlCategory.SelectedValue != "0")
            {
                DataTable productRows = go_brandBusiness.GetListData(" cateid = " + ddlCategory.SelectedValue, "id,name");
                ddBrand.DataSource = productRows;
                ddBrand.DataTextField = "name";
                ddBrand.DataValueField = "id";
                ddBrand.DataBind();
            }
            ddBrand.Items.Insert(0, new ListItem("请选择品牌", "0"));
        }
        /// <summary>
        /// 绑定商品下拉框
        /// </summary>
        private void BindProducts()
        {
            ddlProducts.Items.Clear();
            if (ddlCategory.SelectedValue != "0")
            {
                string where = " categoryId = " + ddlCategory.SelectedValue;
                if (ddBrand.SelectedValue != "0")
                {
                    where += string.Format(" And brandid={0}", ddBrand.SelectedValue);
                }
                DataTable productRows = go_productsBusiness.GetListData(where, "productId,title");
                ddlProducts.DataSource = productRows;
                ddlProducts.DataTextField = "title";
                ddlProducts.DataValueField = "productId";
                ddlProducts.DataBind();
            }
            ddlProducts.Items.Insert(0, new ListItem("请选择商品", "0"));
        }
        //商品分类下拉框事件
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBrand();
            BindProducts();
        }

        //品牌下拉框事件
        protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProducts();
        }
        /// <summary>
        /// 商品下拉框选中事件
        /// </summary>
        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProducts.SelectedValue == "0")
            {
                title.Value = "";
            }
            else
            {
                go_productsEntity product = go_productsBusiness.LoadEntity(Convert.ToInt32(ddlProducts.SelectedValue));
                title.Value = product.Title;
            }
        }
    }
}