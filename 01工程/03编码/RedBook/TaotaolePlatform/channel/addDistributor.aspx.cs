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
using YH.Utility;

namespace RedBookPlatform.channel
{
    public partial class addDistributor : Base
    {
        private int uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            ////验证页面权限
            //if (!this.IsPageRight("admin,city"))
            //{
            //    Response.Redirect("/Resources/403.html");
            //    return;
            //}
            uid = Convert.ToInt32(Request.QueryString["uid"]);
            if (!IsPostBack)
            {

                DataTable dttype = go_channel_typeBusiness.GetListData("", "*");

                ddltype.DataSource = dttype;
                ddltype.DataTextField = "typename";
                ddltype.DataValueField = "tid";
                ddltype.DataBind();
                ddltype.Items.Insert(0, new ListItem("请选择", ""));

                DataTable dtparent = go_channel_userBusiness.GetListData("parentid=0 ", "realname,uid");

                ddlparent.DataSource = dtparent;
                ddlparent.DataTextField = "realname";
                ddlparent.DataValueField = "uid";
                ddlparent.DataBind();
                ddlparent.Items.Insert(0, new ListItem("请选择", ""));

                if (uid > 0)
                {
                    load();
                    ddlparent.Enabled = false;
                }

                if (Request.QueryString["agent"] != null)
                {
                    trfws.Visible = false;
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

            rbState.SelectedValue = classify.Frozenstate.ToString();
            txtgatheringaccount.Value = classify.Gatheringaccount;

            if (classify.Settlementprice.ToString() != "")
            {
                txtsettlementprice.Value = String.Format("{0:F}", Convert.ToDecimal(classify.Settlementprice.ToString()));
            }
            ddlparent.SelectedValue = classify.Parentid.ToString();

            ddltype.SelectedValue = classify.Typeid.ToString();
            if (classify.Typeid.ToString() == "1")
            {
                trjsjg.Visible = true;
                trState.Visible = true;
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            go_channel_userEntity classify = new go_channel_userEntity();
            decimal Settlementprice = 0;
            if (txtsettlementprice.Value.Trim() != "")
            {
                Settlementprice = Convert.ToDecimal(txtsettlementprice.Value.Trim());
            }

            //添加模式
            if (uid == 0)
            {
                int count = (int)CustomsBusiness.GetScalar("go_channel_user", " username ='" + UserName.Value.Trim() + "'", "count(*)");
                if (count > 0)
                {
                    Response.Write("<script>alert('该账号已存在')</script>");
                    return;
                }


                string okpass = SecurityHelper.GetMd5To32(UserMobile.Value.Trim()).ToLower();
                if (Request.QueryString["agent"] != null)
                {
                    classify = new go_channel_userEntity();
                    classify.Username = UserName.Value.Trim();
                    classify.Userpass = okpass;
                    classify.Realname = RealName.Value.Trim();
                    classify.Contacts = Contacts.Value.Trim();
                    classify.Usermobile = UserMobile.Value.Trim();
                    classify.Useremail = UserEmail.Value.Trim();
                    classify.Rebateratio = Convert.ToDecimal(Rebateratio.Value.Trim());
                    classify.Remark = Remark.Value.Trim();
                    classify.Parentid = this.CurrLoginChannelUser.Uid;

                    classify.Addtime = DateTime.Now;
                    classify.Gatheringaccount = txtgatheringaccount.Value.Trim();
                    classify.Typeid = int.Parse(ddltype.SelectedValue);
                    classify.Settlementprice = Settlementprice;
                    classify.Frozenstate = int.Parse(rbState.SelectedValue);
                    if (classify.Frozenstate == 2)
                        classify.Remark = DateTime.Now.ToString();
                }
                else
                {
                    classify = new go_channel_userEntity();

                    classify.Username = UserName.Value.Trim();
                    classify.Userpass = okpass;
                    classify.Realname = RealName.Value.Trim();
                    classify.Contacts = Contacts.Value.Trim();
                    classify.Usermobile = UserMobile.Value.Trim();
                    classify.Useremail = UserEmail.Value.Trim();
                    classify.Rebateratio = Convert.ToDecimal(Rebateratio.Value.Trim());
                    classify.Remark = Remark.Value.Trim();
                    classify.Parentid = int.Parse(ddlparent.SelectedValue);

                    classify.Addtime = DateTime.Now;
                    classify.Gatheringaccount = txtgatheringaccount.Value.Trim();
                    classify.Typeid = int.Parse(ddltype.SelectedValue);
                    classify.Settlementprice = Settlementprice;
                    classify.Frozenstate = int.Parse(rbState.SelectedValue);
                    if (classify.Frozenstate == 2)
                        classify.Remark = DateTime.Now.ToString();


                }

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
                classify.Gatheringaccount = txtgatheringaccount.Value.Trim();
                classify.Parentid = int.Parse(ddlparent.SelectedValue);
                classify.Settlementprice = Settlementprice;
                classify.Typeid = int.Parse(ddltype.SelectedValue);
                classify.Frozenstate = int.Parse(rbState.SelectedValue);
                if (classify.Frozenstate == 2)
                    classify.Remark = DateTime.Now.ToString();

            }
            if (go_channel_userBusiness.SaveEntity(classify, uid <= 0 ? true : false))
            {

                if (uid == 0)
                {
                    base.addAdminLog(operationAction.Add, null, "添加渠道商" + RealName.Value.Trim());
                }
                else
                {
                    base.addAdminLog(operationAction.Update, null, "修改渠道商" + RealName.Value.Trim());
                }

                if (Request.QueryString["agent"] != null)
                {
                    Response.Redirect("distributorList.aspx?agent=true");
                }
                else
                {
                    Response.Redirect("distributorList.aspx");
                }


            }


        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddltype.SelectedValue == "1")
            {

                trjsjg.Visible = true;
                trState.Visible = true;
            }
            else
            {
                trjsjg.Visible = false;
                trState.Visible = false;
            }
        }
    }
}