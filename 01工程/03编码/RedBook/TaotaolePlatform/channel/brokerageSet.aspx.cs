using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;

namespace RedBookPlatform.channel
{
    public partial class brokerageSet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {   
                DataTable dtBrokerage = go_BrokerageSetBusiness.GetListData();
                if (dtBrokerage.Rows.Count > 0)
                {
                    scoreSet.Value = dtBrokerage.Rows[0]["scoreSet"].ToString();
                    giftSet.Value = dtBrokerage.Rows[0]["giftSet"].ToString();
                    agentSet.Value = dtBrokerage.Rows[0]["agentSet"].ToString();
                    obligateSet.Value = dtBrokerage.Rows[0]["obligateSet"].ToString();
                }

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isAdd= false;
            DataTable dtBrokerage = go_BrokerageSetBusiness.GetListData();
            if (dtBrokerage.Rows.Count==0)
            {
                isAdd = true;
            }
            go_BrokerageSetEntity entity = new go_BrokerageSetEntity();
            if (!isAdd)
            {
                entity.BrokerageID = int.Parse(dtBrokerage.Rows[0]["brokerageID"].ToString());
            }
            entity.ScoreSet = int.Parse(scoreSet.Value);
            entity.GiftSet = int.Parse(giftSet.Value);
            entity.AgentSet = int.Parse(agentSet.Value);
            entity.ObligateSet = int.Parse(obligateSet.Value);
            go_BrokerageSetBusiness.SaveEntity(entity, isAdd);
        }


    }
}