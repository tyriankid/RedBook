using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;

namespace RedBookPlatform.Slides
{
    public partial class AddSlidesWechat : Base
    {
        private int id;
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
           
            txtslideorder.Value = "99999";
            txtslidelink.Value = "";
            thumbImg.ImageUrl = Globals.UPLOAD_PATH + "slides/goodsthumb.jpg";

            if (id > 0)
            {
                load();
            }
        }




        protected void load()
        {
            go_slidesEntity slidesEntity = go_slidesBusiness.LoadEntity(id);
            txtslideorder.Value = slidesEntity.Slideorder.ToString();
            txtslidelink.Value = slidesEntity.Slidelink;
            thumbImg.ImageUrl = Globals.UPLOAD_PATH + slidesEntity.Slideimg;
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //保存背景图
            string thumbUrl = thumbImg.ImageUrl;
            if (!string.IsNullOrEmpty(thumbUpload.FileName) && thumbUpload.FileContent.Length > 0 && thumbUpload.FileName.IndexOf("goodsthumb.jpg") < 0)
            {
                string fileName = "product" + "_" + DateTime.Now.ToString("hhmmss") + Path.GetExtension(thumbUpload.FileName);
            //    string fileNames = "products" + "_" + DateTime.Now.ToString("hhmmss") + Path.GetExtension(thumbUpload.FileName);
                string savePath = Server.MapPath("/Resources/uploads/slides" + todayString) + fileName;
             //   string cutPath = Server.MapPath("/Resources/uploads/slides" + todayString) + fileNames;
                if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                }
                thumbUpload.SaveAs(savePath);
            //    ImageHelper.BuildMin(savePath, cutPath, 200, 200);
                thumbUrl = ("slides" + todayString + fileName);
            }

            else
            {
                thumbUrl = thumbUrl.Substring(thumbUrl.IndexOf("slides"));
            }

            int Slidestate=0;  //启用
            
                if(!rbqiyong.Checked)
                {
                Slidestate=1;  //禁用
                }

            go_slidesEntity slidesEntity = new go_slidesEntity();
            //添加模式
            if (id <= 0)
            {
                slidesEntity = new go_slidesEntity()
                {
                    Slideorder =Convert.ToInt32(txtslideorder.Value.Trim()),
                    Slidelink = txtslidelink.Value.Trim(),
                    Slideimg = thumbUrl,
                    Slidetype=1,
                    Slidelastupdatetime=DateTime.Now,
                    Slidestate=Slidestate
                };
            }
            else
            {
                slidesEntity = go_slidesBusiness.LoadEntity(id);
                slidesEntity.Slideorder =Convert.ToInt32(txtslideorder.Value.Trim());
                slidesEntity.Slidelink = txtslidelink.Value.Trim();
                slidesEntity.Slideimg = thumbUrl;
                slidesEntity.Slidetype = 1;
                 slidesEntity.Slidelastupdatetime=DateTime.Now;
                 slidesEntity.Slidestate = Slidestate;
            }

           
            if (go_slidesBusiness.SaveEntity(slidesEntity, id <= 0 ? true : false))
            {
                Response.Redirect("SlidesWechat.aspx");
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