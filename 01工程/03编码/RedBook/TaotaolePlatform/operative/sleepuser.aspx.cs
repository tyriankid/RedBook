using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Taotaole.Bll;
using Taotaole.Model;
using System.Data;

namespace RedBookPlatform.operative
{
    public partial class sleepuser : System.Web.UI.Page
    {
        private int aid = 21;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadData();
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {

            go_activityEntity entity = go_activityBusiness.LoadEntity(aid);
            LoadDropDownList(entity.Code_config_ids.ToString());
            this.tboxRedpackday.Text = entity.Redpackday.ToString();
            this.tboxTitle.Text = entity.Title.ToString();
            this.tboxUsercodeinfo.Text = entity.Usercodeinfo.ToString();
        }
        private void LoadDropDownList(string codelist)
        {
            DataTable redlist = go_code_configBusiness.GetListData(null, "id,title");
            ViewState["redlist"] = redlist;
            ListItem firstItem = new ListItem();
            firstItem.Value = "0";
            firstItem.Text = "--请选择--";
            JArray code = JsonConvert.DeserializeObject<JArray>(codelist);
            for (int i = 1; i <= 2; i++)
            {
                string codeid = "0";
                ((DropDownList)(this.FindControl("DropDownList" + i.ToString()))).DataSource = redlist;//设置数据源
                ((DropDownList)(this.FindControl("DropDownList" + i.ToString()))).DataTextField = "title";//设置所要读取的数据表里的列名
                ((DropDownList)(this.FindControl("DropDownList" + i.ToString()))).DataValueField = "id";

                ((DropDownList)(this.FindControl("DropDownList" + i.ToString()))).DataBind();
                if (code.Count >= i - 1)
                {
                    codeid = code[i - 1]["id"].ToString();
                }
                ((DropDownList)(this.FindControl("DropDownList" + i.ToString()))).SelectedValue = codeid;
                ((DropDownList)(this.FindControl("DropDownList" + i.ToString()))).Items.Insert(0, firstItem);
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable redlist = ViewState["redlist"] as DataTable;
            DataTable dtSave = redlist.Clone();
            dtSave.PrimaryKey = null;
            for (int i = 1; i <= 2; i++)
            {
                DataRow drCurr = redlist.Rows.Find(((DropDownList)(this.FindControl("DropDownList" + i.ToString()))).SelectedValue);
                dtSave.Rows.Add((drCurr != null) ? drCurr.ItemArray : new object[] { "0", "" });
            }
            go_activityEntity entity = go_activityBusiness.LoadEntity(aid);
            entity.Title = this.tboxTitle.Text.Trim();
            entity.Redpackday = string.IsNullOrEmpty(this.tboxRedpackday.Text.Trim()) ? 0 : int.Parse(this.tboxRedpackday.Text.Trim());
            string saveJson = JsonConvert.SerializeObject(dtSave, Newtonsoft.Json.Formatting.None);
            entity.Code_config_ids = saveJson;
            entity.Timespans = "";
            entity.Use_range = "";
            entity.Code_ids_level = "";
            entity.Usercodeinfo = this.tboxUsercodeinfo.Text.Trim();
            go_activityBusiness.SaveEntity(entity, false);
        }
    }
}