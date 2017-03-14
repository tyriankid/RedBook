using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Model;

namespace RedBookPlatform.member
{
    public partial class memberDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uid = Request.QueryString["uid"];
            if (!IsPostBack)
            {
                BindPage(uid);
            }
        }
        private void BindPage(string uid)
        {
            if (!string.IsNullOrEmpty(uid))
            {
                string where = "uid=" + uid + " and isdefault='Y'";
                DataTable userGameInfo = go_member_dizhi_gameBusiness.GetListData(where);
                if (userGameInfo.Rows.Count==0)
                {
                    where = "uid=" + uid + "";
                    userGameInfo = go_member_dizhi_gameBusiness.GetListData(where);
                }
                if (userGameInfo.Rows.Count > 0)
                {
                    litshouhuoren1.Text = userGameInfo.Rows[0]["shouhuoren"].ToString();
                    litMobile1.Text = userGameInfo.Rows[0]["mobile"].ToString();
                    litTell1.Text = userGameInfo.Rows[0]["tell"].ToString();
                    litQQ1.Text = userGameInfo.Rows[0]["qq"].ToString();
                    litGameName.Text = userGameInfo.Rows[0]["gamename"].ToString();
                    litGameZone.Text = userGameInfo.Rows[0]["gamearea"].ToString();
                    litServer.Text = userGameInfo.Rows[0]["gameserver"].ToString();
                    litGameType.Text = userGameInfo.Rows[0]["gametype"].ToString();
                    litgameAccount.Text = userGameInfo.Rows[0]["gameusercode"].ToString();
                }


                DataTable userInfo = go_member_dizhiBusiness.GetListData(where);
                if (userInfo.Rows.Count==0)
                {
                    where = "uid=" + uid + "";
                    userInfo = go_member_dizhiBusiness.GetListData(where);
                }
                if (userInfo.Rows.Count > 0)
                {
                    litDiZhi.Text = userInfo.Rows[0]["sheng"].ToString() + userInfo.Rows[0]["shi"].ToString() + userInfo.Rows[0]["xian"].ToString() + userInfo.Rows[0]["jiedao"].ToString();
                    litEmail.Text = userInfo.Rows[0]["youbian"].ToString();
                    litshouhuoren.Text = userInfo.Rows[0]["shouhuoren"].ToString();
                    litMobile.Text = userInfo.Rows[0]["mobile"].ToString();
                    litTell.Text = userInfo.Rows[0]["tell"].ToString();
                    litQQ.Text = userInfo.Rows[0]["qq"].ToString();
                }
            }
        }
    }
}