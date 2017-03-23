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

namespace RedBookPlatform.RedBook
{
    public partial class AddBook : Base
    {
        private string id;
        public string show;
        private string todayString = "/" + DateTime.Today.ToString("yyyyMMdd") + "/";


        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];
            if (!this.IsPostBack)
            {
                init();
            }
        }

        protected void init()
        {
            DataTable dtcategory = RB_Book_CategoryBusiness.GetListData("1=1", "CategoryName,Id");
            ddlCategory.DataSource = dtcategory;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "Id";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("请选择", "0"));
            Ordersn.Value = "99999";
            title.Value = "";
            keywords.Value = "";
            description.Value = "";
            contents.Value = "";
            thumbImg.ImageUrl = Globals.UPLOAD_PATH + "article/goodsthumb.jpg";

            if (!string.IsNullOrEmpty(id))
            {
                load();
                show = "编辑导购文章";
            }
            else
            {
                show = "添加导购文章";
            }
        }




        protected void load()
        {
            RB_BookEntity bookEntity = RB_BookBusiness.LoadEntity(new Guid(id));
            Ordersn.Value = bookEntity.SortBaseNum.ToString();
            title.Value = bookEntity.MainTitle;
            subTitle.Value = bookEntity.SubTitle;
            keywords.Value = bookEntity.Keyword;
            description.Value = bookEntity.Description;
            DataTable dtBookContent = RB_Book_ContentBusiness.GetListData("BusinessType = 'book' and BusinessId ='" + bookEntity.Id + "'", "[Content]");

            contents.Value = dtBookContent.Rows[0]["Content"].ToString();
            thumbImg.ImageUrl = Globals.UPLOAD_PATH + bookEntity.BackImgUrl;
            ddlCategory.SelectedValue = bookEntity.CategoryId.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //保存背景图
            string thumbUrl = thumbImg.ImageUrl;
            if (!string.IsNullOrEmpty(thumbUpload.FileName) && thumbUpload.FileContent.Length > 0 && thumbUpload.FileName.IndexOf("goodsthumb.jpg") < 0)
            {
                string fileName = "bookBackImg" + "_" + DateTime.Now.ToString("hhmmss") + Path.GetExtension(thumbUpload.FileName);
                string fileNames = "bookBackImg" + "_" + DateTime.Now.ToString("hhmmss") + Path.GetExtension(thumbUpload.FileName);
                string savePath = Server.MapPath("/Resources/uploads/book" + todayString) + fileName;
                string cutPath = Server.MapPath("/Resources/uploads/book" + todayString) + fileNames;
                if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                }
                thumbUpload.SaveAs(savePath);
                //ImageHelper.BuildMin(savePath, cutPath, 200, 200);
                thumbUrl = ("book" + todayString + fileNames);
            }

            else
            {
                thumbUrl = thumbUrl.Substring(thumbUrl.IndexOf("book"));
            }

            RB_BookEntity bookEntity = new RB_BookEntity();
            RB_Book_ContentEntity bookContentEntity = new RB_Book_ContentEntity();

            Guid newGuid = Guid.NewGuid();
            //添加模式
            if (string.IsNullOrEmpty(id))
            {
                //book主体
                bookEntity = new RB_BookEntity()
                {
                    Id= newGuid,
                    SortBaseNum = Convert.ToInt32(Ordersn.Value.Trim()),
                    MainTitle = title.Value.Trim(),
                    SubTitle = subTitle.Value.Trim(),
                    Keyword = keywords.Value.Trim(),
                    Description = description.Value.Trim(),
                    BackImgUrl = thumbUrl,
                    //Contents = contents.Value,
                    AddTime = DateTime.Now,
                    Status = 0,
                    FabulousCount = 0,
                    FavoriteCount = 0,
                    WatchCount = 0,
                    CategoryId = new Guid(ddlCategory.SelectedValue),
                };
                //bookContent
                bookContentEntity = new RB_Book_ContentEntity()
                {
                    Id = Guid.NewGuid(),
                    BusinessType="book",
                    BusinessId = newGuid,
                    Content = contents.Value
                };
            }
            else
            {
                //book主体
                bookEntity = RB_BookBusiness.LoadEntity(new Guid(id));
                bookEntity.MainTitle = title.Value.Trim();
                bookEntity.SubTitle = subTitle.Value.Trim();
                bookEntity.Keyword = keywords.Value.Trim();
                bookEntity.Description = description.Value.Trim();
                bookEntity.BackImgUrl = thumbUrl;
                bookEntity.AddTime = DateTime.Now;
                bookEntity.SortBaseNum = Convert.ToInt32(Ordersn.Value.Trim());
                bookEntity.CategoryId =new Guid(ddlCategory.SelectedValue);
                //bookContent
                bookContentEntity.BusinessId = new Guid(id);
                bookContentEntity.BusinessType = "book";
                bookContentEntity.Id = Guid.NewGuid();
                bookContentEntity.Content = contents.Value;
            }


            if (RB_BookBusiness.SaveEntity(bookEntity,string.IsNullOrEmpty(id) ? true : false) && RB_Book_ContentBusiness.SaveEntity(bookContentEntity, string.IsNullOrEmpty(id) ? true : false))
            {
                if (string.IsNullOrEmpty(id))
                {
                    base.addAdminLog(operationAction.Add, null, "增加了导购文章" + bookEntity.MainTitle);
                }
                else
                {
                    base.addAdminLog(operationAction.Update, null, "修改了导购文章" + bookEntity.MainTitle);
                }
                Response.Redirect("BookList.aspx");
            }
            else
            {

            }

        }


    }
}