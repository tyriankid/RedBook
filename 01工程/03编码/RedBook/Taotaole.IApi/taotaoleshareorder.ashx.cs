using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Business;
using System.Data;
using YH.Utility;
using Newtonsoft.Json;
using Taotaole.Common;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text.RegularExpressions;
using Taotaole.Bll;
using Taotaole.Model;
using System.Text;
using Taotaole.Cache;

namespace Taotaole.IApi
{
    /// <summary>
    /// taotaoleshareorder 的摘要说明
    /// </summary>
    public class taotaoleshareorder : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "productshaidan": //某个商品晒单所有信息
                    productshaidan(context);
                    break;
                case "shaidandetails"://获取晒单详情
                    shaidandetails(context);
                    break;
                case "shaidanreply": //晒单回复列表
                    shaidanreply(context);
                    break;
                case "huifushaidan": //回复晒单
                    huifushaidan(context);
                    break;
                case "shaidandianzan": //晒单点赞 取消点赞
                    shaidandianzan(context);
                    break;
                case "productaffirm": //商品确认
                    productaffirm(context);
                    break;
                case "affirminfo": //用户确认商品信息
                    affirminfo(context);
                    break;
                case "confirmshouhuo": //确认收货
                    confirmshouhuo(context);
                    break;
                case"allshaidan"://所有晒单信息
                    allshaidan(context);
                    break;
                case "giftConfirm"://积分商城奖品确认
                    giftConfirm(context);
                    break;
                case "quanConfirm"://直购商品确认
                    quanConfirm(context);
                    break;
                case "set":
                    string str1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    System.Web.HttpCookie affirminfocookie = new System.Web.HttpCookie("affirminfo")
                        {
                            Value = str1,
                            Expires = System.DateTime.Now.AddDays(1.0)//设置cookies时间
                        };
                        System.Web.HttpContext.Current.Response.Cookies.Add(affirminfocookie);
                        context.Response.Write("写入COOKIE:" + str1);
                    break;
                case "get":
                    if (context.Request.Cookies["affirminfo"] != null)
                    {
                        string okcode = context.Request.Cookies["affirminfo"].Value.ToString();
                        context.Response.Write("读取COOKIE:" + okcode);
                    }
                    break;
            
            }
        }

        /// <summary>
        /// 某个商品晒单所有信息
        /// 参数：productid 商品ID
        /// 返回状态：state  0获取成功 2未知的签名参数  3商品ID有误  4此商品还没有晒单
        /// </summary>
        /// <param name="context"></param>
        private void productshaidan(HttpContext context)
        {
            #region *********************************参数验证和获取*********************************
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                uid = "0";
            }
            string productid = context.Request["productid"];
            if (string.IsNullOrEmpty(productid) || !PageValidate.IsNumberPlus(productid))
            {
                context.Response.Write("{\"state\":3}");    //商品ID有误
                return;
            }
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            #endregion
            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = "(select m.username,m.img,m.headimg,s.sd_id,s.sd_title,s.sd_content,s.sd_ip,s.sd_photolist,s.sd_time,y.originalid AS productid,s.sd_zan,s.sd_ping,praise=(case when sz.sdhf_userid IS NULL then 0 else 1 end)from go_shaidan s join go_orders o on s.sd_orderid=o.orderId join go_yiyuan y on y.yId=o.businessId left join go_member m on s.sd_userid=m.uid left Join go_shaidan_zan SZ ON S.sd_id=SZ.sd_id And sz.sdhf_userid='" + uid + "'";

            string where = string.Format("where originalid={0}) t", productid);
            string orderby = "sd_time desc";

            //获取用户充值列表及页码
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
            if (dtData.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":4}");    //此商品还没有晒单
                return;
            }

            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }

        /// <summary>
        /// 获取晒单详情
        /// 参数：sign 用户签名  shaidanid 晒单ID
        /// 返回状态state   0获取成功 2未知的签名参数 3晒单ID有误 4获取晒单列表失败
        /// </summary>
        /// <param name="context"></param>
        private void shaidandetails(HttpContext context)
        {
            #region *********************************参数验证和获取*********************************
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                uid = "0";
            }
            string shaidanid = context.Request["shaidanid"];
            if (string.IsNullOrEmpty(shaidanid) || !PageValidate.IsNumberPlus(shaidanid))
            {
                context.Response.Write("{\"state\":3}");    //晒单ID有误
                return;
            }
            #endregion

            //获取晒单相关信息
            DataTable dt = CustomsBusiness.GetListData("  go_shaidan s join go_orders o on s.sd_orderid=o.orderId join go_yiyuan y on y.yId=o.businessId join go_member m on s.sd_userid=m.uid left Join go_shaidan_zan SZ ON S.sd_id=SZ.sd_id And sz.sdhf_userid='" + uid + "'", "s.sd_id= '" + shaidanid + "'", "  m.username,s.sd_id,s.sd_title,s.sd_ip,s.sd_photolist,CONVERT(varchar,s.sd_time,121) as sd_time,y.originalid as productid,s.sd_zan,s.sd_ping,m.headimg,y.q_uid,y.title,y.q_code,y.qishu,o.quantity,y.q_end_time,m.img,praise=(case when sz.sdhf_userid IS NULL or sz.start!=1 then 0 else 1 end)", null, 0, DbServers.DbServerName.ReadHistoryDB);
           
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":4}");    //获取晒单列表失败
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                ShopOrders.SetTouxiang(dr);
            }
            dt.Columns.Remove("headimg");
            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"data\":"
                , (dt.Rows.Count > 0) ? 0 : 1, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }

        /// <summary>
        /// 晒单回复,评论
        /// 参数sign 用户签名 shaidanid晒单ID P页码
        /// 返回状态state 0评论成功 2未知的签名参数 3晒单ID有误 1此晒单暂无评论
        /// </summary>
        /// <param name="context"></param>
        private void shaidanreply(HttpContext context)
        {
            #region *********************************参数验证和获取*********************************
            //string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            //if (string.IsNullOrEmpty(uid))
            //{
            //    context.Response.Write("{\"state\":2}");    //未知的签名参数
            //    return;
            //}
            string shaidanid = context.Request["shaidanid"];
            if (string.IsNullOrEmpty(shaidanid))
            {
                context.Response.Write("{\"state\":3}");    //晒单ID有误
                return;
            }
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            #endregion

            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = "(select sh.sdhf_content,CONVERT(varchar,sh.sdhf_time,121) as sdhf_time,m.username,m.img,m.headimg,m.uid from   go_shaidan s right join go_shaidan_huifu sh on sh.sd_id=s.sd_id  join go_member m on sh.sdhf_userid=m.uid ";
            string where = string.Format("where s.sd_id={0}) t", shaidanid);
            string orderby = "sdhf_time asc";

            //获取用户充值列表及页码
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
            if (dtData.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":1}");    //此晒单暂无评论
                return;
            }

            foreach (DataRow dr in dtData.Rows)
            {
                ShopOrders.SetTouxiang(dr);
            }
            dtData.Columns.Remove("headimg");
            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, "");
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }

        /// <summary>
        /// 回复晒单
        /// 参数sign 用户签名 shaidanid晒单ID content回复内容
        /// 返回状态state 0回复成功 2未知的签名参数 3你发表次数过多哦 4你发表过于频繁 5发表失败
        /// </summary>
        private void huifushaidan(HttpContext context)
        {
            #region *********************************参数验证和获取*********************************
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string shaidanid = context.Request["shaidanid"];
            string content = context.Request["content"];
            #endregion

            //根据信息查询用户一个订单发表过多少次，以及判断最后一次发表时间
            DataTable dt = go_shaidan_huifuBusiness.GetListData("sd_id='" + shaidanid + "' and sdhf_userid='" + uid + "'", "*", "sdhf_time desc", 0, DbServers.DbServerName.ReadHistoryDB);
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows.Count >= 10)
                {
                    context.Response.Write("{\"state\":3}");    //你发表次数过多哦
                    return;
                }
                if (ShopSms.Get_Time_Difference(Convert.ToDateTime(dt.Rows[0]["sdhf_time"])) <= ShopOrders.Publish_Spacing_Interval)
                {
                    context.Response.Write("{\"state\":4}");    //你发表过于频繁
                    return;
                }
            }
            go_shaidan_huifuEntity huifuEntity = new go_shaidan_huifuEntity
            {
                Sd_id=Convert.ToInt32( shaidanid),
                Sdhf_time=DateTime.Now,
                Sdhf_userid=Convert.ToInt32(uid),
                Sdhf_content=content
            };
            go_shaidanEntity shaidanEntity = go_shaidanBusiness.LoadEntity(Convert.ToInt32(shaidanid));
            shaidanEntity.Sd_ping = shaidanEntity.Sd_ping + 1;
            if (go_shaidan_huifuBusiness.SaveEntity(huifuEntity, true, DbServers.DbServerName.LatestDB))
            {
                if (go_shaidanBusiness.SaveEntity(shaidanEntity, false))
                {
                    context.Response.Write("{\"state\":0}");    //发表成功
                    return;
                }
            }
            context.Response.Write("{\"state\":5}");    //发表失败
            return;
        }

        /// <summary>
        /// 晒单点赞，取消点赞
        /// 参数sing 用户签名 shaidanid晒单ID
        /// 返回状态state 0操作成功 2未知的签名参数 3操作过于频繁   zan 0代表取消赞 1代表以赞
        /// </summary>
        private void shaidandianzan(HttpContext context)
        {
            #region *********************************参数验证和获取*********************************
            bool isSave = true;
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string shaidanid = context.Request["shaidanid"];
            #endregion

            DataTable dt = go_shaidan_zanBusiness.GetListData("sd_id='" + shaidanid + "' and sdhf_userid='" + uid + "'", "*", null, 1, DbServers.DbServerName.LatestDB);
            go_shaidan_zanEntity zanEntity;
            go_shaidanEntity shaidanEntity;
            if (dt.Rows.Count == 0)
            {
                isSave = true;
                zanEntity= new go_shaidan_zanEntity { 
                Sd_id=Convert.ToInt32(shaidanid),
                Sdhf_time=DateTime.Now,
                Sdhf_userid=Convert.ToInt32(uid),
                Start=1,
                Counts=1
                };
                shaidanEntity = go_shaidanBusiness.LoadEntity(Convert.ToInt32(shaidanid));
                shaidanEntity.Sd_zan = shaidanEntity.Sd_zan + 1;
            }
            else
            {
                isSave = false;
                zanEntity = go_shaidan_zanBusiness.LoadEntity(Convert.ToInt32(dt.Rows[0]["id"]), DbServers.DbServerName.LatestDB);
                //判断规定时间内点赞次数
                if (zanEntity.Counts >= 4 && ShopSms.Get_Time_Difference(zanEntity.Sdhf_time) <= ShopOrders.Dianzan_Spacing_Interval)
                {
                    context.Response.Write("{\"state\":3}");    //操作过于频繁
                    return;
                }
                zanEntity.Start = zanEntity.Start == 0 ? 1 : 0;
                zanEntity.Counts = zanEntity.Counts + 1;
                zanEntity.Sdhf_time = DateTime.Now;
                shaidanEntity = go_shaidanBusiness.LoadEntity(Convert.ToInt32(shaidanid));
                if (zanEntity.Start == 1)
                {
                    shaidanEntity.Sd_zan = shaidanEntity.Sd_zan + 1;
                }
                else
                {
                    shaidanEntity.Sd_zan = shaidanEntity.Sd_zan - 1;
                }
            }
            if (go_shaidan_zanBusiness.SaveEntity(zanEntity, isSave))
            {
                if (go_shaidanBusiness.SaveEntity(shaidanEntity, false))
                {
                    context.Response.Write("{\"state\":0,\"zan\":" + zanEntity .Start+ "}");    //操作成功  //0代表取消赞 1代表以赞
                    return;
                }
            }

        }

        /// <summary>
        /// 直购商品确认展示接口
        /// </summary>
        /// <param name="context"></param>
        /// <summary>
        private void quanConfirm(HttpContext context)
        {
            #region ***************************参数验证和获取***************************
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            //uid = "11684";
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":\"2\"}");    //未知的签名参数
                return;
            }
            string orderid = context.Request["orderid"];
            if (string.IsNullOrEmpty(orderid))
            {
                context.Response.Write("{\"state\":\"3\"}");    //订单ID错误
                return;
            }
            /*
            string shopid = context.Request["shopid"];
            if (string.IsNullOrEmpty(shopid))
            {
                context.Response.Write("{\"state\":\"4\"}");    //一元购ID错误
                return;
            }
             */ 
            #endregion

            string orderType = "";//订单类型,同businesstype.ordertype

            //获取订单信息
            DataTable dtOrder = go_ordersBusiness.LoadData(orderid);
            if (dtOrder.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":\"5\"}");    //获取订单信息失败
                return;
            }
            DataTable dt = null;
            orderType = dtOrder.Rows[0]["ordertype"].ToString();

            switch (orderType)
            {
                case "quan":
                    dt = CustomsBusiness.GetListData(" go_orders [go]  inner join go_zhigou gz on [go].businessid = gz.quanid inner join go_products gp on gz.productid = gp.productid join go_member gm on [go].uid = gm.uid", "[go].orderId='" + orderid + "' and [go].uid='" + uid + "'", "gp.typeid,gp.title,gp.thumb,gp.status,[go].sendtime,[go].time,[go].orderid,gp.money,gm.mobile", null, 1, DbServers.DbServerName.LatestDB);
                    break;
            }
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":\"5\"}");    //获取订单信息失败
                return;
            }
            //DataTable dts = CustomsBusiness.GetListData("go_yiyuan y join go_orders o on o.ordertype='yuan' And y.yId=o.businessId join go_products p on y.productid=p.productid join go_member m on y.q_uid=m.uid", "orderId='" + orderid + "' and y.q_uid='" + uid + "'", "p.typeid, y.title,thumb,y.q_end_time,o.status,p.money,m.mobile,o.orderId ,(select sum(t.quantity) as quantity from (select  y.title,thumb, o.quantity,o.status,o.orderId from go_yiyuan y join go_orders o on y.yId=o.businessId join go_products p on y.productid=p.productid where  businessId='" + shopid + "' and uid='" + uid + "' ) t ) as quantity", null, 1, DbServers.DbServerName.LatestDB);
            string typeid = "0";
            //string status = dtOrder.Rows[0]["status"].ToString();
            DataTable dts = null;
            switch (dtOrder.Rows[0]["ordertype"].ToString())
            {
                case "quan"://直购商品
                    dts = CustomsBusiness.GetListData("go_orders [go] inner join go_zhigou gz on [go].businessid = gz.quanid inner join go_products gp on gz.productid = gp.productid  ", "[go].orderId='" + orderid + "' and [go].uid='" + uid + "'", "gp.typeid,[go].status", null, 1, DbServers.DbServerName.LatestDB);
                    break;
            }
            //实例化订单实体类
            go_ordersEntity ordersEntity;
            go_card_rechargeEntity rechargeEntity=null;
            string result = "";
            string kh = "";
            string km = "";
            string id = "";
            string info = null;
            //1代表商品类型为卡密，若商品类型为卡密，则更改订单状态为“待确认”，同时返回卡号、密码给用户   若商品类型为实物、游戏，则更改订单状态为“代发货”
            DataTable drcard = CustomsBusiness.GetListData("go_card_recharge c join go_orders o on c.orderid=o.orderId", "o.orderId='" + dt.Rows[0]["orderid"].ToString() + "'", "c.code,c.codepwd,c.usetime,c.usebeizhu,c.usetype,o.status", null, 0, DbServers.DbServerName.LatestDB);
            ordersEntity = go_ordersBusiness.LoadEntity(dt.Rows[0]["orderid"].ToString());
            //商品类型为：卡号卡密，购买话费所得   //商品类型为:Q币，购买Q币所得
            if (dt.Rows[0]["typeid"].ToString() == "1" || dt.Rows[0]["typeid"].ToString() == "3")
            {
                switch (ordersEntity.Status)
                {
                    case "待确认":
                        #region 待确认
                        int money = Convert.ToInt32(dt.Rows[0]["money"]);
                        if (string.IsNullOrEmpty(ordersEntity.Order_address_info))//发送卡密
                        {
                            int num = Convert.ToInt32(dt.Rows[0]["money"]);
                            if (num > 100)
                            {
                                num = num / 100;
                                money = money / num;
                            }
                            else
                            {
                                money = Convert.ToInt32(dt.Rows[0]["money"]);
                                num = 1;
                            }
                            for (int i = 1; i <= num; i++)
                            {
                                DataTable kmdt = CustomsBusiness.GetListData("go_card_recharge", "orderId='' and uid='0' ", "*", "rechargetime asc",i, DbServers.DbServerName.LatestDB);
                                kh = kmdt.Rows[0]["code"].ToString();
                                km = kmdt.Rows[0]["codepwd"].ToString();
                                id = kmdt.Rows[0]["id"].ToString();
                                ordersEntity = go_ordersBusiness.LoadEntity(orderid);
                                ordersEntity.Phone = dt.Rows[0]["mobile"].ToString();
                                ordersEntity.Status = "待确认";
                                info += kh + "☆" + km + "★";
                                ordersEntity.Sendtime = Convert.ToDateTime(dt.Rows[0]["time"]);
                                rechargeEntity = go_card_rechargeBusiness.LoadEntity(Convert.ToInt32(id), DbServers.DbServerName.LatestDB);
                                rechargeEntity.OrderId = orderid;
                                rechargeEntity.Uid = Convert.ToInt32(uid);
                                rechargeEntity.Money =money;
                                if (!go_card_rechargeBusiness.SaveEntity(rechargeEntity, false, DbServers.DbServerName.LatestDB))
                                {
                                    context.Response.Write("{\"state\":\"8\"}");    //保存失败
                                    return;
                                }
                            }
                            ordersEntity.Order_address_info = info;
                            if (!go_ordersBusiness.SaveEntity(ordersEntity, false, DbServers.DbServerName.LatestDB))
                            {
                                context.Response.Write("{\"state\":\"8\"}");    //保存失败
                                return;
                            }
                        }
                        DataTable okdt = go_card_rechargeBusiness.GetListData("orderid='" + orderid + "' and isrepeat='0' and uid='" + uid + "'", "*", null, 1, DbServers.DbServerName.LatestDB);
                        //返回已经发放的卡密
                        result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"info\":\"{6}\",\r\"phone\":\"{7}\",\r\"time\":\"{8}\",\r\"kahao\":\"{9}\",\r\"kami\":\"{10}\",\r\"sendtime\":\"{11}\",\r\"imgroot\":\"{12}\",\r\"mobile\":\"{13}\",\r\"money\":\"{14}\""
                            , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"],  dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, dt.Rows[0]["time"], okdt.Rows[0]["code"], okdt.Rows[0]["codepwd"], ordersEntity.Sendtime, Globals.G_UPLOAD_PATH, dt.Rows[0]["mobile"],decimal.Parse(dt.Rows[0]["money"].ToString()).ToString("F0")+"爽乐币") + "\r}";
                            context.Response.Write(result);
                        #endregion
                        break;
                    case "已收货":
                        result = string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"info\":\"{6}\",\r\"phone\":\"{7}\",\r\"kuaidihao\":\"{8}\",\r\"kuaidigongsi\":\"{9}\",\r\"kahao\":\"{10}\",\r\"kami\":\"{11}\",\r\"usetime\":\"{12}\",\r\"usetype\":\"{13}\",\r\"usebeizhu\":\"{14}\",\r\"time\":\"{15}\",\r\"sendtime\":\"{16}\",\r\"imgroot\":\"{17}\",\r\"mobile\":\"{18}\",\r\"money\":\"{19}\""
                            , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, ordersEntity.Express_code, ordersEntity.Express_company, drcard.Rows[0]["code"], drcard.Rows[0]["codepwd"], drcard.Rows[0]["usetime"], drcard.Rows[0]["usetype"], drcard.Rows[0]["usebeizhu"], dt.Rows[0]["time"], ordersEntity.Sendtime, Globals.G_UPLOAD_PATH, dt.Rows[0]["mobile"], decimal.Parse(dt.Rows[0]["money"].ToString()).ToString("F0") + "爽乐币") + "\r}";
                            context.Response.Write(result);
                        break;
                }

                return;
            }
            //ordersEntity = go_ordersBusiness.LoadEntity(dtOrder.Rows[0]["ordertype"].ToString());
            if (typeid == "1" || dts!=null)
            //if (typeid=="1")
            {
                if (dts.Rows[0]["typeid"].ToString() == "1")
                {
                    result += string.Format("{{\r\"state\":{0},\r\"typeid\":{1},\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"info\":\"{6}\",\r\"phone\":\"{7}\",\r\"time\":\"{8}\",\r\"kahao\":\"{9}\",\r\"kami\":\"{10}\",\r\"sendtime\":\"{11}\",\r\"imgroot\":\"{12}\",\r\"money\":\"{13}\""
                        , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["orderId"], dt.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, dt.Rows[0]["time"], kh, km, ordersEntity.Sendtime, Globals.G_UPLOAD_PATH, decimal.Parse(dt.Rows[0]["money"].ToString()).ToString("F0")+"爽乐币") + "\r}";
                    context.Response.Write(result);
                    return;
                }
            }
            string isdefault = "N";
            string dzid = "";
            if (dt.Rows[0]["typeid"].ToString() == "0")
           // if (typeid == "0")
            {
                DataTable dtdz = go_member_dizhiBusiness.GetListData("uid='" + uid + "'", "isdefault,id", "isdefault desc", 1, DbServers.DbServerName.LatestDB);
                if (dtdz.Rows.Count == 0)
                {
                    isdefault = "N";
                }
                else
                {
                    isdefault = dtdz.Rows[0]["isdefault"].ToString() == "N" ? "N" : "Y";
                    dzid = dtdz.Rows[0]["id"].ToString();
                }
            }
            if (dt.Rows[0]["typeid"].ToString() == "2")
            //if (typeid == "2")
            {
                DataTable dtgdz = go_member_dizhi_gameBusiness.GetListData("uid='" + uid + "'", "isdefault,id", "isdefault desc", 1, DbServers.DbServerName.LatestDB);
                if (dtgdz.Rows.Count == 0)
                {
                    isdefault = "N";
                }
                else
                {
                    isdefault = dtgdz.Rows[0]["isdefault"].ToString() == "N" ? "N" : "Y";
                    dzid = dtgdz.Rows[0]["id"].ToString();
                }
            }
            string status = dts.Rows[0]["status"].ToString();
            switch(orderType){
                case "quan":
                    switch (status)
                    {
                        case "待确认":
                             result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"imgroot\":\"{6}\",\r\"isdefault\":\"{7}\",\r\"dzid\":\"{8}\",\r\"sendtime\":\"{9}\",\r\"mobile\":\"{10}\",\r\"money\":\"{11}\""
                            , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], Globals.G_UPLOAD_PATH, isdefault, dzid, dt.Rows[0]["sendtime"], dt.Rows[0]["mobile"], decimal.Parse(dt.Rows[0]["money"].ToString()).ToString("F0") + "爽乐币") + "\r}";
                            break;
                        case "待发货":
                             result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"info\":\"{6}\",\r\"phone\":\"{7}\",\r\"imgroot\":\"{8}\",\r\"isdefault\":\"{9}\",\r\"dzid\":\"{10}\",\r\"sendtime\":\"{11}\",\r\"mobile\":\"{12}\",\r\"money\":\"{13}\""
                            , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, Globals.G_UPLOAD_PATH, isdefault, dzid, dt.Rows[0]["sendtime"], dt.Rows[0]["mobile"], decimal.Parse(dt.Rows[0]["money"].ToString()).ToString("F0") + "爽乐币") + "\r}";
                            break;
                        case "已发货":
                        case "已收货":
                             result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"info\":\"{6}\",\r\"phone\":\"{7}\",\r\"kuaidihao\":\"{8}\",\r\"kuaidigongsi\":\"{9}\",\r\"imgroot\":\"{10}\",\r\"isdefault\":\"{11}\",\r\"dzid\":\"{12}\",\r\"sendtime\":\"{13}\",\r\"mobile\":\"{14}\",\r\"money\":\"{15}\""
                            , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, ordersEntity.Express_code, ordersEntity.Express_company, Globals.G_UPLOAD_PATH, isdefault, dzid, dt.Rows[0]["sendtime"], dt.Rows[0]["mobile"], decimal.Parse(dt.Rows[0]["money"].ToString()).ToString("F0") + "爽乐币") + "\r}";
                            break;
                    }
                    break;
            }
            

            //移出缓存
            CacheHelper.DelCache("affirminfo_" + uid);

            //输出json
            context.Response.Write(result);

        }



        /// <summary>
        /// 积分商城礼品中奖确认展示接口
        /// </summary>
        /// <param name="context"></param>
        /// <summary>
        private void giftConfirm(HttpContext context)
        {
            #region ***************************参数验证和获取***************************
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            //uid = "11684";
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":\"2\"}");    //未知的签名参数
                return;
            }
            string orderid = context.Request["orderid"];
            if (string.IsNullOrEmpty(orderid))
            {
                context.Response.Write("{\"state\":\"3\"}");    //订单ID错误
                return;
            }
            /*
            string shopid = context.Request["shopid"];
            if (string.IsNullOrEmpty(shopid))
            {
                context.Response.Write("{\"state\":\"4\"}");    //一元购ID错误
                return;
            }
             */ 
            #endregion

            string orderType = "";//订单类型,同businesstype.ordertype

            //获取订单信息
            DataTable dtOrder = go_ordersBusiness.LoadData(orderid);
            if (dtOrder.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":\"5\"}");    //获取订单信息失败
                return;
            }
            DataTable dt = null;
            orderType = dtOrder.Rows[0]["ordertype"].ToString();

            switch (orderType)
            {
                case "gift":
                    dt = CustomsBusiness.GetListData("go_gifts gg join go_orders [go] on gg.giftId = [go].businessId inner join go_products gp on gg.productid = gp.productid join go_member gm on [go].uid = gm.uid", "[go].orderId='" + orderid + "' and [go].uid='" + uid + "'", "gp.typeid,gg.title,gp.thumb,gp.status,[go].sendtime,[go].time,[go].orderid,gg.scope,gm.mobile,gp.money", null, 1, DbServers.DbServerName.LatestDB);
                    break;
            }
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":\"5\"}");    //获取订单信息失败
                return;
            }
            //DataTable dts = CustomsBusiness.GetListData("go_yiyuan y join go_orders o on o.ordertype='yuan' And y.yId=o.businessId join go_products p on y.productid=p.productid join go_member m on y.q_uid=m.uid", "orderId='" + orderid + "' and y.q_uid='" + uid + "'", "p.typeid, y.title,thumb,y.q_end_time,o.status,p.money,m.mobile,o.orderId ,(select sum(t.quantity) as quantity from (select  y.title,thumb, o.quantity,o.status,o.orderId from go_yiyuan y join go_orders o on y.yId=o.businessId join go_products p on y.productid=p.productid where  businessId='" + shopid + "' and uid='" + uid + "' ) t ) as quantity", null, 1, DbServers.DbServerName.LatestDB);
            string typeid = "0";
            //string status = dtOrder.Rows[0]["status"].ToString();
            DataTable dts = null;
            switch (dtOrder.Rows[0]["ordertype"].ToString())
            {
                case "gift"://积分商城中奖
                    dts = CustomsBusiness.GetListData("go_gifts gg join go_orders [go] on gg.giftId = [go].businessId inner join go_products gp on gg.productid = gp.productid ", "[go].orderId='" + orderid + "' and [go].uid='" + uid + "'", "gp.typeid,[go].status", null, 1, DbServers.DbServerName.LatestDB);
                    break;
            }
            //实例化订单实体类
            go_ordersEntity ordersEntity;
            go_card_rechargeEntity rechargeEntity = null;
            string result = "";
            string kh = "";
            string km = "";
            string id = "";
            string info = null;
            //1代表商品类型为卡密，若商品类型为卡密，则更改订单状态为“待确认”，同时返回卡号、密码给用户   若商品类型为实物、游戏，则更改订单状态为“代发货”
            DataTable drcard = CustomsBusiness.GetListData("go_card_recharge c join go_orders o on c.orderid=o.orderId", "o.orderId='" + dt.Rows[0]["orderid"].ToString() + "'", "c.code,c.codepwd,c.usetime,c.usebeizhu,c.usetype,o.status", null, 0, DbServers.DbServerName.LatestDB);
            ordersEntity = go_ordersBusiness.LoadEntity(dt.Rows[0]["orderid"].ToString());
            //商品类型为：卡号卡密，购买话费所得   //商品类型为:Q币，购买Q币所得
            if (dt.Rows[0]["typeid"].ToString() == "1" || dt.Rows[0]["typeid"].ToString() == "3")
            {
                switch (ordersEntity.Status)
                {
                    case "待确认":
                        #region 待确认
                        int money = Convert.ToInt32(dt.Rows[0]["money"]);
                        if (string.IsNullOrEmpty(ordersEntity.Order_address_info))//发送卡密
                        {
                            int num = Convert.ToInt32(dt.Rows[0]["money"]);
                            if (num > 100)
                            {
                                num = num / 100;
                                money = money / num;
                            }
                            else
                            {
                                money = Convert.ToInt32(dt.Rows[0]["money"]);
                                num = 1;
                            }
                            for (int i = 1; i <= num; i++)
                            {
                                DataTable kmdt = CustomsBusiness.GetListData("go_card_recharge", "orderId='' and uid='0' ", "*", "rechargetime asc", i, DbServers.DbServerName.LatestDB);
                                kh = kmdt.Rows[0]["code"].ToString();
                                km = kmdt.Rows[0]["codepwd"].ToString();
                                id = kmdt.Rows[0]["id"].ToString();
                                ordersEntity = go_ordersBusiness.LoadEntity(orderid);
                                ordersEntity.Phone = dt.Rows[0]["mobile"].ToString();
                                ordersEntity.Status = "待确认";
                                info += kh + "☆" + km + "★";
                                ordersEntity.Sendtime = Convert.ToDateTime(dt.Rows[0]["time"]);
                                rechargeEntity = go_card_rechargeBusiness.LoadEntity(Convert.ToInt32(id), DbServers.DbServerName.LatestDB);
                                rechargeEntity.OrderId = orderid;
                                rechargeEntity.Uid = Convert.ToInt32(uid);
                                rechargeEntity.Money = money;
                                if (!go_card_rechargeBusiness.SaveEntity(rechargeEntity, false, DbServers.DbServerName.LatestDB))
                                {
                                    context.Response.Write("{\"state\":\"8\"}");    //保存失败
                                    return;
                                }
                            }
                            ordersEntity.Order_address_info = info;
                            if (!go_ordersBusiness.SaveEntity(ordersEntity, false, DbServers.DbServerName.LatestDB))
                            {
                                context.Response.Write("{\"state\":\"8\"}");    //保存失败
                                return;
                            }
                        }
                        DataTable okdt = go_card_rechargeBusiness.GetListData("orderid='" + orderid + "' and isrepeat='0' and uid='" + uid + "'", "*", null, 1, DbServers.DbServerName.LatestDB);
                        //返回已经发放的卡密
                        result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"info\":\"{6}\",\r\"phone\":\"{7}\",\r\"time\":\"{8}\",\r\"kahao\":\"{9}\",\r\"kami\":\"{10}\",\r\"sendtime\":\"{11}\",\r\"imgroot\":\"{12}\",\r\"mobile\":\"{13}\",\r\"money\":\"{14}\""
                            , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, dt.Rows[0]["time"], okdt.Rows[0]["code"], okdt.Rows[0]["codepwd"], ordersEntity.Sendtime, Globals.G_UPLOAD_PATH, dt.Rows[0]["mobile"], dt.Rows[0]["scope"] + "积分") + "\r}";
                        context.Response.Write(result);
                        #endregion
                        break;
                    case "已收货":
                        result = string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"info\":\"{6}\",\r\"phone\":\"{7}\",\r\"kuaidihao\":\"{8}\",\r\"kuaidigongsi\":\"{9}\",\r\"kahao\":\"{10}\",\r\"kami\":\"{11}\",\r\"usetime\":\"{12}\",\r\"usetype\":\"{13}\",\r\"usebeizhu\":\"{14}\",\r\"time\":\"{15}\",\r\"sendtime\":\"{16}\",\r\"imgroot\":\"{17}\",\r\"mobile\":\"{18}\",\r\"money\":\"{19}\""
                            , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, ordersEntity.Express_code, ordersEntity.Express_company, drcard.Rows[0]["code"], drcard.Rows[0]["codepwd"], drcard.Rows[0]["usetime"], drcard.Rows[0]["usetype"], drcard.Rows[0]["usebeizhu"], dt.Rows[0]["time"], ordersEntity.Sendtime, Globals.G_UPLOAD_PATH, dt.Rows[0]["mobile"], dt.Rows[0]["scope"] + "积分") + "\r}";
                        context.Response.Write(result);
                        break;
                }

                return;
            }
            //ordersEntity = go_ordersBusiness.LoadEntity(dtOrder.Rows[0]["ordertype"].ToString());
            if (typeid == "1" || dts != null)
            //if (typeid=="1")
            {
                if (dts.Rows[0]["typeid"].ToString() == "1")
                {
                    result += string.Format("{{\r\"state\":{0},\r\"typeid\":{1},\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"info\":\"{6}\",\r\"phone\":\"{7}\",\r\"time\":\"{8}\",\r\"kahao\":\"{9}\",\r\"kami\":\"{10}\",\r\"sendtime\":\"{11}\",\r\"imgroot\":\"{12}\",\r\"money\":\"{13}\""
                        , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["orderId"], dt.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, dt.Rows[0]["time"], kh, km, ordersEntity.Sendtime, Globals.G_UPLOAD_PATH, dt.Rows[0]["money"]) + "\r}";
                    context.Response.Write(result);
                    return;
                }
            }
            string isdefault = "N";
            string dzid = "";
            if (dt.Rows[0]["typeid"].ToString() == "0")
            // if (typeid == "0")
            {
                DataTable dtdz = go_member_dizhiBusiness.GetListData("uid='" + uid + "'", "isdefault,id", "isdefault desc", 1, DbServers.DbServerName.LatestDB);
                if (dtdz.Rows.Count == 0)
                {
                    isdefault = "N";
                }
                else
                {
                    isdefault = dtdz.Rows[0]["isdefault"].ToString() == "N" ? "N" : "Y";
                    dzid = dtdz.Rows[0]["id"].ToString();
                }
            }
            if (dt.Rows[0]["typeid"].ToString() == "2")
            //if (typeid == "2")
            {
                DataTable dtgdz = go_member_dizhi_gameBusiness.GetListData("uid='" + uid + "'", "isdefault,id", "isdefault desc", 1, DbServers.DbServerName.LatestDB);
                if (dtgdz.Rows.Count == 0)
                {
                    isdefault = "N";
                }
                else
                {
                    isdefault = dtgdz.Rows[0]["isdefault"].ToString() == "N" ? "N" : "Y";
                    dzid = dtgdz.Rows[0]["id"].ToString();
                }
            }
            string status = dts.Rows[0]["status"].ToString();
            switch (orderType)
            {
                case "yuan"://一元购展示
                    switch (status)
                    {
                        case "待确认":
                            result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"quantity\":\"{4}\",\r\"orderId\":\"{5}\",\r\"status\":\"{6}\",\r\"q_end_time\":\"{7}\",\r\"imgroot\":\"{8}\",\r\"isdefault\":\"{9}\",\r\"dzid\":\"{10}\",\r\"sendtime\":\"{11}\",\r\"mobile\":\"{12}\",\r\"money\":\"{13}\""
                                , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["quantity"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], dt.Rows[0]["q_end_time"], Globals.G_UPLOAD_PATH, isdefault, dzid, dt.Rows[0]["sendtime"], dt.Rows[0]["mobile"], dt.Rows[0]["money"]) + "\r}";
                            break;
                        case "待发货":     //返回自己所填写的资料
                            result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"quantity\":\"{4}\",\r\"orderId\":\"{5}\",\r\"status\":\"{6}\",\r\"info\":\"{7}\",\r\"phone\":\"{8}\",\r\"q_end_time\":\"{9}\",\r\"imgroot\":\"{10}\",\r\"isdefault\":\"{11}\",\r\"dzid\":\"{12}\",\r\"sendtime\":\"{13}\",\r\"mobile\":\"{14}\",\r\"money\":\"{15}\""
                                , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["quantity"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, dt.Rows[0]["q_end_time"], Globals.G_UPLOAD_PATH, isdefault, dzid, ordersEntity.Sendtime, dt.Rows[0]["mobile"], dt.Rows[0]["money"]) + "\r}";
                            break;
                        case "已发货":     //返回快递单号，快递公司，以及自己所填写的资料
                            result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"quantity\":\"{4}\",\r\"orderId\":\"{5}\",\r\"status\":\"{6}\",\r\"info\":\"{7}\",\r\"phone\":\"{8}\",\r\"kuaidihao\":\"{9}\",\r\"kuaidigongsi\":\"{10}\",\r\"q_end_time\":\"{11}\",\r\"imgroot\":\"{12}\",\r\"isdefault\":\"{13}\",\r\"dzid\":\"{14}\",\r\"sendtime\":\"{15}\",\r\"mobile\":\"{16}\",\r\"money\":\"{17}\""
                                , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["quantity"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, ordersEntity.Express_code, ordersEntity.Express_company, dt.Rows[0]["q_end_time"], Globals.G_UPLOAD_PATH, isdefault, dzid, ordersEntity.Sendtime, dt.Rows[0]["mobile"], dt.Rows[0]["money"]) + "\r}";
                            break;
                        case "已收货":     //返回快递单号，快递公司，以及自己所填写的资料,订单已完成
                            result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"quantity\":\"{4}\",\r\"orderId\":\"{5}\",\r\"status\":\"{6}\",\r\"info\":\"{7}\",\r\"phone\":\"{8}\",\r\"kuaidihao\":\"{9}\",\r\"kuaidigongsi\":\"{10}\",\r\"q_end_time\":\"{11}\",\r\"imgroot\":\"{12}\",\r\"isdefault\":\"{13}\",\r\"dzid\":\"{14}\",\r\"sendtime\":\"{15}\",\r\"mobile\":\"{16}\",\r\"money\":\"{17}\""
                                , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["quantity"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, ordersEntity.Express_code, ordersEntity.Express_company, dt.Rows[0]["q_end_time"], Globals.G_UPLOAD_PATH, isdefault, dzid, ordersEntity.Sendtime, dt.Rows[0]["mobile"], dt.Rows[0]["money"]) + "\r}";
                            break;
                    }
                    break;
                case "gift":
                    switch (status)
                    {
                        case "待确认":
                            result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"imgroot\":\"{6}\",\r\"isdefault\":\"{7}\",\r\"dzid\":\"{8}\",\r\"sendtime\":\"{9}\",\r\"mobile\":\"{10}\",\r\"money\":\"{11}\""
                           , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], Globals.G_UPLOAD_PATH, isdefault, dzid, dt.Rows[0]["sendtime"], dt.Rows[0]["mobile"], dt.Rows[0]["scope"] + "积分") + "\r}";
                            break;
                        case "待发货":
                            result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"info\":\"{6}\",\r\"phone\":\"{7}\",\r\"imgroot\":\"{8}\",\r\"isdefault\":\"{9}\",\r\"dzid\":\"{10}\",\r\"sendtime\":\"{11}\",\r\"mobile\":\"{12}\",\r\"money\":\"{13}\""
                           , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, Globals.G_UPLOAD_PATH, isdefault, dzid, dt.Rows[0]["sendtime"], dt.Rows[0]["mobile"], dt.Rows[0]["scope"] + "积分") + "\r}";
                            break;
                        case "已发货":
                        case "已收货":
                            result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"orderId\":\"{4}\",\r\"status\":\"{5}\",\r\"info\":\"{6}\",\r\"phone\":\"{7}\",\r\"kuaidihao\":\"{8}\",\r\"kuaidigongsi\":\"{9}\",\r\"imgroot\":\"{10}\",\r\"isdefault\":\"{11}\",\r\"dzid\":\"{12}\",\r\"sendtime\":\"{13}\",\r\"mobile\":\"{14}\",\r\"money\":\"{15}\""
                           , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, ordersEntity.Express_code, ordersEntity.Express_company, Globals.G_UPLOAD_PATH, isdefault, dzid, dt.Rows[0]["sendtime"], dt.Rows[0]["mobile"], dt.Rows[0]["scope"] + "积分") + "\r}";
                            break;
                    }
                    break;
            }




            //移出缓存
            CacheHelper.DelCache("affirminfo_" + uid);

            //输出json
            context.Response.Write(result);

        }


        /// 商品确认
        /// 参数 sign 用户签名 orderid 订单ID 
        /// 返回状态 state 0获取信息成功 2未知的签名参数 3订单ID错误 4获取订单信息失败
        /// </summary>
        /// <param name="context"></param>
        private void productaffirm(HttpContext context)
        {
            #region ***************************参数验证和获取***************************
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            //uid = "11684";
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":\"2\"}");    //未知的签名参数
                return;
            }
            string orderid = context.Request["orderid"];
            if (string.IsNullOrEmpty(orderid))
            {
                context.Response.Write("{\"state\":\"3\"}");    //订单ID错误
                return;
            }
            string shopid = context.Request["shopid"];
            if (string.IsNullOrEmpty(shopid))
            {
                context.Response.Write("{\"state\":\"4\"}");    //一元购ID错误
                return;
            }
            #endregion

            string orderType = "";//订单类型,同businesstype.ordertype

            //获取订单信息
            DataTable dtOrder = go_ordersBusiness.LoadData(orderid);
            if (dtOrder.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":\"5\"}");    //获取订单信息失败
                return;
            }
            DataTable dt = null;
            orderType = dtOrder.Rows[0]["ordertype"].ToString();

            switch (orderType)
            {
                case "yuan":
                    dt = CustomsBusiness.GetListData("go_yiyuan y join go_orders o on o.ordertype='yuan' And y.yId=o.businessId join go_products p on y.productid=p.productid join go_member m on y.q_uid=m.uid", "orderId='" + orderid + "' and y.q_uid='" + uid + "'", "p.typeid, y.title,thumb,CONVERT(varchar,y.q_end_time,121) as q_end_time,o.status,o.sendtime,p.money,m.mobile,o.orderId ,(select sum(t.quantity) as quantity from (select  y.title,thumb, o.quantity,o.status,o.orderId from go_yiyuan y join go_orders o on y.yId=o.businessId join go_products p on y.productid=p.productid where  businessId='" + shopid + "' and uid='" + uid + "' ) t ) as quantity", null, 1, DbServers.DbServerName.LatestDB);
                    break;
                case "tuan":
                    dt = CustomsBusiness.GetListData("go_tuan_listinfo T JOIN go_orders O ON T.tuanlistId=O.businessId JOIN go_tuan ot on ot.tId=t.tuanId join go_products p on ot.productid=p.productid join go_member m on o.uid=m.uid", "orderId='" + orderid + "' and o.uid='" + uid + "'", "p.typeid,ot.title,thumb, convert(varchar,t.q_end_time,121)as q_end_time,p.status,o.sendtime,p.money,m.mobile,o.orderId", null, 1, DbServers.DbServerName.LatestDB);
                    dt.Columns.Add("quantity", typeof(string));
                    dt.Rows[0]["quantity"] = "1";
                    break;
            }
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":\"5\"}");    //获取订单信息失败
                return;
            }
            //DataTable dts = CustomsBusiness.GetListData("go_yiyuan y join go_orders o on o.ordertype='yuan' And y.yId=o.businessId join go_products p on y.productid=p.productid join go_member m on y.q_uid=m.uid", "orderId='" + orderid + "' and y.q_uid='" + uid + "'", "p.typeid, y.title,thumb,y.q_end_time,o.status,p.money,m.mobile,o.orderId ,(select sum(t.quantity) as quantity from (select  y.title,thumb, o.quantity,o.status,o.orderId from go_yiyuan y join go_orders o on y.yId=o.businessId join go_products p on y.productid=p.productid where  businessId='" + shopid + "' and uid='" + uid + "' ) t ) as quantity", null, 1, DbServers.DbServerName.LatestDB);
            string typeid = "0";
            //string status = dtOrder.Rows[0]["status"].ToString();
            DataTable dts = null;
            switch (dtOrder.Rows[0]["ordertype"].ToString())
            {
                case "yuan":
                    dts = CustomsBusiness.GetListData("go_yiyuan y join go_orders o on o.ordertype='yuan' And y.yId=o.businessId join go_products p on y.productid=p.productid join go_member m on y.q_uid=m.uid", "orderId='" + orderid + "' and y.q_uid='" + uid + "'", "p.typeid, y.title,thumb,y.q_end_time,o.status,p.money,m.mobile,o.orderId ,(select sum(t.quantity) as quantity from (select  y.title,thumb, o.quantity,o.status,o.orderId from go_yiyuan y join go_orders o on y.yId=o.businessId join go_products p on y.productid=p.productid where  businessId='" + shopid + "' and uid='" + uid + "' ) t ) as quantity", null, 1, DbServers.DbServerName.LatestDB);
                    break;
                case "tuan":
                    dts = CustomsBusiness.GetListData("go_tuan_listinfo TL right Join go_tuan T ON TL.tuanId=T.tId right Join go_products P ON T.productid=P.productid  join go_orders o on TL.tuanlistId=o.businessId", "TL.tuanlistId='" + shopid + "' and orderId='" + orderid + "'", "P.typeid ,o.status", null, 1, DbServers.DbServerName.LatestDB);
                    break;
            }
            //实例化订单实体类
            go_ordersEntity ordersEntity;
            go_card_rechargeEntity rechargeEntity = null;
            string result = "";
            string kh = "";
            string km = "";
            string id = "";
            string info = null;
            //1代表商品类型为卡密，若商品类型为卡密，则更改订单状态为“待确认”，同时返回卡号、密码给用户   若商品类型为实物、游戏，则更改订单状态为“代发货”
            DataTable drcard = CustomsBusiness.GetListData("go_card_recharge c join go_orders o on c.orderid=o.orderId", "o.orderId='" + dt.Rows[0]["orderid"].ToString() + "'", "c.code,c.codepwd,c.usetime,c.usebeizhu,c.usetype,o.status", null, 0, DbServers.DbServerName.LatestDB);
            ordersEntity = go_ordersBusiness.LoadEntity(dt.Rows[0]["orderid"].ToString());
            //商品类型为：卡号卡密，购买话费所得   //商品类型为:Q币，购买Q币所得
            if (dt.Rows[0]["typeid"].ToString() == "1" || dt.Rows[0]["typeid"].ToString() == "3")
            {
                switch (ordersEntity.Status)
                {
                    case "待确认":
                        #region 待确认
                        int money = Convert.ToInt32(dt.Rows[0]["money"]);
                        if (string.IsNullOrEmpty(ordersEntity.Order_address_info))//发送卡密
                        {
                            int num = Convert.ToInt32(dt.Rows[0]["money"]);
                            if (num > 100)
                            {
                                num = num / 100;
                                money = money / num;
                            }
                            else
                            {
                                money = Convert.ToInt32(dt.Rows[0]["money"]);
                                num = 1;
                            }
                            for (int i = 1; i <= num; i++)
                            {
                                DataTable kmdt = CustomsBusiness.GetListData("go_card_recharge", "orderId='' and uid='0' ", "*", "rechargetime asc", i, DbServers.DbServerName.LatestDB);
                                kh = kmdt.Rows[0]["code"].ToString();
                                km = kmdt.Rows[0]["codepwd"].ToString();
                                id = kmdt.Rows[0]["id"].ToString();
                                ordersEntity = go_ordersBusiness.LoadEntity(orderid);
                                ordersEntity.Phone = dt.Rows[0]["mobile"].ToString();
                                ordersEntity.Status = "待确认";
                                info += kh + "☆" + km + "★";
                                ordersEntity.Sendtime = Convert.ToDateTime(dt.Rows[0]["q_end_time"]);
                                rechargeEntity = go_card_rechargeBusiness.LoadEntity(Convert.ToInt32(id), DbServers.DbServerName.LatestDB);
                                rechargeEntity.OrderId = orderid;
                                rechargeEntity.Uid = Convert.ToInt32(uid);
                                rechargeEntity.Money = money;
                                if (!go_card_rechargeBusiness.SaveEntity(rechargeEntity, false, DbServers.DbServerName.LatestDB))
                                {
                                    context.Response.Write("{\"state\":\"8\"}");    //保存失败
                                    return;
                                }
                            }
                            ordersEntity.Order_address_info = info;
                            if (!go_ordersBusiness.SaveEntity(ordersEntity, false, DbServers.DbServerName.LatestDB))
                            {
                                context.Response.Write("{\"state\":\"8\"}");    //保存失败
                                return;
                            }
                        }
                        DataTable okdt = go_card_rechargeBusiness.GetListData("orderid='" + orderid + "' and isrepeat='0' and uid='" + uid + "'", "*", null, 1, DbServers.DbServerName.LatestDB);
                        //返回已经发放的卡密
                        result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"quantity\":\"{4}\",\r\"orderId\":\"{5}\",\r\"status\":\"{6}\",\r\"info\":\"{7}\",\r\"phone\":\"{8}\",\r\"q_end_time\":\"{9}\",\r\"kahao\":\"{10}\",\r\"kami\":\"{11}\",\r\"sendtime\":\"{12}\",\r\"imgroot\":\"{13}\",\r\"mobile\":\"{14}\",\r\"money\":\"{15}\""
                            , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["quantity"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, dt.Rows[0]["q_end_time"], okdt.Rows[0]["code"], okdt.Rows[0]["codepwd"], ordersEntity.Sendtime, Globals.G_UPLOAD_PATH, dt.Rows[0]["mobile"], dt.Rows[0]["money"]) + "\r}";
                        context.Response.Write(result);
                        #endregion
                        break;
                    case "已收货":
                        result = string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"quantity\":\"{4}\",\r\"orderId\":\"{5}\",\r\"status\":\"{6}\",\r\"info\":\"{7}\",\r\"phone\":\"{8}\",\r\"kuaidihao\":\"{9}\",\r\"kuaidigongsi\":\"{10}\",\r\"kahao\":\"{11}\",\r\"kami\":\"{12}\",\r\"usetime\":\"{13}\",\r\"usetype\":\"{14}\",\r\"usebeizhu\":\"{15}\",\r\"q_end_time\":\"{16}\",\r\"sendtime\":\"{17}\",\r\"imgroot\":\"{18}\",\r\"mobile\":\"{19}\",\r\"money\":\"{20}\""
                            , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["quantity"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, ordersEntity.Express_code, ordersEntity.Express_company, drcard.Rows[0]["code"], drcard.Rows[0]["codepwd"], drcard.Rows[0]["usetime"], drcard.Rows[0]["usetype"], drcard.Rows[0]["usebeizhu"], dt.Rows[0]["q_end_time"], ordersEntity.Sendtime, Globals.G_UPLOAD_PATH, dt.Rows[0]["mobile"], dt.Rows[0]["money"]) + "\r}";
                        context.Response.Write(result);
                        break;
                }

                return;
            }
            //ordersEntity = go_ordersBusiness.LoadEntity(dtOrder.Rows[0]["ordertype"].ToString());
            if (typeid == "1" || dts != null)
            //if (typeid=="1")
            {
                if (dts.Rows[0]["typeid"].ToString() == "1")
                {
                    result += string.Format("{{\r\"state\":{0},\r\"typeid\":{1},\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"quantity\":\"{4}\",\r\"orderId\":\"{5}\",\r\"status\":\"{6}\",\r\"info\":\"{7}\",\r\"phone\":\"{8}\",\r\"q_end_time\":\"{9}\",\r\"kahao\":\"{10}\",\r\"kami\":\"{11}\",\r\"sendtime\":\"{12}\",\r\"imgroot\":\"{13}\",\r\"money\":\"{14}\""
                        , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["quantity"], dt.Rows[0]["orderId"], dt.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, dt.Rows[0]["q_end_time"], kh, km, ordersEntity.Sendtime, Globals.G_UPLOAD_PATH, dt.Rows[0]["money"]) + "\r}";
                    context.Response.Write(result);
                    return;
                }
            }
            string isdefault = "N";
            string dzid = "";
            if (dt.Rows[0]["typeid"].ToString() == "0")
            // if (typeid == "0")
            {
                DataTable dtdz = go_member_dizhiBusiness.GetListData("uid='" + uid + "'", "isdefault,id", "isdefault desc", 1, DbServers.DbServerName.LatestDB);
                if (dtdz.Rows.Count == 0)
                {
                    isdefault = "N";
                }
                else
                {
                    isdefault = dtdz.Rows[0]["isdefault"].ToString() == "N" ? "N" : "Y";
                    dzid = dtdz.Rows[0]["id"].ToString();
                }
            }
            if (dt.Rows[0]["typeid"].ToString() == "2")
            //if (typeid == "2")
            {
                DataTable dtgdz = go_member_dizhi_gameBusiness.GetListData("uid='" + uid + "'", "isdefault,id", "isdefault desc", 1, DbServers.DbServerName.LatestDB);
                if (dtgdz.Rows.Count == 0)
                {
                    isdefault = "N";
                }
                else
                {
                    isdefault = dtgdz.Rows[0]["isdefault"].ToString() == "N" ? "N" : "Y";
                    dzid = dtgdz.Rows[0]["id"].ToString();
                }
            }
            string status = dts.Rows[0]["status"].ToString();
            switch (orderType)
            {
                case "yuan"://一元购展示
                    switch (status)
                    {
                        case "待确认":
                            result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"quantity\":\"{4}\",\r\"orderId\":\"{5}\",\r\"status\":\"{6}\",\r\"q_end_time\":\"{7}\",\r\"imgroot\":\"{8}\",\r\"isdefault\":\"{9}\",\r\"dzid\":\"{10}\",\r\"sendtime\":\"{11}\",\r\"mobile\":\"{12}\",\r\"money\":\"{13}\""
                                , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["quantity"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], dt.Rows[0]["q_end_time"], Globals.G_UPLOAD_PATH, isdefault, dzid, dt.Rows[0]["sendtime"], dt.Rows[0]["mobile"], dt.Rows[0]["money"]) + "\r}";
                            break;
                        case "待发货":     //返回自己所填写的资料
                            result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"quantity\":\"{4}\",\r\"orderId\":\"{5}\",\r\"status\":\"{6}\",\r\"info\":\"{7}\",\r\"phone\":\"{8}\",\r\"q_end_time\":\"{9}\",\r\"imgroot\":\"{10}\",\r\"isdefault\":\"{11}\",\r\"dzid\":\"{12}\",\r\"sendtime\":\"{13}\",\r\"mobile\":\"{14}\",\r\"money\":\"{15}\""
                                , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["quantity"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, dt.Rows[0]["q_end_time"], Globals.G_UPLOAD_PATH, isdefault, dzid, ordersEntity.Sendtime, dt.Rows[0]["mobile"], dt.Rows[0]["money"]) + "\r}";
                            break;
                        case "已发货":     //返回快递单号，快递公司，以及自己所填写的资料
                            result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"quantity\":\"{4}\",\r\"orderId\":\"{5}\",\r\"status\":\"{6}\",\r\"info\":\"{7}\",\r\"phone\":\"{8}\",\r\"kuaidihao\":\"{9}\",\r\"kuaidigongsi\":\"{10}\",\r\"q_end_time\":\"{11}\",\r\"imgroot\":\"{12}\",\r\"isdefault\":\"{13}\",\r\"dzid\":\"{14}\",\r\"sendtime\":\"{15}\",\r\"mobile\":\"{16}\",\r\"money\":\"{17}\""
                                , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["quantity"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, ordersEntity.Express_code, ordersEntity.Express_company, dt.Rows[0]["q_end_time"], Globals.G_UPLOAD_PATH, isdefault, dzid, ordersEntity.Sendtime, dt.Rows[0]["mobile"], dt.Rows[0]["money"]) + "\r}";
                            break;
                        case "已收货":     //返回快递单号，快递公司，以及自己所填写的资料,订单已完成
                            result += string.Format("{{\r\"state\":\"{0}\",\r\"typeid\":\"{1}\",\r\"title\":\"{2}\",\r\"thumb\":\"{3}\",\r\"quantity\":\"{4}\",\r\"orderId\":\"{5}\",\r\"status\":\"{6}\",\r\"info\":\"{7}\",\r\"phone\":\"{8}\",\r\"kuaidihao\":\"{9}\",\r\"kuaidigongsi\":\"{10}\",\r\"q_end_time\":\"{11}\",\r\"imgroot\":\"{12}\",\r\"isdefault\":\"{13}\",\r\"dzid\":\"{14}\",\r\"sendtime\":\"{15}\",\r\"mobile\":\"{16}\",\r\"money\":\"{17}\""
                                , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows[0]["typeid"], dt.Rows[0]["title"], dt.Rows[0]["thumb"], dt.Rows[0]["quantity"], dt.Rows[0]["orderId"], dts.Rows[0]["status"], ordersEntity.Order_address_info, ordersEntity.Phone, ordersEntity.Express_code, ordersEntity.Express_company, dt.Rows[0]["q_end_time"], Globals.G_UPLOAD_PATH, isdefault, dzid, ordersEntity.Sendtime, dt.Rows[0]["mobile"], dt.Rows[0]["money"]) + "\r}";
                            break;
                    }
                    break;
            }


            //移出缓存
            CacheHelper.DelCache("affirminfo_" + uid);

            //输出json
            context.Response.Write(result);

        }

        /// <summary>
        /// 用户确认商品信息
        /// 参数 sign 用户签名 orderid订单ID info用户填写的信息 mobile用户手机号
        /// 返回值 state 0卡密发送成功  2未知的签名参数 3订单ID错误 4用户信息有误 5手机号有误 （6获取订单信息有误 7商品库存不足，请稍后再试，或者联系客服宝宝）  10信息已提交 状态6、7针对发送卡密的状态  10针对实物、游戏，更新状态
        /// </summary>
        /// <param name="context"></param>
        private void affirminfo(HttpContext context)
        {
            #region ***************************参数验证和获取***************************
            //实例化订单实体类
            go_ordersEntity ordersEntity = null;
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            //uid = "11684";
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            //判断频繁调用
            if (!CacheHelper.IsOK("affirminfo_" + uid))
           {
               Globals.DebugLogger("频繁调用接口【affirminfo】_" + uid, "API_Log.txt");
               context.Response.Write("{\"state\":101}");     //同一用户同一功能项,500毫秒只放行一次，入口处判断
               //context.Response.Write("{\"state\":102}");     //同一用户同一功能项,500毫秒只放行一次，入口处判断
               return;
           }
            //黑名单的WXID
            go_memberEntity currMemberEntity = go_memberBusiness.LoadEntity(int.Parse(uid));
            DataTable dtDataUsers = go_memberBusiness.GetListData(string.Format("wxid like '%{0}%' And wxid like '%Test%'", currMemberEntity.Wxid));
            if (dtDataUsers.Rows.Count >= 1)
            {
                context.Response.Write("{\"state\":102}");
                return;
            }
            string kahao = context.Request["kahao"];//卡号
            string kami = context.Request["kami"];//卡密
            string usetype = context.Request["usetype"];//充值类型
            string usenum = context.Request["usenum"];//充值账号
            string orderid = context.Request["orderid"];
            string info = context.Request["info"];
            string mobile = context.Request["mobile"];
            string usename = null;
            go_ordersEntity currentOrderInfo = go_ordersBusiness.LoadEntity(orderid);

            if (currentOrderInfo == null)
            {
                context.Response.Write("{\"state\":3}");    //订单ID错误
                return;
            }

            #endregion
            //如果是使用卡密
            #region ***************************卡密中奖信息***************************
            if (!string.IsNullOrEmpty(kahao) && !string.IsNullOrEmpty(kami) && !string.IsNullOrEmpty(usetype) && !string.IsNullOrEmpty(usenum))
            {
                //查找卡密信息，并判断是否已使用
                DataTable sdt = go_card_rechargeBusiness.GetListData("orderId=( select orderId from go_card_recharge where code='" + kahao + "' and codepwd='" + kami + "') and isrepeat='0'", "*", null, 0, DbServers.DbServerName.LatestDB);
                if (sdt.Rows.Count > 0 && uid == sdt.Rows[0]["uid"].ToString())
                {
                    //获取卡密总金额
                    DataTable mdt = go_card_rechargeBusiness.GetListData("orderId in( select orderId from go_card_recharge where code='" + kahao + "' and codepwd='" + kami + "')", "SUM(money)as  money ", null, 1, DbServers.DbServerName.LatestDB);
                    string currOrderid = sdt.Rows[0]["orderid"].ToString();

                    //充值代码，调用永乐接口。充值类型。1、话费 2、支付宝 4、Q币 11、城市服务商佣金 12、网吧活动结算
                    go_yolly_orderinfoEntity orderinfoEntity;
                    switch (usetype)
                    {
                        case "1"://充值话费
                            #region ***************************充值话费***************************
                            if (!str(sdt, usetype, usename, usename, ordersEntity))//更新数据库成功之后访问永乐接口
                            {
                                context.Response.Write("{\"state\":13}");    //更新失败
                                return;
                            }
                            string Pserialid = yollyinterface.GenerateYollyID(yollyinterface.usetype.Phone);
                            DateTime Pnow = DateTime.Now;
                            object[] Ppost = new object[] { Pnow.ToString("yyyyMMddHHmmssss"), usenum, (Convert.ToInt32(mdt.Rows[0]["money"]) * 100).ToString(), Pserialid };

                            //永乐表先插入数据 新插入的数据状态改为3
                            orderinfoEntity = new go_yolly_orderinfoEntity
                            {
                                Serialid = Pserialid,
                                OrderId = currOrderid,
                                Money = Convert.ToDecimal(mdt.Rows[0]["money"]),
                                Usenum = usenum,
                                Usetime = Pnow,
                                Usetype = 1,
                                Status = 3,
                                Context = "申请已提交"
                            };
                            bool b = go_yolly_orderinfoBusiness.SaveEntity(orderinfoEntity, true, DbServers.DbServerName.LatestDB);
                            orderinfoEntity = go_yolly_orderinfoBusiness.LoadEntity(Pserialid, DbServers.DbServerName.LatestDB);
                            if (!b && orderinfoEntity.Status != 3 && orderinfoEntity.Context != "申请已提交")
                            {
                                context.Response.Write("{\"state\":11}");    //提交信息不存在
                                return;
                            }
                            string Pret = yollyinterface.APIyolly(Ppost, yollyinterface.usetype.Phone);
                            if (Pret == null)
                            {
                                context.Response.Write("{\"state\":12}");    //永乐返回信息有误
                                return;
                            }
                            string status = Pret.Split('|')[0].ToString();//返回状态
                            string strtext = Pret.Split('|')[1].ToString();//返回内容
                            string strstatus = status == "0000" ? "0" : "2";
                            orderinfoEntity.Status = Convert.ToInt32(strstatus);
                            orderinfoEntity.Context = strtext;
                            if (!go_yolly_orderinfoBusiness.SaveEntity(orderinfoEntity, false, DbServers.DbServerName.LatestDB))
                            {
                                context.Response.Write("{\"state\":13}");    //更新失败
                                return;
                            }
                            //移出缓存
                            CacheHelper.DelCache("affirminfo_" + uid);

                            context.Response.Write("{\"state\":0}");    //充值以完成
                            return;

                            #endregion
                            break;
                        case "2"://充值支付宝  永乐游戏充值类型：1.QQ币 2.支付宝账号充值
                        case "4":
                            #region ***************************Q币或支付宝充值***************************
                            if (!str(sdt, usetype, usename, usename, ordersEntity))//更新数据库成功之后访问永乐接口
                            {
                                context.Response.Write("{\"state\":13}");    //更新失败
                                return;
                            }
                            usename = context.Request["usename"];//充值支付宝的真实姓名
                            if (string.IsNullOrEmpty(usename))
                            {
                                usename = "";
                            }
                            yollyinterface.usetype currusetype = (usetype == "4") ? yollyinterface.usetype.QQB : yollyinterface.usetype.Alipay;
                            string Aserialid = yollyinterface.GenerateYollyID(currusetype);
                            DateTime Anow = DateTime.Now;
                            string type = (usetype == "4") ? "1" : "2";
                            double okimoney = Convert.ToDouble(mdt.Rows[0]["money"]) * 0.95 + 0.4;
                            int imoney = Convert.ToInt32(okimoney);
                            object[] Apost = new object[] { Anow.ToString("yyyyMMddHHmmssss"), usenum, imoney, Aserialid, type, usename };
                            orderinfoEntity = new go_yolly_orderinfoEntity
                            {
                                Serialid = Aserialid,
                                OrderId = sdt.Rows[0]["orderid"].ToString(),
                                Money = imoney,
                                Usenum = usenum,
                                Usetime = Anow,
                                Usetype = int.Parse(usetype),
                                Status = 3,
                                Context = "申请已提交"
                            };
                            bool C = go_yolly_orderinfoBusiness.SaveEntity(orderinfoEntity, true, DbServers.DbServerName.LatestDB);
                            orderinfoEntity = go_yolly_orderinfoBusiness.LoadEntity(Aserialid, DbServers.DbServerName.LatestDB);
                            if (!C && orderinfoEntity.Status != 3 && orderinfoEntity.Context != "申请已提交")
                            {
                                context.Response.Write("{\"state\":11}");    //提交信息不存在
                                return;
                            }
                            //访问永乐接口
                            string Aret = yollyinterface.APIyolly(Apost, currusetype);
                            if (Aret == null)
                            {
                                context.Response.Write("{\"state\":12}");    //永乐返回信息有误
                                return;
                            }
                            string Astatus = Aret.Split('|')[0].ToString();//返回状态
                            string Astrtext = Aret.Split('|')[1].ToString();//返回内容
                            string Astrstatus = Astatus == "0000" ? "0" : "0";
                            orderinfoEntity.Status = Convert.ToInt32(Astrstatus);
                            orderinfoEntity.Context = Astrtext;
                            if (!go_yolly_orderinfoBusiness.SaveEntity(orderinfoEntity, false, DbServers.DbServerName.LatestDB))
                            {
                                context.Response.Write("{\"state\":13}");    //更新失败
                                return;
                            }
                            //移出缓存
                            CacheHelper.DelCache("affirminfo_" + uid);

                            context.Response.Write("{\"state\":0}");    //充值以完成
                            return;
                            #endregion
                            break;
                        case "3"://充值爽乐币
                            #region ***************************爽乐币充值***************************
                            ordersEntity = go_ordersBusiness.LoadEntity(currOrderid);
                            if (ordersEntity.Status != "待确认")//防止重复提交
                            {
                                context.Response.Write("{\"state\":12}");    //充值失败
                                return;
                            }
                            if (!str(sdt, usetype, usename, usename, ordersEntity))//更新数据库成功之后访问永乐接口
                            {
                                context.Response.Write("{\"state\":13}");    //更新失败
                                return;
                            }
                            if (go_member_addmoney_recordBusiness.GetListData(string.Format("Code='{0}'", ordersEntity.OrderId)).Rows.Count > 0)//已兑换的订单，防止重复提交
                            {
                                //移出缓存
                                CacheHelper.DelCache("affirminfo_" + uid);
                                context.Response.Write("{\"state\":0}");    //充值以完成
                                return;
                            }
                            go_member_addmoney_recordEntity addmoney_recordEntity;
                            addmoney_recordEntity = new go_member_addmoney_recordEntity
                            {
                                Uid = Convert.ToInt32(uid),
                                Money = Convert.ToDecimal(mdt.Rows[0]["money"]),
                                Pay_type = "话费兑换",
                                Status = "待付款",
                                Time = DateTime.Now,
                                Memo = "add",
                                Score = 2,
                                Tuanid = 0,
                                Tuanlistid = 0,
                                Buy_type = 0,
                                Code = ordersEntity.OrderId //by jhb_20190917_存储中奖订单，一个订单只允许兑一次
                            };
                            if (!go_member_addmoney_recordBusiness.SaveEntity(addmoney_recordEntity, true))
                            {
                                context.Response.Write("{\"state\":13}");    //更新失败
                                return;
                            }
                            DataTable dtm = go_member_addmoney_recordBusiness.GetListData("code='" + ordersEntity.OrderId + "' and uid='"+uid+"'", "*", DbServers.DbServerName.LatestDB);
                            if (dtm.Rows.Count == 0)
                            {
                                context.Response.Write("{\"state\":14}");    //没有此充值记录
                                return;
                            }
                            if (dtm.Rows[0]["Status"].ToString() != "待付款")
                            {
                                context.Response.Write("{\"state\":15}");    //充值已经完成
                                return;
                            }
                            addmoney_recordEntity = go_member_addmoney_recordBusiness.LoadEntity(Convert.ToInt32(dtm.Rows[0]["id"]), DbServers.DbServerName.LatestDB);
                            addmoney_recordEntity.Status = "已付款";
                            if (!go_member_addmoney_recordBusiness.SaveEntity(addmoney_recordEntity, false))
                            {
                                context.Response.Write("{\"state\":13}");    //更新失败
                                return;
                            }
                            go_memberEntity memberEntity = go_memberBusiness.LoadEntity(Convert.ToInt32(uid));
                            memberEntity.Money = memberEntity.Money + Convert.ToInt32(mdt.Rows[0]["money"]);
                            if (!go_memberBusiness.SaveEntity(memberEntity, false, DbServers.DbServerName.LatestDB))
                            {
                                context.Response.Write("{\"state\":13}");    //更新失败
                                return;
                            }
                            //移出缓存
                            CacheHelper.DelCache("affirminfo_" + uid);

                            context.Response.Write("{\"state\":0}");    //充值以完成
                            return;
                            #endregion
                            break;
                    }
                }
                else
                {
                    context.Response.Write("{\"state\":10}");    //卡密已使用
                    return;
                }
            }
            #endregion
            //实物中奖信息，查询用户中奖商品类型,及商品实际价格
            #region ***************************实物中奖信息***************************
            if (string.IsNullOrEmpty(mobile))
            {
                context.Response.Write("{\"state\":5}");    //手机号有误
                return;
            }
            if (string.IsNullOrEmpty(info))
            {
                context.Response.Write("{\"state\":4}");    //用户信息有误
                return;
            }
            if (string.IsNullOrEmpty(orderid))
            {
                context.Response.Write("{\"state\":3}");    //订单ID错误
                return;
            }
            DataTable dt ;
            switch (currentOrderInfo.Ordertype)
            {
                case "yuan":
                    dt = CustomsBusiness.GetListData("go_orders o join go_yiyuan y on o.ordertype='yuan' And o.businessId=y.yId join go_products p on y.productid=p.productid", "o.orderId='" + orderid + "'", "p.typeid", null, 0, DbServers.DbServerName.LatestDB);
                    break;
                case "gift"://积分商城奖品
                    dt = CustomsBusiness.GetListData("go_orders o join go_gifts gg on o.businessId = gg.giftId join go_products gp on gg.productid = gp.productid", "o.orderId = '" + orderid + "'", "gp.typeid", null, 0, DbServers.DbServerName.LatestDB);
                    break;
                case "quan"://直接购买商品
                    dt = CustomsBusiness.GetListData("go_orders o  join go_zhigou gz on o.businessid = gz.quanid join  go_products gp on gz.productid = gp.productid", "o.orderId = '" + orderid + "'", "gp.typeid", null, 0, DbServers.DbServerName.LatestDB);
                    break;
                case "tuan":
                    dt = CustomsBusiness.GetListData("go_tuan_listinfo t  join go_orders o on t.tuanlistId=o.businessId join go_tuan tu on tu.tId=t.tuanId join go_products p  on p.productid=tu.productid", "o.orderId='" + orderid + "'", "p.typeid", null, 0, DbServers.DbServerName.LatestDB);
                    break;
                default :
                    dt = null;
                    break;
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":6}");    //获取订单信息有误
                return;
            }
            /*
            dt = CustomsBusiness.GetListData("go_orders o join go_yiyuan y on o.ordertype='yuan' And o.businessId=y.yId join go_products p on y.productid=p.productid", "o.orderId='" + orderid + "'", "p.typeid", null, 0, DbServers.DbServerName.LatestDB);
            if (dt.Rows.Count == 0)
            {
                dt = CustomsBusiness.GetListData("go_tuan_listinfo t  join go_orders o on t.tuanlistId=o.businessId join go_tuan tu on tu.tId=t.tuanId join go_products p  on p.productid=tu.productid", "o.orderId='" + orderid + "'", "p.typeid", null, 0, DbServers.DbServerName.LatestDB);
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"state\":6}");    //获取订单信息有误
                    return;
                }
            }
             */ 
            ordersEntity = go_ordersBusiness.LoadEntity(orderid);
            if (ordersEntity.Status == "待发货")
            {
                context.Response.Write("{\"state\":7}");    //你的信息已提交过，请等待发货
                return;
            }
            ordersEntity.Order_address_info = info;
            ordersEntity.Phone = mobile;
            ordersEntity.Status = "待发货";
            if (go_ordersBusiness.SaveEntity(ordersEntity, false, DbServers.DbServerName.LatestDB))
            {
                //移出缓存
                CacheHelper.DelCache("affirminfo_" + uid);

                //输出json
                context.Response.Write("{\"state\":10}"); //信息已提交

            }
            #endregion
        }
        /// <summary>
        /// 更新卡密表以及订单表数据
        /// </summary>
        /// <param name="sdt">卡密表数据集</param>
        /// <param name="usetype">使用类型</param>
        /// <param name="usename">用户真实姓名</param>
        /// <param name="usenum">充值账号</param>
        /// <param name="ordersEntity">订单实体类</param>
        /// <returns></returns>
        private static bool str(DataTable sdt, string usetype, string usename, string usenum, go_ordersEntity ordersEntity)
        {
            go_card_rechargeEntity rechargeEntity = null;
            bool isgx = false;
            for (int i = 0; i < sdt.Rows.Count; i++)
            {
                rechargeEntity = go_card_rechargeBusiness.LoadEntity(Convert.ToInt32(sdt.Rows[i]["id"]));
                rechargeEntity.Isrepeat = "1";
                rechargeEntity.Usetime = DateTime.Now;
                rechargeEntity.Usetype = usetype;
                rechargeEntity.Usebeizhu = usename + usenum;
                isgx = go_card_rechargeBusiness.SaveEntity(rechargeEntity, false, DbServers.DbServerName.LatestDB);
                if (isgx)
                {
                    string okorderid = sdt.Rows[0]["orderId"].ToString();
                    if (ordersEntity == null)
                        ordersEntity = go_ordersBusiness.LoadEntity(okorderid);
                        ordersEntity.Status = "已收货";
                    ordersEntity.Order_address_info = ordersEntity.Order_address_info + "。备注：" + usenum;
                    if (go_ordersBusiness.SaveEntity(ordersEntity, false, DbServers.DbServerName.LatestDB))
                    {
                        isgx = true;
                    }
                    else
                    {
                        isgx = false;
                    }
                }
                else
                {
                    isgx = false;
                }
            }
            return isgx;
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        private void confirmshouhuo(HttpContext context)
        {
            #region *********************************参数验证和获取*********************************
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string orderid = context.Request["orderid"];
            if (string.IsNullOrEmpty(orderid))
            {
                context.Response.Write("{\"state\":3}");    //订单ID错误
                return;
            }
            #endregion

            //获取订单信息
            DataTable dt = go_ordersBusiness.GetListData("orderId='" + orderid + "' and uid='" + uid + "'", "*", null, 1, DbServers.DbServerName.LatestDB);
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":4}");    //订单信息有误
                return;
            }
            if (dt.Rows[0]["status"].ToString() == "已发货")
            {
                go_ordersEntity ordersEntity = go_ordersBusiness.LoadEntity(dt.Rows[0]["orderId"].ToString(), DbServers.DbServerName.LatestDB);
                ordersEntity.Status = "已收货";
                if (!go_ordersBusiness.SaveEntity(ordersEntity, false))
                {
                    context.Response.Write("{\"state\":5}");    //保存信息失败
                    return;
                }
                context.Response.Write("{\"state\":0}");    //更改状态成功
                return;
            }
        }




        /// <summary>
        /// 所有晒单信息
        /// </summary>
        private void allshaidan(HttpContext context)
        {
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                uid = "0";
            }
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = "(select m.username,m.img,m.headimg,s.sd_id,s.sd_title,s.sd_content,s.sd_ip,s.sd_photolist,s.sd_time,y.originalid AS productid,s.sd_zan,s.sd_ping,praise=(case when sz.sdhf_userid IS NULL then 0 else 1 end)from go_shaidan s join go_orders o on s.sd_orderid=o.orderId join go_yiyuan y on y.yId=o.businessId left join go_member m on s.sd_userid=m.uid left Join go_shaidan_zan SZ ON S.sd_id=SZ.sd_id And sz.sdhf_userid='" + uid + "'";
            string orderby = "sd_time desc";
            string where = string.Format("where 1=1) t" );
            //获取用户充值列表及页码
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
            if (dtData.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":4}");    //此商品还没有晒单
                return;
            }
            foreach (DataRow dr in dtData.Rows)
            {
                ShopOrders.SetTouxiang(dr);
            }
            dtData.Columns.Remove("headimg");
            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
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