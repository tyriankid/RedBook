using System;
using System . Collections . Generic;
using System . Linq;
using System . Web;
using System . Web . UI;
using System . Web . UI . WebControls;
using Taotaole . Business;
using System . Data;
namespace RedBookPlatform . statistics
{
    public partial class UserStatistics : Base
    {
        public static string times="123";
        public static string datas="456";
        public static string title="";
        protected void Page_Load(object sender , EventArgs e)
        {
            if ( !IsPostBack )
            {
                DateTime dt = DateTime.Now;
                DateTime dt2 = dt.AddMonths(1);
                DateTime startWeek = dt.AddDays(-(dt.Day) + 1);
                DateTime endWeek = dt2.AddDays(-dt.Day);
                bind(startWeek.ToString(), endWeek.ToString());
                //bind("2016-09-04 0:00:00.000", "2016-09-27 0:00:00.000");
            }
        }
        protected void bind(string start,string end)
        {
            //DateTime begin = ;
            //DateTime finish =;
            title = DateTime.Parse(start).ToShortDateString() + "～" +DateTime.Parse(end).ToShortDateString() + "时间段的统计值";
            DataTable dt= CustomsBusiness . GetListData("  go_member where time<'" + start + "') as countss From (select  CONVERT(char(10),time,20) as timeday from go_member where time>'"+start+"' and time<='"+end+"'  )T Group by timeday" , null , "timeday,COUNT(*) as daycount,(select  COUNT(*) as counts " , "timeday asc");
            dt . Columns . Add("now" , typeof(string));
            dt . Columns . Add("scale" , typeof(string));
            for ( int i = 0 ; i < dt.Rows.Count ; i++ )
            {
                if ( i == 0 )
                {
                    times = "['" + DateTime.Parse(dt.Rows[i]["timeday"].ToString()).ToString("M月d日") + "',";
                    dt . Rows [ i ] [ "now" ] = ( Convert . ToInt32(dt . Rows [ i ] [ "countss" ]) + Convert . ToInt32(dt . Rows [ i ] [ "daycount" ]) ) . ToString();
                    datas = "[" + dt . Rows [ i ] [ "daycount" ] . ToString() + ",";
                    dt . Rows [ i ] [ "scale" ] = "100";
                }
                else if ( i == dt . Rows . Count-1 )
                {
                    dt . Rows [ i ] [ "countss" ] = ( Convert . ToInt32(dt . Rows [ i - 1 ] [ "countss" ]) + Convert . ToInt32(dt . Rows [ i - 1 ] [ "daycount" ]) ) . ToString();
                    times += "'" + DateTime.Parse(dt.Rows[i]["timeday"].ToString()).ToString("M月d日") + "']";
                    datas += "" + dt . Rows [ i ] [ "daycount" ] . ToString() + "]";
                    dt . Rows [ i ] [ "now" ] = ( Convert . ToInt32(dt . Rows [ i ] [ "countss" ]) + Convert . ToInt32(dt . Rows [ i ] [ "daycount" ]) ) . ToString();
                    dt . Rows [ i ] [ "scale" ] = ( ( Convert . ToDecimal(dt . Rows [ i ] [ "daycount" ]) / Convert . ToDecimal(dt . Rows [ i - 1 ] [ "daycount" ]) ) * 100 ) . ToString("0.00");
                }
                else
                {
                    dt . Rows [ i ] [ "countss" ] = ( Convert . ToInt32(dt . Rows [ i-1 ] [ "countss" ]) + Convert . ToInt32(dt . Rows [ i-1 ] [ "daycount" ]) ) . ToString();
                    times += "'" + DateTime.Parse(dt.Rows[i]["timeday"].ToString()).ToString("M月d日") + "',";
                    datas += "" + dt . Rows [ i ] [ "daycount" ] . ToString() + ",";
                    dt . Rows [ i ] [ "now" ] = ( Convert . ToInt32(dt . Rows [ i ] [ "countss" ]) + Convert . ToInt32(dt . Rows [ i ] [ "daycount" ]) ) . ToString();
                    dt . Rows [ i ] [ "scale" ] = ( ( Convert . ToDecimal(dt . Rows [ i ] [ "daycount" ]) / Convert . ToDecimal(dt . Rows [ i - 1 ] [ "daycount" ]) ) * 100 ) . ToString("0.00");
                }
            }

            DataView dv=dt . DefaultView;
            dv . Sort = "timeday desc";
            rptbind . DataSource = dv;
            rptbind . DataBind();
        }

        protected void btnSearch_Click(object sender , EventArgs e)
        {
            string start=inpStarTime . Value;
            string end=inpEndTime . Value;
            DateTime dt=Convert . ToDateTime(end) . AddDays(+1);
            bind(start , dt.ToString());
        }

        
    }
}