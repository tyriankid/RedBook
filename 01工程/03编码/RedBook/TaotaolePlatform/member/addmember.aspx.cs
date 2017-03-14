using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;
using System.Data;
using Taotaole.Common;
using System.IO;
using System.Net;
using YH.Utility;

namespace RedBookPlatform.member
{
    public partial class addmember : System.Web.UI.Page
    {
        private int uid;
        private string todayString = "/" + DateTime.Today.ToString("yyyyMMdd") + "/";
        protected void Page_Load(object sender, EventArgs e)
        {
            uid = Convert.ToInt32(Request.QueryString["uid"]);
            if (!IsPostBack)
            {
                BindInfo();
                if (uid > 0)
                {
                    Bindmember(uid);
                }
            }


        }
        public void Bindmember(int uid)
        {

            go_memberEntity memberEntity = go_memberBusiness.LoadEntity(uid);
            Groupid.SelectedIndex = memberEntity.Groupid;
            UserName.Value = memberEntity.Username;
            Email.Value = memberEntity.Email;
            Mobile.Value = memberEntity.Mobile;
            Money.Value = (Convert.ToDouble(memberEntity.Money)).ToString("0.00");
            Jingyan.Value = memberEntity.Jingyan.ToString();
            Score.Value = memberEntity.Score.ToString();
            Qianming.Value = memberEntity.Qianming;
            Mobilecode.SelectedIndex = memberEntity.Mobilecode == null ? 1 : Convert.ToInt32(memberEntity.Mobilecode.ToString() == "1" ? "0" : "1");
            Approuse.SelectedIndex = Convert.ToInt32(memberEntity.Approuse.ToString()=="1"?"0":"1");
        }
        public void BindInfo()
        {
            DataTable dt = go_member_groupBusiness.GetListData(null, "name,groupid");
            Groupid.DataSource = dt;
            Groupid.DataTextField = "name";
            Groupid.DataValueField = "groupid";
            Groupid.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            go_memberEntity memberEntity = new go_memberEntity();
            //添加模式
            if (uid <= 0)
            {
                memberEntity = new go_memberEntity()
                {
                    Groupid =0,
                    Username=UserName.Value.Trim(),
                    Email = Email.Value.Trim(),
                    Mobile=Mobile.Value.Trim(),
                    Password = SecurityHelper.GetMd5To32(Pass.Value.Trim()),
                    Money=Convert.ToDecimal( Money.Value.Trim()),
                    Jingyan=Convert.ToInt32( Jingyan.Value.Trim()),
                    Score = Convert.ToInt32(Score.Value.Trim()),
                    Img="",
                    Qianming=Qianming .Value.Trim(),
                    Mobilecode = Mobilecode.SelectedIndex.ToString() == "0" ? "1" : "-1",
                    Approuse=Convert.ToByte(Approuse.SelectedIndex.ToString()=="0"?"1":"0"),
                    Time=DateTime.Now,
                    User_ip=NetworkHelper.GetBuyIP(),
                    Band="",
                    Headimg="",
                    Wxid="",
                    Addgroup="",
                    Emailcode="-1",
                    Passcode="",
                    Reg_key=""
                };
            }
            else
            {
                memberEntity = go_memberBusiness.LoadEntity(uid);
                memberEntity.Username = UserName.Value.Trim();
                memberEntity.Email = Email.Value.Trim();
                memberEntity.Mobile = Mobile.Value.Trim();
                memberEntity.Password = SecurityHelper.GetMd5To32(Pass.Value.Trim());
                memberEntity.Money = Convert.ToDecimal(Money.Value.Trim());
                memberEntity.Jingyan = Convert.ToInt32(Jingyan.Value.Trim());
                memberEntity.Score = Convert.ToInt32(Score.Value.Trim());
                memberEntity.Img = "";
                memberEntity.Qianming = Qianming.Value.Trim();
                memberEntity.Mobilecode = Mobilecode.SelectedIndex.ToString()=="0"?"1":"-1";
                memberEntity.Approuse = Convert.ToByte(Approuse.SelectedIndex.ToString() == "0" ? "1" : "0");
            }
            if (go_memberBusiness.SaveEntity(memberEntity, uid <= 0 ? true : false))
            {
                Response.Redirect("memberlist.aspx");
            }
            else
            { 
            }

        }
    }
}