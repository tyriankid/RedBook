using System;
using System . Collections . Generic;
using System . Linq;
using System . Web;
using System . Web . UI;
using System . Web . UI . WebControls;
using Taotaole . Business;
using System . Data;
using System . Text;
using RedBookPlatform . Resources . InfoQuery;
using Taotaole . Common;
using Taotaole . Model;
using System . Collections . Specialized;

namespace RedBookPlatform . statistics
{
    public partial class RechargeStatistics : Base
    {
        protected void Page_Load(object sender , EventArgs e)
        {
            if ( !IsPostBack )
            {
                BindPage();
            }
        }

        /// <summary>
        /// 绑定后台用户分页
        /// </summary>
        private void BindPage()
        {
            //获得用户输入的条件
            go_orderQuery query = GetQuery();
            StringBuilder builder = new StringBuilder("1=1");
            if ( !string . IsNullOrEmpty(query . StarTime) )
            {
                builder . AppendFormat(" and time>'{0}'" , DataHelper . CleanSearchString(query . StarTime));
            }
            if ( !string . IsNullOrEmpty(query . EndTime) )
            {
                builder . AppendFormat(" and time <'{0}'" , DataHelper . CleanSearchString(query . EndTime));
            }
            //time>'2016-09-04' and time<='2016-09-07'
            //调用通用分页方法绑定
            string from = "( select t.* ,m.username,m.time,m.mobile,m.email,m.money as moneynow from (select  uid, sum(money) as money from go_member_addmoney_record where status='已付款' and pay_type!='话费兑换' and memo!='tuan'group by uid )t inner join go_member m on t.uid=m.uid where typeid !=9 and m.wxid not like '%test%' )t ";
            DbQueryResult orders = DataHelper . GetPageData(query , from , builder , "uid");
            this . pager1 . TotalRecords = orders . TotalRecords;
            this . inpStarTime . Value = query . StarTime;
            this . inpEndTime . Value = query . EndTime;
            rptAdmin . DataSource = orders . Data;
            rptAdmin . DataBind();
            DataTable all = CustomsBusiness . GetListData(from , builder . ToString() , "SUM(money) as Allmoney" , "" , 0 , YH . Utility . DbServers . DbServerName . ReadHistoryDB);
            if ( all . Rows . Count > 0 )
            {
                if ( all . Rows [ 0 ] [ "Allmoney" ] . ToString() != "" )
                {
                    allMoney . InnerText = Convert . ToDouble(all . Rows [ 0 ] [ "Allmoney" ] . ToString()) . ToString("0.00");
                }
                else
                {
                    allMoney . InnerText = "0";
                }
            }
        }

        /// <summary>
        /// 获取查询条件实体
        /// </summary>
        private go_orderQuery GetQuery()
        {
            go_orderQuery query = new go_orderQuery();
            if ( !string . IsNullOrEmpty(this . Page . Request . QueryString [ "StarTime" ]) )
            {
                query . StarTime = base . Server . UrlDecode(this . Page . Request . QueryString [ "StarTime" ]);
            }
            if ( !string . IsNullOrEmpty(this . Page . Request . QueryString [ "EndTime" ]) )
            {
                query . EndTime = base . Server . UrlDecode(this . Page . Request . QueryString [ "EndTime" ]);
            }
            query . PageSize = this . pager1 . PageSize;
            query . PageIndex = this . pager1 . PageIndex;
            query . SortBy = "money";
            query . SortOrder = SortAction . Desc;
           
            return query;
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="isSearch">是否查询(重置起始页码)</param>
        private void ReLoad(bool isSearch)
        {
            NameValueCollection queryStrings = new NameValueCollection();
            queryStrings . Add("StarTime" , this . inpStarTime . Value . Trim());
            queryStrings . Add("EndTime" , this . inpEndTime . Value . Trim());
            if ( !isSearch )
            {
                queryStrings . Add("PageIndex" , this . pager1 . PageIndex . ToString(System . Globalization . CultureInfo . InvariantCulture));
            }
            queryStrings . Add("SortBy" , "time");
            queryStrings . Add("SortOrder" , SortAction . Desc . ToString());
            base . ReloadPage(queryStrings);
        }
        //响应查询按钮事件
        protected void btnSearch_Click(object sender , EventArgs e)
        {
            ReLoad(true);
        }

        protected void btnSearch_Click1(object sender , EventArgs e)
        {
            ReLoad(true);
        }
     
    }
}