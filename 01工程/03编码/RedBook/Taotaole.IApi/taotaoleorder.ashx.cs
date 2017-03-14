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
    /// taotaoleorder 的摘要说明
    /// </summary>
    public class taotaoleorder : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "mylist": //我的云购记录
                    mylist(context);
                    break;
                case "mywinninglist": //我的中奖记录
                    mywinninglist(context);
                    break;
                case "mysharelist": //我的晒单记录
                    mysharelist(context);
                    break;
                case "partakewin":  //参与详情
                    partakewin(context);
                    break;
                case "otheroneselfpartake"://别人的云购记录
                    otheroneselfpartake(context);
                    break;
                case "otherfpartakewin"://别人的中奖记录
                    otherfpartakewin(context);
                    break;
                case "othersharelist": //别人的晒单记录
                    othersharelist(context);
                    break;
                case "winningremind":   //中奖提醒
                    winningremind(context);
                    break;
                case "ZhiOrderList": //直购订单
                    ZhiOrderList(context);
                    break;
            }
        }
        /// <summary>
        /// 我的直购订单
        /// </summary>
        /// <param name="context"></param>
        private void ZhiOrderList(HttpContext context)
        {
            string orderId= "";
            string money = "";
            string status = "";
            string time = "";
            string thumb = "";
            string title = "";
            string  str="";
            #region  参数的验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            //string uid = "1";
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string ordertype = "quan";
            #endregion
            DataTable dtQuanOrder = go_ordersBusiness.SelectZhiData("uid=" + uid + " and ordertype='" + ordertype + "'","[time] desc");
            if (dtQuanOrder.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":3}");    //无直购订单
                return;
            }
            for(int  i=0;i<dtQuanOrder.Rows.Count;i++)
            {
                orderId= dtQuanOrder.Rows[i]["orderId"].ToString().Trim();
                money= dtQuanOrder.Rows[i]["money"].ToString().Trim();
                status= dtQuanOrder.Rows[i]["status"].ToString().Trim();
                time= dtQuanOrder.Rows[i]["time"].ToString().Trim();
                thumb= dtQuanOrder.Rows[i]["thumb"].ToString().Trim();
                title= dtQuanOrder.Rows[i]["title"].ToString().Trim();
                str += "{" + string.Format("\"orderId\":\"{0}\",\r\"money\":\"{1}\",\r\"status\":\"{2}\",\r\"time\":\"{3}\",\r\"title\":\"{4}\",\r\"thumb\":\"{5}\",\r\"imgroot\":\"{6}\"", orderId, money, status, time, title, thumb, Globals.G_UPLOAD_PATH) + "},";
            }
            string result = "[" + str.TrimEnd(',') + "]";
            context.Response.Write(result);
        }
         
        /// <summary>
        /// 获取我的一元购记录
        /// sign:用户ID随机签名
        /// typeid:类别码，默认为-1。-1表示参与云购的商品_全部，1表示参与云购的商品_进行中，2表示_参与云购的商品已揭晓
        /// p:页码
        /// </summary>
        private void mylist(HttpContext context)
        {
            #region 参数的验证和获取

            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            //类型
            string typeid = context.Request["typeid"];
            if (string.IsNullOrEmpty(typeid) || !PageValidate.IsNumberPlus(typeid)) typeid = "0";
            //页码
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            #endregion

            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = string.Format(@"(select O.*,y.yid,p.operation,Y.title,Y.qishu,P.thumb,P.typeid,Y.zongjiage,Y.zongrenshu,Y.canyurenshu,Y.shengyurenshu,Y.originalid as productid,y.q_showtime,Y.q_uid,Y.q_code,CONVERT(varchar,Y.q_end_time,121) as q_end_time,m.username,m.headimg
	                            ,O2.q_sumquantity,(case when y.canyurenshu<y.zongrenshu then 1 when y.canyurenshu=y.zongrenshu and y.q_end_time is null then 2 when y.q_end_time is not null then 3 end)as yungouState
                                ,(case when y.shengyurenshu=0 then 0 else 1 end ) as yungouJiexiao
                                From (Select uid,recordcode,businessId,SUM(quantity) as quantity,max(CONVERT(varchar,time,121))as time From go_orders where ordertype='yuan' Group By uid,recordcode,businessId)O
                                Inner Join go_yiyuan Y ON O.businessId=Y.yId Inner Join go_products P ON Y.productid=P.productid
                                Left Join go_member M on Y.q_uid=M.uid
                                Left Join(Select uid,businessId,SUM(quantity) as q_sumquantity From go_orders where ordertype='yuan' Group By uid,businessId)O2 ON Y.q_uid=O2.uid And Y.yId=o2.businessId
                                Where o.uid = {0}) u", uid);
            string where = string.Format(" where u.uid={0} ", uid);
            switch (typeid)
            {
                case "1": break;
                case "2":
                    where += " and u.yungouState = 1 ";
                    break;
                case "3":
                    where += " and u.yungouState = 3 ";
                    break;
            }
            string orderby = "u.q_showtime desc, u.yungouJiexiao desc,u.shengyurenshu asc,u.time desc";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);

            dtData.Columns.Add("haomiao", typeof(string));
            foreach (DataRow dr in dtData.Rows)
            {
                if (dr["q_showtime"].ToString() == "1")
                {
                    DateTime dt_q_end_time = DateTime.Parse(dr["q_end_time"].ToString());
                    long timeSpan = dt_q_end_time.Ticks / 10000 - DateTime.Now.Ticks / 10000;
                    dr["haomiao"] = (timeSpan <= 0) ? 0 : timeSpan;
                }
            }
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）


            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);

            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }

        /// <summary>
        /// 获取我的一元购中奖纪录
        /// sign:用户名ID随机签名
        /// p:页码
        /// </summary>
        private void mywinninglist(HttpContext context)
        {
            #region 参数验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            //页码
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            #endregion


            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = string.Format(@"(select o.iswon,o.ordertype,o.uid,CONVERT(varchar,o.time,121) as time,o.orderId,y.originalid as productid,y.q_showtime,y.yid,sh.sd_id,y.title,p.thumb,y.qishu,p.typeid,y.q_code,t.q_sumquantity,ISNULL(s.sd_orderid,0)as hasshaidan
                                                from go_orders o left join go_shaidan sh  on o.orderId=sh.sd_orderid  inner join go_yiyuan y on o.businessId = y.yId and y.q_showtime='0' inner join go_products p on y.productid = p.productid
                                                inner join (select SUM(quantity) as q_sumquantity,uid,businessId from go_orders Where ordertype='yuan'  group by uid,businessId) T on y.q_uid = t.uid and y.yId=t.businessId
                                                left join go_shaidan s on o.orderId = s.sd_orderid
                                                where q_uid = {0} and iswon=1) T", uid);
            string where = string.Format(" where  T.uid={0} ", uid);
            string orderby = " T.time desc";


            //拼接各种信息,并返回Json串
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }

        /// <summary>
        /// 获取我的晒单记录
        /// sign:用户名ID随机签名
        /// p:页码
        /// </summary>
        private void mysharelist(HttpContext context)
        {
            #region 参数验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            //页码
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            #endregion

            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = string.Format(@"(select (case when sz.sd_id >0 then '1' else '0' end) as praise, s.sd_id,s.sd_userid,s.sd_orderid,y.originalid as productid,y.title,s.sd_title,s.sd_content,s.sd_photolist,s.sd_ip,CONVERT(varchar,s.sd_time,121) as sd_time,s.sd_zan,s.sd_ping,m.headimg,m.username
                                    from go_shaidan s inner join go_orders o on s.sd_orderid = o.orderId inner join go_yiyuan y on o.businessId = y.yId left join go_member m on o.uid=m.uid left join go_shaidan_zan sz on s.sd_id=sz.sd_id and sz.start='1' and sz.sdhf_userid='" +uid+"' where sd_userid = {0}) T", uid);
            string where = string.Format(" where 1=1 ", uid);
            string orderby = " T.sd_time desc";

            //拼接各种信息,并返回Json串
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }
        /// <summary>
        /// 参与详情
        /// </summary>
        /// <param name="context"></param>
        private void partakewin(HttpContext context)
        {
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);//查看自己的参与详情
            string shopid = context.Request["shopid"];
            if (string.IsNullOrEmpty(shopid) || !PageValidate.IsNumberPlus(shopid))
            {
                context.Response.Write("{\"state\":3}");    //验证状态，0验证通过 3参数错误
                return;
            }
            string otherid = context.Request["otherid"];    //查看他人的参与详情
            if (!string.IsNullOrEmpty(otherid) && PageValidate.IsNumberPlus(otherid)) uid=otherid ;
            if (string.IsNullOrEmpty(uid) || !PageValidate.IsNumberPlus(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }

            DataTable odt = CustomsBusiness.GetListData("go_yiyuan Y join go_orders O on O.ordertype='yuan' And Y.yId= O.businessId", " y.yId='" + shopid + "' And o.uid='" + uid + "'", "CONVERT(varchar,O.time,121) as time,o.quantity,o.goucode", null, 0);
            DataTable pdt = CustomsBusiness.GetListData("go_yiyuan y  join go_products p on y.productid=p.productid left join go_member m on y.q_uid=m.uid", "y.yId='" + shopid + "'", "y.title,y.originalid as productid,y.q_uid,y.qishu,CONVERT(varchar,y.q_end_time,121) as q_end_time,m.username , p.thumb", null, 0);

            //输出JSON
            string result = string.Format("{{\"state\":\"{0}\",\r\"username\":\"{1}\",\r\"q_end_time\":\"{2}\",\r\"qishu\":\"{3}\",\r\"thumb\":\"{4}\",\r\"title\":\"{5}\",\r\"imgroot\":\"{6}\",\r\"productid\":\"{7}\",\r\"q_uid\":\"{8}\",\r\"data\":"
                , 0, pdt.Rows[0]["username"], pdt.Rows[0]["q_end_time"], pdt.Rows[0]["qishu"], pdt.Rows[0]["thumb"], pdt.Rows[0]["title"], Globals.G_UPLOAD_PATH, pdt.Rows[0]["productid"], pdt.Rows[0]["q_uid"]);
            result += JsonConvert.SerializeObject(odt, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }
        /// <summary>
        /// 别人参与详情
        /// </summary>
        /// <param name="context"></param>
        private void otheroneselfpartake(HttpContext context)
        {
            #region 参数的验证和获取
            //验证签名
            string uid = context.Request["uid"];
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //用户id错误
                return;
            }

            //类型
            string typeid = context.Request["typeid"];
            if (string.IsNullOrEmpty(typeid) || !PageValidate.IsNumberPlus(typeid)) typeid = "0";
            //页码
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            #endregion

            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = string.Format(@"(select O.*,y.yid,p.operation,Y.title,Y.qishu,P.thumb,P.typeid,Y.zongjiage,Y.zongrenshu,Y.canyurenshu,Y.shengyurenshu,Y.originalid as productid,y.q_showtime,Y.q_uid,Y.q_code,CONVERT(varchar,Y.q_end_time,121) as q_end_time,m.username,m.headimg
	                            ,O2.q_sumquantity,(case when y.canyurenshu<y.zongrenshu then 1 when y.canyurenshu=y.zongrenshu and y.q_end_time is null then 2 when y.q_end_time is not null then 3 end)as yungouState
                                ,(case when y.shengyurenshu=0 then 0 else 1 end ) as yungouJiexiao
                                From (Select uid,businessId,SUM(quantity) as quantity,max(CONVERT(varchar,time,121))as time From go_orders where ordertype='yuan' Group By uid,businessId)O
                                Inner Join go_yiyuan Y ON O.businessId=Y.yId Inner Join go_products P ON Y.productid=P.productid
                                Left Join go_member M on Y.q_uid=M.uid
                                Left Join(Select uid,businessId,SUM(quantity) as q_sumquantity From go_orders where ordertype='yuan' Group By uid,businessId)O2 ON Y.q_uid=O2.uid And Y.yId=o2.businessId
                                Where o.uid = {0}) u", uid);
            string where = string.Format(" where u.uid={0} ", uid);
            switch (typeid)
            {
                case "1": break;
                case "2":
                    where += " and u.yungouState = 1 ";
                    break;
                case "3":
                    where += " and u.yungouState = 3 ";
                    break;
            }
            string orderby = "u.q_showtime desc, u.yungouJiexiao desc,u.shengyurenshu asc,u.time desc";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);

            dtData.Columns.Add("haomiao", typeof(string));
            foreach (DataRow dr in dtData.Rows)
            {
                if (dr["q_showtime"].ToString() == "1")
                {
                    DateTime dt_q_end_time = DateTime.Parse(dr["q_end_time"].ToString());
                    long timeSpan = dt_q_end_time.Ticks / 10000 - DateTime.Now.Ticks / 10000;
                    dr["haomiao"] = (timeSpan <= 0) ? 0 : timeSpan;
                }
            }
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）


            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);

            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }
        /// <summary>
        /// 别人获奖记录
        /// </summary>
        private void otherfpartakewin(HttpContext context)
        {
            #region 参数验证和获取
            //验证签名
            string uid = context.Request["uid"];
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //用户id错误
                return;
            }
            //页码
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            #endregion
            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = string.Format(@"(select o.iswon,o.ordertype,o.uid,CONVERT(varchar,o.time,121) as time,o.orderId,y.originalid as productid,y.q_showtime,y.yid,y.title,m.username,o.quantity,p.thumb,p.typeid,y.qishu,y.q_code,t.q_sumquantity,ISNULL(s.sd_orderid,0)as hasshaidan
                                                from go_orders o inner join go_yiyuan y on o.ordertype='yuan' And o.businessId = y.yId and y.q_showtime='0' join go_member m on o.uid=m.uid inner join go_products p on y.productid = p.productid
                                                inner join (select SUM(quantity) as q_sumquantity,uid,businessId from go_orders where ordertype='yuan'  group by uid,businessId) T on y.q_uid = t.uid and y.yId=t.businessId
                                                left join go_shaidan s on o.orderId = s.sd_orderid
                                                where q_uid = {0} and iswon=1) T", uid);
            string where = string.Format(" where T.uid={0} ", uid);
            string orderby = " T.time desc";
            //拼接各种信息,并返回Json串
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
            if (dtData.Rows.Count == 0)
            {
                context.Response.Write("{\"state\":1}");    //还没有中奖纪录
                return;
            }
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"username\":\"{5}\"\r,\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH, dtData.Rows[0]["username"]);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }
        /// <summary>
        /// 别人晒单列表
        /// </summary>
        /// <param name="context"></param>
        private void othersharelist(HttpContext context)
        {
            #region 参数验证和获取
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                 uid = context.Request["otherid"]; 
            }
            //页码
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            #endregion

            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = string.Format(@"(select s.sd_id,s.sd_userid,s.sd_orderid,y.originalid as productid,y.title,s.sd_title,s.sd_content,s.sd_photolist,s.sd_ip,CONVERT(varchar,s.sd_time,121) as sd_time,s.sd_zan,s.sd_ping,m.headimg,m.username
                                    from go_shaidan s inner join go_orders o on s.sd_orderid = o.orderId inner join go_yiyuan y on o.businessId = y.yId left join go_member m on o.uid=m.uid
                                    where sd_userid = {0}) T", uid);
            string where = string.Format(" where 1=1 ", uid);
            string orderby = " T.sd_time desc";

            //拼接各种信息,并返回Json串
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
            string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }

        /// <summary>
        /// 中奖提醒
        /// </summary>
        private void winningremind(HttpContext context)
        {
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);//查看自己的参与详情
            if (string.IsNullOrEmpty(uid) || !PageValidate.IsNumberPlus(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }

            string from="go_orders O Inner Join go_yiyuan Y on O.businessId=Y.yId And O.ordertype='yuan' Inner Join go_products P ON Y.productid=P.productid";
            DataTable dtData = CustomsBusiness.GetListData(from, "O.iswon=1 And Y.q_showtime=0 And O.status='待确认' And O.uid=" + uid + "", "O.uid,y.yid,y.originalid as productid,y.title,p.thumb,y.qishu,o.orderid", "o.time desc", 1);
            int iswin = dtData.Rows.Count;
            if (iswin == 0)
            {
                DataRow drData = dtData.NewRow();
                dtData.Rows.Add(drData);
            }

            //输出JSON
            string result = string.Format("{{\"state\":\"{0}\",\r\"iswin\":\"{1}\",\r\"imgroot\":\"{2}\","
                , 0, iswin, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented).TrimStart('[').TrimEnd(']').Replace("{","").Replace("}","");
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
