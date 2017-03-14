using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YH.Utility;

namespace RedBookPlatform.operative
{
    public partial class newUserRecharge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadData(17);
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData(int id)
        {
            go_activityEntity entity = go_activityBusiness.LoadEntity(id);
            this.labTitle.Text = entity.Title.ToString();
            this.labAmount.Text = entity.Amount.ToString();
            this.labCount.Text = entity.Count.ToString();
            this.labRedpackday.Text = entity.Redpackday.ToString();
            this.labUsercodeinfo.Text = entity.Usercodeinfo.ToString();
            this.labDatatime.Text = entity.Starttime.ToString()+" — "+entity.Endtime.ToString();
            JArray code = JsonConvert.DeserializeObject<JArray>(entity.Code_config_ids.ToString());
            for (int i = 1; i <= code.Count; i++)
            {
                string codetitle = code[i - 1]["title"].ToString();
                ((Label)(this.FindControl("labred" + i.ToString()))).Text = codetitle;
               
            }
        }

    }
        
}