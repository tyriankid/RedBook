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
    public partial class AddZhiGou :Base
    {
        public int QuanId;

        protected void Page_Load(object sender, EventArgs e)
        {
            QuanId = Convert.ToInt32(Request.QueryString["QuanId"]);
            if (!this.IsPostBack)
            {
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
            if (QuanId > 0)
            {

                load();
            }
        }

        protected void load()
        {
            go_zhigouEntity entity = go_zhigouBusiness.LoadEntity(QuanId);
            hidChange.Value = QuanId.ToString();
            title.Value = entity.Title;
            stock.Value = entity.Stock.ToString();
            zhiMoney.Value = entity.QuanMoney.ToString("0.00");
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            go_zhigouEntity quanProduct = new go_zhigouEntity();
            //添加模式
            if (QuanId <= 0)
            {   
                quanProduct = new go_zhigouEntity()

                {
                    Productid = Convert.ToInt32(ddlProducts.SelectedValue),
                    Title = title.Value,
                    Stock = int.Parse(stock.Value),
                    AddTime = DateTime.Now,
                };
                if (string.IsNullOrEmpty(zhiMoney.Value))
                {
                    zhiMoney.Value = money.Value;
                    quanProduct.QuanMoney = decimal.Parse(zhiMoney.Value);
                }
                else
                {
                    quanProduct.QuanMoney = decimal.Parse(zhiMoney.Value);
                }
                if (go_zhigouBusiness.SaveEntity(quanProduct, true))
                {
                    base.addAdminLog(operationAction.Add, null, "添加了直购商品" + title.Value);
                }

            }
            else//编辑模式
            {
                quanProduct = go_zhigouBusiness.LoadEntity(QuanId);
                quanProduct.Title = title.Value;
                quanProduct.Stock = int.Parse(stock.Value);
                quanProduct.QuanMoney = decimal.Parse(zhiMoney.Value);
                if (go_zhigouBusiness.SaveEntity(quanProduct, false))
                {
                    base.addAdminLog(operationAction.Update, null, "修改了直购商品" + title.Value);
                }
            }
            go_configsEntity ec = go_configsBusiness.LoadEntity(3);
            ec.Ctime = DateTime.Now;
            go_configsBusiness.SaveEntity(ec, false);
            //新增or修改成功后跳转到列表页面
            Response.Redirect("zhiProduct.aspx");
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
            go_productsEntity product = go_productsBusiness.LoadEntity(Convert.ToInt32(ddlProducts.SelectedValue));
            title.Value = product.Title;
            money.Value = product.Money.ToString("0.00");
        }
    }
}