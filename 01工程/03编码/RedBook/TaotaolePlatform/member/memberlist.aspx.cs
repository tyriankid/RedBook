using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;
using YH.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RedBookPlatform.member
{
    public partial class memberlist : Base
    {
        public string display = "";
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
                    case "sentredpack":
                        sentredpack();
                        break;
                    case "ceaseuser":
                        ceasememberuser();
                        break;
                }

               int code=getcode();
               if (code != 3) 
               {
                   display = "display:none";
               }

            }
        }
        /// <summary>
        /// 用户账号启用或停用
        /// </summary>
        public void ceasememberuser()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            int uid = Convert.ToInt32(Request.Form["uid"]);
            string  ceas = Request.Form["ucease"];
            
            go_memberEntity memberEntity = go_memberBusiness.LoadEntity(uid);
            memberEntity.Ceaseuser = Convert.ToInt32(ceas == "1" ? "0" : "1");
            go_memberBusiness.SaveEntity(memberEntity, false);
            builder.Append("{\"success\":true,\"msg\":\"操作成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();

        }

        /// <summary>
        /// 发送睡眠用户红包
        /// </summary>
        public void sentredpack()
        {
            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            int uid = Convert.ToInt32(Request.Form["uid"]);
            int aid = 21;
            //发包模块start
            go_activityEntity activityentity = go_activityBusiness.LoadEntity(aid);
            DataTable redlist = JsonConvert.DeserializeObject<DataTable>(activityentity.Code_config_ids);
            foreach (DataRow rl in redlist.Rows)
            {
                if (rl[0].ToString() != "0")
                {
                    go_code_configEntity code_config = go_code_configBusiness.LoadEntity(int.Parse(rl[0].ToString()));
                    go_activity_codeEntity codeentity = new go_activity_codeEntity();
                    codeentity.Activity_id = aid;
                    codeentity.Code_config_id = int.Parse(rl[0].ToString());
                    codeentity.Codetitle = rl[1].ToString() + activityentity.Usercodeinfo;
                    codeentity.Uid = uid.ToString();
                    codeentity.Amount = code_config.Amount;
                    codeentity.Discount = code_config.Discount;
                    codeentity.State = 1;
                    codeentity.From = 0;
                    codeentity.Addtime = DateTime.Now;
                    codeentity.Senttime = DateTime.Now;
                    codeentity.Overtime = DateTime.Now.AddDays(activityentity.Redpackday);
                    go_activity_codeBusiness.SaveEntity(codeentity, true);
                }

            }
            
            //发包模块end
            builder.Append("{\"success\":true,\"msg\":\"发送成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        public void deleteUser()
        {

            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();
            int uid = Convert.ToInt32(Request.Form["uid"]);
            go_memberBusiness.Del(uid);
            builder.Append("{\"success\":true,\"msg\":\"删除成功！\"}");
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
            go_adminQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (uid Like '%{0}%' or username Like '%{0}%' or email like '%{0}%' or mobile like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }
            if (!string.IsNullOrEmpty(query.type))
            {

                string type = DataHelper.CleanSearchString(query.type);
                if (query.type == "weixin")
                {
                    builder.AppendFormat("and (typeid=0 )", type);
                }
                else if (query.type == "baidu")
                {
                    builder.AppendFormat("and (typeid=9)", type);
                }
            }
            //调用通用分页方法绑定
            DbQueryResult managers = DataHelper.GetPageData(query, "go_member", builder, "uid","*");
            this.rptmember.DataSource = managers.Data;
            this.rptmember.DataBind();
            this.selMultiple.Value = this.Page.Request.QueryString["multiple"];
            this.dropType.SelectedValue = query.type;
            this.tboxKeyword.Text = query.keyword;
            this.pager1.TotalRecords = managers.TotalRecords;
        }
        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_adminQuery GetQuery()
        {
            go_adminQuery query = new go_adminQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            query.SortBy = "time";
            query.SortOrder = SortAction.Desc;
            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortBy"]))
            {
                query.SortBy = this.Page.Request.QueryString["SortBy"];
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["type"]))
            {
                query.type = base.Server.UrlDecode(this.Page.Request.QueryString["type"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortOrder"]))
            {
                query.SortOrder = SortAction.Desc;
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["multiple"]))
            {
                //公共筛选
                switch (this.Page.Request.QueryString["multiple"])
                {
                    case "0":
                        query.SortBy = "time";
                        query.SortOrder = SortAction.Desc;
                        break;
                    case "1":
                        query.SortBy = "time";
                        query.SortOrder = SortAction.Asc;
                        break;
                }
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
            queryStrings.Add("SortBy", "uid");
            queryStrings.Add("Type", this.dropType.SelectedValue);
            queryStrings.Add("multiple", this.selMultiple.Value);
            queryStrings.Add("SortOrder", SortAction.Asc.ToString());
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }


        
    }
}