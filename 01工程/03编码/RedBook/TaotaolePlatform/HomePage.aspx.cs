using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;

namespace RedBookPlatform
{
    public partial class HomePage : Base
    {
        public static int uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            uid = this.CurrLoginChannelUser.Uid;
            //System.Data.DataTable dtData1 = go_channel_userBusiness.GetListData();                                 //查询所有后台用户
            //System.Data.DataTable dtData2 = go_channel_userBusiness.GetListData("useremail like '%q%'");           //根据条件查询后台用户
            //System.Data.DataTable dtData3 = go_channel_userBusiness.GetListData("1=1", "uid,mid");                 //根据条件查询后台用户部分字段
            //System.Data.DataTable dtData4 = go_channel_userBusiness.GetListData("1=1", "uid,mid", "uid desc");     //根据条件查询后台用户部分字段并排序
            //System.Data.DataTable dtData5 = go_channel_userBusiness.GetListData("1=1", "uid,mid", "uid desc", 2);   //根据条件查询top后台用户部分字段并排序
            object oState = go_channel_userBusiness.GetScalar(string.Format("uid = '{0}'", uid), "frozenstate");
            if (oState != null && oState.ToString() == "1")
            {
                Response.Write("<script>alert('对不起！该账号已被冻结')</script>");
                this.Page.Response.Redirect(Globals.ApplicationPath + "/agent.aspx", true);
            }
            if (Request.QueryString["out"] != null)
            {
                if (HttpContext.Current.Request.Cookies.Get("ChannelUser")!=null)
                WebCache.Remove("ChannelUser_" + HttpContext.Current.Request.Cookies.Get("ChannelUser").Value);
                System.Web.HttpCookie cookies = new System.Web.HttpCookie("ChannelUser")
                {
                    Expires = System.DateTime.Now
                };
                System.Web.HttpContext.Current.Response.Cookies.Add(cookies);
                base.Response.Redirect("agent.aspx", true);
                if (HttpContext.Current.Request.Cookies.Get("TTL_Admin")!=null)
                WebCache.Remove("TTL_Admin_" + HttpContext.Current.Request.Cookies.Get("TTL_Admin").Value);
                System.Web.HttpCookie cookies2 = new System.Web.HttpCookie("TTL_Admin")
                {
                    Expires = System.DateTime.Now
                };
                System.Web.HttpContext.Current.Response.Cookies.Add(cookies2);
                base.Response.Redirect("agent.aspx", true);
            }
        }
    }
}