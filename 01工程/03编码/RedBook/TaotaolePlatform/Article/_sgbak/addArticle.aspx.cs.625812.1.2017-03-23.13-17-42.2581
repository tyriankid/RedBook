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

namespace RedBookPlatform.Article
{
    public partial class addArticle : Base
    {
        private int id;
        public string show;
        private string todayString = "/" + DateTime.Today.ToString("yyyyMMdd") + "/";


        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.QueryString["id"]);
            if (!this.IsPostBack)
            {
                init();
            }
        }

        protected void init()
        {
            DataTable dtcategory = go_categoryBusiness.GetListData("model=2", "name,cateid");
            ddlCategory.DataSource = dtcategory;
            ddlCategory.DataTextField = "name";
            ddlCategory.DataValueField = "cateid";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("请选择", "0"));
            Ordersn.Value = "99999";
            title.Value = "";
            keywords.Value = "";
            description.Value = "";
            contents.Value = "";
            thumbImg.ImageUrl = Globals.UPLOAD_PATH + "article/goodsthumb.jpg";

            if (id > 0)
            {
                load();
                show = "编辑文章";
            }
            else
            {
                show = "添加文章";
            }
        }




        protected void load()
        {
            go_articleEntity articleEntity = go_articleBusiness.LoadEntity(id);
            Ordersn.Value = articleEntity.Ordersn.ToString();
            title.Value = articleEntity.Title;
            keywords.Value = articleEntity.Keywords;
            description.Value = articleEntity.Description;
            contents.Value = articleEntity.Contents;
            thumbImg.ImageUrl = Globals.UPLOAD_PATH + articleEntity.Thumb;
            ddlCategory.SelectedValue = articleEntity.Cateid;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //保存背景图
            string thumbUrl = thumbImg.ImageUrl;
            if (!string.IsNullOrEmpty(thumbUpload.FileName) && thumbUpload.FileContent.Length > 0 && thumbUpload.FileName.IndexOf("goodsthumb.jpg") < 0)
            {
                string fileName = "product" + "_" + DateTime.Now.ToString("hhmmss") + Path.GetExtension(thumbUpload.FileName);
                string fileNames = "products" + "_" + DateTime.Now.ToString("hhmmss") + Path.GetExtension(thumbUpload.FileName);
                string savePath = Server.MapPath("/Resources/uploads/article" + todayString) + fileName;
                string cutPath = Server.MapPath("/Resources/uploads/article" + todayString) + fileNames;
                if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                }
                thumbUpload.SaveAs(savePath);
                ImageHelper.BuildMin(savePath, cutPath, 200, 200);
                thumbUrl = ("article" + todayString + fileNames);
            }

            else
            {
                thumbUrl = thumbUrl.Substring(thumbUrl.IndexOf("article"));
            }

         



            go_articleEntity articleEntity = new go_articleEntity();
            //添加模式
            if (id <= 0)
            {
                articleEntity = new go_articleEntity()
                {
                    Ordersn = Convert.ToInt32(Ordersn.Value.Trim()),
                    Title = title.Value.Trim(),
                    Keywords = keywords.Value.Trim(),
                    Description = description.Value.Trim(),
                    Thumb = thumbUrl,
                    Contents = contents.Value,
                    Posttime = DateTime.Now,
                    Hit = 0,
                    Cateid= ddlCategory.SelectedValue
                };
            }
            else
            {
                articleEntity = go_articleBusiness.LoadEntity(id);
              
                articleEntity.Title = title.Value.Trim();
                articleEntity.Keywords = keywords.Value.Trim();
                articleEntity.Description = description.Value.Trim();
                articleEntity.Thumb = thumbUrl;
                articleEntity.Contents = contents.Value;
                articleEntity.Posttime = DateTime.Now;
                articleEntity.Ordersn = Convert.ToInt32(Ordersn.Value.Trim());
                articleEntity.Cateid = ddlCategory.SelectedValue;
            }


            if (go_articleBusiness.SaveEntity(articleEntity, id <= 0 ? true : false))
            {
                Response.Redirect("ArticleList.aspx");
            }
            else
            {

            }

        }

        /// <summary>
        /// 完整图片上传路径
        /// </summary>
        public static string G_UPLOAD_PATH = "http://" + HttpContext.Current.Request.Url.Authority + "/Resources/uploads/";
        /// <summary>
        /// 相对图片上传路径
        /// </summary>
        public static readonly string UPLOAD_PATH = "/Resources/uploads/";
        public static string IMG_PATH = "/Resources/images/";
        public static string JS_PATH = "/Resources/js/";
        public static string CSS_PATH = "/Resources/css/";
    }
}