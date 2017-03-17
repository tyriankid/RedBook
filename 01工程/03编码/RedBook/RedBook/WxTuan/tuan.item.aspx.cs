using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Common;
using YH.Utility;
using YH.Weixin.MP;

namespace RedBook.WxTuan
{
    public partial class quan_item : VWeiXinOAuth
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(base.Request.QueryString["tuanid"]))
            {
                Response.Redirect("/errPage.aspx?errMsg=参数错误");
            }
        }

    }
}