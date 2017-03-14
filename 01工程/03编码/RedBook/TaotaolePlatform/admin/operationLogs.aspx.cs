using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Common;
using Taotaole.Model;
using RedBookPlatform.Resources.InfoQuery;

namespace RedBookPlatform.admin
{
    public partial class operationLogs : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
          
            if (!IsPostBack)
            {
                

                BindPage();
                Bindddladmin();
                string action = Request["action"];
                switch (action)
                {
                    case "goDelete":
                        goDelete();
                        break;
                }
            }
        }


        private void Bindddladmin()
        {
            DataTable dtadmin =  go_adminBusiness.GetListData("", "*");

            ddladmin.DataSource = dtadmin;
            ddladmin.DataTextField = "username";
            ddladmin.DataValueField = "username";
            ddladmin.DataBind();
            ddladmin.Items.Insert(0, new ListItem("全部", ""));

        }
        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            go_adminLogsQuery query = GetQuery();
            StringBuilder builder = new StringBuilder(" 1=1 ");

            if (!string.IsNullOrEmpty(query.StarTime))
            {
                builder.AppendFormat(string.Format(" And actiontime>='{0}'", query.StarTime));
            }
            if (!string.IsNullOrEmpty(query.EndTime))
            {
                builder.AppendFormat(string.Format(" And actiontime<='{0}'", query.EndTime + " 23:59:59"));
            }
            if (!string.IsNullOrEmpty(query.Username))
            {
                builder.AppendFormat(string.Format(" And username='{0}'", query.Username ));
            }

            if (!string.IsNullOrEmpty(query.action))
            {
                builder.AppendFormat(" AND (action ='{0}')", DataHelper.CleanSearchString(query.action));
            }

            if (!string.IsNullOrEmpty(query.keyword))
            {
                builder.AppendFormat(" AND (username Like '%{0}%' or actionform like '%{0}%' or infobefore like '%{0}%' or infoafter like '%{0}%')", DataHelper.CleanSearchString(query.keyword));
            }


            ////调用通用分页方法绑定
            string from = "(select  * from go_adminlogs )T";
            DbQueryResult managers = DataHelper.GetPageData(query, from, builder, "id");
            this.rptAdmin.DataSource = managers.Data;
            this.rptAdmin.DataBind();
            this.pager1.TotalRecords = managers.TotalRecords;
            this.tboxKeyword.Text = query.keyword;
            this.txtTimeStart.Value = query.StarTime;
            this.txtTimeEnd.Value = query.EndTime;

            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["action"]))
            {
                this.ddlAction.SelectedValue = this.Page.Request.QueryString["action"];
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["username"]))
            {
                this.ddladmin.SelectedValue = this.Page.Request.QueryString["username"];
            }

        }


        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_adminLogsQuery GetQuery()
        {
            go_adminLogsQuery query = new go_adminLogsQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["keyword"]))
            {
                query.keyword = base.Server.UrlDecode(this.Page.Request.QueryString["keyword"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["starttime"]))
            {
                query.StarTime = base.Server.UrlDecode(this.Page.Request.QueryString["starttime"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["endtime"]))
            {
                query.EndTime = base.Server.UrlDecode(this.Page.Request.QueryString["endtime"]);
            }

            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["username"]))
            {
                query.Username = base.Server.UrlDecode(this.Page.Request.QueryString["username"]);
            }

            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["action"]))
            {
                if (this.Page.Request.QueryString["action"] == "1")
                {
                    query.action = operationAction.Add.ToString();
                }
                else if (this.Page.Request.QueryString["action"] == "2")
                {
                    query.action = operationAction.Delete.ToString();
                }
                else if (this.Page.Request.QueryString["action"] == "3")
                {
                    query.action = operationAction.Update.ToString();
                }
                else if (this.Page.Request.QueryString["action"] == "4")
                {
                    query.action = operationAction.Review.ToString();
                }
                else if (this.Page.Request.QueryString["action"] == "5")
                {
                    query.action = operationAction.Login.ToString();
                }

            }



            query.PageSize = this.pager1.PageSize;
            query.PageIndex = this.pager1.PageIndex;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortBy"]))
            {
                query.SortBy = this.Page.Request.QueryString["SortBy"];
            }
          //  if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SortOrder"]))
         //   {
                query.SortOrder = SortAction.Desc;
         //   }

            //设置界面默认值
            if (this.Page.Request.QueryString["SortOrder"] == null)
            {
                int MaxDays = Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                query.StarTime = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + "1";
                query.EndTime = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + MaxDays;
            }

            return query;
        }


        protected string getAction(string action)
        {
            string act = "";
            if (action == operationAction.Add.ToString())
            {
                act = "增加";
            }
            else if (action == operationAction.Delete.ToString())
            {
                act = "删除";
            }
            else if (action == operationAction.Update.ToString())
            {
                act = "修改";
            }
            else if (action == operationAction.Review.ToString())
            {
                act = "审核";
            }
            else if (action == operationAction.Login.ToString())
            {
                act = "登陆";
            }
            return act;
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="isSearch">是否查询(重置起始页码)</param>
        private void ReLoad(bool isSearch)
        {
            NameValueCollection queryStrings = new NameValueCollection();
            queryStrings.Add("keyword", this.tboxKeyword.Text.Trim());
            queryStrings.Add("starttime", this.txtTimeStart.Value.Trim());
            queryStrings.Add("endtime", this.txtTimeEnd.Value.Trim());
            if (!isSearch)
            {
                queryStrings.Add("PageIndex", this.pager1.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            queryStrings.Add("SortBy", "actiontime");
            queryStrings.Add("SortOrder", SortAction.Desc.ToString());

            queryStrings.Add("action", ddlAction.SelectedValue);
            queryStrings.Add("username", ddladmin.SelectedValue);
            base.ReloadPage(queryStrings);
        }

        //响应查询按钮事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReLoad(true);
        }
        
       //删除日志
        public  void goDelete()
        {
            CustomsBusiness.ExecuteSql("delete from go_adminlogs where actiontime<'" + DateTime.Now.AddMonths(-3) + "'");
            base.addAdminLog( operationAction.Delete, null, "删除了" + DateTime.Now.AddMonths(-3).ToString() + "前的日志");

            Response.ContentType = "text/json";
            StringBuilder builder = new StringBuilder();

            builder.Append("{\"success\":true,\"msg\":\"删除成功！\"}");
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(builder.ToString());
            Response.End();

        }





        /// <summary>
        /// 导出指定日期内的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Export_Click(object sender, EventArgs e)
        {
            string StarTime = this.txtTimeStart.Value.Trim();//开始时间
            string EndTime = this.txtTimeEnd.Value.Trim();//结束时间
            StringBuilder where =new StringBuilder( "1=1");
            if (this.tboxKeyword.Text.Trim() != "")
            {
                where.Append(string.Format("AND (username Like '%{0}%' or actionform like '%{0}%' or infobefore like '%{0}%' or infoafter like '%{0}%')", this.tboxKeyword.Text.Trim()));
             
            }
            if (!string.IsNullOrEmpty(StarTime))
            {
                where.AppendFormat(string.Format(" And actiontime>='{0}'", StarTime));
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                where.AppendFormat(string.Format(" And actiontime<='{0}'", EndTime + " 23:59:59"));
            }
            if (!string.IsNullOrEmpty(ddladmin.SelectedValue))
            {
                where.AppendFormat(string.Format(" And username='{0}'", ddladmin.SelectedValue));
            }

            if (ddlAction.SelectedValue!="0")
            {
                if (ddlAction.SelectedValue == "1")
                {
                    where.AppendFormat(" AND (action ='{0}')", operationAction.Add.ToString());
                }
                else if (this.Page.Request.QueryString["action"] == "2")
                {
                    where.AppendFormat(" AND (action ='{0}')", operationAction.Delete.ToString());
                }
                else if (this.Page.Request.QueryString["action"] == "3")
                {
                    where.AppendFormat(" AND (action ='{0}')", operationAction.Update.ToString());
                }
                else if (this.Page.Request.QueryString["action"] == "4")
                {
                    where.AppendFormat(" AND (action ='{0}')", operationAction.Review.ToString());
                }
               
            }
            //需要导出的数据
            DataTable dt = CustomsBusiness.GetListData("go_adminlogs", where.ToString(), " username,loginip,actiontime,actionform,action,infobefore,infoafter", null, 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            System.Collections.Generic.IList<string> fields = new System.Collections.Generic.List<string>();
            System.Collections.Generic.IList<string> list2 = new System.Collections.Generic.List<string>();
            fields.Add("username");
            list2.Add("操作人");
            fields.Add("loginip");
            list2.Add("IP");
            fields.Add("actiontime");
            list2.Add("时间");
            fields.Add("actionform");
            list2.Add("操作表单");
            fields.Add("action");
            list2.Add("动作");
            fields.Add("infobefore");
            list2.Add("操作前信息");
            fields.Add("infoafter");
            list2.Add("操作后信息");
        
         
            StringBuilder builder = new StringBuilder();
            foreach (string str in list2)
            {
                builder.Append(str + ",");
                if (str == list2[list2.Count - 1])
                {
                    builder = builder.Remove(builder.Length - 1, 1);
                    builder.Append("\r\n");
                }
            }
            foreach (System.Data.DataRow row in dt.Rows)
            {
                foreach (string str2 in fields)
                {
                    builder.Append(row[str2]).Append(",");
                    if (str2 == fields[list2.Count - 1])
                    {

                        builder = builder.Remove(builder.Length - 1, 1);
                        builder.Append("\r\n");
                    }
                }
            }
            this.Page.Response.Clear();
            this.Page.Response.Buffer = false;
            this.Page.Response.Charset = "GB2312";
            this.Page.Response.AppendHeader("Content-Disposition", "attachment;filename=operationLogs.csv");
            this.Page.Response.ContentType = "application/octet-stream";
            this.Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            this.Page.EnableViewState = false;
            this.Page.Response.Write(builder.ToString());
            this.Page.Response.End();

        }
    }
}