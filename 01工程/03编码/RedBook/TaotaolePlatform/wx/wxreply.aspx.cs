using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;

namespace RedBookPlatform.wx
{
    public partial class wxreply : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindPage();
            }
        }

        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //调用通用分页方法绑定
            DataTable wxreplies = go_wxreplyBusiness.GetListData();
            this.rptWxReply.DataSource = wxreplies;
            this.rptWxReply.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string goDelete(int replyid)
        {
            go_wxreplyBusiness.Del(replyid);
            return "删除成功";
        }
    }
}