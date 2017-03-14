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
    public partial class BriskStatistics : Base
    {
        public static string times = "123";
        public static string datas = "456";
        public static string data = "456";
        public static string titles = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dt = DateTime.Now;
                DateTime dt2 = dt.AddMonths(1);
                DateTime startWeek = dt.AddDays(-(dt.Day) + 1);
                DateTime endWeek = dt2.AddDays(-dt.Day);
                bind(startWeek.ToString(), endWeek.ToString());
                //bind("2015-08-04 0:00:00.000", "2016-09-27 0:00:00.000");
            }
        }
        protected void bind(string start, string end)
        {

            titles = DateTime.Parse(start).ToString().Split(' ')[0] + "～" + DateTime.Parse(end).AddDays(-1).ToString().Split(' ')[0] + "时间段的数据统计";
            //获取时间差
            TimeSpan ts = DateTime.Parse(end) - DateTime.Parse(start);
            DateTime prevStartTime = DateTime.Parse(start).AddDays(-ts.Days);
            DateTime prevEndTime = DateTime.Parse(start);

            string uid = "";
            DataTable dt = CustomsBusiness.GetListData("go_member_account ga right join (select uid,count(orderId) as counts from go_orders where ordertype='yuan' and time<'" + end + "' and time>'" + start + "'  group by uid ) as o on ga.uid=o.uid right join go_member gm on o.uid=gm.uid", "gm.typeid <> 9 and gm.wxid not like '%test%' and ga.money>=1 and ga.time<'" + end + "' and ga.time>'" + start + "' group by ga.uid,gm.username,o.counts ", "top 30 ga.uid,gm.username,o.counts,SUM(ga.money)as money ", " money desc");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    times = "['" + dt.Rows[i]["uid"].ToString()+ "',";
                    datas = "[" + dt.Rows[i]["money"].ToString() + ",";
                    data = "[" + dt.Rows[i]["counts"].ToString() + ",";
                    uid = "(" + dt.Rows[i]["uid"].ToString()+",";
                }
                else if (i == dt.Rows.Count - 1)
                {
                    times += "'" + dt.Rows[i]["uid"].ToString() + "']";
                    datas += "" + dt.Rows[i]["money"].ToString() + "]";
                    data += "" + dt.Rows[i]["counts"].ToString() + "]";
                    uid += dt.Rows[i]["uid"].ToString() + ")";
                }
                else
                {
                    times += "'" + dt.Rows[i]["uid"].ToString() + "',";
                    datas += "" + dt.Rows[i]["money"].ToString() + ",";
                    data += "" + dt.Rows[i]["counts"].ToString() + ",";
                    uid += dt.Rows[i]["uid"].ToString() + ",";
                }
            }
            DataTable dtbl = CustomsBusiness.GetListData(" go_orders where uid in " + uid + " and time<'" + prevEndTime + "' and time>'" + prevStartTime + "' group by uid", null, " uid,SUM(money)as money", null, 0, YH.Utility.DbServers.DbServerName.ReadHistoryDB);
            dt.Columns.Add("ztmoney", typeof(string));
            dt.Columns.Add("scale", typeof(string));
            if (dtbl.Rows.Count > 0)
            {
                for (int i = 0; i < dtbl.Rows.Count; i++)
                {
                
                    for (int p = 0; p < dt.Rows.Count; p++)
                    {
                        if (dtbl.Rows[i]["uid"].ToString() == dt.Rows[p]["uid"].ToString())
                        {
                            dt.Rows[p]["ztmoney"] = dtbl.Rows[i]["money"];
                            dt.Rows[p]["scale"] = Convert.ToInt32(dt.Rows[p]["money"]) - Convert.ToInt32(dtbl.Rows[i]["money"]);
                        }
                        else
                        {
                            if (dt.Rows[p]["ztmoney"].ToString() == "")
                            {
                                dt.Rows[p]["ztmoney"] = "0";
                                dt.Rows[p]["scale"] = Convert.ToInt32(dt.Rows[p]["money"]) ;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["ztmoney"] = "0";
                    dt.Rows[i]["scale"] = Convert.ToInt32(dt.Rows[i]["money"]);
                }
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "money desc";
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
    }
}