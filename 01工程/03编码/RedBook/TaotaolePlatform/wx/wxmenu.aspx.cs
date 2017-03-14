using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Weixin.MP.Api;
using YH.Weixin.MP.Domain;
using YH.Weixin.MP.Domain.Menu;

namespace RedBookPlatform.wx
{
    public partial class wxmenu : System.Web.UI.Page
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
            DataTable wxmenus = go_wxmenuBusiness.GetListData();
            this.rptWxMenu.DataSource = wxmenus;
            this.rptWxMenu.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string goDelete(int menuid)
        {
            go_wxmenuBusiness.Del(menuid);
            return "删除成功";
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            //获取所有微信菜单实体类
            IList<go_wxmenuEntity> initMenus = go_wxmenuBusiness.GetInitMenus();
            YH.Weixin.MP.Domain.Menu.Menu menu = new YH.Weixin.MP.Domain.Menu.Menu();
            
            foreach (go_wxmenuEntity info in initMenus)
            {
                if (info.Chilren == null || info.Chilren.Count == 0)
                {
                    menu.menu.button.Add(this.BuildMenu(info));
                }
                else
                {
                    SubMenu item = new SubMenu
                    {
                        name = info.Name
                    };
                    foreach (go_wxmenuEntity info2 in info.Chilren)
                    {
                        item.sub_button.Add(this.BuildMenu(info2));
                    }
                    menu.menu.button.Add(item);
                }
            }
            string json = JsonConvert.SerializeObject(menu.menu);
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            if (string.IsNullOrEmpty(masterSettings.WeixinAppId) || string.IsNullOrEmpty(masterSettings.WeixinAppSecret))
            {
                base.Response.Write("<script>alert('您的服务号配置存在问题，请您先检查配置！');</script>");
            }
            else
            {
                string strs = MenuApi.CreateMenus(JsonConvert.DeserializeObject<Token>(TokenApi.GetToken(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret)).access_token, json);
                if (strs.Contains("ok"))
                {
                    //this.ShowMsg("成功的把自定义菜单保存到了微信", true);
                    base.Response.Write("<script>alert('成功的把自定义菜单保存到了微信！');</script>");
                }
                else
                {
                    base.Response.Write("<script>alert('操作失败!服务号配置信息错误或没有微信自定义菜单权限');</script>");
                    Globals.DebugLogger("服务号配置："+json);
                    Globals.DebugLogger("服务号配置信息错误：" + strs);
                }
            }
            
        }




        private SingleButton BuildMenu(go_wxmenuEntity menu)
        {
            SingleButton result;
            result = new SingleViewButton
            {
                name = menu.Name,
                url = menu.Url
            };
            return result;
        }
        //更新排序号
        [System.Web.Services.WebMethod]
        public static string UpOrder(string menuid, string sort)
        {
            string[] menuids = menuid.TrimEnd(',').Split(',');
            string[] sorts = sort.TrimEnd(',').Split(',');

            if (menuids.Count() > 0 && sorts.Count() > 0)
            {
                for (int i = 0; i < menuids.Count(); i++)
                {
                    go_wxmenuEntity wxmenu = go_wxmenuBusiness.LoadEntity(int.Parse(menuids.GetValue(i).ToString()));
                    wxmenu.Sort=sorts.GetValue(i).ToString();
                    go_wxmenuBusiness.SaveEntity(wxmenu,false);
                }
            }
            return "更新排序成功";
        }

    }
}