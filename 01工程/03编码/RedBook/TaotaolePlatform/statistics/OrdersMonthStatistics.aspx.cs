using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Business;

namespace RedBookPlatform.statistics
{
    public partial class OrdersMonthStatistics :Base
    {
        public static string times = "123";
        public static string datas = "456";
        public static string money = "456";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind("2016-09-04 0:00:00.000", "2016-09-27 0:00:00.000");
            }
        }
        protected void bind(string start, string end)
        {
            DataTable dt = CustomsBusiness.GetListData(" (select LEFT(CONVERT(varchar,o.time,112),6) as  timeday ,o.money from go_orders o join go_member m on o.uid=m.uid where m.typeid!=9 and m.wxid not like '%test%'  and o.ordertype ='yuan' and o.time<'" + end + "' and o.time>'"+start+"')t group by t.timeday ", null, "t.timeday,COUNT(*) as counts,SUM(t.money)as moneys,(select COUNT(*) from go_orders   god join go_member gm on god.uid=gm.uid  where LEFT(CONVERT(varchar,god.time,112),6)<(timeday+1) and gm.typeid!=9 and gm.wxid not like '%test%' and god.ordertype ='yuan' ) as allcount,(select sum(god.money) from go_orders  god join go_member gm on god.uid=gm.uid where LEFT(CONVERT(varchar,god.time,112),6)<(timeday+1) and gm.typeid!=9 and gm.wxid not like '%test%' and god.ordertype ='yuan' ) as allmoney", " timeday asc");
            dt.Columns.Add("scale", typeof(string));
            dt.Columns.Add("moneyscale", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {

                    times = "['" + dt.Rows[i]["timeday"].ToString().Substring(2, 2) +"年"+dt.Rows[i]["timeday"].ToString().Substring(4, 2)+ "月"+"',";
                    datas = "[" + dt.Rows[i]["counts"].ToString() + ",";
                    money = "[" + dt.Rows[i]["moneys"].ToString() + ",";
                    dt.Rows[i]["scale"] = "100";
                    dt.Rows[i]["moneyscale"] = "100";
                }
                else if (i == dt.Rows.Count - 1)
                {
                    times += "'" + dt.Rows[i]["timeday"].ToString().Substring(2, 2) + "年" + dt.Rows[i]["timeday"].ToString().Substring(4, 2) + "月" + "']";
                    datas += "" + dt.Rows[i]["counts"].ToString() + "]";
                    money += "" + dt.Rows[i]["moneys"].ToString() + "]";
                    dt.Rows[i]["scale"] = ((Convert.ToDecimal(dt.Rows[i]["counts"]) / Convert.ToDecimal(dt.Rows[i - 1]["counts"])) * 100).ToString("0.00");
                    dt.Rows[i]["moneyscale"] = ((Convert.ToDecimal(dt.Rows[i]["moneys"]) / Convert.ToDecimal(dt.Rows[i - 1]["moneys"])) * 100).ToString("0.00");
                }
                else
                {
                    times += "'" + dt.Rows[i]["timeday"].ToString().Substring(2, 2) + "年" + dt.Rows[i]["timeday"].ToString().Substring(4, 2) + "月" + "',";
                    datas += "" + dt.Rows[i]["counts"].ToString() + ",";
                    money += "" + dt.Rows[i]["moneys"].ToString() + ",";
                    dt.Rows[i]["scale"] = ((Convert.ToDecimal(dt.Rows[i]["counts"]) / Convert.ToDecimal(dt.Rows[i - 1]["counts"])) * 100).ToString("0.00");
                    dt.Rows[i]["moneyscale"] = ((Convert.ToDecimal(dt.Rows[i]["moneys"]) / Convert.ToDecimal(dt.Rows[i - 1]["moneys"])) * 100).ToString("0.00");
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
            string startstart = "";
            string endstart = "";
            string startend = "";
            string endend = "";
            if (end.Split('-')[1] == "12")
            {
                 startend = (Convert.ToInt32( end.Split('-')[0]) + 1).ToString();
                 endend = "1";
            }
            else
            {
                startend = end.Split('-')[0];
                endend = (Convert.ToInt32(end.Split('-')[1]) + 1).ToString();
            }


            if (start.Split('-')[1] == "1")
            {
                startstart = (Convert.ToInt32(start.Split('-')[0]) - 1).ToString();
                endstart = "12";
            }
            else
            {
                startstart = end.Split('-')[0];
                endstart = (Convert.ToInt32(start.Split('-')[1]) - 1).ToString();
            }

            bind(startstart + "-" + endstart + "-1 00:00:00", startend + "-" + endend + "-1 00:00:00");
        }
    }
}