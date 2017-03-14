using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;
using YH.Weixin.MP.QrCode;
using Ionic.Zip;
using YH.Utility;

namespace RedBookPlatform.channel
{
    public partial class distributorList : Base
    {
        private int uid;
        public string allmoney;
        public string allusercount;
        public string allusermoney="0";
        public string agent;
        public string style;
        public string action;
        protected void Page_Load(object sender, EventArgs e)
        {
            uid = Convert.ToInt32(Request.QueryString["uid"]);
            this.Form.DefaultButton = "btnSearch";
            if (Request.QueryString["agent"] != null)
            {
                agent = "&agent=true";
                style = "display:none";
                action = "&action=modify";
            }
          
            if (!IsPostBack)
            {

                string action = Request["action"];
                switch (action)
                {
                    case "FrozenUid":
                        FrozenUid();
                        break;
                    case"Initpass":
                        InitPassword();
                        break;
                    case "goDelete":
                        goDelete(int.Parse(Request["id"]), int.Parse(Request["count"]), Request["name"].ToString());
                        break;
                        

                }

                
                Binddltype();
                BindPage();
            }
        }


        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void Binddltype()
        {
            DataTable dttype = go_channel_typeBusiness.GetListData("", "*");
            ViewState["dttype"] = dttype;
            ddltype.DataSource = dttype;
            ddltype.DataTextField = "typename";
            ddltype.DataValueField = "tid";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new ListItem("全部", ""));
        }

        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            StringBuilder builder;
            go_channellistQuery query = GetQuery();
            if (uid == 0)
                builder = new StringBuilder("parentid<>0");
            else
                builder = new StringBuilder("parentid=" + uid + "");
            if (Request.QueryString["agent"] != null)
                builder = new StringBuilder("parentid=" + this.CurrLoginChannelUser.Uid + "");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (realname Like '%{0}%' or contacts like '%{0}%' or usermobile like '%{0}%' or useremail like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            if (!string.IsNullOrEmpty(query.type))
            {
                builder.AppendFormat(" AND (tid = {0}) ", DataHelper.CleanSearchString(query.type));
            }


            //调用通用分页方法绑定
            string from = @"(select distinct u.uid,u.typeid,u.realname,u.cityid,u.contacts,u.usermobile,u.useremail,u.rebateratio,u.usercount,u.usercountmoney,u.parentid,u.frozenstate,k.tid,k.typename ,z.realname as fwsname  ,
(select sum(money) from go_channel_recodes where uid=u.uid) as m  from go_channel_user u  left join  go_channel_user z on z.uid=u.parentid  left join go_channel_type k on u.typeid=k.tid )T";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "uid");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();



            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
            this.ddltype.SelectedValue = query.type;

            if (Request.QueryString["SortOrder"] == "Asc")
            {
                this.ddlmoney.SelectedValue = "1";
            }


            allmoney = Taotaole.Bll.go_channel_recodesBusiness.SelectAllMoney(from, builder.ToString(),"channel");
            allusercount = Taotaole.Bll.go_channel_userBusiness.SelectAllUserCount(from, builder.ToString());
            allusermoney = Taotaole.Bll.go_channel_userBusiness.SelectAllUserMoney(from, builder.ToString());
              

                if (allmoney == "")
                {
                    allmoney = "0";
                } if (allusermoney == "")
                {
                    allusermoney = "0";
                }
                if (allusercount == "")
                {
                    allusercount = "0";
                }

        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_channellistQuery GetQuery()
        {
            go_channellistQuery query = new go_channellistQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortBy"]))
            {
                query.SortBy = this.Page.Request.QueryString["SortBy"];
            }
            else
            {
                query.SortBy = "m";
            }
            query.SortOrder = SortAction.Desc;
            if (this.Page.Request.QueryString["SortOrder"] == "Asc")
            {
                query.SortOrder = SortAction.Asc;
            }

            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["type"]))
            {
                query.type = this.Page.Request.QueryString["type"];
            }


            return query;
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="isSearch">是否查询(重置起始页码)</param>
        private void ReLoad(bool isSearch)
        {
            NameValueCollection queryStrings = new NameValueCollection();
            queryStrings.Add("keyword", this.tboxKeyword.Text.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }



            queryStrings.Add("SortBy", "m");
            if (ddlmoney.SelectedValue == "0")
            {
                queryStrings.Add("SortOrder", SortAction.Desc.ToString());
            }
            else
            {
                queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            }

            queryStrings.Add("type", ddltype.SelectedValue);
            if (Request.QueryString["agent"] != null)
            {
                queryStrings.Add("agent", "true");
            }
            if (Request.QueryString["uid"] != null)
            {
                queryStrings.Add("uid", Request.QueryString["uid"]);
            }
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }

     
        public  void goDelete(int id, int count,string name)
        {

            StringBuilder builder = new StringBuilder();

            if (count == 0)
            {
                go_channel_userBusiness.Del(id);
                base.addAdminLog(operationAction.Delete, null, "删除了渠道商" + name );
                builder.Append("{\"success\":true,\"msg\":\"删除成功！\"}");
            }
            else
            {
                builder.Append("{\"success\":true,\"msg\":\"该渠道商下面有推广人不能删除！\"}");
               
            }

            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End(); 
        }

        protected void rptAdmin_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Down":
                    string uid = e.CommandArgument.ToString();
                    go_channel_userEntity entity = go_channel_userBusiness.LoadEntity(int.Parse(uid));
                    if (entity != null)
                    {
                        string fileName = string.Format("{0}_{1}", entity.Realname, entity.Usermobile);
                        SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                        string savepath = System.Web.HttpContext.Current.Server.MapPath("~/Resources/TicketImage") + "\\" + fileName + ".jpg";
                        string savepathex = System.Web.HttpContext.Current.Server.MapPath("~/Resources/TicketImage") + "\\" + fileName + ".bmp";
                        if (!File.Exists(savepathex))
                        {
                            YH.Weixin.MP.Api.TicketAPI.GetTicketImage(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret, string.Format("channel_{0}", uid), false, fileName);
                        }
                        string qrCodeBackImgUrl = "/Resources/TicketImage/" + string.Format("channel_{0}", uid) + ".bmp";
                        DownFile(this, savepathex);
                    }
                    break;
            }
        }

        public void DownFile(System.Web.UI.Page page, string path)
        {
            try
            {
                System.IO.FileInfo myFile = new System.IO.FileInfo(path);
                page.Response.Clear();
                page.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(myFile.Name));
                page.Response.AddHeader("Content-Length", myFile.Length.ToString());
                page.Response.ContentType = "application/octet-stream";
                page.Response.TransmitFile(myFile.FullName);
                page.Response.End();
            }
            catch
            {

            }
        }
        /// <summary>
        /// 密码初始化
        /// </summary>
        public void InitPassword()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            int uid = Convert.ToInt32(Request.Form["id"]);
            string mobile = Request.Form["mobile"];
            go_channel_userEntity userEntity = go_channel_userBusiness.LoadEntity(uid);
            if (!string.IsNullOrEmpty(mobile))
                userEntity.Userpass = SecurityHelper.GetMd5To32(mobile).ToLower();
            else
                userEntity.Userpass = SecurityHelper.GetMd5To32("123456").ToLower();
            go_channel_userBusiness.SaveEntity(userEntity, false);
            base.addAdminLog(operationAction.Update, null, "渠道商" + userEntity.Realname+"密码初始化");

            builder.Append("{\"success\":true,\"msg\":\"操作成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();

        }

        /// <summary>
        /// 服务商账号冻结或解冻
        /// </summary>
        public void FrozenUid()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            int uid = Convert.ToInt32(Request.Form["uid"]);
            string ustate = Request.Form["ufrozen"];

            go_channel_userEntity userEntity = go_channel_userBusiness.LoadEntity(uid);
            userEntity.Frozenstate = Convert.ToByte(ustate == "1" ? "0" : "1");
            go_channel_userBusiness.SaveEntity(userEntity, false);

            string action = "";
            if (ustate == "0")
                action = "冻结";
            else
                action = "解冻";

            base.addAdminLog(operationAction.Update, null, action+"渠道商" + userEntity.Realname);


            builder.Append("{\"success\":true,\"msg\":\"操作成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();

        }

        protected void BtnBatchDownImage_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "filename=渠道商推广码.zip");
            using (ZipFile zip = new ZipFile(System.Text.Encoding.Default))//解决中文乱码问题  
            {
                DataTable dtUser = go_channel_userBusiness.GetListData("parentid<>0 and typeid=1", "uid");
                foreach (DataRow dr in dtUser.Rows)
                {
                    int uid =Convert.ToInt32(dr["uid"]);
                    go_channel_userEntity entity = go_channel_userBusiness.LoadEntity(uid);
                    if (entity != null)
                    {
                        string fileName = string.Format("{0}_{1}", entity.Realname, entity.Usermobile);
                        SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                        string savepathex = System.Web.HttpContext.Current.Server.MapPath("~/Resources/TicketImage") + "\\" + fileName + ".bmp";
                        if (!File.Exists(savepathex))
                        {
                            YH.Weixin.MP.Api.TicketAPI.GetTicketImage(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret, string.Format("channel_{0}", uid), false, fileName);
                        }
                        zip.AddFile(savepathex, "images");
                    }
                  

                }
                zip.Save(Response.OutputStream);
            }
            Response.End();
        }

        //protected void BtnBatchInitPassword_Click(object sender, EventArgs e)
        //{

        //    DataTable dtUser = go_channel_userBusiness.GetListData("parentid<>0 and typeid=1", "uid,usermobile");
        //    foreach (DataRow dr in dtUser.Rows)
        //    {
        //        int uid = Convert.ToInt32(dr["uid"]);
        //        string mobile = dr["usermobile"]==null ? "" : dr["usermobile"].ToString();
        //        go_channel_userEntity userEntity = go_channel_userBusiness.LoadEntity(uid);
        //        if (!string.IsNullOrEmpty(mobile))
        //            userEntity.Userpass = SecurityHelper.GetMd5To32(mobile).ToLower();
        //        else
        //            userEntity.Userpass = SecurityHelper.GetMd5To32("123456").ToLower();
        //        go_channel_userBusiness.SaveEntity(userEntity, false);
        //    }
        //}

    }
}