using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Model;
using YH.Utility;

namespace RedBookPlatform.products
{
    public partial class addYuangou : Base
    {
        public int yid;

        protected void Page_Load(object sender, EventArgs e)
        {
            yid = Convert.ToInt32(Request.QueryString["yid"]);
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

            if (yid > 0)
            {
                
                load();
            }
        }

        protected void load()
        {
            go_yiyuanEntity yiyuanProduct = go_yiyuanBusiness.LoadEntity(yid);
            title.Value = yiyuanProduct.Title;
            zongjiage.Value = yiyuanProduct.Zongjiage.ToString("F2");
            yunjiage.Value = yiyuanProduct.Yunjiage.ToString("F2");
            zongrenshu.Value = yiyuanProduct.Zongrenshu.ToString();
            maxqishu.Value = yiyuanProduct.Maxqishu.ToString();
            pricerange.Value = yiyuanProduct.Pricerange.ToString();
            tdOrder.Value = yiyuanProduct.Orders.ToString();
            labZongJiaGe.Text = yiyuanProduct.Zongjiage.ToString("F2");
            labYunJiaGe.Text = yiyuanProduct.Yunjiage.ToString("F2");
            labZongRenShu.Text = yiyuanProduct.Zongrenshu.ToString();
            hidChange.Value = yid.ToString();
            aPrompt.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            go_yiyuanEntity yiyuanProduct = new go_yiyuanEntity();

           

            //添加模式
            if (yid <= 0)
            {
                int orders =0;
                if (tdOrder.Value != "")
                {
                    orders = int.Parse(tdOrder.Value);
                }
                if (pricerange.Value.TrimStart(',').TrimEnd(',').IndexOf(',') < 1)
                {
                    base.Response.Write("<script>alert('快速购买价格区域格式错误！');</script>");
                    return;
                }
                yiyuanProduct = new go_yiyuanEntity()
                {
                    Productid = Convert.ToInt32(ddlProducts.SelectedValue),
                    Title = title.Value,
                    Yunjiage = Convert.ToDecimal(yunjiage.Value), //云价格为整数
                    Zongjiage = Convert.ToDecimal(zongjiage.Value),
                    Zongrenshu = Convert.ToInt32(zongrenshu.Value),
                    Canyurenshu = 0,
                    Shengyurenshu = Convert.ToInt32(zongrenshu.Value),
                    Qishu = 1,
                    Maxqishu = Convert.ToInt32(maxqishu.Value),
                    Time = DateTime.Now,
                    Pricerange = pricerange.Value,
                    Orders =orders,
                    recover=0,
                };

                string max_originalid = go_yiyuanBusiness.GetScalar("qishu=1", "max(originalid)",null, DbServers.DbServerName.LatestDB).ToString();
                yiyuanProduct.originalid = (string.IsNullOrEmpty(max_originalid)?0: int.Parse(max_originalid))+1;

                yid = go_yiyuanBusiness.AddEntity(yiyuanProduct);
                if (yid > 0)
                {
                    //创建云购码
                    bool flag = go_yiyuanBusiness.CreateShopCode(Convert.ToInt32(zongrenshu.Value), yid);
                    if(flag)
                    {
                        base.Response.Write("<script>alert('添加云购商品成功！');</script>");
                    }
                    else
                    {
                        base.Response.Write("<script>alert('添加云购商品失败！');</script>");
                    }
                }

                base.addAdminLog(operationAction.Add, null, "添加了一元购商品" + title.Value);
            }
            else//编辑模式
            {
                yiyuanProduct = go_yiyuanBusiness.LoadEntity(yid);
                yiyuanProduct.Title = title.Value;
                yiyuanProduct.Maxqishu = Convert.ToInt32(maxqishu.Value);
                yiyuanProduct.Pricerange = pricerange.Value;
                yiyuanProduct.Orders =int.Parse(tdOrder.Value);
                go_yiyuanBusiness.UpdateEntity(yiyuanProduct);
                base.addAdminLog(operationAction.Update, null, "修改了一元购商品" + title.Value);
                
            }

            go_configsEntity ec = go_configsBusiness.LoadEntity(3);
            ec.Ctime = DateTime.Now;
            go_configsBusiness.SaveEntity(ec, false);
            //新增or修改成功后跳转到列表页面
            Response.Redirect("yuangouList.aspx");
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
                yunjiage.Value = "";
                zongjiage.Value = "";
                maxqishu.Value = "";
            }
            else
            {
                go_productsEntity product = go_productsBusiness.LoadEntity(Convert.ToInt32(ddlProducts.SelectedValue));
                title.Value = product.Title;
                yunjiage.Value = "1";
                zongjiage.Value = product.Money.ToString("F2");
                maxqishu.Value = "100";
            }
        }

       
    }
}