using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Model;

namespace RedBookPlatform.admin
{
    public partial class modifymenujson : Base
    {
      public static string uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            uid = Request.QueryString["uid"];
        }

        /// <summary>
        /// 获取当前角色的所有菜单
        /// </summary>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string GetZnodes()
        {

           
            string znodes = string.Empty;

           DataTable dt=  CustomsBusiness.GetListData("go_website");

           for (int i = 0; i < dt.Rows.Count; i++)
           {

               string id = dt.Rows[i]["id"].ToString();
               string pid = id.Length <= 2 ? "00" : id.Substring(0, id.Length - 2);//上级id,如果当前layout是一级菜单,则为00,否则就是去掉后两位.(向前一级)
               string name = dt.Rows[i]["name"].ToString();
          
               znodes += "{\"id\":\"" + id + "\",\"pId\":\"" + pid + "\",\"name\":\"" + name + "\",\"open\":\"true\" },";
           }
           znodes = znodes.TrimEnd(',');
            return znodes;
        }

       

        [System.Web.Services.WebMethod]
        public static string toSave(string json)
        {
            json = "["+json.Replace("}", "},").TrimEnd(',')+"]";

            go_adminEntity entity=go_adminBusiness.LoadEntity(int.Parse(uid));
            entity.Menujson = json;
            go_adminBusiness.SaveEntity(entity,false);
          
            return "保存成功";
        }



        /// <summary>
        /// 获取当前角色的所有菜单
        /// </summary>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string GetSelect()
        {
            go_adminEntity entity = go_adminBusiness.LoadEntity(int.Parse(uid));
            return entity.Menujson;
        }

    }
}