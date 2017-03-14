using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Model;
using Taotaole.Bll;
using YH.Utility;
using System.Data;
using Taotaole.Business;

namespace RedBookPlatform.channel
{
    public partial class addChannel : Base
    {
        private int uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            //验证页面权限
            if (!this.IsPageRight("admin,city"))
            {
                Response.Redirect("/Resources/403.html");
                return;
            }
            uid = Convert.ToInt32(Request.QueryString["uid"]);
            if (!IsPostBack)
            {
                if (uid > 0)
                {
                    load();
                }
            }
        }

       
        protected void load()
        {
            this.trClassify.Style.Add("display", "");
            UserName.Disabled = true;
            go_channel_userEntity classify = go_channel_userBusiness.LoadEntity(uid);
            UserName.Value = classify.Username;
            RealName.Value = classify.Realname;
            Contacts.Value = classify.Contacts;
            UserMobile.Value = classify.Usermobile;
            UserEmail.Value = classify.Useremail;
            Rebateratio.Value = classify.Rebateratio.ToString();
            Remark.Value = classify.Remark;
            DropDownList1.Text = classify.Provinceid;
            DropDownList2.Items.Insert(0, new ListItem(classify.Cityid, ""));
            txtgatheringaccount.Value = classify.Gatheringaccount;
            if (Request.QueryString["action"] != null)
            {
                DropDownList1.Enabled = false;
                DropDownList2.Enabled=false;
            }
          
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            go_channel_userEntity classify = new go_channel_userEntity();
            //添加模式
            if (uid<= 0)
            {

              int count=(int)CustomsBusiness.GetScalar("go_channel_user", " username ='"+ UserName.Value.Trim()+"'" , "count(*)");
              if (count > 0)
              {
                  Response.Write("<script>alert('该账号已存在')</script>");
                  return;
              }
              string okpass = SecurityHelper.GetMd5To32("123456").ToLower();
              
                    classify = new go_channel_userEntity()
                    {
                        Username = UserName.Value.Trim(),
                        Userpass = okpass,
                        Realname = RealName.Value.Trim(),
                        Contacts = Contacts.Value.Trim(),
                        Usermobile = UserMobile.Value.Trim(),
                        Useremail = UserEmail.Value.Trim(),
                        Rebateratio = Convert.ToDecimal(Rebateratio.Value.Trim()),
                        Remark = Remark.Value.Trim(),
                        // Parentid = this.CurrLoginChannelUser.Uid,
                        Provinceid = Request.Form.Get("DropDownList1"),
                        Cityid = Request.Form.Get("DropDownList2"),
                        Addtime = DateTime.Now,
                        Gatheringaccount = txtgatheringaccount.Value.Trim(),
                        Parentid = 0
                    };
             
            }
            else
            {


                classify = go_channel_userBusiness.LoadEntity(uid);
                classify.Username = UserName.Value.Trim();
                classify.Realname = RealName.Value.Trim();
                classify.Contacts = Contacts.Value.Trim();
                classify.Usermobile = UserMobile.Value.Trim();
                classify.Useremail = UserEmail.Value.Trim();
                classify.Rebateratio = Convert.ToDecimal(Rebateratio.Value.Trim());
                classify.Remark = Remark.Value.Trim();
                classify.Provinceid = DropDownList1.Text;
                classify.Cityid = DropDownList2.SelectedItem.Text;
                classify.Gatheringaccount = txtgatheringaccount.Value.Trim();
                classify.Parentid =0;
               


            }
            if (go_channel_userBusiness.SaveEntity(classify, uid <= 0 ? true : false))
            {

                  //添加模式
                if (uid <= 0)
                {
                   base.addAdminLog( operationAction.Add, null, "添加服务商" + UserName.Value.Trim());
                }
                else
                {
                    base.addAdminLog(operationAction.Update, null, "修改服务商" + UserName.Value.Trim() );
                }



                Response.Redirect("channelList.aspx");
            }
           

        }
    }
}