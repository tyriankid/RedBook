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
    /// taotaoletuan 的摘要说明
    /// </summary>
    public class taotaoletuan : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "tuanlist": 
                    tuanlist(context);
                    break;
                case "tuandetail":
                    tuandetail(context);
                    break;
                case "tuanjoinlist":
                    tuanjoinlist(context);
                    break;
                case "tuanjoinlistdetail":
                    tuanjoinlistdetail(context);
                    break;
                case "tuanmylist":
                    tuanmylist(context);
                    break;
                case "tuanorderinfo":
                    tuanorderinfo(context);
                    break;
                case "tuanwinnerinfo":
                    tuanwinnerinfo(context);
                    break;
                case "tuanlike":
                    tuanlike(context);
                    break;
                case "tuankeep":
                    tuankeep(context);
                    break;
                case "tuankeeplist":
                    tuankeeplist(context);
                    break;
            }
        }

        private void tuandetail(HttpContext context)
        {
            //接收传参
            string tid = context.Request["tid"];
            if (string.IsNullOrEmpty(tid) || !PageValidate.IsNumberPlus(tid))
            {
                context.Response.Write("{\"state\":2}"); return;//参数错误
            }

            string from = @"(select gt.tId,gt.productid,gt.title,gt.per_price,gt.total_num,gt.max_sell,gt.sort,gt.is_delete,gp.thumb,gp.picarr,gp.contents,gp.money,gp.description
                                    from go_tuan gt left join go_products gp on gt.productid=gp.productid where is_delete = 0 and  tid = " + tid + ") t";
            DataTable dtData = CustomsBusiness.GetListData(from);
            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }

        private void tuanlist(HttpContext context)
        {
            //接收传参
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";

            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = @"(select gt.tId,gt.productid,gt.title,gt.per_price,gt.total_num,gt.start_time,gt.end_time,gt.sort,gt.is_delete,gp.thumb,gp.money
                                    from go_tuan gt left join go_products gp on gt.productid=gp.productid ) t";
            string where = " where is_delete = 0 and GETDATE() between start_time and ISNULL(end_time,'9999-12-30') ";
            string orderby = " [sort] desc";

            //获取用户充值列表及页码
            int count = CustomsBusiness.GetDataCount(from, where);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where);
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
        /// 参团信息
        /// tid:
        /// </summary>
        private void tuanjoinlist(HttpContext context)
        {
            try
            {
                //接收传参并解密
                string tid = context.Request["tid"];
                if (string.IsNullOrEmpty(tid) || !PageValidate.IsNumberPlus(tid))
                {
                    context.Response.Write("{\"state\":2}"); return;//参数错误
                }

                //接收传参
                string p = context.Request["p"];
                if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";

                //根据页码获取数据集,总条数,下一页等信息
                int pagesize = 10;
                int currPage = int.Parse(p);
                //构建查询表
                string from = @"(select gtl.tuanid,rtrim(gtl.tuanlistId) as tuanlistId,gt.start_time,gt.end_time,gt.deadline,gtl.uid,gtl.addtime,gtl.remain_num ,gm.username,gm.user_ip,gm.headimg
                            from go_tuan_listinfo as gtl 
                            inner join go_tuan as gt on gtl.tuanId = gt.tId 
                            inner join go_member gm on gtl.uid=gm.uid) t";
                string where = string.Format(" where tuanId = {0} ",tid);
                string orderby = " addtime ";
                //获取列表及页码
                int count = CustomsBusiness.GetDataCount(from, where);
                //得到数据集
                DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where);
                int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
                int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
                foreach (DataRow dr in dtData.Rows)
                {
                    dr["tuanlistId"] = SecurityHelper.Encrypt(Globals.TaotaoleWxKey, dr["tuanlistId"].ToString());
                }

                //输出JSON
                string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                    , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
                result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
                result += "}";

                ////输出JSON
                //string result = string.Format("{{\"state\":{0},\r\"data\":", (dtData.Rows.Count > 0) ? 0 : 1);
                //result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
                //result += "}";

                context.Response.Write(result);
            }
            catch (Exception ex)
            {
                Globals.DebugLogger(ex.Message);
            }
        }

        /// <summary>
        /// 参团明细
        /// tid:
        /// tlistid:
        /// </summary>
        private void tuanjoinlistdetail(HttpContext context)
        {
            //验证签名
            //string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            //if (string.IsNullOrEmpty(uid))
            //{
            //    context.Response.Write("{\"state\":2}");    //未知的签名参数
            //    return;
            //}

            try
            {
                //接收传参并解密
                string tid = context.Request["tid"];
                string tlistid = context.Request["tlistid"];
                tlistid = SecurityHelper.Decrypt(Globals.TaotaoleWxKey, tlistid);
                if (string.IsNullOrEmpty(tlistid)) tlistid = context.Request["tlistid"];
                if (string.IsNullOrEmpty(tid) || !PageValidate.IsNumberPlus(tid) || string.IsNullOrEmpty(tlistid) || !PageValidate.IsNumberPlus(tlistid))
                {
                    context.Response.Write("{\"state\":2}"); return;//参数错误
                }

                string selectSql = string.Format(@"(select tuan.tId,tuan.productid,tuan.title,tuan.per_price,tuan.start_time,tuan.end_time,tuan.deadline,tuan.status,gp.thumb,tl.uid as tzid,tl.tuanlistId,tl.total_num,tl.involvement_num,tl.remain_num,o.uid,o.time,m.username as tzname,m.user_ip as tzip,m.headimg as tzimg,om.username as tyname,om.user_ip as tyip,om.headimg as tyimg,tl.status tuanstate  from go_tuan as tuan 
						inner join go_tuan_listinfo as tl on tuan.tId = tl.tuanId 
                        inner join (select * from go_orders where ordertype = 'tuan') as o on o.businessId = tl.tuanlistId 
                        left join go_products gp on tuan.productid=gp.productid 
                        left join dbo.go_member m on tl.uid=m.uid 
                        left join dbo.go_member om on o.uid=om.uid) t 
                        where tuanlistId = {0} ", tlistid);
                string strOrderby = " time ";
                //查询数据集
                DataTable dtData = CustomsBusiness.GetListData(selectSql, null, "*", strOrderby);

                int iTuanState = 1;//默认1为已开奖
                string strStateTuan = (dtData.Rows.Count > 0) ? dtData.Rows[0]["status"].ToString() : "";//团状态
                if (strStateTuan == "0")
                {
                    //团时间
                    DateTime StartTuan = (dtData.Rows.Count > 0) ? Convert.ToDateTime(dtData.Rows[0]["start_time"].ToString()) : DateTime.MinValue;//开始时间
                    DateTime EndTuan = (dtData.Rows.Count > 0) ? Convert.ToDateTime(dtData.Rows[0]["end_time"].ToString()) : DateTime.MinValue;//结束时间

                    DateTime Deadline = DateTime.MinValue;
                    if (dtData.Rows.Count > 0 && !string.IsNullOrEmpty(dtData.Rows[0]["deadline"].ToString()))
                    {
                        Deadline = Convert.ToDateTime(dtData.Rows[0]["deadline"].ToString());//截至时间
                    }
                    //当前时间
                    DateTime dtNow = DateTime.Now;
                    if (Deadline != DateTime.MinValue)
                    {
                        //大团
                        if (EndTuan > dtNow)
                        {
                            if (Deadline > dtNow)
                            {
                                iTuanState = 0;//进行中
                            }
                            else
                            {
                                iTuanState = -1;//已截止，等待开奖
                            }
                        }
                    }
                    else
                    {
                        //小团
                        if (EndTuan > dtNow)
                            iTuanState = 0;//进行中
                    }
                }

                //团名称
                string strTitle = (dtData.Rows.Count > 0) ? dtData.Rows[0]["title"].ToString() : "";
                //团图片
                string strTImg = (dtData.Rows.Count > 0) ? dtData.Rows[0]["thumb"].ToString() : "";
                //团长ID
                string strTzId = (dtData.Rows.Count > 0) ? dtData.Rows[0]["tzid"].ToString() : "";
                //团长名称
                string strTzName = (dtData.Rows.Count > 0) ? dtData.Rows[0]["tzname"].ToString() : "";
                //团长图片
                string strTzImg = (dtData.Rows.Count > 0) ? dtData.Rows[0]["tzimg"].ToString() : "";
                //价格
                string strPrice = (dtData.Rows.Count > 0) ? dtData.Rows[0]["per_price"].ToString() : "";
                //团总人数
                string strTNum = (dtData.Rows.Count > 0) ? dtData.Rows[0]["total_num"].ToString() : "";
                //参与人数
                string strCyNum = (dtData.Rows.Count > 0) ? dtData.Rows[0]["involvement_num"].ToString() : "";
                //剩余人数
                string strSyNum = (dtData.Rows.Count > 0) ? dtData.Rows[0]["remain_num"].ToString() : "";
                if (dtData.Rows.Count > 0) iTuanState = int.Parse(dtData.Rows[0]["tuanstate"].ToString());

                int isMy = int.Parse(go_ordersBusiness.GetScalar(string.Format("ordertype='tuan' And businessId={0}", tlistid),"count(*)").ToString());

                //输出JSON
                string result = string.Format("{{\"state\":{0},\r\"title\":\"{1}\",\r\"imgroot\":\"{2}\",\r\"timg\":\"{3}\",\r\"tzid\":\"{4}\",\r\"tzname\":\"{5}\",\r\"tzimg\":\"{6}\",\r\"price\":\"{7}\",\r\"tnum\":\"{8}\",\r\"cynum\":\"{9}\",\r\"synum\":\"{10}\",\r\"tuanstate\":\"{11}\",\r\"ismy\":{12},\r\"data\":"
                    , (dtData.Rows.Count > 0) ? 0 : 1, strTitle, Globals.G_UPLOAD_PATH, strTImg, strTzId, strTzName, strTzImg, strPrice, strTNum, strCyNum, strSyNum, iTuanState, isMy);
                result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
                result += "}";

                context.Response.Write(result);
            }
            catch (Exception ex)
            {
                Globals.DebugLogger(ex.Message);
            }
        }

        /// <summary>
        /// 我的团购信息
        /// tid:
        /// </summary>
        private void tuanmylist(HttpContext context)
        {
            try
            {
                //验证签名
                string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
                if (string.IsNullOrEmpty(uid))
                {
                    context.Response.Write("{\"state\":2}");    //未知的签名参数
                    return;
                }
                //验证团状态
                string strStatus = context.Request["status"].ToString();
                if (string.IsNullOrEmpty(strStatus))
                {
                    context.Response.Write("{\"state\":3}");    //参数传递错误
                    return;
                }
                int status = -1;
                switch (strStatus)
                { 
                    case "0" :
                        status = -1;//全部
                        break;
                    case "1":
                        status = 0;//进行中
                        break;
                    case "2":
                        status = 1;//成团
                        break;
                    case "3":
                        status = 2;//失败
                        break;
                    case "4":
                        status = 3;//已开奖
                        break;
                }

                //接收传参
                string p = context.Request["p"].ToString();
                if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";

                //根据页码获取数据集,总条数,下一页等信息
                int pagesize = 10;
                int currPage = int.Parse(p);
                //构建查询表
                string from = @"(select gt.tId,gt.productid,gt.title,gt.per_price,gt.total_num,gt.start_time,gt.end_time,gt.deadline,gt.is_delete,
                        gt.status,gtl.tuanlistId,gtl.addtime,gtl.uid as tzid,gtl.involvement_num,gtl.remain_num,
                        gtl.winners_id,gtl.winning_code,gtl.status as tuanstate,ot.orderId,ot.uid cyid,ot.quantity,ot.goucode,ot.money,ot.ip,ot.time as cytime,
                        tzgm.username as tzname,tzgm.headimg as tzimg,cygm.username as cyname,cygm.headimg as cyimg,gp.thumb as picimg,ot.iswon,mar.status as cstatus 
                        from dbo.go_tuan_listinfo as gtl
                        inner join dbo.go_tuan as gt on gtl.tuanId = gt.tId 
                        inner join dbo.go_products as gp on gt.productid = gp.productid 
                        inner join (select * from dbo.go_orders where ordertype = 'tuan') as ot on ot.businessId = gtl.tuanlistId 
                        Inner Join go_member_addmoney_record mar on ot.recordcode=mar.code
                        inner join dbo.go_member tzgm on tzgm.uid = gtl.uid 
                        inner join dbo.go_member cygm on cygm.uid = ot.uid) t";
                //构建条件
                string where = string.Format(" where is_delete = 0 and cyid = {0} ", uid);
                if (status >= 0)
                {
                    if (status!=1)
                        where += string.Format(" and tuanstate={0}", status);
                    else
                        where += string.Format(" and tuanstate in(1,3)");
                }
                //排序
                string orderby = " cytime desc ";
                //获取列表及页码
                int count = CustomsBusiness.GetDataCount(from, where);
                //得到数据集
                DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where);
                int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
                int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）

                //输出JSON
                string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"imgroot\":\"{4}\",\r\"data\":"
                    , (dtData.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage, Globals.G_UPLOAD_PATH);
                result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
                result += "}";

                context.Response.Write(result);
            }
            catch (Exception ex)
            {
                Globals.DebugLogger(ex.Message);
            }
        }


        private void tuanorderinfo(HttpContext context)
        {
            //接收传参
            string orderid = context.Request["orderid"];
            if (string.IsNullOrEmpty(orderid))
            {
                context.Response.Write("{\"state\":2}"); return;//参数错误
            }

            string from = @"(select gt.tId,gt.productid,gt.title,gt.per_price,gt.total_num,gt.start_time,gt.end_time,gt.deadline,gt.is_delete,
                    gt.status,gtl.tuanlistId,gtl.addtime,gtl.uid as tzid,gtl.involvement_num,gtl.remain_num,
                    gtl.winners_id,gtl.winning_code,gtl.status as tuanstate,ot.orderId,ot.uid cyid,ot.quantity,ot.goucode,ot.money,ot.ip,ot.time as cytime,ot.pay_type,ot.iswon,ot.status as orderstate,ot.express_company,ot.express_code,ot.sendtime,ot.card_use_type,
                    tzgm.username as tzname,tzgm.headimg as tzimg,cygm.username as cyname,cygm.headimg as cyimg,gp.thumb as picimg 
                    from dbo.go_tuan_listinfo as gtl
                    inner join dbo.go_tuan as gt on gtl.tuanId = gt.tId 
                    inner join dbo.go_products as gp on gt.productid = gp.productid 
                    inner join (select * from dbo.go_orders where ordertype = 'tuan') as ot on ot.businessId = gtl.tuanlistId 
                    inner join dbo.go_member tzgm on tzgm.uid = gtl.uid 
                    inner join dbo.go_member cygm on cygm.uid = ot.uid 
                    where gt.is_delete = 0) t";
            string where = string.Format("orderId = '{0}'", orderid);

            DataTable dtData = CustomsBusiness.GetListData(from, where);
            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"data\":"
                , (dtData.Rows.Count > 0) ? 0 : 1, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtData, Newtonsoft.Json.Formatting.Indented);
            result += "}";
            context.Response.Write(result);
        }
        //获取参团中奖人信息
        // /ilerrtuan?action=tuanwinnerinfo&tid=17&type=1&p=1
        private void tuanwinnerinfo(HttpContext context)
        {
            //接收传参
            string tid = context.Request["tid"];
            string type = context.Request["type"];
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";
            if (string.IsNullOrEmpty(tid) || string.IsNullOrEmpty(type))
            {
                context.Response.Write("{\"state\":2}"); return;//参数错误
            }
            if (type == "0")//小团 及时开奖团
            {
                string where = "Where ordertype='tuan' And iswon=1 And businessId=" + tid;
                string orderby = "time desc";
                //获取总条数、当前页数据、总页码
                int pagesize = 10;
                int currPage = int.Parse(p);
                string from = "(select a.orderId,a.uid,a.goucode,a.ordertype,a.iswon,a.businessId,a.time,b.username,b.mobile,b.headimg from go_orders as a left join go_member as b on a.uid=b.uid) T";
                int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
                DataTable dtRes = CustomsBusiness.GetPageData(from, orderby, "orderId,uid,goucode,username,mobile,headimg"
                    , currPage, pagesize, where, DbServers.DbServerName.LatestDB);
                foreach (DataRow dr in dtRes.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["mobile"].ToString()))
                    {
                        System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("(\\d{3})(\\d{4})(\\d{4})", System.Text.RegularExpressions.RegexOptions.None);
                        dr["mobile"] = re.Replace(dr["mobile"].ToString(), "$1****$3");
                    }
                }
                int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
                int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
                string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"data\":"
              , (dtRes.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage);
                result += JsonConvert.SerializeObject(dtRes, Newtonsoft.Json.Formatting.Indented);
                result += "}";

                context.Response.Write(result);
            }
            else {
               
                string where = "Where ordertype='tuan' And iswon=1 And businessId in (Select tuanlistId From go_tuan_listinfo Where tuanId=" + tid + ")";
                string orderby = "time desc";
                //获取总条数、当前页数据、总页码
                int pagesize = 10;
                int currPage = int.Parse(p);
                string from = "(select a.orderId,a.uid,a.goucode,a.ordertype,a.iswon,a.businessId,a.time,b.username,b.mobile,b.headimg from go_orders as a left join go_member as b on a.uid=b.uid) T";
                int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
                DataTable dtRes = CustomsBusiness.GetPageData(from, orderby, "orderId,uid,goucode,username,mobile,headimg"
                    , currPage, pagesize, where, DbServers.DbServerName.LatestDB);
                foreach (DataRow dr in dtRes.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["mobile"].ToString()))
                    {
                        System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("(\\d{3})(\\d{4})(\\d{4})", System.Text.RegularExpressions.RegexOptions.None);
                        dr["mobile"] = re.Replace(dr["mobile"].ToString(), "$1****$3");
                    }
                }
                int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
                int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）
                string result = string.Format("{{\"state\":{0},\r\"count\":{1},\r\"pageCount\":{2},\r\"nextPage\":{3},\r\"data\":"
              , (dtRes.Rows.Count > 0) ? 0 : 1, count, pageCount, nextPage);
                result += JsonConvert.SerializeObject(dtRes, Newtonsoft.Json.Formatting.Indented);
                result += "}";

                context.Response.Write(result);
            }
        }
        //获取猜你喜欢信息
        // /ilerrtuan?action=tuanlike&tid=49
        private void tuanlike(HttpContext context)
        {
            string tid = context.Request["tid"];
             string from = "(select a.tId,a.title,a.per_price,a.is_delete,b.thumb from go_tuan as a left join go_products as b on a.productid=b.productid where b.pos = 1) T";
            string where = "is_delete=0 and tId<>"+tid;
            DataTable dtRes = CustomsBusiness.GetListData(from,where, "tId,title,per_price,thumb",null,6);
            string yiyuanfrom = "(select a.yId,a.title,a.yunjiage,a.recover,b.thumb from go_yiyuan as a left join go_products as b on a.productid=b.productid where b.renqi = 1 and a.q_uid IS NULL) T";
            string yiyuanwhere = "recover=0";
            DataTable dtYyRes = CustomsBusiness.GetListData(yiyuanfrom, yiyuanwhere, "yId,title,yunjiage,thumb", null, 6);
           
            string result = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"data\":"
                , (dtRes.Rows.Count > 0) ? 0 : 1, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(dtRes, Newtonsoft.Json.Formatting.Indented);
            result += ",\r\"yydata\":";
            result += JsonConvert.SerializeObject(dtYyRes, Newtonsoft.Json.Formatting.Indented);
            result += "}";

            context.Response.Write(result);
        }
        //收藏操作
        // /ilerrtuan?action=tuankeep&sign=&uid=1954&tid=49&type=1
        private void tuankeep(HttpContext context)
        {
            //验证签名
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":5}");    //未知的签名参数
                return;
            }
            string tid = context.Request["tid"];
            string type = context.Request["type"];
            string where = "Where tId=" + tid + " And uid=" + uid;
            string from = "go_tuankeep";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            int state = 4;
            if (type == "1")//取消收藏
            {
                if (count > 0)
                {
                    go_tuankeepBusiness.DelListData(" tId=" + tid + " And uid=" + uid);
                    state = 0;
                }
                else {
                    state = 1;
                }
            }
            else if (type == "0")
            { //添加收藏
                if (count != 0)
                {
                    state = 3;
                }
                else
                {
                    go_tuankeepEntity tkEntity = new go_tuankeepEntity();
                    tkEntity.TId = int.Parse(tid);
                    tkEntity.Uid = int.Parse(uid);
                    tkEntity.Addtime = DateTime.Now;
                    go_tuankeepBusiness.SaveEntity(tkEntity, true);
                    state = 2;
                }
            }
            else {
                if (count > 0)
                {
                    state = 0;
                }
                else
                {
                    state = 1;
                }
            }
            string result = string.Format("{{\"state\":{0}", state);
            result += "}";
            context.Response.Write(result);
        }
        private void tuankeeplist(HttpContext context)
        {
            //接收传参
            string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";

            //根据页码获取数据集,总条数,下一页等信息
            int pagesize = 10;
            int currPage = int.Parse(p);
            string from = @"(select gt.tId,gt.productid,gt.title,gt.per_price,gt.total_num,gt.start_time,gt.end_time,gt.sort,gt.is_delete,gp.thumb,gp.money
                                    from go_tuan gt left join go_products gp on gt.productid=gp.productid ) t";
            string where = " where is_delete = 0 and tId in (select tId from go_tuankeep where uid=" + uid + ") and GETDATE() between start_time and ISNULL(end_time,'9999-12-30') ";
            string orderby = " [sort] desc";

            //获取用户充值列表及页码
            int count = CustomsBusiness.GetDataCount(from, where);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where);
            int pageCount = count / pagesize + (count % pagesize == 0 ? 0 : 1);
            int nextPage = (currPage < pageCount) ? (currPage + 1) : 0; //下一页为0时，表示无数据可加载（数据加载完毕）

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