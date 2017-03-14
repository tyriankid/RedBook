using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Common;
using YH.Weixin.MP;

namespace RedBook
{
    public partial class index_old : VWeiXinOAuth
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// 解决微信跨域请求问题，后台模拟请求再返回到前端
        /// </summary>
        private void TestGet()
        {
            string json = new WebUtils().DoGet(Globals.API_Domain + "ilerrshop?action=listinfo");
        }


    }
}