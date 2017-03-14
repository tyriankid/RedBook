using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Taotaole.Bll;
using Taotaole.Business;
using Taotaole.Common;
using Taotaole.Model;
using YH.Utility;
using YH.Weixin.MP.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Taotaole.Cache;
namespace Taotaole.IApi
{
    /// <summary>
    /// taotaolepay 的摘要说明
    /// </summary>
    public class taotaolepay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string action = context.Request["action"];
            switch (action)
            {
                case "userpayment":         //用户付款(校验)
                    userpayment(context);
                    break;
                case "userpaymenttuan":         //用户团购付款(校验)
                    userpaymenttuan(context);
                    break;
                case "userpaymentsubmit":   //用户提交付款
                    userpaymentsubmit(context);
                    break;
                case "userpaymentQuan": // 直接购买付款验证
                    userpaymentQuan(context);
                    break;
                case "userpaymentQuanSubmit": // 用户提交直接购买付款
                    userpaymentQuanSubmit(context);
                    break;
                case "userpaymenttuansubmit":   //用户提交团购付款
                    userpaymenttuansubmit(context);
                    break;
                case "appwxrecharge":      //(App)微信充值
                    appwxrecharge(context);
                    break;
            }
        }
        /// <summary>
        /// 用户付款
        /// </summary>
        private void userpayment(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            //防沉迷
            string  isPayOut="";
            string  starTime=DateTime.Now.ToString("yyyy-MM-dd ")+" 00:00:00";
            string  endTime=DateTime.Now.ToString("yyyy-MM-dd ")+" 23:59:59";
            string strwhere=string.Format("uid={0} and  '{1}'<time  and  time<'{2}'",uid,starTime,endTime);
            DataTable dtOrderInfo = go_ordersBusiness.GetListData(strwhere, "SUM(money) as sumMoney");
            if (dtOrderInfo.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtOrderInfo.Rows[0]["sumMoney"].ToString()))
                {   
                    if(decimal.Parse(dtOrderInfo.Rows[0]["sumMoney"].ToString())>4999)
                    {
                        isPayOut ="11";
                    }
                }
            }

            //验证用户ID、商品ID、购买数量
            string shopid = context.Request["shopid"];
            string quantity = context.Request["quantity"];
            if (string.IsNullOrEmpty(shopid) || string.IsNullOrEmpty(quantity) || !PageValidate.IsNumberPlus(uid) || !PageValidate.IsNumberPlus(shopid) || !PageValidate.IsNumberPlus(quantity))
            {
                context.Response.Write("{\"state\":3}");    //缺少参数
                return;
            }

            //判断商品是否过期
            go_yiyuanEntity entity = go_yiyuanBusiness.LoadEntity(int.Parse(shopid));
            if (entity == null)
            {
                context.Response.Write("{\"state\":4}");    //商品不存在
                return;
            }
            if (entity.Shengyurenshu == 0)
            {
                if (entity.Qishu == entity.Maxqishu)
                {
                    context.Response.Write("{\"state\":5}");    //商品已售罄
                    return;
                }
                string newshopid=go_yiyuanBusiness.GetScalar(string.Format("productid={0}", entity.Productid), "yId", " qishu desc").ToString();
                context.Response.Write("{\"state\":4,\"newshopid\":" + newshopid + "}");    //6商品已过期
                return;
            }
            
            //调用支付
            int iquantity=int.Parse(quantity);
            if (int.Parse(quantity) > entity.Shengyurenshu) iquantity = entity.Shengyurenshu;
            decimal paymoeny = iquantity * entity.Yunjiage;
            Guid aPIsubmitcode=Guid.NewGuid();
            System.Web.HttpCookie cookie = new System.Web.HttpCookie("APIsubmitcode")
            {
                Value =aPIsubmitcode.ToString(),
                Expires = System.DateTime.Now.AddMinutes(30)
            };
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            go_memberEntity memberEntity = go_memberBusiness.LoadEntity(int.Parse(uid));
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string where = "[state]=1 AND uid=" + uid + " AND (amount=0 or amount<=" + paymoeny + ") AND (overtime is null or overtime>'" + now + "') AND (senttime is null or senttime <'" + now + "') AND discount<=" + paymoeny;
            DataTable redlist = go_activity_codeBusiness.GetListData(where,"id,codetitle,amount,discount,overtime");

            //缓存移出
            CacheHelper.DelCache("pay_" + uid);

            //输出JSON
            string result = string.Format("{{\"isPayOut\":\"{0}\",\r\"state\":{1},\r\"quantity\":\"{2}\",\r\"paymoney\":\"{3}\",\r\"APIsubmitcode\":\"{4}\",\r\"usemoney\":\"{5}\",\r\"useredpaper\":\"{6}\",\r\"money\":\"{7}\",\r\"data\":"
                , isPayOut, 0, iquantity, paymoeny.ToString("0.00"), aPIsubmitcode.ToString(), (memberEntity.Money >= paymoeny) ? 1 : 0, (redlist.Rows.Count > 0) ? 1 : 0, memberEntity.Money);
            result += JsonConvert.SerializeObject(redlist, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }

        /// <summary>
        /// 用户付款
        /// </summary>
        private void userpaymentQuan(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }

            //防沉迷
            string isPayOut = "";
            string starTime = DateTime.Now.ToString("yyyy-MM-dd ") + " 00:00:00";
            string endTime = DateTime.Now.ToString("yyyy-MM-dd ") + " 23:59:59";
            string strwhere = string.Format("uid={0} and  '{1}'<time  and  time<'{2}'", uid, starTime, endTime);
            DataTable dtOrderInfo = go_ordersBusiness.GetListData(strwhere, "SUM(money) as sumMoney");
            if (dtOrderInfo.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtOrderInfo.Rows[0]["sumMoney"].ToString()))
                {
                    if (decimal.Parse(dtOrderInfo.Rows[0]["sumMoney"].ToString()) > 4999)
                    {
                        isPayOut = "11";
                    }
                }
            }
            //验证用户ID、商品ID、购买数量
            string quanid = context.Request["quanid"];
            if (string.IsNullOrEmpty(quanid) || !PageValidate.IsNumberPlus(quanid))
            {
                context.Response.Write("{\"state\":3}");    //缺少参数
                return;
            }



            Guid aPIsubmitcode = Guid.NewGuid();
            System.Web.HttpCookie cookie = new System.Web.HttpCookie("APIsubmitcode")
            {
                Value = aPIsubmitcode.ToString(),
                Expires = System.DateTime.Now.AddMinutes(30)
            };
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            //判断用户余额是否足够支付
            go_memberEntity memberEntity = go_memberBusiness.LoadEntity(int.Parse(uid));
            go_zhigouEntity zhigouEntity = go_zhigouBusiness.LoadEntity(int.Parse(quanid));

            //go_productsEntity productEntity = go_productsBusiness.LoadEntity(int.Parse(productid));
            int leftMoneyEnough = 0;//默认余额不足
            if (memberEntity.Money >= zhigouEntity.QuanMoney)
            {
                leftMoneyEnough = 1;
            }
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string where = "[state]=1 AND uid=" + uid + " AND (amount=0 or amount<=" + zhigouEntity.QuanMoney + ") AND (overtime is null or overtime>'" + now + "') AND (senttime is null or senttime <'" + now + "') AND discount<=" + zhigouEntity.QuanMoney;
            DataTable redlist = go_activity_codeBusiness.GetListData(where, "id,codetitle,amount,discount,overtime");

            //缓存移出
            CacheHelper.DelCache("pay_" + uid);

            //输出JSON
            string result = string.Format("{{\"isPayOut\":{0},\"moneyEnough\":{1},\"leftmoney\":{2},\"paymoney\":{3},\"state\":{4},\"data\":",isPayOut, leftMoneyEnough, memberEntity.Money, zhigouEntity.QuanMoney, 0);
            result += JsonConvert.SerializeObject(redlist, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }


        //用户提交团购付款
        private void userpaymenttuan(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }

            //验证参数合法性
            string businessId = context.Request["businessId"];
            businessId = SecurityHelper.Decrypt(Globals.TaotaoleWxKey, businessId);
            if (string.IsNullOrEmpty(businessId)) businessId = context.Request["businessId"];
            string typeid = context.Request["typeid"];
            if (string.IsNullOrEmpty(businessId) || string.IsNullOrEmpty(typeid)
                || !PageValidate.IsNumberPlus(businessId) || !PageValidate.IsNumberPlus(typeid)
                || !(int.Parse(typeid) == 1 || int.Parse(typeid) == 2))
            {
                context.Response.Write("{\"state\":3}");    //缺少参数
                return;
            }
            int businessmxId = 0; ;

            //参团判断
            if (typeid == "2")
            {
                go_tuan_listinfoEntity entity = go_tuan_listinfoBusiness.LoadEntity(int.Parse(businessId), DbServers.DbServerName.LatestDB);
                if (entity == null)
                {
                    context.Response.Write("{\"state\":4}");    //团购已过期
                    return;
                }
                if (entity.Remain_num <= 0)
                {
                    context.Response.Write("{\"state\":5}");    //团购数已满
                    return;
                }
                businessId = entity.TuanId.ToString();
                businessmxId = entity.TuanlistId;

                int ordersCount = int.Parse(go_ordersBusiness.GetScalar(string.Format("ordertype='tuan' And businessId in(Select tuanlistid From go_tuan_listinfo Where tuanid={0}) And uid={1}", businessId, uid)
                    , "count(*)", null, DbServers.DbServerName.LatestDB).ToString());
                if (ordersCount > 0)
                {
                    context.Response.Write("{\"state\":6}");    //已参过此团
                    return;
                }
            }
            else
            {
                /*开团不限制
                int ordersCount = int.Parse(go_ordersBusiness.GetScalar(string.Format("ordertype='tuan' And businessId in(Select tuanlistId From go_tuan_listinfo Where tuanId={0}) And uid={1}", businessId, uid)
                    , "count(*)", null, DbServers.DbServerName.LatestDB).ToString());
                if (ordersCount > 0)
                {
                    context.Response.Write("{\"state\":6}");    //已参过此团
                    return;
                }*/
            }

            //判断团购是否已过期
            DataTable dtTuan = go_tuanBusiness.GetListData(string.Format("tId={0} And is_delete=0 And GETDATE() between start_time and ISNULL(end_time,'9999-12-30')", businessId)
                    , "max_sell,deadline", null, 0, DbServers.DbServerName.LatestDB);
            if (dtTuan.Rows.Count == 0 || (dtTuan.Rows[0]["deadline"] != DBNull.Value && DateTime.Parse(dtTuan.Rows[0]["deadline"].ToString()) <= DateTime.Now))
            {
                context.Response.Write("{\"state\":4}");    //团购已过期
                return;
            }

            //开团判断
            if (typeid == "1")
            {
                DataRow drTuan = dtTuan.Rows[0];
                if (int.Parse(drTuan["max_sell"].ToString()) > 0)
                {
                    int canyurenshu = int.Parse(go_tuan_listinfoBusiness.GetScalar("tuanId=" + businessId, "count(*)", null, DbServers.DbServerName.LatestDB).ToString());
                    if (canyurenshu >= int.Parse(drTuan["max_sell"].ToString()))
                    {
                        context.Response.Write("{\"state\":5}");    //团购数已满
                        return;
                    }
                }
            }

            //输出JSON
            context.Response.Write("{\"state\":0}"); 
        }

        /// <summary>
        /// 用户提交付款
        /// </summary>
        private void userpaymentsubmit(HttpContext context)
        {
            string paytype = "1";//付款来源(1微信公众号、2APP)
            if (string.IsNullOrEmpty(context.Request["uid"])) paytype = "2";

            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }

            //判断频繁调用
            if (!CacheHelper.IsOK("pay_" + uid))
            {
                Globals.DebugLogger("频繁调用接口【userpaymentsubmit】_" + uid, "API_Log.txt");
                return;
            }

            //验证用户ID、商品ID、购买数量
            string shopid = context.Request["shopid"];
            string productid = context.Request["productid"];
            if (string.IsNullOrEmpty(shopid) || !PageValidate.IsNumberPlus(shopid)) shopid = productid;
            string quantity = context.Request["quantity"];
            string redpaper = context.Request["redpaper"];
            if (string.IsNullOrEmpty(redpaper) || !PageValidate.IsNumberPlus(redpaper)) redpaper = "0";
            if (string.IsNullOrEmpty(shopid) || string.IsNullOrEmpty(quantity) 
                || !PageValidate.IsNumberPlus(uid) || !PageValidate.IsNumberPlus(shopid) || !PageValidate.IsNumberPlus(quantity))
            {
                context.Response.Write("{\"state\":3}");    //缺少参数
                return;
            }

            //商品业务ID
            if (!string.IsNullOrEmpty(productid) && PageValidate.IsNumberPlus(productid) )
            {
               object o_yId= go_yiyuanBusiness.GetScalar(string.Format("originalid={0}", productid), "yId", "qishu desc");
               if (o_yId != null)
               {
                   shopid = o_yId.ToString();
               }
            }

            //判断商品是否过期
            go_yiyuanEntity entity = go_yiyuanBusiness.LoadEntity(int.Parse(shopid));
            if (entity == null)
            {
                context.Response.Write("{\"state\":4}");    //商品不存在
                return;
            }
            if (entity.Shengyurenshu == 0)
            {
                if (entity.Qishu == entity.Maxqishu)
                {
                    context.Response.Write("{\"state\":5}");    //商品已售罄
                    return;
                }
                string newshopid = go_yiyuanBusiness.GetScalar(string.Format("productid={0}", entity.Productid), "yId", " qishu desc").ToString();
                context.Response.Write("{\"state\":4,\"newshopid\":" + newshopid + "}");    //6商品已过期
                return;
            }

            //判断订单唯一性(不可重复提交)
            string APIsubmitcode = context.Request["APIsubmitcode"];
            if (string.IsNullOrEmpty(APIsubmitcode)) APIsubmitcode = "0";
            System.Web.HttpCookie cookie = new System.Web.HttpCookie("APIsubmitcode");
            if (cookie != null && cookie.Value == APIsubmitcode)
            {
                System.Web.HttpContext.Current.Response.Cookies.Remove("APIsubmitcode");
            }
            else
            {
               // context.Response.Write("{\"state\":6}");    //请不要重复提交
                //return;
            }
            string ip = context.Request["ip"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = NetworkHelper.GetBuyIP();
            }

            //支付
            int state = -1;
            int q_uid = -1;
            string orderid = ShopOrders.GenerateOrderID(ShopOrders.BusinessType.yuan);
            int[] iarray = CustomsBusiness.CreateOrder(orderid, int.Parse(uid), int.Parse(shopid), int.Parse(quantity), int.Parse(redpaper), ShopOrders.Q_end_time_second, ip,paytype);
            state = iarray[0];//购买成功
            q_uid = iarray[1];//中奖人(0表示未开奖——)

            //移出缓存
            CacheHelper.DelCache("pay_" + uid);

            //处理支付结果
            int re=state;
            switch (state)
            {
                case 0:
                    re = 0;    //购买成功
                    //新ID首次支付5元
                    try
                    {
                        if (!go_activity_userBusiness.valQualification(uid, "23"))//验证用户是否获得资格
                        {
                            go_activityEntity ae = go_activityBusiness.LoadEntity(23);
                            if (ae.Shopnum <= int.Parse(quantity) && ae.Starttime < DateTime.Now && ae.Endtime > DateTime.Now)//验证是否满足活动购买商品数量 和活动时间
                            {
                                JArray firstfiverange = JsonConvert.DeserializeObject<JArray>(ae.Use_range.ToString());
                                bool valUseRange = false;
                                if (firstfiverange == null)
                                {
                                    valUseRange = true;
                                }
                                else
                                {
                                    for (int ir = 0; ir < firstfiverange.Count; ir++)
                                    {
                                        if (productid == (firstfiverange[ir]["id"].ToString()))
                                        {
                                            valUseRange = true;
                                        }
                                    }
                                }
                                //发放用户活动资格
                                if (valUseRange)
                                {
                                    go_activity_userBusiness.Qualification(uid, "23");
                                }
                            }
                        }
                        if (q_uid != 0)//已开奖 触发活动流程【出现异常】
                        {
                            go_activity_codeBusiness.sentBidRedpack(shopid);//根据业务ID触发的活动
                        }

                        //网吧渠道商业务
                        if (ShopOrders.Wangba_activity_shopid.Split(',').Contains(entity.originalid.ToString()))//判断id是否在中奖商品里面
                        {
                            go_channel_activitydetailBusiness.AddActivityDetails(int.Parse(uid), entity.originalid, orderid);
                           
                        }
                        //给中奖人发送推送消息
                        if (q_uid != 0)
                        {
                            //根据用户来源类型推送消息,分别是微信用户,app用户
                            go_memberEntity qMember = go_memberBusiness.LoadEntity(q_uid);
                            if (qMember != null)
                            {
                                //微信用户推送消息
                                if (qMember.Wxid!=null && qMember.Wxid.Length > 10)
                                {
                                    NetworkHelper.SetTimeout(ShopOrders.Q_end_time_second * 1000, delegate
                                    {
                                        Messenger.SendYuanSuccess(go_memberBusiness.LoadEntity(q_uid).Wxid, "爽乐购", entity.Title, "感谢您的参与，进入个人中心确认收货地址。");
                                    });
                                }
                                //app用户推送消息
                                if (qMember.Appwxid != null && qMember.Appwxid.Length > 10)
                                {
                                    NetworkHelper.SetTimeout(ShopOrders.Q_end_time_second * 1000, delegate
                                    {
                                        PushHelper.SendAppMsg(q_uid.ToString(), "中奖啦", "恭喜您在爽乐购中奖了，进入会员中心收货！");
                                    });
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Globals.DebugLogger("支付成功后出现BUG:  "+ex.Message);
                    }

                    break;
                case -1:
                    re = 7;    //请不要重复提交订单
                    break;
                case 1:
                    re = 8;    //用户不存在
                    break;
                case 2:
                    re = 9;    //余额不足
                    break;
                case 3:
                    re = 10;    //该期已截止
                    break;
                case 4:
                    re = 11;    //提交订单失败,请稍后重试
                    break;
            }

            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"orderid\":\"{1}\""
                , re, (re==0)?orderid:"");
            result += "}";
            context.Response.Write(result);
        }


        /// <summary>
        /// 用户提交直购商品付款
        /// </summary>
        private void userpaymentQuanSubmit(HttpContext context)
        {
            string paytype = "1";//付款来源(1微信公众号、2APP)
            if (string.IsNullOrEmpty(context.Request["uid"])) paytype = "2";

            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            //红包id
            string redpaper = context.Request["redpaper"];
            if (string.IsNullOrEmpty(redpaper) || !PageValidate.IsNumberPlus(redpaper)) redpaper = "0";
            //判断频繁调用
            if (!CacheHelper.IsOK("pay_" + uid))
            {
                Globals.DebugLogger("频繁调用接口【userpaymentsubmit】_" + uid, "API_Log.txt");
                return;
            }

            //验证商品ID
            //string shopid = context.Request["shopid"];
            string quanid = context.Request["quanid"];
            if (string.IsNullOrEmpty(quanid) || !PageValidate.IsNumberPlus(uid) || !PageValidate.IsNumberPlus(quanid))
            {
                context.Response.Write("{\"state\":3}");    //缺少参数
                return;
            }


            //判断商品是否有库存
            go_zhigouEntity entity = go_zhigouBusiness.LoadEntity(int.Parse(quanid));
            if (entity == null)
            {
                context.Response.Write("{\"state\":4}");    //商品不存在
                return;
            }
            if (entity.Stock <= 0)
            {
                context.Response.Write("{\"state\":5}");    //商品已售罄
                return;
            }

            //判断订单唯一性(不可重复提交)
            string APIsubmitcode = context.Request["APIsubmitcode"];
            if (string.IsNullOrEmpty(APIsubmitcode)) APIsubmitcode = "0";
            System.Web.HttpCookie cookie = new System.Web.HttpCookie("APIsubmitcode");
            if (cookie != null && cookie.Value == APIsubmitcode)
            {
                System.Web.HttpContext.Current.Response.Cookies.Remove("APIsubmitcode");
            }
            else
            {
                // context.Response.Write("{\"state\":6}");    //请不要重复提交
                //return;
            }
            string ip = context.Request["ip"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = NetworkHelper.GetBuyIP();
            }

            //支付
            int state = -1;
            int q_uid = -1;
            string orderid = ShopOrders.GenerateOrderID(ShopOrders.BusinessType.quan);
            int iarray = CustomsBusiness.CreateQuanOrder(orderid, int.Parse(uid), int.Parse(quanid),int.Parse(redpaper),ip, paytype);
            state = iarray;//购买成功

            //移出缓存
            CacheHelper.DelCache("pay_" + uid);

            //处理支付结果
            int re = state;
            switch (state)
            {
                case 0:
                    re = 0;    //购买成功

                    try
                    {
                        go_activity_codeBusiness.sendQuanidRedpack(orderid);//根据直购业务ID触发的活动
                    }
                    catch (Exception ex)
                    {
                        Globals.DebugLogger("支付成功后出现BUG:  " + ex.Message);
                    }

                    break;
                case -1:
                    re = 7;    //请不要重复提交订单
                    break;
                case 1:
                    re = 8;    //用户不存在
                    break;
                case 2:
                    re = 9;    //余额不足
                    break;
                case 4:
                    re = 11;    //提交订单失败,请稍后重试
                    break;
            }

            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"orderid\":\"{1}\""
                , re, (re == 0) ? orderid : "");
            result += "}";
            context.Response.Write(result);
        }

        /// <summary>
        /// 用户提交付款(团购)
        /// </summary>
        private void userpaymenttuansubmit(HttpContext context)
        {
            //string _uid = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, "1948");
            //string _sign = SecurityHelper.GetSign(_uid);

            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }

            //验证参数合法性
            string businessId = context.Request["businessId"];
            string typeid = context.Request["typeid"];
            string tuanorderid = context.Request["tuanorderid"];
            businessId = SecurityHelper.Decrypt(Globals.TaotaoleWxKey, businessId);
            if (string.IsNullOrEmpty(businessId)) businessId = context.Request["businessId"];
            if (string.IsNullOrEmpty(businessId) || string.IsNullOrEmpty(typeid) || string.IsNullOrEmpty(tuanorderid)
                || !PageValidate.IsNumberPlus(businessId) || !PageValidate.IsNumberPlus(typeid) 
                || !(int.Parse(typeid) == 1 || int.Parse(typeid) == 2))
            {
                context.Response.Write("{\"state\":3}");    //缺少参数
                return;
            }

            //支付
            string orderid = ShopOrders.GenerateOrderID(ShopOrders.BusinessType.tuan);
            int[] states = CustomsBusiness.CreateOrderTuan(orderid, int.Parse(uid), int.Parse(businessId), int.Parse(typeid), ShopOrders.Q_end_time_second, tuanorderid);

            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"orderid\":\"{1}\",\r\"businesstype\":\"{2}\",\r\"tid\":\"{3}\",\r\"tuanlistId\":\"{4}\""
                , states[0], (states[0] == 0) ? orderid : "", states[1], states[2], states[3]);
            result += "}";
            context.Response.Write(result);
        }

        /// <summary>
        /// 微信充值
        /// </summary>
        private void appwxrecharge(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }

            //验证参数合法性
            string type = context.Request["type"];
            if (string.IsNullOrEmpty(type)) type = "recharge";
            string paytype = context.Request["paytype"];
            if (string.IsNullOrEmpty(paytype)) paytype = "APP微信支付";
            if (paytype=="2") paytype = "APP支付宝支付";
            string money = context.Request["money"];
            if (string.IsNullOrEmpty(money) || !PageValidate.IsDecimal(money))
            {
                context.Response.Write("{\"state\":3}");    //缺少参数
                return;
            }

            //支付方式：微信支付、APP微信支付、APP支付宝支付
            switch (paytype)
            { 
                case "2":
                case "wxpay":
                    paytype = "APP微信支付";
                    break;
                case "3":
                case "alipay":
                    paytype = "APP支付宝支付";
                    break;
            }

            //分业务处理
            string memo = "";
            switch (type)
            {
                case "add":    //仅充值
                case "recharge":
                    memo = "add";
                    break;
                case "yuan":        //一元购，购买前充值
                    memo = "yuan";
                    break;
                case "tuan":
                    memo = "tuan";  //团购在线支付
                    break;
            }

            //生成充值记录（待付款）
            string orderid = ShopOrders.GenerateOrderID(ShopOrders.BusinessType.add);//用户充值订单ID
            go_member_addmoney_recordEntity addmoneyEntity = new go_member_addmoney_recordEntity()
            {
                Uid = int.Parse(uid),
                Code = orderid,
                Money = decimal.Parse(money),
                Pay_type = paytype,
                Status = "未付款",
                Time = DateTime.Now,
                Buy_type = 0,
                Score = (memo=="tuan")?1:0,
                Memo = memo
            };
            bool isSuccess=go_member_addmoney_recordBusiness.SaveEntity(addmoneyEntity, true);

            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"orderid\":\"{1}\""
                , (isSuccess) ? 0 : 4, (isSuccess) ? orderid : "");
            result += "}";
            context.Response.Write(result);
        }



        private void WriteLog(string log)
        {
            System.IO.StreamWriter writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("~/wx_login.txt"));

            writer.WriteLine(System.DateTime.Now);

            writer.WriteLine(log);

            writer.Flush();

            writer.Close();

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