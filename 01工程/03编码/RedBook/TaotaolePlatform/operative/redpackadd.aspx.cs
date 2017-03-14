using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;
using YH.Utility;

namespace RedBookPlatform.operative
{
    public partial class redpackadd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string id = Request.QueryString["id"];
                string action = Request["action"];
                if (!string.IsNullOrEmpty(id) && PageValidate.IsNumberPlus(id))
                {
                    if (!string.IsNullOrEmpty(action) && action == "delete")
                    {
                        DeleteRedpack(id);
                    }
                    else
                    {
                        LoadData(id);
                    }
                }
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData(string id)
        {
            go_code_configEntity entity = go_code_configBusiness.LoadEntity(int.Parse(id));
            if (entity != null)
            {
                this.tboxAmount.Text = entity.Amount.ToString();
                this.tboxDiscount.Text = entity.Discount.ToString();
                this.tboxTitle.Text = entity.Title.ToString();
                litTitle.Text = "修改红包模板";
                ViewState["IsEdit"] = true;
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        private void DeleteRedpack(string id)
        {
           Response.ContentType = "text/json";
           StringBuilder builder = new StringBuilder();
           go_code_configBusiness.Del( Convert.ToInt32(id));
           builder.Append("{\"success\":true,\"msg\":\"删除成功！\"}");
           Response.Clear();
           Response.ContentType = "text/plain";
           Response.ContentEncoding = System.Text.Encoding.UTF8;
           Response.Write(builder.ToString());
           Response.End(); 
        }

        /// <summary>
        /// 获取用户输入的数据实体
        /// </summary>
        private go_code_configEntity SaveData(go_code_configEntity entity)
        {
            entity.Addtime = DateTime.Now;
            entity.Title = this.tboxTitle.Text.Trim();
            entity.Amount = string.IsNullOrEmpty(this.tboxAmount.Text.Trim()) ? 0 : int.Parse(this.tboxAmount.Text.Trim());
            entity.Discount = int.Parse(this.tboxDiscount.Text.Trim());
            return entity;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isAdd = (ViewState["IsEdit"] == null) ? true : false;
            go_code_configEntity entity = (isAdd) ? new go_code_configEntity() : go_code_configBusiness.LoadEntity(int.Parse(Request.QueryString["id"]));
            entity = SaveData(entity);
            go_code_configBusiness.SaveEntity(entity, isAdd);
            Response.Redirect("redpacklist.aspx");
        }
    }
}