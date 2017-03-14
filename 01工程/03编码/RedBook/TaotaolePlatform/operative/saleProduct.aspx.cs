using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;

namespace RedBookPlatform.operative
{
    public partial class saleProduct : System.Web.UI.Page
    {
        private int aid =32;
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            switch (action)
            {
                case "catelist":
                    getcatelist();
                    break;
                case "shoplist":
                    string cid = Request["cid"];
                    getshoplist(cid);
                    break;
                case "getinfo":
                    getinfo();
                    break;
            }
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

            this.tboxShopnum.Text = entity.Shopnum.ToString();
            this.tboxStarttime.Text = entity.Starttime.ToString();
            this.tboxRedpackday.Text = entity.Redpackday.ToString();
            this.tboxEndtime.Text = entity.Endtime.ToString();
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
        protected void getcatelist()
        {
            DataTable catelist = go_categoryBusiness.GetListData("model=1", "cateid,name");
            StringBuilder builder = new StringBuilder();
            string catelistJson = JsonConvert.SerializeObject(catelist, Newtonsoft.Json.Formatting.None);
            builder.Append(catelistJson);
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder);
            Response.End();
        }
        protected void getshoplist(string cid)
        {
            DataTable productslist = go_productsBusiness.GetListData("categoryid=" + cid, " productid,title");
            StringBuilder builder = new StringBuilder();
            string productsJson = JsonConvert.SerializeObject(productslist, Newtonsoft.Json.Formatting.None);
            builder.Append(productsJson);
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder);
            Response.End();
        }
        protected void getinfo()
        {
            DataTable infolist = go_activityBusiness.GetListData("id=" + aid, " use_range");
            StringBuilder builder = new StringBuilder();
            string infoJson = JsonConvert.SerializeObject(infolist, Newtonsoft.Json.Formatting.None);
            builder.Append(infoJson);
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder);
            Response.End();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable shopData = new DataTable();
            shopData.Columns.Add("id", typeof(int));
            shopData.Columns.Add("name", typeof(string));
            shopData.Columns.Add("amountmax", typeof(int));
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
            entity.Shopnum = string.IsNullOrEmpty(this.tboxShopnum.Text.Trim()) ? 0 : int.Parse(this.tboxShopnum.Text.Trim());
            entity.Starttime = DateTime.Parse(this.tboxStarttime.Text.Trim());
            entity.Endtime = DateTime.Parse(this.tboxEndtime.Text.Trim());
            entity.Redpackday = string.IsNullOrEmpty(this.tboxRedpackday.Text.Trim()) ? 0 : int.Parse(this.tboxRedpackday.Text.Trim());
            string saveJson = JsonConvert.SerializeObject(dtSave, Newtonsoft.Json.Formatting.None);
            entity.Code_config_ids = saveJson;
            entity.Timespans = "";
            string[] userange = (Request.Form["shoprange[]"]).Trim(',').Split(',');
            for (int i = 0; i < userange.Count(); i++)
            {
                var temp = Regex.Split(userange[i], "{}", RegexOptions.IgnoreCase);
                string t = Request.Form["redpackmax" + temp[0]];
                shopData.Rows.Add(new object[] { temp[0], temp[1], t });
            }
            entity.Use_range = JsonConvert.SerializeObject(shopData, Newtonsoft.Json.Formatting.None);
            entity.Code_ids_level = "";
            entity.Usercodeinfo = this.tboxUsercodeinfo.Text.Trim();
            go_activityBusiness.SaveEntity(entity, false);
        }
    }
}