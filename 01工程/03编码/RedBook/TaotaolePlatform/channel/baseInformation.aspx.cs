using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;

namespace RedBookPlatform.channel
{
    public partial class baseInformation : Base
    {
        private int uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            //验证页面权限
            if (!this.IsPageRight("city,channel"))
            {
                Response.Redirect("/Resources/403.html");
                return;
            }
            if (!IsPostBack)
            {
                uid = this.CurrLoginChannelUser.Uid;
                if (uid > 0)
                {
                    load();
                }
            } 
        }

        protected void load()
        {
            go_channel_userEntity classify = go_channel_userBusiness.LoadEntity(uid);
            lbUserName.Text = classify.Username;
            lbRealName.Text = classify.Realname;
            lbContacts.Text = classify.Contacts;
            lbUserMobile.Text = classify.Usermobile;
            lbUserEmail.Text = classify.Useremail;
            lbProvince.Text = classify.Provinceid + classify.Cityid;
            lbRebateratio.Text = classify.Rebateratio + "%";
            lbNowithdrawcash.Text = String.Format("{0:F}", classify.Nowithdrawcash);//保留两位小数
            lbNowithdrawcash.Style.Add("color", "red");
            lbWithdrawcash.Text = String.Format("{0:F}", classify.Withdrawcash);
            lbUserCount.Text = classify.Usercount.ToString();

        }
    }
}