using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;

namespace RedBookPlatform.productClassify
{   

    public partial class addBrand :Base
    {
        private int brandsId;
        protected void Page_Load(object sender, EventArgs e)
        {
            brandsId = Convert.ToInt32(Request.QueryString["brandId"]);
            if (!this.IsPostBack)
            {
                init();
            }
        }

        protected void init()
        {
            //载入分类下拉框
            DataTable categoryRows = go_categoryBusiness.GetListData("model = 1", "name,cateid");
            drpBrandClass.DataSource = categoryRows;
            drpBrandClass.DataTextField = "name";
            drpBrandClass.DataValueField = "cateid";
            drpBrandClass.DataBind();
            drpBrandClass.Items.Insert(0, new ListItem("请选择", "0"));
            if (brandsId > 0)
            {
                load();
            }
        }
        protected void load()
        {
            this.trBrand.Style.Add("display", "");
            brandId.Disabled = true;
            go_brandEntity brandInfo = go_brandBusiness.LoadEntity(brandsId);
            drpBrandClass.Items.FindByValue(brandInfo.Cateid.ToString()).Selected = true;
            brandId.Value = brandInfo.Id.ToString();
            brandName.Value = brandInfo.Name;
            inpOrder.Value = brandInfo.Order.ToString();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            go_brandEntity brandInfo = new go_brandEntity();
            //添加模式
            if (brandsId<= 0)
            {
                brandInfo = new go_brandEntity()
                {   
                    Order=int.Parse(inpOrder.Value.Trim()),
                    Name = brandName.Value.Trim(),
                    Cateid = Convert.ToInt32(drpBrandClass.SelectedValue),
                };
            }
            else
            {
                brandInfo = go_brandBusiness.LoadEntity(brandsId);
                brandInfo.Name = brandName.Value.Trim();
                brandInfo.Order = int.Parse(inpOrder.Value.Trim());
                brandInfo.Cateid = Convert.ToInt32(drpBrandClass.SelectedValue);
            }
            //判断输入同一类型的分类名是否重复
            DataTable classInfo = go_brandBusiness.GetListData("Name='" + brandInfo.Name + "' and  cateid=" + brandInfo.Cateid + "", "Name,cateid", null, 0);
            if (classInfo.Rows.Count >= 1)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('该分类下已存在此名称')</script>", false);
                return;
            }
            //保存数据
            if (go_brandBusiness.SaveEntity(brandInfo, brandsId <= 0 ? true : false))
            {
                if (brandsId <= 0)
                {
                    base.addAdminLog(operationAction.Add, null, "增加品牌" + brandName.Value.Trim());
                }
                else 
                {
                    base.addAdminLog(operationAction.Update, null, "修改品牌" + brandName.Value.Trim());
                }


                Response.Redirect("brandList.aspx");
            }
            else
            {

            }

        }
    }
}