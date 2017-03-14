using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;

namespace RedBookPlatform.channel
{
    public partial class applyCash : Base
    {
      
        public string summoney;
        public decimal sumMoney;//可提现总金额
        public decimal drawMoney;//当前申请提现的金额

        public decimal showDrawableMoney;//可提现金额(用于展示)

        protected void Page_Load(object sender, EventArgs e)
        {
        
            if (!IsPostBack)
            {

                    go_channel_userEntity classify = go_channel_userBusiness.LoadEntity(this.CurrLoginChannelUser.Uid);
                    summoney = classify.Nowithdrawcash.ToString();
                    if (summoney != "")
                    {
                        summoney = String.Format("{0:F}", Convert.ToDecimal(summoney));
                    }
                    this.lbMoney.Text = summoney;


                    SiteSettings masterSettings = Globals.GetMasterSettings(false);
                    decimal maxDrawMoney = 0;                                                                       //当前用户最大可提现金额
                    decimal totalSumMoney = Convert.ToDecimal(summoney) * 100 / masterSettings.AgentSet;       //当前(月度结算)用户推广充值总额
                    decimal rangeMoney = 50000;//区间 为5万*N+1

                    while (totalSumMoney > rangeMoney)
                    {
                        maxDrawMoney += (rangeMoney / 100 * masterSettings.AgentSet);
                        totalSumMoney -= rangeMoney;
                    }
                    showDrawableMoney = maxDrawMoney;


            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            sumMoney = Convert.ToDecimal(this.lbMoney.Text);
            drawMoney = Convert.ToDecimal(Money.Value);
            //基本判断
            if (sumMoney > drawMoney)
            {
                Response.Write("<script>alert('您输入的提现金额不能大于您的可提现金额')</script>");
                return;
            }
            //冻结判断
            go_channel_userEntity entity = go_channel_userBusiness.LoadEntity(this.CurrLoginChannelUser.Uid);

            string state = entity.Frozenstate.ToString();
            if (state == "1")
            {
                Response.Write("<script>alert('对不起！该账号已被冻结')</script>");
                return;
            }
            /*
             * 2016-12-22爽乐购功能:总分佣金额(可提现金额/佣金提现比例)未满5万不允许提现,
             * 超过5万并在5万*(n+1)的区间内,依然只能提 5万*(n+1)*佣金提现比例 的金额,剩余金额留下处理
             * 一个月只能提现一次?
             */
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            if (sumMoney < 50000 / 100 * masterSettings.AgentSet)
            {
                Response.Write("<script>alert('对不起！您的月度结算金额未满2500！')</script>");
                return;
            }

            decimal maxDrawMoney = 0;                                                                       //当前用户最大可提现金额
            decimal totalSumMoney = Convert.ToDecimal(summoney) * 100 / masterSettings.AgentSet;       //当前(月度结算)用户推广充值总额
            decimal rangeMoney = 50000;//区间 为5万*N+1

            while (totalSumMoney > rangeMoney)
            {
                maxDrawMoney += (rangeMoney / 100 * masterSettings.AgentSet);
                totalSumMoney -= rangeMoney;
            }

            if (maxDrawMoney < drawMoney)
            {
                Response.Write("<script>alert('对不起！您当月可提现"+maxDrawMoney+"元！')</script>");
                return;
            }


            go_channel_cash_outEntity classify = new go_channel_cash_outEntity();

            //添加提现记录
            classify.Uid = this.CurrLoginChannelUser.Uid;
            classify.Money = drawMoney;//Convert.ToDecimal(Money.Value);
            classify.Username = UserName.Value;
            classify.Bankname = BankName.Value;
            classify.Branch = Branch.Value;
            classify.Banknumber = BankNumber.Value;
            classify.Linkphone = LinkPhone.Value;
            classify.Time = DateTime.Now.ToString();
            classify.Reviewtime = DateTime.Now;
            classify.Auditstatus = 0;
            classify.Procefees = 0;



            go_channel_cash_outBusiness.SaveEntity(classify, true);

            Response.Redirect("agentCashout.aspx?agent=true");

        }
    }
}