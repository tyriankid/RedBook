using ASPNET.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;

namespace RedBookPlatform.products
{
    public partial class addProduct : Base
    {
        private int productid;
        private string todayString = "/" + DateTime.Today.ToString("yyyyMMdd") + "/";


        protected void Page_Load(object sender, EventArgs e)
        {
            //rdoProductType.Items[0].Selected = true;
            productid = Convert.ToInt32(Request.QueryString["productId"]);
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
            ddlCategory.Items.Insert(0,new ListItem("请选择", "0"));
            //载入品牌下拉框
            DataTable brandRows = go_brandBusiness.GetListData(null, "name,id");
            ddlBrand.DataSource = brandRows;
            ddlBrand.DataTextField = "name";
            ddlBrand.DataValueField = "id";
            ddlBrand.DataBind();
            ddlBrand.Items.Insert(0,new ListItem("请选择", "0"));
            //缩略图默认图
            thumbImg.ImageUrl = Globals.UPLOAD_PATH + "shopimg/goodsthumb.jpg";

            if (productid > 0)
            {
                load();
            }
        }

        protected void load()
        {
            go_productsEntity product = go_productsBusiness.LoadEntity(productid);
            ddlCategory.Items.FindByValue(product.Categoryid.ToString()).Selected = true;
            ddlBrand.Items.FindByValue(product.Brandid.ToString()).Selected = true;
            title.Value = product.Title;
            title2.Value = product.Title2;
            keyword.Value = product.Keywords;
            description.Value = product.Description;
            money.Value = product.Money.ToString("F2");
            thumbImg.ImageUrl = Globals.UPLOAD_PATH + product.Thumb;
            flashUpload.Value = product.Picarr;
            string temp = this.flashUpload.Value.Trim();
            content.Value = product.Contents;
            chkProductAttr.Items.FindByValue("人气").Selected = product.Renqi==1?true:false;
            chkProductAttr.Items.FindByValue("推荐").Selected = product.Pos==1?true:false;
            chkProductAttr.Items.FindByValue("爆款").Selected = product.Baokuan == 1 ? true : false;
            chkProductAttr.Items.FindByValue("特价").Selected = product.Tejia == 1 ? true : false;
            chkProductAttr.Items.FindByValue("优惠").Selected = product.Youhui == 1 ? true : false;
            chkProductAttr.Items.FindByValue("新手").Selected = product.NewHand == 1 ? true : false;
            rdoProductType.Items.FindByValue(product.Typeid.ToString()).Selected = true;
            operationtype.Items.FindByValue(product.Operation.ToString()).Selected = true;
             number.Value = product.Number;
             stock.Value = product.Stock.ToString();
        }

        private void btnUpoad_Click(object sender, System.EventArgs e)
        {
            
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //保存背景图
            string thumbUrl = thumbImg.ImageUrl;
            if (!string.IsNullOrEmpty(thumbUpload.FileName) && thumbUpload.FileContent.Length > 0 && thumbUpload.FileName.IndexOf("goodsthumb.jpg") < 0)
            {
                string fileName = "product" + "_" + DateTime.Now.ToString("hhmmss") + Path.GetExtension(thumbUpload.FileName);
                string fileNames = "products" + "_" + DateTime.Now.ToString("hhmmss") + Path.GetExtension(thumbUpload.FileName);
                string savePath = Server.MapPath("/Resources/uploads/shopimg" + todayString) + fileName;
                string cutPath = Server.MapPath("/Resources/uploads/shopimg" + todayString) + fileNames;
                if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                }
                thumbUpload.SaveAs(savePath);
                ImageHelper.BuildMin(savePath, cutPath, 200, 200);
                thumbUrl = ("shopimg" + todayString + fileNames);
            }
           
            else
            {
                thumbUrl = thumbUrl.Substring(thumbUrl.IndexOf("shopimg"));
            }

            //推荐,人气是否选中
            string selectAttrs = "";
            foreach (ListItem li in chkProductAttr.Items)
            {
                if (li.Selected) selectAttrs += li.Value + ",";
            }


            go_productsEntity product = new go_productsEntity();
            //添加模式
            if (productid <= 0)
            {
                product = new go_productsEntity()
                {
                    Categoryid = Convert.ToInt32(ddlCategory.SelectedValue),
                    Brandid = Convert.ToInt32(ddlBrand.SelectedValue),
                    Title = title.Value.Trim(),
                    Title2 = title2.Value.Trim(),
                    Keywords = keyword.Value.Trim(),
                    Description = description.Value.Trim(),
                    Money = Convert.ToDecimal(money.Value),
                    Thumb = thumbUrl,
                    Contents = content.Value,
                    Picarr = this.flashUpload.Value.Trim(),
                    Time = DateTime.Now,
                    Renqi = selectAttrs.IndexOf("人气") > -1 ? 1 : 0,
                    Pos = selectAttrs.IndexOf("推荐") > -1 ? 1 : 0,
                    Baokuan = selectAttrs.IndexOf("爆款") > -1 ? 1 : 0,
                    Tejia = selectAttrs.IndexOf("特价") > -1 ? 1 : 0,
                    Youhui = selectAttrs.IndexOf("优惠") > -1 ? 1 : 0,
                    NewHand = selectAttrs.IndexOf("新手") > -1 ? 1 : 0,
                    Typeid = Convert.ToInt32(rdoProductType.SelectedValue),
                    Operation = Convert.ToInt32(operationtype.SelectedValue),
                    Stock = int.Parse(stock.Value),
                    Number=number.Value,
                   
                };
            }
            else
            {
                product = go_productsBusiness.LoadEntity(productid);
                product.Categoryid = Convert.ToInt32(ddlCategory.SelectedValue);
                product.Brandid = Convert.ToInt32(ddlBrand.SelectedValue);
                product.Title = title.Value.Trim();
                product.Title2 = title2.Value.Trim();
                product.Keywords = keyword.Value.Trim();
                product.Description = description.Value.Trim();
                product.Money = Convert.ToDecimal(money.Value);
                product.Thumb = thumbUrl;
                product.Contents = content.Value;
                product.Picarr = this.flashUpload.Value.Trim();
                product.Stock = int.Parse(stock.Value);
                if (!string.IsNullOrEmpty(product.Picarr))
                {   
                    string[] pictear=product.Picarr.Split(',');
                    if (product.Picarr.IndexOf("shopimg") == -1)
                    {
                        product.Picarr = "";
                        for (int i = 0; i < pictear.Length; i++)
                        {
                            product.Picarr +="shopimg/" + pictear.GetValue(i) + ",";
                            
                        }
                    }
                }
                product.Renqi = selectAttrs.IndexOf("人气") > -1 ? 1 : 0;
                product.Pos = selectAttrs.IndexOf("推荐") > -1 ? 1 : 0;
                product.Baokuan = selectAttrs.IndexOf("爆款") > -1 ? 1 : 0;
                product.Tejia = selectAttrs.IndexOf("特价") > -1 ? 1 : 0;
                product.Youhui = selectAttrs.IndexOf("优惠") > -1 ? 1 : 0;
                product.NewHand  = selectAttrs.IndexOf("新手") > -1 ? 1 : 0;
                product.Typeid = Convert.ToInt32(rdoProductType.SelectedValue);
                product.Operation = Convert.ToInt32(operationtype.SelectedValue);
                product.Number = number.Value;
            }
            if (go_productsBusiness.SaveEntity(product, productid<=0?true:false))
            {
                Response.Redirect("productlist.aspx");
            }
            else
            {

            }
            
        }
    }
}