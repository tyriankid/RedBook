using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;

namespace RedBookPlatform
{
    public partial class Base : System.Web.UI.Page
    {


        protected override void OnInit(EventArgs e)
        {
            this.CheckPageAccess();
            base.OnInit(e);


            if (HttpContext.Current.Request.Cookies.Get("TTL_Admin") != null && Globals.menujson == null)
            {

                DataTable dt = go_adminBusiness.GetListData("username='" + CurrLoginAdmin.Username.ToString() + "'");
                Globals.menujson = dt.Rows[0]["menujson"].ToString();
            }


        }



        /// <summary>
        /// 权限页面判断
        /// </summary>
        /// <param name="funcodes">功能码（admin,city,channel）</param>
        public bool IsPageRight(string funcodes)
        {
            bool b = false;
            int currLoginUserType = GetLoginUserType();
            switch (currLoginUserType)
            {
                case 1:
                    b = funcodes.IndexOf("admin") > -1 ? true : false;
                    break;
                case 2:
                    b = funcodes.IndexOf("city") > -1 ? true : false;
                    break;
                case 3:
                    b = funcodes.IndexOf("channel") > -1 ? true : false;
                    break;
            }
            return b;
        }
        /// <summary>
        /// 获取当前登录用户的身份
        /// </summary>
        /// <returns>返回：0未登录 1平台管理员 2城市服务商 3渠道推广员</returns>
        private int GetLoginUserType()
        {
            int i = 0;
            if (HttpContext.Current.Request.Cookies.Get("TTL_Admin") != null)
            {
                i = 1;
            }
            if (HttpContext.Current.Request.Cookies.Get("ChannelUser") != null)
            {
                //int.Parse(Request.Cookies["ChannelUser"].Value)
                go_channel_userEntity entity = go_channel_userBusiness.LoadEntity(CurrLoginChannelUser.Uid);
                if (entity != null)
                {
                    i = (entity.Parentid == 0) ? 2 : 3;
                }
            }
            return i;
        }

        /// <summary>
        /// 检查用户是否登录
        /// </summary>
        private void CheckPageAccess()
        {
            /*if (HttpContext.Current.Request.Url.ToString().ToLower().IndexOf(this.platformDom) > -1)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("TTL_Admin");
                if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                {
                    this.Page.Response.Redirect(Globals.ApplicationPath + "/Login.aspx", true);
                }
            }
            else {
                HttpCookie cookie2 = HttpContext.Current.Request.Cookies.Get("ChannelUser");
                if (cookie2 == null || string.IsNullOrEmpty(cookie2.Value))
                {
                    this.Page.Response.Redirect(Globals.ApplicationPath + "/SignIn.aspx", true);
                }
            }*/
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("TTL_Admin");
            HttpCookie cookie2 = HttpContext.Current.Request.Cookies.Get("ChannelUser");
            if ((cookie == null || string.IsNullOrEmpty(cookie.Value)) && (cookie2 == null || string.IsNullOrEmpty(cookie2.Value)))
            {
                this.Page.Response.Redirect(Globals.ApplicationPath + "/Login.aspx", true);
            }
        }

        public go_adminEntity CurrLoginAdmin
        {
            get
            {
                if (WebCache.Get("TTL_Admin_" + SecurityHelper.Decrypt("uid", HttpContext.Current.Request.Cookies.Get("TTL_Admin").Value)) == null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("TTL_Admin");
                    string uid = SecurityHelper.Decrypt("uid", Request.Cookies["TTL_Admin"].Value);
                    go_adminEntity entity = go_adminBusiness.LoadEntity(int.Parse(uid));
                    WebCache.Insert("TTL_Admin_" + uid, entity, 60 * 60 * 24 * 7);
                }
                return WebCache.Get("TTL_Admin_" + SecurityHelper.Decrypt("uid", HttpContext.Current.Request.Cookies.Get("TTL_Admin").Value)) as go_adminEntity;
            }
        }


        public go_channel_userEntity CurrLoginChannelUser
        {
            get
            {
                if (WebCache.Get("ChannelUser_" + SecurityHelper.Decrypt("uid", HttpContext.Current.Request.Cookies.Get("ChannelUser").Value)) == null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("ChannelUser");
                    string uid = SecurityHelper.Decrypt("uid", Request.Cookies["ChannelUser"].Value);
                    go_channel_userEntity entity = go_channel_userBusiness.LoadEntity(int.Parse(uid));
                    WebCache.Insert("ChannelUser_" + uid, entity, 60 * 60 * 24 * 7);
                }
                return WebCache.Get("ChannelUser_" + SecurityHelper.Decrypt("uid", HttpContext.Current.Request.Cookies.Get("ChannelUser").Value)) as go_channel_userEntity;
            }
        }
        protected void ReloadPage(NameValueCollection queryStrings)
        {
            base.Response.Redirect(this.GenericReloadUrl(queryStrings));
        }


        private string GenericReloadUrl(NameValueCollection queryStrings)
        {
            if ((queryStrings == null) || (queryStrings.Count == 0))
            {
                return base.Request.Url.AbsolutePath;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(base.Request.Url.AbsolutePath).Append("?");
            foreach (string str2 in queryStrings.Keys)
            {
                string str = queryStrings[str2].Trim().Replace("'", "");
                if (!string.IsNullOrEmpty(str) && (str.Length > 0))
                {
                    builder.Append(str2).Append("=").Append(base.Server.UrlEncode(str)).Append("&");
                }
            }
            queryStrings.Clear();
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        //获取当前管理员对当前页面的权限
        // //code：0查看、1维护、2全部。维护权限时不能做审核、结算等操作，只有基本的增删改。
        protected int getcode()
        {
            return Globals.getcode(this.Page.Title);
        }


        //获取当前管理员对当前页面的权限
        // //code：0查看、1维护、2全部。维护权限时不能做审核、结算等操作，只有基本的增删改。
        protected void addAdminLog(operationAction action, string before = null, string after = null)
        {
            go_adminlogsEntity entity = new go_adminlogsEntity();


            if (CurrLoginAdmin != null)
            {
                entity.Username = CurrLoginAdmin.Username;
            }
            else
            {
                entity.Username = CurrLoginChannelUser.Username;
            }

            entity.Loginip = NetworkHelper.GetRequestIp();
            entity.Actiontime = DateTime.Now;
            entity.Actionform = Page.Title;
            entity.Action = action;
            entity.Infobefore = before;
            entity.Infoafter = after;
            go_adminlogsBusiness.SaveEntity(entity, true);
        }

        /// <summary>
        /// 完整图片上传路径
        /// </summary>
        public static string G_UPLOAD_PATH = "http://" + HttpContext.Current.Request.Url.Authority + "/Resources/uploads/";
        /// <summary>
        /// 相对图片上传路径
        /// </summary>
        public static readonly string UPLOAD_PATH = "/Resources/uploads/";
        public static string IMG_PATH = "/Resources/images/";
        public static string JS_PATH = "/Resources/js/";
        public static string CSS_PATH = "/Resources/css/";
    }
}