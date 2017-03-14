using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YH.Weixin.MP;

namespace RedBook
{
    public partial class errPage : VWeiXinOAuth
    {

        public string errMsg="出错啦！";
        protected void Page_Load(object sender, EventArgs e)
        {
            errMsg = base.Request.QueryString["errMsg"];
        }
    }
}