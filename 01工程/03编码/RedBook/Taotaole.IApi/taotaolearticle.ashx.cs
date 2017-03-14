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
    /// taotaolearticle 的摘要说明
    /// </summary>
    public class taotaolearticle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           context.Response.ContentType = "application/json";
            string action = context.Request["action"];
            switch (action)
            {
                case "article": //文章列表
                    articleListInfo(context);
                    break;
                case "articleDetail":          //文章详情
                    articleDetail(context);
                    break;
                case "slides": //幻灯片
                    slidesList(context);
                    break;
                case "slidesDetail": //幻灯片详情
                    slidesDetail(context);
                    break;
                case "ispass": //判断用户参与网吧活动的资格
                    ispass(context);
                    break;
                //case "exchange": //兑换接口
                //    exchange(context);
                //    break;
            }
        }

        //private void exchange(HttpContext context)
        //{

        //    //验证签名
        //    string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
        //    if (string.IsNullOrEmpty(uid))
        //    {
        //        context.Response.Write("{\"state\":2}");    //未知的签名参数
        //        return;
        //    }
        //     object o = go_channel_activitydetailBusiness.GetScalar("uid=" + uid, "exchange");

        //     if (o == null)
        //     {
        //         context.Response.Write("{\"state\":4}");    //该用户未参与
        //         return;
        //     }
        //     else
        //     {
        //         if (o.ToString() == "1")
        //         {
        //             context.Response.Write("{\"state\":3}");    //已经兑换了
        //             return;
        //         }
        //     }
                           
        //    go_channel_activitydetailEntity entity=new go_channel_activitydetailEntity();
        //    entity.Uid = int.Parse(uid);
        //    entity.Exchange=0;
        //    entity.Createtime = DateTime.Now;
        //    bool flag = go_channel_activitydetailBusiness.Modify(entity);

        //    context.Response.Write("{\"state\":0}");    //修改成功
        //}



        private void ispass(HttpContext context)
        {

            //验证签名
           string uid = Globals.ValidateSign(context.Request["sign"], context.Request["uid"]);
          //  string uid = context.Request["uid"];
            if (string.IsNullOrEmpty(uid))
            {
                context.Response.Write("{\"state\":2}");    //未知的签名参数
                return;
            }

            DataTable  dtcount = go_channel_activitydetailBusiness.GetListData("uid=" + uid, "exchange","",1);
            //if (dtcount != null && dtcount.Rows.Count > 0 && dtcount.Rows[0]["exchange"].ToString() == "0")
            //{
            //    context.Response.Write("{\"state\":4}");    //已经兑换了
            //    return;
            //}
            if (dtcount != null && dtcount.Rows.Count>0)
            {
                context.Response.Write("{\"state\":3}");    //已经参与过了
                 return;
            }
            //查询该用户的渠道商类型是否是网吧类型
            DataTable dt = CustomsBusiness.GetListData("  go_member a left join   dbo.go_channel_user b on a.servicechannelid=b.uid left join dbo.go_channel_type c on b.typeid=c.tid ", " a.uid=" + uid, " c.tid ,b.frozenstate");
            if (dt == null || dt.Rows.Count==0 || dt.Rows[0]["tid"].ToString() != ShopOrders.Wangba_type_ID.ToString())
            {
                context.Response.Write("{\"state\":5}");    //渠道商不是网吧 没资格
                return;
            }

            //有资格，但活动已过期(6)
            if (dt.Rows[0]["frozenstate"].ToString() == "2")
            {
                context.Response.Write("{\"state\":6}");    //网吧活动被终止
                return;
            }

            go_memberEntity memberEntity = go_memberBusiness.LoadEntity(int.Parse(uid));
            if (memberEntity.Money >= 1)
            {
                context.Response.Write("{\"state\":1}");    //有资格并且余额足够
                return;
            }
            context.Response.Write("{\"state\":0}");    //有资格
        }



        /// <summary>
        /// 幻灯片详情
        /// </summary>
        /// <param name="context"></param>
        private void slidesDetail(HttpContext context)
        {

            //获取传参
            string slideid = context.Request["slideid"];

            if (string.IsNullOrEmpty(slideid) || !PageValidate.IsNumberPlus(slideid))
            {
                context.Response.Write("{\"state\":1}");    //缺少参数
                return;
            }
            long k = 0;
            DataTable dtslideid = go_slidesBusiness.getapp( int.Parse(slideid));
            if (dtslideid == null || dtslideid.Rows.Count==0)
            {
                context.Response.Write("{\"state\":2}");    //幻灯片不存在
                return;
            }
          
            //输出JSON
            string result = string.Format("{{ \"imgroot\":\"{0}\",", Globals.G_UPLOAD_PATH);
            string str = JsonConvert.SerializeObject(dtslideid, Newtonsoft.Json.Formatting.Indented).TrimStart('[').TrimEnd(']').TrimStart('{').TrimEnd('}');
            result += str.Replace('{', ' ').Replace('}', ' ');
            result += "}";
            context.Response.Write(result);
        }



        /// <summary>
        /// 幻灯片
        /// </summary>
        /// <param name="context"></param>
        private void slidesList(HttpContext context)
        {
            string slidetype = context.Request["slidetype"];
            if (string.IsNullOrEmpty(slidetype))
            {
                context.Response.Write("{\"state\":2}");   //缺少参数 
                return;
            }

            if (slidetype != "1" && slidetype != "2" && slidetype != "3")  //错误的参数 
            {
                context.Response.Write("{\"state\":3}");
                return;
            }

            //slidetype =1 是微信 =2是app=3是商城

            string select = "*";
            DataTable dtslides = new DataTable();
            if (slidetype != "2")  //查询app时不需要[slidelink] 有可能数据量过大 
            {
                //slidestate=0只查询已启用状态的幻灯片
                dtslides = go_slidesBusiness.GetListData(" slidestate=0   and slidetype=" + slidetype + "", select, "slideorder asc");

            }
            else
            {
                //获取活动时间段
                double currTimespanStart = 0;
                double currTimespanEnd = 0;
               
                dtslides = go_slidesBusiness.getapplist(int.Parse(slidetype));
                DataTable dtTimespanAll = new DataTable();
                dtTimespanAll.Columns.Add("ID", typeof(int));               //活动ID
                dtTimespanAll.Columns.Add("title", typeof(string));         //活动标题
                dtTimespanAll.Columns.Add("starttime", typeof(string));    //活动开始时间
                dtTimespanAll.Columns.Add("endtime", typeof(string));     //活动结束时间
                dtTimespanAll.Columns.Add("typeid", typeof(int));        //1即将开始、2进行中 0不显示
                dtTimespanAll.Columns.Add("timespan", typeof(long));     //倒计时(秒)
                foreach (DataRow dr in dtslides.Rows)
                {
                    //验证是否有合法的时间段
                    if (dr["timespans"] == DBNull.Value || string.IsNullOrEmpty(dr["timespans"].ToString().Trim())) continue;
                    DataTable dtTimespan = DataHelper.JsonToDataTable(dr["timespans"].ToString());
                    if (dtTimespan.Columns.Contains("starttime") == false || dtTimespan.Columns.Contains("endtime") == false) continue;

                    foreach (DataRow row in dtTimespan.Rows)
                    {
                        currTimespanStart = ShopSms.Get_second_Difference(Convert.ToDateTime(row["starttime"].ToString()), false);
                        currTimespanEnd = ShopSms.Get_second_Difference(Convert.ToDateTime(row["endtime"].ToString()), false);
                        int typeid = 0;
                        double currTimespan = 0;
                        if (currTimespanStart > 0 && currTimespanStart < 86400)//86400秒一天是
                        {
                            typeid = 1;//即将开始
                            currTimespan = currTimespanStart;
                        }
                        if (currTimespanStart < 0 && currTimespanEnd > 0)
                        {
                            typeid = 2;//进行中
                            currTimespan = currTimespanEnd;
                        }
                        DataRow drNew = dtTimespanAll.NewRow();

                        drNew["typeid"] = typeid;

                      
                            drNew["ID"] = dr["slideid"];
                            drNew["title"] = dr["title"];
                            drNew["starttime"] = row["starttime"];
                            drNew["endtime"] = row["endtime"];
                            drNew["timespan"] = currTimespan;
                       
                      
                        dtTimespanAll.Rows.Add(drNew);



                    }




                    //获取待显示的最近时间段
                    DataRow[] drsTimespanAll = dtTimespanAll.Select("typeid<>0", "timespan");
                    if (drsTimespanAll.Length > 0)
                    {
                        string res = "";
                        //if (drsTimespanAll[0]["typeid"].ToString() == "0")
                        //{
                        //     res = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"type\":{2},\r\"data\":"
                        //     , 0, Globals.G_UPLOAD_PATH, drsTimespanAll[0]["typeid"].ToString());
                        //    res += JsonConvert.SerializeObject(dtslides, Newtonsoft.Json.Formatting.Indented);
                        //    res += "}";
                        //    context.Response.Write(res);
                        //    return;
                        //}
                        dtslides.Columns.Remove("timespans");
                       
                         res = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"id\":{2},\r\"title\":\"{3}\",\r\"endtime\":{4},\r\"type\":{5},\r\"data\":"
                            , 0, Globals.G_UPLOAD_PATH, drsTimespanAll[0]["ID"].ToString(), drsTimespanAll[0]["title"].ToString()
                            , drsTimespanAll[0]["timespan"].ToString(), drsTimespanAll[0]["typeid"].ToString());
                        res += JsonConvert.SerializeObject(dtslides, Newtonsoft.Json.Formatting.Indented);
                        res += "}";
                        context.Response.Write(res);
                        return;
                    }
                }
                dtslides.Columns.Remove("timespans");
                string result = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"type\":{2},\r\"data\":"
                    , 0, Globals.G_UPLOAD_PATH, "0");
                result += JsonConvert.SerializeObject(dtslides, Newtonsoft.Json.Formatting.Indented);
                result += "}";
                context.Response.Write(result);
                return;

            }

            string result2 = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\",\r\"data\":", 0, Globals.G_UPLOAD_PATH);
            result2 += JsonConvert.SerializeObject(dtslides, Newtonsoft.Json.Formatting.Indented);
            result2 += "}";
            context.Response.Write(result2);


        }

         /// <summary>
        /// 文章详情
        /// </summary>
        /// <param name="context"></param>
        private void articleDetail(HttpContext context)
        {

            //获取传参
            string id = context.Request["id"];

            if (string.IsNullOrEmpty(id) || !PageValidate.IsNumberPlus(id))
            {
                context.Response.Write("{\"state\":1}");    //缺少参数
                return;
            }
        
            go_articleEntity articleEntity = go_articleBusiness.LoadEntity(int.Parse(id));
            if (articleEntity == null)
            {
                context.Response.Write("{\"state\":2}");    //文章不存在
                return;
            }

            //输出JSON
            string result = string.Format("{{\"state\":{0},\r\"imgroot\":\"{1}\","
                , 0, Globals.G_UPLOAD_PATH);
            result += JsonConvert.SerializeObject(articleEntity, Newtonsoft.Json.Formatting.Indented).TrimStart('{').TrimEnd('}') ;
            result += "}";
            context.Response.Write(result);
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="context"></param>
        private void articleListInfo(HttpContext context)
        {

            //获取传参
            string cateid = context.Request["cateid"];
            if (string.IsNullOrEmpty(cateid) || !PageValidate.IsNumberPlus(cateid)) cateid = "";
            
            string p = context.Request["p"];
            if (string.IsNullOrEmpty(p) || !PageValidate.IsNumberPlus(p)) p = "1";

            //设置条件、排序

            int pagesize = 10;
            int currPage = int.Parse(p);

            string where = "Where 1=1 ";
            string orderby = "ordersn asc";    //默认根据主键排序(分页最快)
            
                 
            string cateName = "";
            //类型筛选
            if (!string.IsNullOrEmpty(cateid))
            {
                cateName = string.Format(" and a.cateid='{0}'", cateid);
            }

            string from = "(select a.ordersn,a.thumb, a.id, a.title ,a.hit,a.posttime  from go_article a  left join go_category b on a.cateid=b.cateid  where 1=1 " + cateName + ") T ";

          

            //获取用户充值列表及页码
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.ReadHistoryDB);
            DataTable dtData = CustomsBusiness.GetPageData(from, orderby, "*", currPage, pagesize, where, DbServers.DbServerName.ReadHistoryDB);
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