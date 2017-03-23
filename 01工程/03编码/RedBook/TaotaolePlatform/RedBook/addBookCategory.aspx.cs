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

namespace RedBookPlatform.RedBook
{
    public partial class addBookCategory :Base
    {

        private string Id="";
        private string todayString = "/" + DateTime.Today.ToString("yyyyMMdd") + "/";


        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Request.QueryString["Id"];
            hidCateid.Value = Id.ToNullString();
            if (!IsPostBack)
            {
                if (Id != null)
                {
                    load();
                }
            }

        }


        protected void load()
        {
            this.trClassify.Style.Add("display", "");
            //修改  数据绑定
            tdCateid.Disabled = true;
            RB_Book_CategoryEntity classify = RB_Book_CategoryBusiness.LoadEntity(new Guid(Id));
            tdCateid.Value = classify.Id.ToString();
            tdName.Value = classify.CategoryName;
            inpInfo.Value = classify.Description;
            inpOrder.Value = classify.SortBaseNum.ToString();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            RB_Book_CategoryEntity bookCategory = new RB_Book_CategoryEntity();
            //添加模式
            if (string.IsNullOrEmpty(Id))
            {
                bookCategory = new RB_Book_CategoryEntity()
                {
                    Id = Guid.NewGuid(),
                    CategoryName = tdName.Value.Trim(),
                    Description = inpInfo.Value,
                    SortBaseNum = int.Parse(inpOrder.Value),
                };
            }
            else
            {
                bookCategory = RB_Book_CategoryBusiness.LoadEntity(new Guid(Id));
                bookCategory.CategoryName = tdName.Value.Trim();
                bookCategory.Description = inpInfo.Value;
                bookCategory.SortBaseNum = int.Parse(inpOrder.Value);
            }
            //判断输入同一类型的分类名是否重复
            if (string.IsNullOrEmpty(Id))
            {
                DataTable classInfo = RB_Book_CategoryBusiness.GetListData(" CategoryName='" + bookCategory.CategoryName + "'", "CategoryName", null, 0);
                if (classInfo.Rows.Count > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('该类型中已存在此名称')</script>", false);
                    return;
                }
            }
            //保存数据
            if (RB_Book_CategoryBusiness.SaveEntity(bookCategory, Id == null ? true : false))
            {
                if (string.IsNullOrEmpty(Id))
                {
                    base.addAdminLog(operationAction.Add, null, "增加了小红书分类" + tdName.Value);
                }
                else
                {
                    base.addAdminLog(operationAction.Update, null, "修改了小红书分类" + tdName.Value);
                }
                Response.Redirect("BookCategoryList.aspx");
            }
            else
            {

            }

        }
    }
}