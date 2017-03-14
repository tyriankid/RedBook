using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;
using YH.Utility;

namespace RedBookPlatform.operative
{
    public partial class zeroBuy : System.Web.UI.Page
    {
        private int aid = 19;
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

            this.tboxRedpackday.Text = entity.Redpackday.ToString();
            this.tboxTitle.Text = entity.Title.ToString();
            this.ddlNextweeknum.SelectedValue = entity.Nextweeknum.ToString();
            this.tboxCount.Text = entity.Count.ToString();
            this.tboxUsercodeinfo.Text = entity.Usercodeinfo.ToString();
        }
        /*protected void btnAdd_Click(object sender, EventArgs e)
        {
            //DateTime starttime = Convert.ToDateTime(this.tboxStarttime.Text);
            //DateTime endtime = Convert.ToDateTime(this.tboxEndtime.Text);
            //if (endtime < starttime)
            //{
            //    this.labVal.Text = "结束时间小于开始时间";
            //    return;
            //}
            //else {
            //    timespans = timespans + 1;
            //    this.labVal.Text = "";
            //    TableRow row = new TableRow();
            //    TableCell cell = new TableCell();
            //    Label lab = new Label();
            //    lab.ID = "lab" + timespans.ToString();
            //    lab.Text = this.tboxStarttime.Text +"—"+ this.tboxEndtime.Text;
            //    cell.Controls.Add(lab); 
            //    row.Cells.Add(cell);
            //    HolderTable.Rows.Add(row);
            //}
        }*/
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
            DataTable productslist = CustomsBusiness.GetListData("go_products a join go_yiyuan b on a.productid=b.productid", "a.categoryid=" + cid, " DISTINCT b.productid,b.originalid,b.title", "", 0, DbServers.DbServerName.ReadHistoryDB);
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
            DataTable infolist = go_activityBusiness.GetListData("id=" + aid, " timespans,use_range");
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
            DataTable timeData = new DataTable();
            timeData.Columns.Add("starttime", typeof(string));
            timeData.Columns.Add("endtime", typeof(string));

            go_activityEntity entity = go_activityBusiness.LoadEntity(aid);
            entity.Title = this.tboxTitle.Text.Trim();
            entity.Nextweeknum = int.Parse(this.ddlNextweeknum.SelectedValue);
            entity.Redpackday = string.IsNullOrEmpty(this.tboxRedpackday.Text.Trim()) ? 0 : int.Parse(this.tboxRedpackday.Text.Trim());
            entity.Code_config_ids = "";
            entity.Code_ids_level = "";
            entity.Count = int.Parse(this.tboxCount.Text.Trim());
            string[] timespans = Request.Form["timespans[]"].Trim(',').Split(',');
            string[] userange = (Request.Form["shoprange[]"]).Trim(',').Split(',');
            for (int i = 0; i < timespans.Count(); i++)
            {
                var temp = Regex.Split(timespans[i], "—", RegexOptions.IgnoreCase);
                timeData.Rows.Add(new object[] { temp[0], temp[1] });
            }
            for (int i = 0; i < userange.Count(); i++)
            { 
                var temp = Regex.Split(userange[i], "{}", RegexOptions.IgnoreCase);
                string t = Request.Form["redpackmax" + temp[0]];
                shopData.Rows.Add(new object[] { temp[0], temp[1], t });
            }
            entity.Use_range = JsonConvert.SerializeObject(shopData, Newtonsoft.Json.Formatting.None);
            entity.Timespans = JsonConvert.SerializeObject(timeData, Newtonsoft.Json.Formatting.None);
            entity.Usercodeinfo = this.tboxUsercodeinfo.Text.Trim();
                go_activityBusiness.SaveEntity(entity,false);

        }
    }
}