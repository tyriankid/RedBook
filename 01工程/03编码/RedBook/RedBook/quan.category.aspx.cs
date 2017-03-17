using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Common;
using YH.Weixin.MP;

namespace RedBook
{
    public partial class quan_category : VWeiXinOAuth
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void btnTest_Click(object sender, EventArgs e)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("user-location");
        //    if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
        //    {
        //        Globals.DebugLogger("获取到的地址为：" + Server.UrlDecode(cookie.Value));
        //    }
        //}
    }
}