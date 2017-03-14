using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Weixin.MP;

namespace RedBook
{
    public partial class gift_index : VWeiXinOAuth
    {
        public int currentScore = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            go_memberEntity currentMember = go_memberBusiness.GetCurrentMember();
            currentScore = currentMember.Score;
        }
    }
}