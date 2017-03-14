using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;

namespace RedBookPlatform.channel
{
    public partial class channellist : Base
    {
        public string allmoney ;
        public string allusercount;
        public string allusermoney;
        public string agent;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = "btnSearch";
            if (!this.IsPostBack)
            {
                
             
                BindPage();
                string action = Request["action"];
                switch (action)
                {
                    case "FrozenUid":
                        FrozenUid();
                        break;
                    case "goDelete":
                        goDelete(Convert.ToInt32(Request["id"]), Request["name"]);
                        break;
                        
                }
            }
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
            string action="";
            if (ustate == "0")
                action = "冻结";
            else
                action = "解冻";
                base.addAdminLog(operationAction.Update, null, action + "服务商账号为" + userEntity.Username);

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
            //获得用户输入的条件
            go_channellistQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("parentid=0");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (realname Like '%{0}%' or contacts like '%{0}%' or usermobile like '%{0}%' or useremail like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }

            //调用通用分页方法绑定
            // And parentid<>0
            string from = "(select distinct u.addtime,u.uid,u.realname,u.cityid,u.contacts,u.usermobile,u.useremail,u.logintime,u.rebateratio,u.usercount,u.usercountmoney,u.parentid,u.frozenstate from go_channel_user u )T";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "uid");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
            this.ddlorder.SelectedValue = query.order.ToString();

            string fromone = "(select r.money, u.addtime,u.uid,u.realname,u.cityid,u.contacts,u.usermobile,u.useremail,u.logintime,u.rebateratio,u.usercount,u.usercountmoney,u.parentid,u.frozenstate from go_channel_user u left join go_channel_recodes r on r.uid=u.uid)T";
            allmoney = Taotaole.Bll.go_channel_recodesBusiness.SelectAllMoney(fromone, builder.ToString());
            allusercount = Taotaole.Bll.go_channel_userBusiness.SelectAllUserCount(from, builder.ToString());
            allusermoney = Taotaole.Bll.go_channel_userBusiness.SelectAllUserMoney(from, builder.ToString());

            if (allmoney == "")
            {
                allmoney = "0";
            }
            if (allusercount == "")
            {
                allusercount = "0";
            }
            if (allusermoney == "")
            {
                allusermoney = "0";
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

            query.SortBy = "addtime";
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortBy"]))
            {
                query.SortBy = this.Page.Request.QueryString["SortBy"];
            }

            query.SortOrder = SortAction.Desc;

            if (this.Page.Request.QueryString["order"] == "1" || this.Page.Request.QueryString["order"] == "3")
            {

                query.SortOrder = SortAction.Asc;
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["order"]))
            {
                query.order = this.Page.Request.QueryString["order"];
            }
            else
            {
                query.order = "0";
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
          

            if (ddlorder.SelectedValue == "1")
            {


                queryStrings.Add("SortBy", "addtime");
                queryStrings.Add("SortOrder", SortAction.Asc.ToString());

            }
            else if(ddlorder.SelectedValue == "2")
            {
                queryStrings.Add("SortBy", "usercountmoney");
                queryStrings.Add("SortOrder", SortAction.Desc.ToString());
            }
            else if (ddlorder.SelectedValue == "3")
            {
                queryStrings.Add("SortBy", "usercountmoney");
                queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            }
            else
            {
                queryStrings.Add("SortBy", "addtime");
                queryStrings.Add("SortOrder", SortAction.Desc.ToString());
            }
            queryStrings.Add("order", ddlorder.SelectedValue);
            if (Request.QueryString["agent"] != null)
            {
                queryStrings.Add("agent", "true");
            }
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }

       
        public  void goDelete(int id,string name)
        {
            StringBuilder builder = new StringBuilder();

            if (int.Parse(go_channel_userBusiness.SelectChannelCount(id)) == 0)
            {
                go_channel_userBusiness.Del(id);
                base.addAdminLog(operationAction.Delete, null, "删除服务商" + name);
                builder.Append("{\"success\":true,\"msg\":\"删除成功！\"}");
            }
            else
            {
                builder.Append("{\"success\":true,\"msg\":\"该服务商下面有渠道商不能删除！\"}");
            }
           
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();
        }

      
        
    }
}