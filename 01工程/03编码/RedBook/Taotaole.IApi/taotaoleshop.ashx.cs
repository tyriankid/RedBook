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

namespace Taotaole.IApi
{
    /// <summary>
    /// taotaoleshop 的摘要说明
    /// </summary>
    public class taotaoleshop : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string action = context.Request["action"];
            switch (action)
            {
                case "listinfo":        //获取商品列表
                    listinfo(context);
                    break;
                case "detail":          //获取商品详情
                    detail(context);
                    break;
                case "detailrenovate":          //获取商品详情刷新
                    detailrenovate(context);
                    break;
                case "shopidbyqishu":     //获取一元购商品ID
                    shopidbyqishu(context);
                    break;
                case "buyrecord":       //商品参入记录
                    buyrecord(context);
                    break;
                case "history":          //获取往期揭晓
                    history(context);
                    break;
                case "algorithmwon":    //获取计算详情
                    algorithmwon(context);
                    break;
                case "getshopcategory":    //获取商品分类列表
                    getshopcategory(context);
                    break;
                case "reflistinfo":    //获取商品分类列表
                    reflistinfo(context);
                    break;
                case "quanProductList":
                    quanProductList(context);//获取直购商品列表
                        break;
                case "quanDetail":
                        quanDetail(context);//获取直购商品详情
                        break;
                case "quanCategoryList"://获取直购页面的分类展示信息
                        quanCategoryList(context);
                        break;
                case "getshopBrand"://获取直购页面的品牌展示信息
                        getshopBrand(context);
                        break;
                    
            }
        }

        /// <summary>
        /// 获取分类,品牌列表
        /// </summary>
        /// <param name="context"></param>
        private void quanCategoryList(HttpContext context)
        {
            DataTable dtCategories = go_categoryBusiness.GetListData("model='1'", "cateid,name,orders", "orders");
            string result = "{\"category\":[";
            foreach (DataRow row in dtCategories.Rows)
            {
                result += "{";
                result += "\"cateid\":" + "\"" + row["cateid"] + "\"," +
                             "\"name\":" + "\"" + row["name"] + "\",";
                DataTable dtBrands = go_brandBusiness.GetListData("cateid = " + row["cateid"], "name,cateid,id,[order]", "[order]");

                result += "\"brand\":[";
                foreach (DataRow brandRow in dtBrands.Rows)
                {
                    result += "{";
                    result += "\"cateid\":" + "\"" + brandRow["cateid"] + "\"," +
                                 "\"name\":" + "\"" + brandRow["name"] + "\"," +
                                 "\"id\":" + "\"" + brandRow["id"] + "\"" + "},";
                }
                result = result.TrimEnd(',');
                result += "]";
                result += "},";

            }
            result = result.TrimEnd(',');
            result += " ]}";
            context.Response.Write(result);
        }
        /// <summary>
        /// 直购商品详情
        /// </summary>
        /// <param name="context"></param>
        private void quanDetail(HttpContext context)
        {
            string QuanId = context.Request["QuanId"];
            //string QuanId = "2";
            if (string.IsNullOrEmpty(QuanId) || !PageValidate.IsNumberPlus(QuanId))
            {
                QuanId = "0";
            }
            if (QuanId == "0")
            {
                context.Response.Write("{\"state\":2}");    //验证状态，0验证通过 1商品不存在
                return;
            }
            string uid = context.Request["uid"];
            if (string.IsNullOrEmpty(uid) || !PageValidate.IsNumberPlus(uid))
            {
                uid = "0";
            }

            else
            {
                uid = SecurityHelper.Decrypt("uid", uid);
                if (uid == "") uid = "0";
            }
            string from = "select  PD.picarr,Z.QuanMoney,Z.title,Z.stock,PD.productid from go_zhigou Z   left join  go_products   PD  on PD.productid=Z.productid  where  Z.QuanId=" + QuanId + "";
            string where="Z.QuanId=" + QuanId + "";
            DataTable dt = CustomsBusiness.GetListData("go_zhigou Z left join  go_products   PD  on PD.productid=Z.productid", where, "PD.picarr,Z.QuanMoney,Z.title,Z.stock,PD.productid,PD.description,PD.contents", "", 0, DbServers.DbServerName.LatestDB);
            string result = string.Format("{{\"imgroot\":\"{0}\",",Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented).Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
          
            result += "}";
            context.Response.Write(result);
            
        }
        /// <summary>
        /// 获取直购商品列表
        /// </summary>
        /// <param name="context"></param>
        private void quanProductList(HttpContext context)
        {
            string where = "where 1=1";
            string p = context.Request["p"];
            string category = context.Request["category"];
            string brandId = context.Request["brandId"];
            string productType = context.Request["type"];
            string productName = context.Request["productName"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            if (!string.IsNullOrEmpty(brandId))
            {
                where += "  and  PD.brandid=" + brandId + "";
            }
            if (!string.IsNullOrEmpty(category))
            {
                where += " and PD.categoryid=" + category + "";
                
            }
            if (!string.IsNullOrEmpty(productName))
            {
                where += " and Z.title like '%" + productName + "%'";
            }
            if (!string.IsNullOrEmpty(productType))
            {
                switch (productType)
                {
                    case "baokuan":
                        where += "  and  PD.baokuan = 1";
                        break;
                    case "tejia":
                        where += "  and  PD.tejia = 1";
                        break;
                    case "xinshou":
                        where += "  and  PD.newhand= 1";
                        break;
                    case "youhui":
                        where += "  and  PD.youhui= 1";
                        break;
                }
            }
            
            string orderby = "QuanId  asc";
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = "(select  PD.baokuan,PD.tejia,PD.newhand,PD.categoryid,PD.brandid,PD.typeid,PD.thumb,PD.money,Z.*from go_zhigou Z   left join  go_products   PD  on PD.productid=Z.productid  " + where + ")t";
            int count = CustomsBusiness.GetDataCount(from, "", DbServers.DbServerName.LatestDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*"
                , currPage, pagesize, "", DbServers.DbServerName.LatestDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0;
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
              , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }
        /// <summary>
        /// 获取商品列表
        /// typeid @分类ID，orderid @排序规则，page @页码，从1开始。
        /// 返回值 state: 0成功获取 1未获取到数据或数据已加载完毕
        /// </summary>
        private void listinfo(HttpContext context)
        {
            //获取传参
            string typeid = context.Request["typeid"];
            if (string.IsNullOrEmpty(typeid) || !PageValidate.IsNumberPlus(typeid)) typeid = "0";
            string order = context.Request["order"];
            if (string.IsNullOrEmpty(order)) order = "0";
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            //获取总条数、当前页数据、总页码
            int pagesize = 10;
            string ps = context.Request["pagesize"];
            if (string.IsNullOrEmpty(ps) || !PageValidate.IsNumberPlus(ps)) ps = "1";

            //设置条件、排序
            string where = "Where 1=1 ";

            string secsOpen = context.Request["secsOpen"];
            if (!string.IsNullOrEmpty(secsOpen))
            {

                where += " and  zongrenshu<100";

            }
            string orderby = "orders asc";    //默认根据主键排序(分页最快)
            if (typeid != "0") where += string.Format(" And categoryid={0}", typeid);
            //switch (order)
            //{
            //    case "10":  //按剩余人数升序
            //        orderby = "shengyurenshu asc";
            //        break;
            //    case "20":  //人气商品
            //        where += " And renqi=1";
            //        break;
            //    case "30":  //按剩余人数倒序
            //        orderby = "shengyurenshu desc";
            //        break;
            //    case "40":  //按更新时间倒序
            //        orderby = "time desc";
            //        break;
            //    case "50":  //按商品金额倒序
            //        orderby = "zongjiage desc";
            //        break;
            //    case "60":  //按商品金额升序
            //        orderby = "zongjiage asc";
            //        break;
            //}
            if (int.Parse(ps) > 1)
            {
                pagesize = pagesize * int.Parse(ps);
            }
            
            int currPage = int.Parse(p);
            string from = "(Select Y.yId,p.operation,y.originalid as productid,Y.title,y.orders,p.thumb,p.categoryid,y.qishu,y.yunjiage,y.zongrenshu,y.shengyurenshu,y.canyurenshu,p.renqi,y.time,y.zongjiage,y.pricerange From go_yiyuan Y Inner Join go_products P on y.productid=p.productid AND Y.q_uid IS NULL and p.status='0' AND Y.recover=0 and p.operation=0 ) t";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*"
                , currPage, pagesize, where, DbServers.DbServerName.LatestDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
            if (int.Parse(ps) > 1)
                nextPage = (currPage < pageCount) ? (int.Parse(ps) + 1) : 0; //下一页为0时，表示无数据可加载  ;否则为当前的pagesize+1 (用于一次性加载相应数量的商品,避免重复调用该接口)
            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }
        /// <summary>
        /// 获取无刷新商品数据
        /// typeid @分类ID，orderid @排序规则，page @页码，从1开始。reftime @客户端cookie时间
        /// 返回值 state: 0成功获取 1未获取到数据或数据已加载完毕
        /// </summary>
        private void reflistinfo(HttpContext context)
        {
            //获取传参
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            string reftime = context.Request["reftime"];
            if (string.IsNullOrEmpty(reftime)) reftime = DateTime.Now.ToString();
            go_configsEntity ce = go_configsBusiness.LoadEntity(3);
            if (DateTime.Parse(reftime) < ce.Ctime)
                {
                    context.Response.Write("{\"state\":2}");    //需要更新数据
                    return;
                }
            
            //设置条件、排序
            string where = "Where 1=1 ";
            string orderby = "orders asc";    //默认根据主键排序(分页最快)
            int pagesize = 10 * int.Parse(p);
            int currPage = 1;
            string from = "(Select Y.yId,y.originalid as productid,y.shengyurenshu,y.canyurenshu,y.zongrenshu,y.orders From go_yiyuan Y Inner Join go_products P on y.productid=p.productid AND Y.q_uid IS NULL and p.status='0' AND Y.recover=0 and p.operation=0) t";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*"
                , currPage, pagesize, where, DbServers.DbServerName.LatestDB);

            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }
        /// <summary>
        /// 获取商品详情
        /// </summary>
        private void detail(HttpContext context)
        {
            //获取传参
            string shopid = context.Request["shopid"];
            string productid = context.Request["productid"];
            if (string.IsNullOrEmpty(productid) || !PageValidate.IsNumberPlus(productid))
            {
                productid = "0";
            }
            if (string.IsNullOrEmpty(shopid) || !PageValidate.IsNumberPlus(shopid))
            {
                shopid = "0";
            }
            if (productid == "0" && shopid == "0")
            {
                context.Response.Write("{\"state\":2}");    //验证状态，0验证通过 1商品不存在
                return;
            }
            string uid = context.Request["uid"];
            if (string.IsNullOrEmpty(uid) || !PageValidate.IsNumberPlus(uid))
            {
                uid = "0";
            }
            else
            {
                uid = SecurityHelper.Decrypt("uid", uid);
                if (uid == "") uid = "0";
            }

            //判断是否需要获取最新一期的一元购商品信息
            if (productid != "0")
            {
                object o = go_yiyuanBusiness.GetScalar(string.Format("originalid={0}", productid), "yId", "qishu Desc");
                if (o != null)
                {
                    shopid = o.ToString();
                }
            }
            //获取商品详情数据
            DataTable dtproduct = null;
            DataTable dtyiyuan = go_yiyuanBusiness.GetListData("Yid='" + shopid + "'", "YId,Productid,Title,Yunjiage,Zongjiage,Zongrenshu,Canyurenshu,Shengyurenshu,Qishu,Maxqishu,CONVERT(varchar,Time,121) as Time,Q_uid,Q_code,Q_counttime,CONVERT(varchar,Q_end_time,121) as Q_end_time,Q_showtime,Zhiding,HadConfirm,Pricerange,Codetable,Orders,recover,(case when Shengyurenshu>0 then '1'  when Q_showtime=1 then '2' else  '3' end)as Q_showstate,originalid", DbServers.DbServerName.LatestDB);
            if (dtyiyuan.Rows.Count > 0)
            {
                dtproduct = go_productsBusiness.GetListData("productid='" + dtyiyuan.Rows[0]["Productid"] + "'", "productid as QuanProductid,Categoryid,Brandid,Keywords,Description,Money,Thumb,Picarr,Typeid,Renqi,Pos,Status,Contents,Stock,number,Operation ", null, 1, DbServers.DbServerName.LatestDB);

            }
            if (dtyiyuan.Rows.Count == 0 || dtproduct.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":2}");    //验证状态，0验证通过 1商品不存在
                return;
            }
            //我是否购买过
            int isMyBuy = 0;
            if (uid != "0")
            {
                object o = go_ordersBusiness.GetScalar(string.Format("businessId={0} and uid={1}", dtyiyuan.Rows[0]["Q_uid"], uid), "count(*)");
                isMyBuy = int.Parse(o.ToString()) > 0 ? 1 : 0;
            }
            //（如果已揭晓）获取中奖信息
            DataTable dtmember = null;
            int wonSumQuantity = 0;
            if (dtyiyuan.Rows[0]["Q_uid"].ToString() != "")
            {
                dtmember = go_memberBusiness.GetListData("uid='" + dtyiyuan.Rows[0]["Q_uid"] + "'", "Uid,Username,Email,Mobile,Password,User_ip ,Img,Qianming,Groupid,Addgroup,Money,Tuanmoney,Emailcode,Mobilecode,Othercode,Passcode,Reg_key,Score,Luckyb,Jingyan,Yaoqing,Band, CONVERT(varchar,Time,121) as Time,Headimg,Wxid,Typeid,Auto_user,Servicecityid,Servicechannelid,Approuse,Telcode,Ceaseuser,Unionid,Appwxid", null, 0, DbServers.DbServerName.LatestDB);
                object o = go_ordersBusiness.GetScalar(string.Format("businessId={0} and uid={1}", dtyiyuan.Rows[0]["YId"], dtyiyuan.Rows[0]["Q_uid"]), "sum(quantity)");
                wonSumQuantity = int.Parse(o.ToString());
            }
            //我是否中奖（当前期当前商品）
            int isMyWon = (uid != "0" && int.Parse(uid) == Convert.ToInt32(dtyiyuan.Rows[0]["Q_uid"]) ? 1 : 0);

            //输出JSON
            string result = string.Format("{{\"state\":\"{0}\",\r\"imgroot\":\"{1}\",\r\"ismybuy\":{2},\r\"ismywon\":{3},"
                , 0, Globals.G_UPLOAD_PATH, isMyBuy, isMyWon);
            if (dtmember != null)
            {
                if (dtmember.Rows.Count > 0)
                {
                    string touxiang = dtmember.Rows[0]["Headimg"].ToString();
                    if (touxiang == "")
                    {
                        touxiang = Globals.G_UPLOAD_PATH + "/" + dtmember.Rows[0]["Img"].ToString();
                    }
                    result += string.Format("\r\"q_username\":\"{0}\",\r\"q_img\":\"{1}\",\r\"q_sumquantity\":\"{2}\","
                        , dtmember.Rows[0]["Username"], touxiang, wonSumQuantity);
                }
            }
            dtyiyuan.Rows[0]["Productid"] = dtyiyuan.Rows[0]["originalid"];
            result += JsonConvert.SerializeObject(dtyiyuan, Newtonsoft.Json.Formatting.Indented).Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "") + ",";
            result += JsonConvert.SerializeObject(dtproduct, Newtonsoft.Json.Formatting.Indented).Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
            result += "}";
            context.Response.Write(result);
        }

        /// <summary>
        /// 获取商品详情刷新
        /// </summary>
        private void detailrenovate(HttpContext context)
        {
            //获取传参
            string shopid = context.Request["shopid"];
            string productid = context.Request["productid"];
            if (string.IsNullOrEmpty(productid) || !PageValidate.IsNumberPlus(productid))
            {
                productid = "0";
            }
            if (string.IsNullOrEmpty(shopid) || !PageValidate.IsNumberPlus(shopid))
            {
                shopid = "0";
            }
            if (productid == "0" && shopid == "0")
            {
                context.Response.Write("{\"state\":2}");    //验证状态，0验证通过 1商品不存在
                return;
            }
            string uid = context.Request["uid"];
            if (string.IsNullOrEmpty(uid) || !PageValidate.IsNumberPlus(uid))
            {
                uid = "0";
            }
            else
            {
                uid = SecurityHelper.Decrypt("uid", uid);
                if (uid == "") uid = "0";
            }

            //判断是否需要获取最新一期的一元购商品信息
            if (productid != "0")
            {
                object o = go_yiyuanBusiness.GetScalar(string.Format("originalid={0}", productid), "yId", "qishu Desc");
                if (o != null)
                {
                    shopid = o.ToString();
                }
            }
            //获取商品详情数据
            DataTable dtyiyuan = go_yiyuanBusiness.GetListData("Yid='" + shopid + "'", "YId,Productid,Title,Yunjiage,Zongjiage,Zongrenshu,Canyurenshu,Shengyurenshu,Qishu,Maxqishu,CONVERT(varchar,Time,121) as Time,Q_uid,Q_code,Q_counttime,CONVERT(varchar,Q_end_time,121) as Q_end_time,Q_showtime,Zhiding,HadConfirm,Pricerange,Codetable,Orders,recover,(case when Shengyurenshu>0 then '1'  when Q_showtime=1 then '2' else  '3' end)as Q_showstate,originalid", DbServers.DbServerName.LatestDB);
            if (dtyiyuan.Rows.Count == 0 )
            {
                context.Response.Write("{\"state\":2}");    //验证状态，0验证通过 1商品不存在
                return;
            }
            //我是否购买过
            int isMyBuy = 0;
            if (uid != "0")
            {
                object o = go_ordersBusiness.GetScalar(string.Format("businessId={0} and uid={1}", dtyiyuan.Rows[0]["Q_uid"], uid), "count(*)");
                isMyBuy = int.Parse(o.ToString()) > 0 ? 1 : 0;
            }
            //（如果已揭晓）获取中奖信息
            DataTable dtmember = null;
            int wonSumQuantity = 0;
            if (dtyiyuan.Rows[0]["Q_uid"].ToString() != "")
            {
                dtmember = go_memberBusiness.GetListData("uid='" + dtyiyuan.Rows[0]["Q_uid"] + "'", "Uid,Username,Email,Mobile,Password,User_ip ,Img,Qianming,Groupid,Addgroup,Money,Tuanmoney,Emailcode,Mobilecode,Othercode,Passcode,Reg_key,Score,Luckyb,Jingyan,Yaoqing,Band, CONVERT(varchar,Time,121) as Time,Headimg,Wxid,Typeid,Auto_user,Servicecityid,Servicechannelid,Approuse,Telcode,Ceaseuser,Unionid,Appwxid", null, 0, DbServers.DbServerName.LatestDB);
                object o = go_ordersBusiness.GetScalar(string.Format("businessId={0} and uid={1}", dtyiyuan.Rows[0]["YId"], dtyiyuan.Rows[0]["Q_uid"]), "sum(quantity)");
                wonSumQuantity = int.Parse(o.ToString());
            }
            //我是否中奖（当前期当前商品）
            int isMyWon = (uid != "0" && int.Parse(uid) == Convert.ToInt32(dtyiyuan.Rows[0]["Q_uid"]) ? 1 : 0);
            DataTable DT = go_productsBusiness.GetListData("productid='" + dtyiyuan.Rows[0]["Productid"] + "'", "Description", null, 1, DbServers.DbServerName.ReadHistoryDB);
            //输出JSON
            string result = string.Format("{{\"state\":\"{0}\",\r\"imgroot\":\"{1}\",\r\"ismybuy\":{2},\r\"Description\":\"{3}\",\r\"ismywon\":{4},"
                , 0, Globals.G_UPLOAD_PATH, isMyBuy, DT.Rows[0]["Description"], isMyWon);
            if (dtmember != null)
            {
                if (dtmember.Rows.Count > 0)
                {
                    string touxiang = dtmember.Rows[0]["Headimg"].ToString();
                    if (touxiang == "")
                    {
                        touxiang = Globals.G_UPLOAD_PATH + "/" + dtmember.Rows[0]["Img"].ToString();
                    }
                    result += string.Format("\r\"q_username\":\"{0}\",\r\"q_img\":\"{1}\",\r\"q_sumquantity\":\"{2}\","
                        , dtmember.Rows[0]["Username"], touxiang, wonSumQuantity);
                }
            }
           
            dtyiyuan.Rows[0]["Productid"] = dtyiyuan.Rows[0]["originalid"];
            
            result += JsonConvert.SerializeObject(dtyiyuan, Newtonsoft.Json.Formatting.Indented).Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
            result += "}";
            context.Response.Write(result);
        }


        /// <summary>
        /// 获取一元购商品ID
        /// </summary>
        private void shopidbyqishu(HttpContext context)
        {
            //获取传参
            string qishu = context.Request["qishu"];
            string productid = context.Request["productid"];
            if (string.IsNullOrEmpty(qishu) || !PageValidate.IsNumberPlus(qishu)) qishu = "0";
            if (string.IsNullOrEmpty(productid) || !PageValidate.IsNumberPlus(productid)) productid = "0";
            if (qishu == "0" || productid == "0")
            {
                context.Response.Write("{\"state\":2}");    //验证状态，0验证通过 2商品不存在
                return;
            }

            object oid=go_yiyuanBusiness.GetScalar(string.Format("productid={0} And qishu={1}",productid,qishu),"yId");
            string yid=(oid==null)?"":oid.ToString();

            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"yid\":\"{1}\""
                , 0, yid);
            result += "}";
            context.Response.Write(result);
        }

        /// <summary>
        /// 商品参入记录
        /// </summary>
        /// <param name="context"></param>
        private void buyrecord(HttpContext context)
        {
            //获取传参
            string ShopID = context.Request["ShopID"];
            if (string.IsNullOrEmpty(ShopID) || !PageValidate.IsNumberPlus(ShopID))
            {
                context.Response.Write("{\"state\":2}");    //验证状态，0验证通过 1商品不存在 2商品id错误
                return;
            }
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";

            //设置条件、排序
            string where = "Where 1=1";
            string orderby = "orderId asc";    //默认根据主键排序(分页最快)
            //获取总条数、当前页数据、总页码
            int pagesize = 10;
            int currPage = int.Parse(p);
            if (ShopID != "0") where += string.Format(" And yid={0}", ShopID);
  
             orderby = "time desc";

             string from = "(select A.username, A.uid,A.img,B.ip,CONVERT(varchar,B.time,121) as time,B.quantity,B.businessId as yid,A.headimg from go_member A inner join go_orders B on A.uid=B.uid and b.ordertype='yuan') t";
             int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
             DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*"
                 , currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
             if (dtData.Rows.Count == 0)
             {
                 context.Response.Write("{\"state\":3}");    //验证状态，0验证通过 1商品不存在 3暂无记录
                 return;
             }
             int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
             int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
             string firsttime = "";
             if (pageCount == count)
             {
                 DataView dv = dtData.DefaultView;
                 dv.Sort = "time asc";
                 DataTable dt = dv.ToTable();
                 firsttime = dt.Rows[0]["time"].ToString();
             }
             else
             {
                 object o = go_ordersBusiness.GetScalar("businessId=" + ShopID + "", "time", "time asc");
                 firsttime = o.ToString();
             }
            
             foreach (DataRow dr in dtData.Rows)
             {
                 ShopOrders.SetTouxiang(dr);
             }
             //输出JSON
             string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"firsttime\":\"{4}\",\r\"imgroot\":\"{5}\",\r\"data\":"
                 , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, firsttime, "");
             result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
             result += "}";
             context.Response.Write(result);

        }
        /// <summary>
        /// 获取往期揭晓
        /// </summary>
        /// <param name="context"></param>
        /// <param name="isApp"></param>
        private void history(HttpContext context)
        {
            //获取传参
            string productid = context.Request["productid"];
            if (string.IsNullOrEmpty(productid) || !PageValidate.IsNumberPlus(productid))
            {
                context.Response.Write("{\"state\":2}");    //验证状态，0验证通过 1商品不存在 2商品参数有误
                return;
            }
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";

            //设置条件、排序
            string where = "Where 1=1";
            string orderby = "yId asc";    //默认根据主键排序(分页最快)
            //获取总条数、当前页数据、总页码
            int pagesize = 10;
            int currPage = int.Parse(p);
            if (productid != "0") where += string.Format(" And productid={0} and q_uid is not null And q_showtime=0", productid);
            orderby = "qishu desc";
            string from = @"(select Y.yId,p.operation, M.username,M.img,M.uid,m.headimg ,y.originalid as productid,Y.qishu,Y.q_showtime ,O2.quantity ,Y.q_code, Y.q_uid ,CONVERT(varchar,Y.q_end_time,121) as q_end_time 
                    from go_orders O JOIN go_yiyuan Y ON O.ordertype='yuan' And O.businessId=Y.yid And O.iswon=1 
                    join (Select uid,businessId,SUM(quantity) as quantity From go_orders  where ordertype='yuan' Group by uid,businessId) O2 ON O.uid=O2.uid And o.businessId=o2.businessId
                    join go_member M on Y.q_uid=M.uid  join go_products p on y.productid=p.productid ) t";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*"
                , currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）

            //设置用户头像
            foreach (DataRow dr in dtData.Rows)
            {
                ShopOrders.SetTouxiang(dr);
            }
            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, "");
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }
        /// <summary>
        /// 计算详情
        /// </summary>
        /// <param name="context"></param>
        private void algorithmwon(HttpContext context)
        {
            //获取传参
            string shopid = context.Request["shopid"];
            if (string.IsNullOrEmpty(shopid) || !PageValidate.IsNumberPlus(shopid))
            {
                context.Response.Write("{\"state\":2}");    //验证状态，0验证通过 1商品不存在
                return;
            }
            go_yiyuanEntity yiyuanEntity = go_yiyuanBusiness.LoadEntity(int.Parse(shopid));
            if (yiyuanEntity == null)
            {
                context.Response.Write("{\"state\":4}");    //验证状态，4代表获取信息失败，没有此商品的信息
                return;
            }
            //如果剩余人数等于0 则代表已开奖或正在开奖
            if (yiyuanEntity.Shengyurenshu != 0)
            {
                context.Response.Write("{\"state\":3}");    //验证状态，0验证通过 1商品不存在 3改商品还在进行销售中
                return;
            }
            //揭晓时间
            string jiexiaotime = yiyuanEntity.Q_end_time.ToString();
            //获取总人数
            int zongrenshu = yiyuanEntity.Zongrenshu;
            //获取本商品本期最后购买时间
            DateTime dtnow = Convert.ToDateTime(go_ordersBusiness.GetScalar("businessId='" + shopid + "' order by time desc", "top 1 time", null));
            string o = dtnow.AddSeconds(-10).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //查询购买人名称和购买时间
            DataTable odt = CustomsBusiness.GetListData("go_orders O Inner Join go_member M on o.uid=M.uid", " o.time <'" + o + " 'order by time desc", "  M.username,CONVERT(varchar,o.time,121) as time", null, 100);
            //添加一列，时间转换列
            odt.Columns.Add("changedata", typeof(string));
            long qiuhe = 0;
            //将每一行的时间进行转换，并添加到新的行
            for (int i = 0; i < odt.Rows.Count; i++)
            {
                odt.Rows[i]["changedata"] = Convert.ToDateTime(odt.Rows[i]["time"]).ToString("HHmmssfff");
                //每循环一行，则将转换的数据累加起来
                qiuhe += Convert.ToInt32(odt.Rows[i]["changedata"]);
            }
            //取模
            long y = qiuhe % zongrenshu;
            string result = string.Format("{{\"state\":{0},\r\"qiuhe\":{1},\r\"zongrenshu\":{2},\r\"y\":{3},\r\"maxtime\":\"{4}\",\r\"jiexiaotime\":\"{5}\",\r\"quyu\":{6},\r\"data\":"
            , (odt.Rows.Count > 0) ? 0 : 1, qiuhe, zongrenshu, y + 10000001, o, jiexiaotime, y, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(odt, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }
        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="context"></param>
        private void getshopcategory(HttpContext context)
        {   
           DataTable dt = go_categoryBusiness.GetListData("model='1'", "name,cateid,pic_url", "orders asc", 0);
           if (dt.Rows.Count == 0)
           {
               context.Response.Write("{\"state\":2}");    //验证状态，2商品分类不存在
               return;
           }
           dt.Columns.Add("xuanzhong", typeof(string));
           dt.Columns.Add("weixuanzhong", typeof(string));
           for (int i = 0; i< dt.Rows.Count; i++)
           {
               dt.Rows[i]["xuanzhong"] = dt.Rows[i]["pic_url"].ToString().Split('|')[0];
               dt.Rows[i]["weixuanzhong"] = dt.Rows[i]["pic_url"].ToString().Split('|')[1];
           }
           dt.Columns.Remove("pic_url");
           //输出JSON
           string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"imgroot\":\"{2}\",\r\"data\":"
               , (dt.Rows.Count > 0) ? 0 : 1, dt.Rows.Count, Globals.G_UPLOAD_PATH);
           result += JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
           result += "}";
           context.Response.Write(result);

        }
        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <param name="context"></param>
        private void getshopBrand(HttpContext context)
        {
            DataTable dtBrands = new DataTable();
            string cateId = context.Request["cateId"];
            if (!string.IsNullOrEmpty(cateId))
            {
                dtBrands = go_brandBusiness.GetListData("cateid = " + cateId, "name,cateid,id,[order]", "[order]");
            }
            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"data\":"
                , (dtBrands.Rows.Count > 0) ? 0 : 1, dtBrands.Rows.Count);
            result += JsonConvert.SerializeObject(dtBrands, Newtonsoft.Json.Formatting.Indented);
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
