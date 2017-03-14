using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taotaole.Bll;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;
using YH.Weixin.Pay;

namespace RedBook.WxPay
{
    public partial class wx_Pay : System.Web.UI.Page
    {
        protected go_ordersEntity Order;
        protected string OrderId;
        protected decimal total_fee;
        protected int uid;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SiteSettings masterSettings = Globals.GetMasterSettings(false);
            PayNotify payNotify = new NotifyClient(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret, masterSettings.WeixinPartnerID, masterSettings.WeixinPartnerKey, masterSettings.WeixinPaySignKey).GetPayNotify(base.Request.InputStream);
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

                    go_BrokerageSetEntity brokerageset = go_BrokerageSetBusiness.LoadEntity(1);//获取返佣,奖池等比例

                    if (go_memberBusiness.SaveMemberRechargeInfo(uid, addmoneyEntity[0]))
                    {
                        switch (addmoneyEntity[0].Memo)
                        {
                            case "add": //充值
                                //增加佣金记录
                                go_channel_recodesBusiness.AddRecodes(OrderId, uid, total_fee);

                                //增加用户积分.充值赠送的爽乐币 2016-12-9
                                go_memberBusiness.UpdateScoreInfo(total_fee,(total_fee / 100 * brokerageset.ScoreSet),uid); //根据比例获取爽乐币
                                //插入用户爽乐币获取详情
                                addmoneyEntity[0].Pay_type = "系统赠送";
                                addmoneyEntity[0].Money = total_fee / 100 * brokerageset.ScoreSet;
                                go_member_addmoney_recordBusiness.SaveEntity(addmoneyEntity[0],true);
                                //插入用户积分获取详情
                                go_scoreDetailEntity score = new go_scoreDetailEntity();
                                score.Uid = uid;
                                score.Usetime = DateTime.Now;
                                score.Money = total_fee;
                                score.Use_range = "用户充值";
                                score.Detail = "用户在线充值";
                                score.OrderId = OrderId;
                                score.Type = "1";
                                go_scoreDetailBusiness.SaveEntity(score, true);

                                //调用活动接口 start 17为冲20送118活动
                                go_activityEntity entity = go_activityBusiness.LoadEntity(17);
                                if (go_activity_userBusiness.valQualificationCount(uid.ToString(), "17") && entity.Amount <= this.total_fee)
                                {
                                    go_activity_codeBusiness.sentUserRedpack(uid.ToString(), "17");
                                }
                                Globals.DebugLogger("奖池");
                                //总部奖池记录
                                go_moneyPoolDetailBusiness.AddMoneyPoolRecord(uid, total_fee / 100 * brokerageset.GiftSet,OrderId);//根据比例计算奖池获取量
                                //总部预留记录
                                go_moneyPoolDetailBusiness.AddMoneyReserveRecord(uid, total_fee / 100 * brokerageset.ObligateSet,OrderId);//根据比例计算预留金额获取量
                                break;
                            case "yuan"://一元购支付
                                if (addmoneyEntity[0].Tuanid == 0)
                                {
                                    //增加佣金记录
                                    go_channel_recodesBusiness.AddRecodes(OrderId, uid, total_fee);
                                    //增加用户积分.充值赠送的爽乐币 2016-12-9
                                    go_memberBusiness.UpdateScoreInfo(total_fee, (total_fee / 100 * brokerageset.ScoreSet), uid); //根据比例获取爽乐币
                                    //插入用户爽乐币获取详情
                                    addmoneyEntity[0].Pay_type = "系统赠送";
                                    addmoneyEntity[0].Money = total_fee / 100 * brokerageset.ScoreSet;
                                    go_member_addmoney_recordBusiness.SaveEntity(addmoneyEntity[0], true);
                                    //插入用户积分获取详情
                                    go_scoreDetailEntity yuanScore = new go_scoreDetailEntity();
                                    yuanScore.Uid = uid;
                                    yuanScore.Usetime = DateTime.Now;
                                    yuanScore.Money = total_fee;
                                    yuanScore.Use_range = "用户一元购充值";
                                    yuanScore.Detail = "用户一元购充值余额";
                                    yuanScore.OrderId = OrderId;
                                    yuanScore.Type = "1";
                                    go_scoreDetailBusiness.SaveEntity(yuanScore, true);
                                }

                                //总部奖池记录
                                go_moneyPoolDetailBusiness.AddMoneyPoolRecord(uid, total_fee / 100 * brokerageset.GiftSet, OrderId);
                                //总部预留记录
                                go_moneyPoolDetailBusiness.AddMoneyReserveRecord(uid, total_fee / 100 * brokerageset.ObligateSet, OrderId);

                                break;
                            case "quan":
                                if (addmoneyEntity[0].Tuanid == 0)
                                {
                                    //增加佣金记录
                                    go_channel_recodesBusiness.AddRecodes(OrderId, uid, total_fee);
                                    //增加用户积分.充值赠送的爽乐币 2016-12-9
                                    go_memberBusiness.UpdateScoreInfo(total_fee, (total_fee / 100 * brokerageset.ScoreSet), uid); //根据比例获取爽乐币
                                    //插入用户爽乐币获取详情
                                    addmoneyEntity[0].Pay_type = "系统赠送";
                                    addmoneyEntity[0].Money = total_fee / 100 * brokerageset.ScoreSet;
                                    go_member_addmoney_recordBusiness.SaveEntity(addmoneyEntity[0], true);
                                    //插入用户积分获取详情
                                    go_scoreDetailEntity quanScore = new go_scoreDetailEntity();
                                    quanScore.Uid = uid;
                                    quanScore.Usetime = DateTime.Now;
                                    quanScore.Money = total_fee;
                                    quanScore.Use_range = "用户直购充值";
                                    quanScore.Detail = "用户直购充值余额";
                                    quanScore.OrderId = OrderId;
                                    quanScore.Type = "1";
                                    go_scoreDetailBusiness.SaveEntity(quanScore, true);
                                }

                                //总部奖池记录
                                go_moneyPoolDetailBusiness.AddMoneyPoolRecord(uid, total_fee / 100 * brokerageset.GiftSet, OrderId);
                                //总部预留记录
                                go_moneyPoolDetailBusiness.AddMoneyReserveRecord(uid, total_fee / 100 * brokerageset.ObligateSet, OrderId);

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