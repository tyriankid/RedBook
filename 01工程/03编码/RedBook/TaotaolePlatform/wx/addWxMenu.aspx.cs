using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;

namespace RedBookPlatform.wx
{
    public partial class addWxMenu : Base
    {

        public int menuid=0;
        public int parentid=0;

        protected void Page_Load(object sender, EventArgs e)
        {
            menuid = Convert.ToInt32(Request.QueryString["menuid"]);
            parentid = Convert.ToInt32(Request.QueryString["parentid"]);
            if (!this.IsPostBack)
            {
                if (parentid > 0)
                {
                    loadParent();
                }
                else if (menuid > 0)
                {
                    load();
                }
            }
        }
        /// <summary>
        /// 载入菜单信息
        /// </summary>
        protected void load()
        {
            go_wxmenuEntity menu = go_wxmenuBusiness.LoadEntity(menuid);
            tdMenuname.Value = menu.Name;
            tdUrl.Value = menu.Url;
        }

        /// <summary>
        /// 载入父菜单信息
        /// </summary>
        protected void loadParent()
        {
            go_wxmenuEntity parentMenu = go_wxmenuBusiness.LoadEntity(parentid);
            parentMenuName.Text = parentMenu.Name;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            go_wxmenuEntity wxmenuentity = new go_wxmenuEntity();
            if (menuid <= 0 && parentid<=0)//添加
            {
                wxmenuentity = new go_wxmenuEntity()
                {
                    Parentid = 0,
                    Name = tdMenuname.Value,
                    Type = "",
                    Url = tdUrl.Value
                };
                if (go_wxmenuBusiness.SaveEntity(wxmenuentity, true))
                {
                    base.Response.Write("<script>alert('添加成功！');</script>");
                }
                else
                {
                    base.Response.Write("<script>alert('添加失败！');</script>");
                }
                
            }
            else if (parentid > 0)//添加子菜单
            {
                wxmenuentity = new go_wxmenuEntity()
                {
                    Parentid = parentid,
                    Name = tdMenuname.Value,
                    Type = "",
                    Url = tdUrl.Value
                };
                if (go_wxmenuBusiness.SaveEntity(wxmenuentity, true))
                {
                    base.Response.Write("<script>alert('添加子菜单成功！');</script>");
                }
                else
                {
                    base.Response.Write("<script>alert('添加子菜单失败！');</script>");
                }
            }
            else if (menuid > 0)
            {
                wxmenuentity = go_wxmenuBusiness.LoadEntity(menuid);
                wxmenuentity.Name = tdMenuname.Value.Trim();
                wxmenuentity.Url = tdUrl.Value.Trim();
                if (go_wxmenuBusiness.SaveEntity(wxmenuentity, false))
                {
                    base.Response.Write("<script>alert('编辑成功！');</script>");
                }
                else
                {
                    base.Response.Write("<script>alert('编辑失败！');</script>");
                }
            }

        }
    }
}