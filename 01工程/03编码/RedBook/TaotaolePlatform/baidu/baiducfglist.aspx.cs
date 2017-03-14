using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Common;
using Taotaole.Model;
using Taotaole.Bll;
using RedBookPlatform.Resources.InfoQuery;
using Taotaole.Business;
using System.Data;
namespace RedBookPlatform.baidu
{
    public partial class baiducfglist : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPage();
                string action = Request["action"];
                switch (action)
                {
                    case "delete":
                        deleteUser();
                        break;
                    case "ceaseuser":
                        ceasememberuser();
                        break;
                }

                int code = getcode();
                if (code != 3)
                {
                   
                }

            }
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        public void deleteUser()
        {

            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            int id = Convert.ToInt32(Request.Form["id"]);
            go_baidu_cfgBusiness.Del(id);
            builder.Append("{\"success\":true,\"msg\":\"删除成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();
        }
        /// <summary>
        /// 用户账号启用或停用
        /// </summary>
        public void ceasememberuser()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            int cfgid = Convert.ToInt32(Request.Form["cfgid"]);
            string ceas = Request.Form["ucease"];
            go_baidu_cfgEntity cfgEntity = go_baidu_cfgBusiness.LoadEntity(cfgid, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            cfgEntity.Isdelete = Convert.ToInt32(ceas == "0" ? "-1" : "0");
            go_baidu_cfgBusiness.SaveEntity(cfgEntity, false);
            builder.Append("{\"success\":true,\"msg\":\"操作成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();

        }
        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //调用通用分页方法绑定
            DataTable dt = CustomsBusiness.GetListData("go_baidu_cfg c right join go_yiyuan y on c.cfgoriginalid=y.originalid", "y.shengyurenshu>0 and c.cfgid is not null", "y.title,c.cfgid,y.originalid,c.codequantitys,c.isdelete,c.cfgtype,c.buytimes", "y.orders asc", 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            this.rptmember.DataSource = dt;
            this.rptmember.DataBind();
            
        }



    }
}