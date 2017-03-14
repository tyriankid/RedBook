using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Common;
using Taotaole.Model;
using Taotaole.Bll;
using System.Data;
using Taotaole.Business;

namespace RedBookPlatform.baidu
{
    public partial class addbaiducfglist : Base
    {
        private int cfgid=0;
        private int cfgoriginalid=0;
        go_baidu_cfgEntity cfgEntity = null;
        private DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            cfgid = Convert.ToInt32(Request.QueryString["cfgid"]);
            if (!this.IsPostBack)
            {
                if (cfgid != 0)
                {
                    cfgEntity = go_baidu_cfgBusiness.LoadEntity(cfgid);
                    cfgoriginalid = cfgEntity.Cfgoriginalid;
                    dt = CustomsBusiness.GetListData("go_yiyuan y join go_products p on y.productid=p.productid join go_category c on c.cateid=p.categoryid", " y.originalid='" + cfgEntity.Cfgoriginalid + "' and y.shengyurenshu>0", "c.cateid,c.name,y.originalid", null, 1, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
                }
                bindddl();
            }

            
        }
        protected void bindddl()
        {
           DataTable dtcateg= go_categoryBusiness.GetListData("model=1", "*", null, 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
           this.ddproducttype.DataSource = dtcateg;
           this.ddproducttype.DataTextField = "name";
           this.ddproducttype.DataValueField = "cateid";
           this.ddproducttype.DataBind();
           if (dt != null && dt.Rows.Count > 0)
           {
               this.ddproducttype.SelectedValue = dt.Rows[0]["cateid"].ToString();
           }
           ddproducttype.Items.Insert(0, new ListItem("请选择商品", "0"));

           bindproduct(Convert.ToInt32( this.ddproducttype.SelectedValue));
        }
        public void bindproduct(int id)
        {
            ddshoptitle.Items.Clear();
            DataTable dtproduct = CustomsBusiness . GetListData("go_yiyuan" , "productid in (select productid from go_products where categoryid='" + id + "') and shengyurenshu>0 and recover=0" , "originalid,title" , null , 0 , YH . Utility . DbServers . DbServerName . LatestDB);
            this.ddshoptitle.DataSource = dtproduct;
            this.ddshoptitle.DataTextField = "title";
            this.ddshoptitle.DataValueField = "originalid";
            this.ddshoptitle.DataBind();
            if (dt != null && dt.Rows.Count > 0)
            {
                this.ddshoptitle.SelectedValue = dt.Rows[0]["originalid"].ToString();
                tdOrder.Value = cfgEntity.Codequantitys;
                operationtype.SelectedValue = cfgEntity.Isdelete.ToString() == "-1" ? "1" : "0";
                txtbuytimes.Value = cfgEntity.Buytimes;
                txtcfgtype.Value = cfgEntity.Cfgtype.ToString();
            }
            ddshoptitle.Items.Insert(0, new ListItem("请选择商品", "0"));
        }

        protected void ddproducttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindproduct(Convert.ToInt32(this.ddproducttype.SelectedValue));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            go_baidu_cfgEntity cfgEntity;
            if (cfgid != 0)
            {
                cfgEntity = go_baidu_cfgBusiness.LoadEntity(cfgid);
                cfgEntity.Codequantitys = tdOrder.Value.Trim();
                cfgEntity.Cfgtype = -1;
                cfgEntity.Cfgoriginalid = Convert.ToInt32(ddshoptitle.SelectedValue);
                cfgEntity.Isdelete = Convert.ToInt32(operationtype.SelectedValue);
                cfgEntity.Buytimes = txtbuytimes.Value.Trim();
                cfgEntity.Cfgtype = int.Parse(txtcfgtype.Value);
                go_baidu_cfgBusiness.SaveEntity(cfgEntity, false);
            }
            else
            {
                cfgEntity = new go_baidu_cfgEntity
                {
                    Cfgoriginalid = Convert.ToInt32(ddshoptitle.SelectedValue),
                    Isdelete = Convert.ToInt32(operationtype.SelectedValue),
                    Cfgtype = int.Parse(txtcfgtype.Value),
                    Codequantitys = tdOrder.Value.Trim(),
                    Buytimes = txtbuytimes.Value.Trim()
                };
                go_baidu_cfgBusiness.SaveEntity(cfgEntity, true);
            }
            Response.Redirect("baiducfglist.aspx");
        }

    }
}