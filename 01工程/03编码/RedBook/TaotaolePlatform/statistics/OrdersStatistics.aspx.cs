using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Business;
using System.Data;

namespace RedBookPlatform . statistics
{
    public partial class OrdersStatistics :Base
    {
        public static string times = "123";
        public static string datas = "456";
        public static string money = "456";
        public static string title = "";
        protected void Page_Load(object sender , EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dt = DateTime.Now;
                DateTime dt2 = dt.AddMonths(1);
                DateTime startWeek = dt.AddDays(-(dt.Day) + 1);
                DateTime endWeek = dt2.AddDays(-dt.Day);
                bind(startWeek.ToString(), endWeek.ToString());
                //bind("2016-09-04 0:00:00.000", "2016-09-27 0:00:00.000");
            }
        }
        protected void bind(string start, string end)
        {
            title = DateTime.Parse(start).ToString().Split(' ')[0] + "～" + DateTime.Parse(end).AddDays(-1).ToString().Split(' ')[0] + "时间段的数据统计";
            DataTable dt = CustomsBusiness.GetListData(" (select CONVERT(char(10),o.time,20)  as timeday,o.money from go_orders o join go_member m on o.uid=m.uid where o.time<'"+end+"' and o.time>'"+start+"'  and o.ordertype ='yuan' and m.wxid not like '%test%'  and m.typeid!=9)t group by timeday  ", null, "timeday,COUNT(timeday)as counts,SUM(money)as money ,(select COUNT(*) from go_orders   god join go_member gm on god.uid=gm.uid where god.time<DATEADD(d,1,timeday) and gm.typeid!=9 and gm.wxid not like '%test%' and god.ordertype ='yuan' ) as allcount  ,(select sum(god.money) from go_orders  god join go_member gm on god.uid=gm.uid where god.time<DATEADD(d,1,timeday) and gm.typeid!=9 and gm.wxid not like '%test%' and god.ordertype ='yuan' ) as allmoney", " timeday asc");
            dt.Columns.Add("now", typeof(string));
            dt.Columns.Add("nowmoney", typeof(string));
            dt.Columns.Add("scale", typeof(string));
            dt.Columns.Add("moneyscale", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {

                    times = "['" + DateTime.Parse(dt.Rows[i]["timeday"].ToString()).ToString("M月d日") + "',";
                    datas = "[" + dt.Rows[i]["counts"].ToString() + ",";
                    money = "[" + dt.Rows[i]["money"].ToString() + ",";
                    dt.Rows[i]["scale"] = "100";
                    dt.Rows[i]["moneyscale"] = "100";
                }
                else if (i == dt.Rows.Count - 1)
                {
                    times += "'" + DateTime.Parse(dt.Rows[i]["timeday"].ToString()).ToString("M月d日") + "']";
                    datas += "" + dt.Rows[i]["counts"].ToString() + "]";
                    money += "" + dt.Rows[i]["money"].ToString() + "]";
                    dt.Rows[i]["scale"] = ((Convert.ToDecimal(dt.Rows[i]["counts"]) / Convert.ToDecimal(dt.Rows[i - 1]["counts"])) * 100).ToString("0.00") ;
                    dt.Rows[i]["moneyscale"] = ((Convert.ToDecimal(dt.Rows[i]["money"]) / Convert.ToDecimal(dt.Rows[i - 1]["money"])) * 100).ToString("0.00");
                }
                else
                {
                    times += "'" + DateTime.Parse(dt.Rows[i]["timeday"].ToString()).ToString("M月d日") + "',";
                    datas += "" + dt.Rows[i]["counts"].ToString() + ",";
                    money += "" + dt.Rows[i]["money"].ToString() + ",";
                    dt.Rows[i]["scale"] = ((Convert.ToDecimal(dt.Rows[i]["counts"]) / Convert.ToDecimal(dt.Rows[i - 1]["counts"])) * 100).ToString("0.00");
                    dt.Rows[i]["moneyscale"] = ((Convert.ToDecimal(dt.Rows[i]["money"]) / Convert.ToDecimal(dt.Rows[i - 1]["money"])) * 100).ToString("0.00");
                }
            }

            DataView dv = dt.DefaultView;
            dv.Sort = "timeday desc";
            rptbind.DataSource = dv;
            rptbind.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string start = inpStarTime.Value;
            string end = inpEndTime.Value;
            DateTime dt = Convert.ToDateTime(end).AddDays(+1);
            bind(start, dt.ToString());
        }

        //protected void btnThisWeek_Click(object sender, EventArgs e)
        //{
            
        //}

        //protected void btnLastWeek_Click(object sender, EventArgs e)
        //{
        //    DateTime dt = DateTime.Now;
        //    int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
        //    int dayspan = (-1) * weeknow + 1;
        //    DateTime startWeek = DateTime.Now.AddDays(dayspan);
        //    DateTime endWeek = startWeek.AddDays(6);
        //    inpStarTime.Value = startWeek.AddDays(-7).ToShortDateString();
        //    inpEndTime.Value = endWeek.AddDays(-7).ToShortDateString();
        //}

        //protected void btnThisMonth_Click(object sender, EventArgs e)
        //{
        //    DateTime dt = DateTime.Now;
        //    DateTime dt2 = dt.AddMonths(1);
        //    DateTime startWeek = dt.AddDays(-(dt.Day) + 1);
        //    DateTime endWeek = dt2.AddDays(-dt.Day);
        //    inpStarTime.Value = startWeek.ToShortDateString();
        //    inpEndTime.Value = endWeek.ToShortDateString();
        //}

        //protected void btnLastMonth_Click(object sender, EventArgs e)
        //{
        //    DateTime dt = DateTime.Now;
        //    DateTime startWeek = dt.AddMonths(-1).AddDays(-dt.Day + 1);
        //    DateTime endWeek = dt.AddDays(-dt.Day);
        //    inpStarTime.Value = startWeek.ToShortDateString();
        //    inpEndTime.Value = endWeek.ToShortDateString();
        //}
    }
}