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

namespace RedBookPlatform.productClassify
{
    public partial class addCategory :Base
    {

        private int cateid;
        private string todayString = "/" + DateTime.Today.ToString("yyyyMMdd") + "/";


        protected void Page_Load(object sender, EventArgs e)
        {
            cateid = Convert.ToInt32(Request.QueryString["cateid"]);
            hidCateid.Value = cateid.ToString();
            if (!IsPostBack)
            {
                if (cateid > 0)
                {
                    load();
                }
                else
                {
                    string action = Request["action"];
                    switch (action)
                    {
                        case "addspfl":        //添加商品绑定信息
                            addspfl();
                            break;
                        case "addwzfl":         //添加文章绑定信息
                            addwzfl();
                            break;
                    }
                }
            }

        }
        private void addspfl()
        {
            drmodel.SelectedIndex = 1;
            drmodel.Enabled = false;
            bindfenlei(drmodel.SelectedIndex);
        }
        private void addwzfl()
        {
            drmodel.SelectedIndex = 2;
            drmodel.Enabled = false;
            bindfenlei(drmodel.SelectedIndex);
        }
        private void bindfenlei(int model)
        {
            DataTable dt = go_categoryBusiness.GetListData("model='"+model+"'", "name,cateid", null, 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            sjfl.DataSource = dt;
            sjfl.DataValueField = "cateid";
            sjfl.DataTextField = "name";
            sjfl.DataBind();
            sjfl.Items.Insert(0, new ListItem("不属于任何分类", "0"));
        }
        protected void load()
        {
            //禁用分类类型
            drmodel.Enabled = false;
            this.trClassify.Style.Add("display", "");
            //修改  数据绑定
            tdCateid.Disabled = true;
            go_categoryEntity classify = go_categoryBusiness.LoadEntity(cateid);
            drmodel.SelectedIndex = Convert.ToInt32(classify.Model);
            tdCateid.Value = classify.Cateid.ToString();
            tdName.Value = classify.Name;
            imPictureUrl.ImageUrl =  classify.Pic_url == "" ? "0" : Globals.UPLOAD_PATH +classify.Pic_url.Split('|')[0];
            imPictureUrltwo.ImageUrl = classify.Pic_url == "" ? "0" : Globals.UPLOAD_PATH +classify.Pic_url.Split('|')[1];
            inpUrl.Value = classify.Url;
            inpInfo.Value = classify.Info;
            inpOrder.Value = classify.Orders.ToString();
            //获取分类下拉框列表
            bindfenlei(Convert.ToInt32(classify.Model));
            sjfl.SelectedIndex = Convert.ToInt32(classify.Parentid);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            string pictureUrl = imPictureUrl.ImageUrl;
            string pictureUrltwo = imPictureUrltwo.ImageUrl;

    
            //保存商品分类信息
            if (drmodel.SelectedValue == "1")
            {
                if (!string.IsNullOrEmpty(thumbUpload.FileName) && thumbUpload.FileContent.Length > 0 && thumbUpload.FileName.IndexOf("goodsthumb.jpg") < 0)
                {
                    string fileName = "category" + "_" + DateTime.Now.ToString("hhmmss.fff") + Path.GetExtension(thumbUpload.FileName);
                    string savePath = Server.MapPath("/Resources/uploads/shopimg" + todayString) + fileName;
                    if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                    }
                    thumbUpload.SaveAs(savePath);
                    pictureUrl = ("shopimg" + todayString + fileName);
                }

                else
                {
                    pictureUrl = pictureUrl.Substring(pictureUrl.IndexOf("shopimg"));
                }
                if (!string.IsNullOrEmpty(thumbUploadtwo.FileName) && thumbUploadtwo.FileContent.Length > 0 && thumbUploadtwo.FileName.IndexOf("goodsthumb.jpg") < 0)
                {
                    string fileNametwo = "category" + "_" + DateTime.Now.ToString("hhmmss.fff") + "2" + Path.GetExtension(thumbUploadtwo.FileName);
                    string savePathtwo = Server.MapPath("/Resources/uploads/shopimg" + todayString) + fileNametwo;

                    if (!Directory.Exists(Path.GetDirectoryName(savePathtwo)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(savePathtwo));
                    }
                    thumbUploadtwo.SaveAs(savePathtwo);
                    pictureUrltwo = ("shopimg" + todayString + fileNametwo);
                }
                else
                {
                    pictureUrltwo = pictureUrltwo.Substring(pictureUrltwo.IndexOf("shopimg"));
                }
            }
            //保存背景图
            go_categoryEntity classify = new go_categoryEntity();
            //添加模式
            if (cateid== 0)
            {
                if (drmodel.SelectedValue == "1")
                {
                    classify = new go_categoryEntity()
                    {
                        Name = tdName.Value.Trim(),
                        Pic_url = pictureUrl + "|" + pictureUrltwo,
                        Model = Convert.ToInt32(drmodel.SelectedValue),
                        Parentid=sjfl.SelectedIndex,
                        Url = inpUrl.Value,
                        Info = inpInfo.Value,
                        Orders = int.Parse(inpOrder.Value),
                        Channel=0,
                    };
                }
                if (drmodel.SelectedValue == "2")
                {
                    classify = new go_categoryEntity()
                    {
                        Name = tdName.Value.Trim(),
                        Pic_url = "",
                        Model = Convert.ToInt32(drmodel.SelectedValue),
                        Parentid = sjfl.SelectedIndex,
                        Url = inpUrl.Value,
                        Info = inpInfo.Value,
                        Orders = int.Parse(inpOrder.Value),
                    };
                }

            }
            else
            {
                classify = go_categoryBusiness.LoadEntity(cateid);
                if (drmodel.SelectedValue == "1")
                {
                    classify.Name = tdName.Value.Trim();
                    classify.Pic_url = pictureUrl + "|" + pictureUrltwo;
                    classify.Model = Convert.ToInt32(drmodel.SelectedValue);
                    classify.Parentid = sjfl.SelectedIndex;
                    classify.Url = inpUrl.Value;
                    classify.Info = inpInfo.Value;
                    classify.Orders = int.Parse(inpOrder.Value);
                }
                if (drmodel.SelectedValue == "2")
                {
                    classify.Name = tdName.Value.Trim();
                    classify.Pic_url = "";
                    classify.Model = Convert.ToInt32(drmodel.SelectedValue);
                    classify.Parentid = sjfl.SelectedIndex;
                    classify.Url = inpUrl.Value;
                    classify.Info = inpInfo.Value;
                    classify.Orders = int.Parse(inpOrder.Value);
                }

            }
            //判断输入同一类型的分类名是否重复
            if (cateid == 0)
            {
                DataTable classInfo = go_categoryBusiness.GetListData("Model=" + classify.Model.ToString() + "and Name='" + classify.Name + "'", "Model,Name", null, 0);
                if (classInfo.Rows.Count > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('该类型中已存在此名称')</script>", false);
                    return;
                }
            }
            //保存数据
            if (go_categoryBusiness.SaveEntity(classify, cateid <= 0 ? true : false))
            {

                if (cateid == 0)
                {
                    base.addAdminLog(operationAction.Add, null, "增加了分类" + tdName.Value);
                }
                else
                {
                    base.addAdminLog(operationAction.Update, null, "修改了分类" + tdName.Value);
                }


                if (drmodel.SelectedValue == "2")
                {
                    Response.Redirect("CategoryList.aspx?action=wzlist");
                }
                else
                {
                    Response.Redirect("CategoryList.aspx");
                }
            }
            else
            {

            }

        }
    }
}