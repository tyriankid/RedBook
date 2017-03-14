using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;

namespace TaotaolePlatform.products
{
    public partial class GiftUse : Base
    {   

        public int giftId;
        protected void Page_Load(object sender, EventArgs e)
        {
            giftId =Convert.ToInt32(Request.QueryString["giftId"]);
            if (!IsPostBack)
            {
                load();
            }
           
        }

       protected void load()
       {
            go_giftsEntity  giftUse=go_giftsBusiness.LoadEntity(giftId);
            if(giftUse.Giftsuse!=null)
            {    
                string[] giftUses=giftUse.Giftsuse.Split(',');
                foreach (ListItem item in chk_use.Items)
                {
                    for (int i = 0; i < giftUses.Length;i++)
                    {
                        if (item.Value.Equals(giftUses.GetValue(i)))
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
       }

       protected void btn_Save_Click(object sender, EventArgs e)
       {
           go_giftsEntity giftUse = go_giftsBusiness.LoadEntity(giftId);
           giftUse.Giftsuse = null;
           foreach (ListItem item in chk_use.Items)
           {   
             if (item.Selected)
             {  
                 giftUse.Giftsuse += item.Value + ",";
             }
           }
           giftUse.Giftsuse = giftUse.Giftsuse.TrimEnd(',');
           if (go_giftsBusiness.SaveEntity(giftUse, false))
           {
               base.addAdminLog(operationAction.Update, null, "保存成功");
               Response.Redirect("gifts.aspx");
           }
              
       }
    }
}