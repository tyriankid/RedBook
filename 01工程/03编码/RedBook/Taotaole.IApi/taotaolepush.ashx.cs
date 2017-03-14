using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Cache;
using Taotaole.Common;
using Taotaole.Model;
using YH.Weixin.MP.Messages;
using YH.Weixin.Pay;

namespace Taotaole.IApi
{
    /// <summary>
    /// 服务定时推送消息（组团失败提醒、组团成功中奖提醒、永乐接口超时再发放）
    /// </summary>
    public class taotaolepush : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "application/json";
                string action = context.Request["action"];
                switch (action)
                {
                    case "sendtuanfail":         //组团失败提醒
                        sendtuanfail(context);
                        break;
                    case "sendtuansuccess":         //组团成功中奖提醒
                        sendtuansuccess(context);
                        break;
                    case "sendwxmsg":       //微信推送提醒
                        sendwxmsg(context);
                        break;
                    case "test":                //临时
                        string key = Globals.GetMasterSettings(false).WX_Template_00;
                        string orderid = context.Request[key];
                        string money = context.Request["money"];
                        bool b = Refund.Run("", orderid, int.Parse(money), int.Parse(money), false);
                        context.Response.Write("test_退款" + b);
                        break;
                    case "yolly":       //永乐接口超时再发放
                        //yolly(context);
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        private void WriteLog(string log)
        {
            System.IO.StreamWriter writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("~/wx_push.txt"));

            writer.WriteLine(System.DateTime.Now);

            writer.WriteLine(log);

            writer.Flush();

            writer.Close();

        }

        /// <summary>
        /// 组团失败提醒
        /// </summary>
        private void sendtuanfail(HttpContext context)
        {
            string id = context.Request["id"];
            if (string.IsNullOrEmpty(id)) id = "";
            string sign = context.Request["sign"];
            if (string.IsNullOrEmpty(sign)) sign = "";
            if (!Globals.ValidateSignAppInner(sign, id))
            {
                context.Response.Write("{\"state\":2}");    //未知签名
                return;
            }

            #region 获取组团失败待退款的团列表
            string ordertime = string.Empty;
            string ordermoney = string.Empty;
            string form = @"go_serverworklog SWL
Inner Join go_tuan_listinfo TLI ON SWL.dataid=TLI.tuanlistId
Inner Join go_orders O on ordertype='tuan' And TLI.tuanlistId=o.businessId
Inner Join go_member M on O.uid=M.uid
Inner Join go_tuan T on TLI.tuanId=t.tId";
            #endregion
            DataTable dtData = CustomsBusiness.GetListData(form, "SWL.funcode='TuanRefund' And SWL.manstate=0", " distinct SWL.*,M.wxid,t.title,O.time,O.money,O.pay_type,o.recordcode");
            
            DataTable dtData2 = CustomsBusiness.GetListData("go_member_addmoney_record MAR Inner Join go_member M on MAR.uid=m.uid Left Join go_orders O on MAR.code=O.recordcode"
                , "MAR.score=1 And MAR.status='已付款' And MAR.time<DATEADD(MI,5,GETDATE()) And O.orderId is null", "MAR.uid,MAR.code,MAR.time,MAR.money,M.wxid");
            #region 充值成功，但未消费成功，加入到退款列表
            foreach (DataRow dr in dtData2.Rows)
            {
                DataRow drNew = dtData.NewRow();
                drNew["wxid"] = dr["wxid"];
                drNew["time"] = dr["time"];
                drNew["money"] = dr["money"];
                drNew["recordcode"] = dr["code"];
                drNew["title"] = "团购";
                drNew["dataid"] = dr["uid"];
                dtData.Rows.Add(drNew);
            }
            #endregion



            //循环处理待退款的数据
            foreach (DataRow dr in dtData.Rows)
            {
                Globals.payType paytype = (Globals.payType)Enum.Parse(typeof(Globals.payType), dr["pay_type"].ToString());
                switch (paytype)
                {
                    #region 微信网页支付退款
                    case Globals.payType.wxPay:
                            if (dr["wxid"] == DBNull.Value || string.IsNullOrEmpty(dr["wxid"].ToString())) continue;
                            ordertime = DateTime.Parse(dr["time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            ordermoney = decimal.Parse(dr["money"].ToString()).ToString("0.00");
                            int ordermoneyfen = (int)(decimal.Parse(ordermoney) * 100);

                            //调用微信退款
                            bool b = Refund.Run("", dr["recordcode"].ToString(), ordermoneyfen, ordermoneyfen,true);
                            if (b) {

                                //标记服务工作已处理
                                if (dr["logid"] != DBNull.Value)
                                    CustomsBusiness.ExecuteSql(string.Format("Update go_serverworklog Set manstate=1,mantime=getdate() Where logid='{0}'", dr["logid"].ToString()));

                                //标记团购充值订单为已退款
                                CustomsBusiness.ExecuteSql(string.Format("Update go_member_addmoney_record Set status='已退款' Where score=1 And code='{0}'", dr["recordcode"].ToString()));

                                //发送退款微信公共号提醒
                                Messenger.SendTuanFail(dr["wxid"].ToString(), dr["title"].ToString(), ordertime, ordermoney);

                                //充值成功未消费，减少余额
                                if (dr["logid"] == DBNull.Value)
                                {
                                    CustomsBusiness.ExecuteSql(string.Format("Update go_member Set tuanmoney=tuanmoney-{0} Where uid={1}", dr["money"], dr["dataid"]));
                                }
                            }
                        break;
                    #endregion
                    #region app微信支付退款
                    case Globals.payType.appWxPay://app微信支付
                            if (dr["wxid"] == DBNull.Value || string.IsNullOrEmpty(dr["wxid"].ToString())) continue;
                            ordertime = DateTime.Parse(dr["time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            ordermoney = decimal.Parse(dr["money"].ToString()).ToString("0.00");
                            int ordermoneyfenApp = (int)(decimal.Parse(ordermoney) * 100);

                            //调用微信退款
                            bool bApp = Refund.Run("", dr["recordcode"].ToString(), ordermoneyfenApp, ordermoneyfenApp, true);
                            if (bApp) {

                                //标记服务工作已处理
                                if (dr["logid"] != DBNull.Value)
                                    CustomsBusiness.ExecuteSql(string.Format("Update go_serverworklog Set manstate=1,mantime=getdate() Where logid='{0}'", dr["logid"].ToString()));

                                //标记团购充值订单为已退款
                                CustomsBusiness.ExecuteSql(string.Format("Update go_member_addmoney_record Set status='已退款' Where score=1 And code='{0}'", dr["recordcode"].ToString()));

                                //发送退款微信公共号提醒
                                Messenger.SendTuanFail(dr["wxid"].ToString(), dr["title"].ToString(), ordertime, ordermoney);

                                //充值成功未消费，减少余额
                                if (dr["logid"] == DBNull.Value)
                                {
                                    CustomsBusiness.ExecuteSql(string.Format("Update go_member Set tuanmoney=tuanmoney-{0} Where uid={1}", dr["money"], dr["dataid"]));
                                }
                            }
                        break;
                    #endregion
                }

                
                
            }
            context.Response.Write("{\"state\":0}");
        }

        /// <summary>
        /// 组团成功开奖时的提醒
        /// </summary>
        private void sendtuansuccess(HttpContext context)
        {
            string id = context.Request["id"];
            if (string.IsNullOrEmpty(id)) id = "";
            string sign = context.Request["sign"];
            if (string.IsNullOrEmpty(sign)) sign = "";
            if (!Globals.ValidateSignAppInner(sign, id))
            {
                context.Response.Write("{\"state\":2}");    //未知签名
                return;
            }

            #region 获取组团成功待处理的团列表
            string ordertime = string.Empty;
            string ordermoney = string.Empty;
            string form = @"go_serverworklog SWL
Inner Join go_tuan_listinfo TLI ON SWL.dataid=TLI.tuanlistId
Inner Join go_orders O on ordertype='tuan' And TLI.tuanlistId=o.businessId
Inner Join go_member M on O.uid=M.uid
Inner Join go_tuan T on TLI.tuanId=t.tId";
            DataTable dtData = CustomsBusiness.GetListData(form, "SWL.funcode='TuanWin' And SWL.manstate=0", "distinct SWL.*,M.wxid,t.title,O.money,O.uid,O.iswon,O.pay_type,o.recordcode,O.time");
            #endregion


            foreach (DataRow dr in dtData.Rows)
            {
                Globals.payType paytype = (Globals.payType)Enum.Parse(typeof(Globals.payType), dr["pay_type"].ToString());
                switch (paytype)
                {
                    #region 微信退款
                    case Globals.payType.wxPay:
                        //Globals.DebugLogger("测试中奖进入——" + dr["uid"].ToString());
                        if (string.IsNullOrEmpty(dr["wxid"].ToString()))
                        {
                            CustomsBusiness.ExecuteSql(string.Format("Update go_serverworklog Set manstate=1,mantime=getdate() Where logid='{0}'", dr["logid"].ToString()));
                            continue;
                        }
                        //团购参数人ID
                        string userid = dr["uid"].ToString();
                        //根据用户来源类型推送消息,分别是微信用户,app用户
                        go_memberEntity qMember = go_memberBusiness.LoadEntity(Convert.ToInt32(userid));

                        //中奖处理
                        if (dr["iswon"].ToString() == "1")
                        {
                            WriteLog("wx中奖");
                            //微信用户推送消息
                            if (qMember != null && qMember.Wxid != null && qMember.Wxid.Length > 10)
                                Messenger.SendTuanSuccess(dr["wxid"].ToString(), "[团购]" + dr["title"].ToString(), "[一等奖]" + dr["title"].ToString(), "感谢您的参与，进入个人中心确认收货地址。");
                            //app用户推送消息
                            if (qMember != null && qMember.Appwxid != null && qMember.Appwxid.Length > 10)
                                PushHelper.SendAppMsg(userid, "[团购]" + dr["title"].ToString()+ "[一等奖]" + dr["title"].ToString(), "感谢您的参与，进入个人中心确认收货地址。");
                        }
                        else
                        {
                            WriteLog("wx未中奖");
                            //未中奖处理_调用微信退款
                            ordertime = DateTime.Parse(dr["time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            ordermoney = decimal.Parse(dr["money"].ToString()).ToString("0.00");
                            int ordermoneyfen = (int)(decimal.Parse(ordermoney) * 100);
                            bool b = Refund.Run("", dr["recordcode"].ToString(), ordermoneyfen, ordermoneyfen, true);
                            if (b)
                            {
                                //标记团购充值订单为已退款
                                CustomsBusiness.ExecuteSql(string.Format("Update go_member_addmoney_record Set status='已退款' Where score=1 And code='{0}'", dr["recordcode"].ToString()));

                                //微信用户推送消息
                                if (qMember != null && qMember.Wxid != null && qMember.Wxid.Length > 10)
                                    Messenger.SendTuanSuccess(dr["wxid"].ToString(), "[团购]" + dr["title"].ToString(), "[二等奖]已退款并送红包", "感谢您的参与，进入个人中心查看红包。");
                                //app用户推送消息(支付方式为微信的话退款推送只发到微信,app目前不作处理)
                                if (qMember != null && qMember.Appwxid != null && qMember.Appwxid.Length > 10)
                                    PushHelper.SendAppMsg(userid, dr["wxid"].ToString() + "[团购]" + dr["title"].ToString() + "[二等奖]已退款并送红包", "感谢您的参与，进入个人中心查看红包。");
                                
                                int i = go_activity_codeBusiness.sentUserRedpack(userid, "25");//团购未中奖发红包
                            }
                        }
                        break;
                    #endregion
                    #region app微信退款
                    case Globals.payType.appWxPay:
                        //Globals.DebugLogger("测试中奖进入——" + dr["uid"].ToString());
                        if (string.IsNullOrEmpty(dr["wxid"].ToString()))
                        {
                            CustomsBusiness.ExecuteSql(string.Format("Update go_serverworklog Set manstate=1,mantime=getdate() Where logid='{0}'", dr["logid"].ToString()));
                            continue;
                        }
                        //团购参数人ID
                        string useridApp = dr["uid"].ToString();
                        //根据用户来源类型推送消息,分别是微信用户,app用户
                        go_memberEntity qMemberApp = go_memberBusiness.LoadEntity(Convert.ToInt32(useridApp));

                        //中奖处理
                        if (dr["iswon"].ToString() == "1")
                        {
                            WriteLog("app中奖");
                            //微信用户推送消息
                            if (qMemberApp != null && qMemberApp.Wxid != null && qMemberApp.Wxid.Length > 10)
                                Messenger.SendTuanSuccess(dr["wxid"].ToString(), "[团购]" + dr["title"].ToString(), "[一等奖]" + dr["title"].ToString(), "感谢您的参与，进入个人中心确认收货地址。");

                            //app用户推送消息
                            if (qMemberApp != null && qMemberApp.Appwxid != null && qMemberApp.Appwxid.Length > 10)
                                PushHelper.SendAppMsg(useridApp.ToString(), "[团购]" + dr["title"].ToString()+ "[一等奖]" + dr["title"].ToString(), "感谢您的参与，进入个人中心确认收货地址。");
                            
                        }
                        else
                        {
                            WriteLog("app未中奖");
                            //未中奖处理_调用微信退款
                            ordertime = DateTime.Parse(dr["time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            ordermoney = decimal.Parse(dr["money"].ToString()).ToString("0.00");
                            int ordermoneyfen = (int)(decimal.Parse(ordermoney) * 100);
                            bool b = AppRefund.Run("", dr["recordcode"].ToString(), ordermoneyfen, ordermoneyfen, true);
                            if (b)
                            {
                                //微信用户推送消息
                                if (qMemberApp != null && qMemberApp.Wxid != null && qMemberApp.Wxid.Length > 10)
                                    Messenger.SendTuanSuccess(dr["wxid"].ToString(), "[团购]" + dr["title"].ToString(), "[二等奖]已退款并送红包", "感谢您的参与，进入个人中心查看红包。");
                                //app用户推送消息(支付方式为微信的话退款推送只发到微信,app目前不作处理)
                                if (qMemberApp != null && qMemberApp.Appwxid != null && qMemberApp.Appwxid.Length > 10)
                                    PushHelper.SendAppMsg(useridApp, dr["wxid"].ToString() + "[团购]" + dr["title"].ToString() + "[二等奖]已退款并送红包", "感谢您的参与，进入个人中心查看红包。");


                                int i = go_activity_codeBusiness.sentUserRedpack(useridApp, "25");//团购未中奖发红包
                            }
                        }
                        break;
                    #endregion
                }

                

                //标记服务工作已处理
                CustomsBusiness.ExecuteSql(string.Format("Update go_serverworklog Set manstate=1,mantime=getdate() Where logid='{0}'", dr["logid"].ToString()));
            }

            context.Response.Write("{\"state\":0}");
        }

        /// <summary>
        /// 推送微信提醒
        /// </summary>
        private void sendwxmsg(HttpContext context)
        {
            string id = context.Request["id"];
            string yid = context.Request["yid"];
            if (string.IsNullOrEmpty(id)) id = "";
            if (string.IsNullOrEmpty(yid)) yid = "0";
            string sign = context.Request["sign"];
            if (string.IsNullOrEmpty(sign)) sign = "";
            if (!Globals.ValidateSignAppInner(sign, id))
            {
                context.Response.Write("{\"state\":2}");    //未知签名
                return;
            }

            //根据用户来源类型推送消息,分别是微信用户,app用户
            go_memberEntity qMember = go_memberBusiness.LoadEntity(int.Parse(id));
            go_yiyuanEntity yiyuanEntity = go_yiyuanBusiness.LoadEntity(int.Parse(yid));
            if (qMember != null && yiyuanEntity!=null)
            {
                //Globals.DebugLogger("微信用户推送消息：" + qMember.Username + "___" + yiyuanEntity.Title);
                //微信用户推送消息
                if (qMember.Wxid != null && qMember.Wxid.Length > 10)
                {
                    NetworkHelper.SetTimeout(ShopOrders.Q_end_time_second * 1000, delegate
                    {
                        Messenger.SendYuanSuccess(go_memberBusiness.LoadEntity(int.Parse(id)).Wxid, "爽乐购", yiyuanEntity.Title, "感谢您的参与，进入个人中心确认收货地址。");
                    });
                }
                //app用户推送消息
                if (qMember.Appwxid != null && qMember.Appwxid.Length > 10)
                {
                    NetworkHelper.SetTimeout(ShopOrders.Q_end_time_second * 1000, delegate
                    {
                        PushHelper.SendAppMsg(id, "中奖啦", "恭喜您在爽乐购中奖了，进入会员中心收货！");
                    });
                }
            }
        }

        /// <summary>
        /// 永乐接口超时再发放(每分钟调用一次)
        /// </summary>
        private void yolly(HttpContext context)
        {/*
            string id = context.Request["id"];
            if (string.IsNullOrEmpty(id)) id = "";
            string sign = context.Request["sign"];
            if (string.IsNullOrEmpty(sign)) sign = "";
            if (!Globals.ValidateSignAppInner(sign, id))
            {
                context.Response.Write("{\"state\":2}");    //未知签名
                return;
            }

            //调用接口发放 获取发送失败的信息
            DataTable dtData = go_yolly_orderinfoBusiness.GetListData("status<>1", "*", "usetime", 3);
  
            //发送失败的，删除重发
            DataRow[] drs = dtData.Select("status=2");
            foreach (DataRow dr in drs)
            {
                DataRow drNew = dtData.NewRow();
                drNew["status"] = 0;
                dtData.Rows.Add(drNew);
            }
            for (int i = 0; i < drs.Length; i++)
            {
                drs[i].Delete();
            }
            CustomsBusiness.CommitDataTable(dtData, "Select * From go_yolly_orderinfo");

            //发送超时的，删除重发
            DataRow[] drs2 = dtData.Select("status=0 And ...");

                foreach (DataRow dr in dtData.Rows)
                {
                    string str1 = DateTime.Now.ToString("yyyyMMddHHmmssss");
                    string str2 = "13349946975";
                    string str3 = "1";
                    string str4 = yollyinterface.GenerateYollyID(yollyinterface.usetype.Phone);
                    //string strs = yollyinterface.APIyolly(new object[] { str1, str2, str3, str4 }, yollyinterface.usetype.Phone);
                    bool b = true;
                    if (b)
                    {
                        dr["status"] = 1;
                        System.Threading.Thread.Sleep(10);
                    }
                    else
                        break;
                }
            if (dtData.GetChanges() != null)
            {
                CustomsBusiness.CommitDataTable(dtData, "Select * From go_yolly_orderinfo");
            }
            */
            

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

}