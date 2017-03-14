using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Weixin.Pay;

namespace RedBook.WxPay
{
    public partial class app_Pay : System.Web.UI.Page
    {
        protected go_ordersEntity Order;
        protected string OrderId;
        protected decimal total_fee;
        protected int uid;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            PayNotify payNotify = new NotifyClient("wxb27647b1059f91e1", "c66c309e418e62b64f7cf631ad8aef67", "1373197902", "GFDSAtrewqyuiopnbvcxHJKLM1234576", "").GetPayNotify(base.Request.InputStream);
            if (payNotify != null)
            {
                try
                {
                    this.OrderId = payNotify.PayInfo.OutTradeNo;
                    this.total_fee = payNotify.PayInfo.TotalFee;
                    IList<go_member_addmoney_recordEntity> addmoneyEntity = go_member_addmoney_recordBusiness.GetListEntity(string.Format("code = '{0}'", OrderId));
                    if (addmoneyEntity.Count <= 0 || addmoneyEntity[0].Status != "未付款") return;
                    //判断回调的金钱和提交的金钱是否一致,若不一致,以支付回调过来的金钱为准
                    uid = addmoneyEntity[0].Uid;
                    addmoneyEntity[0].Money = total_fee;
                    addmoneyEntity[0].Status = "已付款";
                    if (go_memberBusiness.SaveMemberRechargeInfo(uid, addmoneyEntity[0]))
                    {
                        switch (addmoneyEntity[0].Memo)
                        {
                            case "add": //充值
                                //增加佣金记录
                                go_channel_recodesBusiness.AddRecodes(OrderId,uid, total_fee);
                                //调用活动接口 start 17为冲20送118活动
                                go_activityEntity entity = go_activityBusiness.LoadEntity(17);
                                if (go_activity_userBusiness.valQualificationCount(uid.ToString(), "17") && entity.Amount <= this.total_fee)
                                {
                                    go_activity_codeBusiness.sentUserRedpack(uid.ToString(), "17");
                                }
                                break;
                            case "yuan"://一元购支付
                                if (addmoneyEntity[0].Tuanid == 0)
                                {
                                    //增加佣金记录
                                    go_channel_recodesBusiness.AddRecodes(OrderId,uid, total_fee);
                                }
                                break;
                            case "tuan"://团购支付
                                break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Globals.DebugLogger("wx_Pay  Exception ：" + ex.Message);
                }

            }
        }


    }
}