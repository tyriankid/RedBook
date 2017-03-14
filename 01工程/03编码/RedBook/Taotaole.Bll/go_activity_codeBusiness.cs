using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Taotaole.Business;
using Taotaole.Common;
using Taotaole.Dal;
using Taotaole.Model;
using YH.Utility;

namespace Taotaole.Bll
{
    /// <summary>
    /// -业务操作类
    /// </summary>
    public class go_activity_codeBusiness
    {
        /// <summary>
        /// 根据主键加载数据集
        /// </summary>
        public static DataTable LoadData(int ID)
        {
            if (Globals.GetMasterSettings().OpenCacheServer)
            {
                return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
            }
            else
            {
                return go_activity_codeManager.LoadData(ID);
            }
        }

        /// <summary>
        /// 根据主键加载实体
        /// </summary>
        public static go_activity_codeEntity LoadEntity(int ID)
        {
            return go_activity_codeManager.LoadEntity(ID);
        }

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            return go_activity_codeManager.SelectListData(where, selectFields, orderby, top);
        }

        /// <summary>
        /// 根据条件查询首行首列
        /// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null)
        {
            return go_activity_codeManager.SelectScalar(where, selectFields, orderby);
        }

        /// <summary>
        /// 根据条件查询数据实体
        /// </summary>
        public static IList<go_activity_codeEntity> GetListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            return go_activity_codeManager.SelectListEntity(where, selectFields, orderby, top);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        public static void Del(int ID)
        {
            go_activity_codeManager.Del(ID);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        public static void DelListData(string where = null)
        {
            go_activity_codeManager.DelListData(where);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public static bool SaveEntity(go_activity_codeEntity entity, bool isAdd)
        {
            return go_activity_codeManager.SaveEntity(entity, isAdd);
        }
        /// <summary>
        /// 获取用户可使用红包列表
        /// </summary>
        public static DataTable getUserRedpack(string uid, string money)
        {
            DateTime now = DateTime.Now;
            string where = "[state]=1 AND uid=" + uid + " AND (amount=0 or amount<=" + money + ") AND (overtime is null or overtime>'" + now + "') AND (senttime is null or senttime <'" + now + "') AND discount<=" + money;
            DataTable redlist = go_activity_codeBusiness.GetListData(where, "id,codetitle,amount,discount");
            return redlist;
        }
        //更新用户红包状态方法
        public static void ValRedpackState(string uid)
        {
            string from = "go_activity_code";
            string where = "uid=" + uid + " AND state<2";
            DateTime now = DateTime.Now;
            DataTable codelist = CustomsBusiness.GetListData(from, where, "id,uid,state,senttime,overtime");
            DataRow[] DataRows = codelist.Select("state=0 and senttime<'" + now + "'");
            foreach (DataRow dr in DataRows)
            {
                dr["state"] = 1;
            }
            bool up = CustomsBusiness.CommitDataTable(codelist, "select id,uid,state,senttime,overtime from go_activity_code");

            DataTable overcodelist = CustomsBusiness.GetListData(from, where, "id,uid,state,senttime,overtime");
            DataRow[] overDataRows = codelist.Select("state=1 and overtime<'" + now + "'");
            foreach (DataRow dr in overDataRows)
            {
                dr["state"] = 3;
            }
            bool overup = CustomsBusiness.CommitDataTable(codelist, "select id,uid,state,senttime,overtime from go_activity_code");
        }
        public static int getRedpackDoNum(string uid)
        {
            string where = "Where 1=1 AND state <= 1 And uid=" + uid;
            string from = "go_activity_code";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            return count;
        }

        /// <summary>
        /// 根据直购商品id发放红包
        /// </summary>
        /// <param name="quanid"></param>
        /// <returns></returns>
        public static bool sendQuanidRedpack(string orderid)
        {
            go_ordersEntity orderEntity = go_ordersBusiness.LoadEntity(orderid);

            go_zhigouEntity zhigouEntity = go_zhigouBusiness.LoadEntity(orderEntity.BusinessId);
            go_productsEntity zhigouProductEntity = go_productsBusiness.LoadEntity(zhigouEntity.Productid);

            bool valUseRange = false;

            #region 优惠商品发放红包
            int youhuiSendRedpackId = 0;
            if (zhigouProductEntity.Youhui==1)
            {
                youhuiSendRedpackId = 32;
            }
            Globals.DebugLogger("11");
            go_activityEntity youhuiSendRedpackEntity = go_activityBusiness.LoadEntity(youhuiSendRedpackId);
            //判断是否在活动时间内start
            if (youhuiSendRedpackEntity.Starttime < DateTime.Now && youhuiSendRedpackEntity.Endtime > DateTime.Now)//判断是否在活动时间内end
            {
                Globals.DebugLogger("22");
                DataTable redlist = JsonConvert.DeserializeObject<DataTable>(youhuiSendRedpackEntity.Code_config_ids);
                foreach (DataRow rl in redlist.Rows)
                {
                    Globals.DebugLogger("33");
                    if (rl[0].ToString() != "0")
                    {
                        Globals.DebugLogger("44");
                        go_code_configEntity code_config = go_code_configBusiness.LoadEntity(int.Parse(rl[0].ToString()));
                        go_activity_codeEntity codeentity = new go_activity_codeEntity();
                        codeentity.Activity_id = youhuiSendRedpackId;
                        codeentity.Code_config_id = int.Parse(rl[0].ToString());
                        codeentity.Codetitle = rl[1].ToString() + youhuiSendRedpackEntity.Usercodeinfo;
                        codeentity.Uid = orderEntity.Uid.ToString();
                        codeentity.Amount = code_config.Amount;
                        codeentity.Discount = code_config.Discount;
                        codeentity.State = 1;
                        codeentity.From = 0;
                        codeentity.Addtime = DateTime.Now;
                        codeentity.Senttime = DateTime.Now;
                        codeentity.Overtime = DateTime.Now.AddDays(youhuiSendRedpackEntity.Redpackday);
                        go_activity_codeBusiness.SaveEntity(codeentity, true);
                    }

                }
            }
            #endregion
            return true;
        }

        /// <summary>
        /// 根据业务ID发放红包 适合留存活动 bid 业务ID
        /// </summary>
        public static bool sentBidRedpack(string bid)
        {
            go_yiyuanEntity yiyuanEntity = go_yiyuanBusiness.LoadEntity(int.Parse(bid));
            //0元购活动发包start
           // Globals.DebugLogger("0元购活动发包start");
            int zerobuyaid = 19;
            go_activityEntity zerobuyEntity = go_activityBusiness.LoadEntity(zerobuyaid);
            //判断是否在活动时间内start
            JArray zerobuytimespans = JsonConvert.DeserializeObject<JArray>(zerobuyEntity.Timespans.ToString());
           
            bool valUseRange = false;
            string datewhere = "and (";
            if (zerobuytimespans != null)
            {
                for (int i = 0; i < zerobuytimespans.Count; i++)
                {
                    datewhere += " (time>'" + zerobuytimespans[i]["starttime"].ToString() + "' and time<'" + zerobuytimespans[i]["endtime"].ToString() + "')";
                    if ((i + 1) < zerobuytimespans.Count)
                    {
                        datewhere += " or";
                    }

                }
                datewhere += ")";
            }
            else {
                datewhere = "";
            }
                //Globals.DebugLogger("0元购活动发包在时间范围内");
                JArray zerobuyrange = JsonConvert.DeserializeObject<JArray>(zerobuyEntity.Use_range.ToString());
                if (zerobuyrange != null)
                {
                    for (int i = 0; i < zerobuyrange.Count; i++)
                    {

                        if (yiyuanEntity.originalid == int.Parse(zerobuyrange[i]["id"].ToString()))//判断商品是否在活动范围内
                        {
                            // Globals.DebugLogger("0元购活动发包在商品范围内");
                            DataTable dtOrderlist = go_ordersBusiness.GetListData("businessId=" + bid + " and ordertype='yuan' and uid=(select uid from go_orders where businessId=" + bid + " and iswon=1) " + datewhere + " group by uid", "uid,SUM(money) as money");
                            string winuid = dtOrderlist.Rows[0]["uid"].ToString();
                            //判断用户活动资格start
                            if (!go_activity_userBusiness.valQualification(winuid, zerobuyaid.ToString()))
                            {
                                //Globals.DebugLogger("0元购活动发包 用户没有活动资格" + winuid);
                                //没有活动资格
                                break;
                            }

                            if (!go_activity_userBusiness.valQualificationCount(winuid, zerobuyaid.ToString()))
                            {
                                // Globals.DebugLogger("0元购活动发包 用户已达到活动上限" + winuid);
                                //已达到参与次数上限
                                break;
                            }
                            //判断用户活动资格end
                            int RedpackMoney = (int)float.Parse(dtOrderlist.Rows[0]["money"].ToString());
                            RedpackMoney = RedpackMoney > int.Parse(zerobuyrange[i]["amountmax"].ToString()) ? int.Parse(zerobuyrange[i]["amountmax"].ToString()) : RedpackMoney;
                            int[] redlist = getredpacklist(RedpackMoney / 2 + RedpackMoney % 2);
                            int[] oredlist = getredpacklist(RedpackMoney / 2);
                            DataTable dtuserredpack = go_activity_codeBusiness.GetListData("1=2", "activity_id,code_config_id,codetitle,channel_id,uid,mobile,order_id,starttime,endtime,raisetime,timespans,use_range,usetime,descript,d_type,amount,discount,state,[from],addtime,senttime,overtime");
                            DataTable dtredpack = go_code_configBusiness.GetListData("ID in (84,83,82,81,80,78,79)", "id,title,amount,discount");
                            foreach (int rid in redlist)
                            {
                                DataRow dr = dtuserredpack.NewRow();
                                dr["activity_id"] = zerobuyaid;
                                dr["code_config_id"] = rid;
                                //dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"];
                                dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"]  +  zerobuyEntity.Usercodeinfo;
                                dr["channel_id"] = 0;
                                dr["uid"] = winuid;
                                dr["mobile"] = "";
                                dr["order_id"] = "";
                                dr["starttime"] = DBNull.Value;
                                dr["endtime"] = DBNull.Value;
                                dr["raisetime"] = 0;
                                dr["timespans"] = "";
                                dr["use_range"] = "";
                                dr["usetime"] = DBNull.Value;
                                dr["descript"] = "";
                                dr["d_type"] = 2;
                                dr["amount"] = dtredpack.Select("id=" + rid)[0]["amount"];
                                dr["discount"] = dtredpack.Select("id=" + rid)[0]["discount"];
                                dr["state"] = 0;
                                dr["from"] = 1;
                                dr["addtime"] = DateTime.Now;
                                dr["senttime"] = DateTime.Now;
                                dr["overtime"] = DateTime.Now.AddDays(zerobuyEntity.Redpackday);
                                dtuserredpack.Rows.Add(dr);
                            }
                            foreach (int rid in oredlist)
                            {
                                DataRow dr = dtuserredpack.NewRow();
                                dr["activity_id"] = zerobuyaid;
                                dr["code_config_id"] = rid;
                                //dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"];
                                dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"] + zerobuyEntity.Usercodeinfo;
                                dr["channel_id"] = 0;
                                dr["uid"] = winuid;
                                dr["mobile"] = "";
                                dr["order_id"] = "";
                                dr["starttime"] = DBNull.Value;
                                dr["endtime"] = DBNull.Value;
                                dr["raisetime"] = 0;
                                dr["timespans"] = "";
                                dr["use_range"] = "";
                                dr["usetime"] = DBNull.Value;
                                dr["descript"] = "";
                                dr["d_type"] = 2;
                                dr["amount"] = dtredpack.Select("id=" + rid)[0]["amount"];
                                dr["discount"] = dtredpack.Select("id=" + rid)[0]["discount"];
                                dr["state"] = 0;
                                dr["from"] = 1;
                                dr["addtime"] = DateTime.Now;
                                dr["senttime"] = DateTime.Now.AddDays(zerobuyEntity.Nextweeknum - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")) + 7).Date;
                                dr["overtime"] = DateTime.Now.AddDays(zerobuyEntity.Nextweeknum - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")) + zerobuyEntity.Redpackday + 7).Date;
                                dtuserredpack.Rows.Add(dr);
                            }
                            CustomsBusiness.CommitDataTable(dtuserredpack, "select * from go_activity_code");
                            IList<go_activity_userEntity> dtUser = go_activity_userBusiness.GetListEntity("uid=" + winuid + " and activity_id=" + zerobuyaid, "*");
                            go_activity_userEntity drUser = dtUser[0];
                            drUser.Alreadycount++;
                            go_activity_userBusiness.SaveEntity(drUser, false);
                        }
                    }
                
            }
            //0元购活动发包end
            //五折狂欢活动发包start
            int halfoffaid = 18;
            go_activityEntity halfoffEntity = go_activityBusiness.LoadEntity(halfoffaid);
            JArray halfofftimespans = JsonConvert.DeserializeObject<JArray>(halfoffEntity.Timespans.ToString());
            //判断是否在活动时间内start
            bool boolHalfoff = false;
            if (halfofftimespans != null)
            {
                for (int i = 0; i < halfofftimespans.Count; i++)
                {

                    if (DateTime.Now > DateTime.Parse(halfofftimespans[i]["starttime"].ToString()) && DateTime.Now < DateTime.Parse(halfofftimespans[i]["endtime"].ToString()))
                    {
                        boolHalfoff = true;
                        break;
                    }
                }
            }
            else {
                boolHalfoff = true;
            }
            if (boolHalfoff)//判断是否在活动时间内end
            {
                JArray halfoffrange = JsonConvert.DeserializeObject<JArray>(halfoffEntity.Use_range.ToString());
                if (halfoffrange != null)
                {
                    for (int i = 0; i < halfoffrange.Count; i++)
                    {
                        if (yiyuanEntity.originalid == int.Parse(halfoffrange[i]["id"].ToString()))
                        {
                            DataTable dtOrderlist = go_ordersBusiness.GetListData("businessId=" + bid + " and ordertype='yuan' and uid=(select uid from go_orders where businessId=" + bid + " and iswon=1) group by uid", "uid,SUM(money) as money");
                            string winuid = dtOrderlist.Rows[0]["uid"].ToString();
                            //判断用户活动资格start

                            if (!go_activity_userBusiness.valQualification(winuid, halfoffaid.ToString()))
                            {
                                //Globals.DebugLogger("五折狂欢活动没资格 " + winuid);
                                //没有活动资格
                                break;
                            }

                            if (!go_activity_userBusiness.valQualificationCount(winuid, halfoffaid.ToString()))
                            {
                                // Globals.DebugLogger("五折狂欢活动上限 " + winuid);
                                //已达到参与次数上限
                                break;
                            }
                            //判断用户活动资格end

                            int RedpackMoney = (int)float.Parse(dtOrderlist.Rows[0]["money"].ToString());
                            RedpackMoney = RedpackMoney / 2;
                            RedpackMoney = RedpackMoney > int.Parse(halfoffrange[i]["amountmax"].ToString()) ? int.Parse(halfoffrange[i]["amountmax"].ToString()) : RedpackMoney;
                            
                            int[] redlist = getredpacklist(RedpackMoney / 2 + RedpackMoney % 2);
                            int[] oredlist = getredpacklist(RedpackMoney / 2);
                            DataTable dtuserredpack = go_activity_codeBusiness.GetListData("1=2", "activity_id,code_config_id,codetitle,channel_id,uid,mobile,order_id,starttime,endtime,raisetime,timespans,use_range,usetime,descript,d_type,amount,discount,state,[from],addtime,senttime,overtime");
                            DataTable dtredpack = go_code_configBusiness.GetListData("ID in (84,83,82,81,80,78,79)", "id,title,amount,discount");
                            foreach (int rid in redlist)
                            {
                                DataRow dr = dtuserredpack.NewRow();
                                dr["activity_id"] = halfoffaid;
                                dr["code_config_id"] = rid;
                                //dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"];
                                dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"] + halfoffEntity.Usercodeinfo ;
                                dr["channel_id"] = 0;
                                dr["uid"] = winuid;
                                dr["mobile"] = "";
                                dr["order_id"] = "";
                                dr["starttime"] = DBNull.Value;
                                dr["endtime"] = DBNull.Value;
                                dr["raisetime"] = 0;
                                dr["timespans"] = "";
                                dr["use_range"] = "";
                                dr["usetime"] = DBNull.Value;
                                dr["descript"] = "";
                                dr["d_type"] = 2;
                                dr["amount"] = dtredpack.Select("id=" + rid)[0]["amount"];
                                dr["discount"] = dtredpack.Select("id=" + rid)[0]["discount"];
                                dr["state"] = 0;
                                dr["from"] = 1;
                                dr["addtime"] = DateTime.Now;
                                dr["senttime"] = DateTime.Now;
                                dr["overtime"] = DateTime.Now.AddDays(halfoffEntity.Redpackday);
                                dtuserredpack.Rows.Add(dr);
                            }
                            foreach (int rid in oredlist)
                            {
                                DataRow dr = dtuserredpack.NewRow();
                                dr["activity_id"] = halfoffaid;
                                dr["code_config_id"] = rid;
                                //dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"];
                                dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"] + halfoffEntity.Usercodeinfo;
                                dr["channel_id"] = 0;
                                dr["uid"] = winuid;
                                dr["mobile"] = "";
                                dr["order_id"] = "";
                                dr["starttime"] = DBNull.Value;
                                dr["endtime"] = DBNull.Value;
                                dr["raisetime"] = 0;
                                dr["timespans"] = "";
                                dr["use_range"] = "";
                                dr["usetime"] = DBNull.Value;
                                dr["descript"] = "";
                                dr["d_type"] = 2;
                                dr["amount"] = dtredpack.Select("id=" + rid)[0]["amount"];
                                dr["discount"] = dtredpack.Select("id=" + rid)[0]["discount"];
                                dr["state"] = 0;
                                dr["from"] = 1;
                                dr["addtime"] = DateTime.Now;
                                dr["senttime"] = DateTime.Now.AddDays(halfoffEntity.Nextweeknum - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")) + 7).Date;
                                dr["overtime"] = DateTime.Now.AddDays(halfoffEntity.Nextweeknum - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")) + halfoffEntity.Redpackday + 7).Date;
                                dtuserredpack.Rows.Add(dr);
                            }
                            CustomsBusiness.CommitDataTable(dtuserredpack, "select * from go_activity_code");
                            IList<go_activity_userEntity> dtUser = go_activity_userBusiness.GetListEntity("uid=" + winuid + " and activity_id=" + halfoffaid, "*");
                            go_activity_userEntity drUser = dtUser[0];
                            drUser.Alreadycount++;
                            go_activity_userBusiness.SaveEntity(drUser, false);
                        }
                    }
                }
            }
            //五折狂欢活动发包end
            //买爆款不中包赔活动发包start
            //Globals.DebugLogger("爆款不中包赔活动开始");
            int buyhotaid = 20;
            go_activityEntity buyhotEntity = go_activityBusiness.LoadEntity(buyhotaid);
            //判断是否在活动时间内start
            JArray buyhottimespans = JsonConvert.DeserializeObject<JArray>(buyhotEntity.Timespans.ToString());
            bool boolBuyhot = false;
            if (buyhottimespans != null)
            {
                for (int i = 0; i < buyhottimespans.Count; i++)
                {

                    if (DateTime.Now > DateTime.Parse(buyhottimespans[i]["starttime"].ToString()) && DateTime.Now < DateTime.Parse(buyhottimespans[i]["endtime"].ToString()))
                    {
                        boolBuyhot = true;
                        break;
                    }
                }
            }
            else {
                boolBuyhot = true;
            }
            if (boolBuyhot)//判断是否在活动时间内end
            {
               // Globals.DebugLogger("爆款不中包赔活动在时间范围内");
                JArray buyhotrange = JsonConvert.DeserializeObject<JArray>(buyhotEntity.Use_range.ToString());
                if (buyhotrange != null)
                {
                    for (int i = 0; i < buyhotrange.Count; i++)
                    {

                        if (yiyuanEntity.originalid == int.Parse(buyhotrange[i]["id"].ToString()))
                        {
                            DataTable dtOrderlist = go_ordersBusiness.GetListData("businessId=" + bid + " and ordertype='yuan' and uid not in (select uid from go_orders where businessId=" + bid + " and iswon=1) group by uid", "uid,SUM(money) as money");
                            foreach (DataRow drUser in dtOrderlist.Rows)
                            {
                                string winuid = drUser["uid"].ToString();
                                //判断用户活动资格start
                                if (!go_activity_userBusiness.valQualification(winuid, buyhotaid.ToString()))
                                {
                                    //Globals.DebugLogger("爆款不中包赔活动没资格" + winuid);
                                    //没有活动资格
                                    continue;
                                }

                                if (!go_activity_userBusiness.valQualificationCount(winuid, buyhotaid.ToString()))
                                {
                                    //Globals.DebugLogger("爆款不中包赔活动上限" + winuid);
                                    //已达到参与次数上限
                                    continue;
                                }
                                //判断用户活动资格end
                                int RedpackMoney = (int)float.Parse(drUser["money"].ToString());
                                RedpackMoney = RedpackMoney > int.Parse(buyhotrange[i]["amountmax"].ToString()) ? int.Parse(buyhotrange[i]["amountmax"].ToString()) : RedpackMoney;
                                RedpackMoney = RedpackMoney / 2;
                                int[] redlist = getredpacklist(RedpackMoney / 2 + RedpackMoney % 2);
                                int[] oredlist = getredpacklist(RedpackMoney / 2);
                                DataTable dtuserredpack = go_activity_codeBusiness.GetListData("1=2", "activity_id,code_config_id,codetitle,channel_id,uid,mobile,order_id,starttime,endtime,raisetime,timespans,use_range,usetime,descript,d_type,amount,discount,state,[from],addtime,senttime,overtime");
                                DataTable dtredpack = go_code_configBusiness.GetListData("ID in (84,83,82,81,80,78,79)", "id,title,amount,discount");
                                foreach (int rid in redlist)
                                {
                                    DataRow dr = dtuserredpack.NewRow();
                                    dr["activity_id"] = buyhotaid;
                                    dr["code_config_id"] = rid;
                                    dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"] + buyhotEntity.Usercodeinfo;
                                    //dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"];
                                    dr["channel_id"] = 0;
                                    dr["uid"] = winuid;
                                    dr["mobile"] = "";
                                    dr["order_id"] = "";
                                    dr["starttime"] = DBNull.Value;
                                    dr["endtime"] = DBNull.Value;
                                    dr["raisetime"] = 0;
                                    dr["timespans"] = "";
                                    dr["use_range"] = "";
                                    dr["usetime"] = DBNull.Value;
                                    dr["descript"] = "";
                                    dr["d_type"] = 2;
                                    dr["amount"] = dtredpack.Select("id=" + rid)[0]["amount"];
                                    dr["discount"] = dtredpack.Select("id=" + rid)[0]["discount"];
                                    dr["state"] = 0;
                                    dr["from"] = 1;
                                    dr["addtime"] = DateTime.Now;
                                    dr["senttime"] = DateTime.Now;
                                    dr["overtime"] = DateTime.Now.AddDays(buyhotEntity.Redpackday);
                                    dtuserredpack.Rows.Add(dr);
                                }
                                foreach (int rid in oredlist)
                                {
                                    DataRow dr = dtuserredpack.NewRow();
                                    dr["activity_id"] = buyhotaid;
                                    dr["code_config_id"] = rid;
                                    dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"] + buyhotEntity.Usercodeinfo;
                                    //dr["codetitle"] = dtredpack.Select("id=" + rid)[0]["title"];
                                    dr["channel_id"] = 0;
                                    dr["uid"] = winuid;
                                    dr["mobile"] = "";
                                    dr["order_id"] = "";
                                    dr["starttime"] = DBNull.Value;
                                    dr["endtime"] = DBNull.Value;
                                    dr["raisetime"] = 0;
                                    dr["timespans"] = "";
                                    dr["use_range"] = "";
                                    dr["usetime"] = DBNull.Value;
                                    dr["descript"] = "";
                                    dr["d_type"] = 2;
                                    dr["amount"] = dtredpack.Select("id=" + rid)[0]["amount"];
                                    dr["discount"] = dtredpack.Select("id=" + rid)[0]["discount"];
                                    dr["state"] = 0;
                                    dr["from"] = 1;
                                    dr["addtime"] = DateTime.Now;
                                    dr["senttime"] = DateTime.Now.AddDays(buyhotEntity.Nextweeknum - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")) + 7).Date;
                                    dr["overtime"] = DateTime.Now.AddDays(buyhotEntity.Nextweeknum - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")) + buyhotEntity.Redpackday + 7).Date;
                                    dtuserredpack.Rows.Add(dr);
                                }
                                CustomsBusiness.CommitDataTable(dtuserredpack, "select * from go_activity_code");
                                IList<go_activity_userEntity> ilUser = go_activity_userBusiness.GetListEntity("uid=" + winuid + " and activity_id=" + buyhotaid, "*");
                                go_activity_userEntity eUser = ilUser[0];
                                eUser.Alreadycount++;
                                go_activity_userBusiness.SaveEntity(eUser, false);
                            }
                        }
                    }
                }
            }
            //以下买爆款为单次发放 新版为一起发放
            //买爆款不中包赔活动发包end
            /*
            //首次支付5元不中包赔start
            //Globals.DebugLogger("首次支付5元不中包赔活动开始");
            int firstfiveaid = 23;
            go_activityEntity firstfiveEntity = go_activityBusiness.LoadEntity(firstfiveaid);
            //判断是否在活动时间内start
            if (firstfiveEntity.Starttime < DateTime.Now && firstfiveEntity.Endtime > DateTime.Now)//判断是否在活动时间内end
            {
                //Globals.DebugLogger("首次支付5元不中包赔活动 在时间范围内");
                        DataTable dtOrderlist = go_ordersBusiness.GetListData("businessId=" + bid + " and ordertype='yuan' and uid not in (select uid from go_orders where businessId=" + bid + " and iswon=1) group by uid", "uid,SUM(money) as money");
                      
                        foreach (DataRow drUser in dtOrderlist.Rows)
                        {
                            string uid = drUser["uid"].ToString();
                            int RedpackMoney = (int)float.Parse(drUser["money"].ToString());
                            if (firstfiveEntity.Shopnum <= RedpackMoney)//验证是否满足活动购买商品数量
                            {
                                //发放红包
                                sentUserRedpack(uid, firstfiveaid.ToString());
                            }
                           
                        }
            } 
            //首次支付5元不中包赔end*/


            //首次支付5元不中包赔start
            //Globals.DebugLogger("首次支付5元不中包赔活动开始");
            int firstfiveaid = 23;
            go_activityEntity firstfiveEntity = go_activityBusiness.LoadEntity(firstfiveaid);
            //判断是否在活动时间内start
            if (firstfiveEntity.Starttime < DateTime.Now && firstfiveEntity.Endtime > DateTime.Now)//判断是否在活动时间内end
            {
                 valUseRange = false;
                JArray firstfiverange = JsonConvert.DeserializeObject<JArray>(firstfiveEntity.Use_range.ToString());
                if (firstfiverange == null)
                {
                    valUseRange = true;
                }
                else
                {
                    for (int ir = 0; ir < firstfiverange.Count; ir++)
                    {
                        if (yiyuanEntity.originalid == int.Parse(firstfiverange[ir]["id"].ToString()))
                        {
                            valUseRange = true;
                        }
                    }
                }

                    if(valUseRange)
                    {
                        //Globals.DebugLogger("首次支付5元不中包赔活动 在时间范围内");
                        DataTable dtOrderlist = go_ordersBusiness.GetListData("businessId=" + bid + " and ordertype='yuan' and uid not in (select uid from go_orders where businessId=" + bid + " and iswon=1) group by uid", "uid,SUM(quantity) as quantity");
                        DataTable dtuserredpack = go_activity_codeBusiness.GetListData("1=2", "activity_id,code_config_id,codetitle,channel_id,uid,mobile,order_id,starttime,endtime,raisetime,timespans,use_range,usetime,descript,d_type,amount,discount,state,[from],addtime,senttime,overtime");
                
                        foreach (DataRow drUser in dtOrderlist.Rows)
                        {
                            string uid = drUser["uid"].ToString();
                            //判断用户活动资格start
                            if (!go_activity_userBusiness.valQualification(uid, firstfiveaid.ToString()))
                            {
                                //Globals.DebugLogger("爆款不中包赔活动没资格" + winuid);
                                //没有活动资格
                                continue;
                            }

                            if (firstfiveEntity.Shopnum <= int.Parse(drUser["quantity"].ToString()))//验证是否满足活动购买商品数量
                            {
                                IList<go_activity_userEntity> ilUser = go_activity_userBusiness.GetListEntity("uid=" + uid + " and activity_id=" + firstfiveaid, "*");
                                go_activity_userEntity eUser = ilUser[0];
                                if (firstfiveEntity.Count != 0 && eUser.Alreadycount >= firstfiveEntity.Count)
                                {
                                    //已达到参与次数上限
                                    continue;
                                }

                                JArray code = JsonConvert.DeserializeObject<JArray>(firstfiveEntity.Code_config_ids.ToString());
                                if (code != null)
                                {
                                    for (int i = 0; i < code.Count; i++)
                                    {
                                        if (code[i]["id"].ToString() != "0")
                                        {
                                            go_code_configEntity codeconfigEntity = go_code_configBusiness.LoadEntity(Convert.ToInt32(code[i]["id"].ToString()));
                                            DataRow dr = dtuserredpack.NewRow();
                                            dr["activity_id"] = firstfiveaid;
                                            dr["code_config_id"] = int.Parse(code[i]["id"].ToString());
                                            //dr["codetitle"] = code[i]["title"].ToString();
                                            dr["codetitle"] = code[i]["title"].ToString() + firstfiveEntity.Usercodeinfo;
                                            dr["channel_id"] = 0;
                                            dr["uid"] = uid;
                                            dr["mobile"] = "";
                                            dr["order_id"] = "";
                                            dr["starttime"] = DBNull.Value;
                                            dr["endtime"] = DBNull.Value;
                                            dr["raisetime"] = 0;
                                            dr["timespans"] = "";
                                            dr["use_range"] = "";
                                            dr["usetime"] = DBNull.Value;
                                            dr["descript"] = "";
                                            dr["d_type"] = 2;
                                            dr["amount"] = codeconfigEntity.Amount;
                                            dr["discount"] = codeconfigEntity.Discount;
                                            dr["state"] = 0;
                                            dr["from"] = 1;
                                            dr["addtime"] = DateTime.Now;
                                            dr["senttime"] = DateTime.Now;
                                            dr["overtime"] = DateTime.Now.AddDays(firstfiveEntity.Redpackday);
                                            dtuserredpack.Rows.Add(dr);
                                        }
                                    }
                                    eUser.Alreadycount++;
                                    go_activity_userBusiness.SaveEntity(eUser, false);
                                }
                            }
                        }
                        CustomsBusiness.CommitDataTable(dtuserredpack, "select * from go_activity_code");
                    }
                
               
            }
            //首次支付5元不中包赔end
            return true;
        }
        /// <summary>
        /// 根据用户ID和活动ID发放红包 适合拉新及促活活动
        /// </summary>
        public static int sentUserRedpack(string uid,string aid)
        {
             List<string> pullActivity = new List<string> { "17", "25", "31" };//拉新及促活活动列表 17：冲20送红包 25 拼团抽奖 31 晒单送红包
            if (!pullActivity.Contains(aid))
            {
                //未知的活动ID
                return 1;
            }
            string where = "Where 1=1 AND activity_id=" + aid + " And uid=" + uid;
            string from = "go_activity_user";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            if (count == 0)
            {
                if (aid == "17")
                {
                    //没有活动资格，请先领取资格
                    return 3;
                }
                else
                {
                    go_activity_userBusiness.Qualification(uid, aid);
                }
            }
            
            go_activityEntity activityEntity = go_activityBusiness.LoadEntity(int.Parse(aid));

            IList<go_activity_userEntity> dtUser = go_activity_userBusiness.GetListEntity("uid=" + uid + " and activity_id=" + aid, "*");
            go_activity_userEntity drUser = dtUser[0];
            if (activityEntity.Count != 0 && drUser.Alreadycount >= activityEntity.Count)
            {
                //已达到参与次数上限
                return 4;
            }
            if (activityEntity.Starttime>DateTime.Now || activityEntity.Endtime<DateTime.Now)
            {
                //不在活动时间范围内

                return 5;
            }
                JArray code = JsonConvert.DeserializeObject<JArray>(activityEntity.Code_config_ids.ToString());
                DateTime dt = DateTime.Now.Date;
                int iss = Convert.ToInt32(dt.DayOfWeek.ToString("d"));
                DateTime startweek = dt.AddDays((iss == 0 || iss > 3 ? 8 : 1) - iss).Date;
                bool sentbool = false;
                for (int i = 0; i < code.Count; i++)
                {
                    if (code[i]["id"].ToString() != "0")
                    {

                        go_activity_codeEntity codeEntity = new go_activity_codeEntity();
                        go_code_configEntity codeconfigEntity = go_code_configBusiness.LoadEntity(Convert.ToInt32(code[i]["id"].ToString()));
                        codeEntity.Activity_id = int.Parse(aid);
                        codeEntity.Code_config_id = int.Parse(code[i]["id"].ToString());
                        codeEntity.Codetitle = code[i]["title"].ToString() + activityEntity.Usercodeinfo;
                        //codeEntity.Codetitle = code[i]["title"].ToString();
                        codeEntity.Channel_id = 0;
                        codeEntity.Uid = uid;
                        codeEntity.Order_id = "";
                        codeEntity.Amount = codeconfigEntity.Amount;
                        codeEntity.Discount = codeconfigEntity.Discount;
                        codeEntity.State = 0;
                        codeEntity.From = 1;
                        codeEntity.Addtime = DateTime.Now;
                        switch (aid)
                        {
                            case "17":        //冲20送118元红包
                                string[] dateinfo = new string[] { "0", "0", "0", "0", "0", "0", "2", "2", "4", "4", "7", "7", "14", "14", "21", "21", "28", "28", "35", "35", "42", "42", "49", "49", "56", "56", "63", "63", "70", "70", "77", "77", "84", "84", "91", "91", "98", "98", "105", "105" };

                                if (i < 10)
                                {
                                    codeEntity.Senttime = dt.AddDays(int.Parse(dateinfo[i]));

                                }
                                else
                                {
                                    codeEntity.Senttime = startweek.AddDays(int.Parse(dateinfo[i]));
                                }
                                break;
                            case "23":        //新ID首次支付5元不中送红包
                                codeEntity.Senttime = DateTime.Now;
                                break;
                            case "25":        //拼团抽奖
                                codeEntity.Senttime = DateTime.Now;
                                break;
                            case "31":        //晒单送红包
                                codeEntity.Senttime = DateTime.Now;
                                break;

                        }

                        codeEntity.Overtime = codeEntity.Senttime.AddDays(activityEntity.Redpackday);
                        sentbool=go_activity_codeBusiness.SaveEntity(codeEntity, true);
                    }
                }
                if (sentbool)
                {
                    drUser.Alreadycount++;
                    go_activity_userBusiness.SaveEntity(drUser, false);
                }
            return 0;
        }
        public static int[] getredpacklist(int money)
        {
            int[] redlist = { };
            int[] redinfo100 = { 84, 83, 83, 82, 82, 81, 81 };
            int[] redinfo50 = { 84, 83 };
            int[] redinfo10 = { 82 };
            int[] redinfo5 = { 81 };
            int[] redinfo2 = { 79 };
            int[] redinfo1 = { 78 };
            int monet100 = int.Parse(Math.Floor(money * 1.0 / 100).ToString());
            int resmoney = money;
            for (int i = 0; i < monet100; i++)
            {
                redlist = redlist.Concat(redinfo100).ToArray();
                resmoney = resmoney - 100;
            }
            if (resmoney >= 50)
            {
                redlist = redlist.Concat(redinfo50).ToArray();
                resmoney = resmoney - 50;
            }
            int num10 = resmoney;
            for (int i = 0; i < num10 / 10; i++)
            {
                redlist = redlist.Concat(redinfo10).ToArray();
                resmoney = resmoney - 10;
            }
            if (resmoney >= 5)
            {
                redlist = redlist.Concat(redinfo5).ToArray();
                resmoney = resmoney - 5;
            }
            int num2 = resmoney;
            for (int i = 0; i < num2 / 2; i++)
            {
                redlist = redlist.Concat(redinfo2).ToArray();
                resmoney = resmoney - 2;
            }
            if (resmoney >= 1)
            {
                redlist = redlist.Concat(redinfo1).ToArray();
            }
            return redlist;
        }

        /// <summary>
        /// 根据tuanlistid发放红包 tuanlistid
        /// </summary>
        public static bool sentTuanRedpack(string tuanlistid)
        {
            go_tuan_listinfoEntity tuanlistEntity = go_tuan_listinfoBusiness.LoadEntity(int.Parse(tuanlistid));
            
            int tuanaid = 25;
            go_activityEntity tuanEntity = go_activityBusiness.LoadEntity(tuanaid);
            //判断是否在活动时间内start

            if (tuanEntity.Starttime < DateTime.Now && tuanEntity.Endtime > DateTime.Now)//判断是否在活动时间内end
            {


                DataTable dtOrderlist = go_ordersBusiness.GetListData("businessId=" + tuanlistid + " and ordertype='tuan' and iswon!=1 ", "uid");
                DataTable dtuserredpack = go_activity_codeBusiness.GetListData("1=2", "activity_id,code_config_id,codetitle,channel_id,uid,mobile,order_id,starttime,endtime,raisetime,timespans,use_range,usetime,descript,d_type,amount,discount,state,[from],addtime,senttime,overtime");

                foreach (DataRow drUser in dtOrderlist.Rows)
                {
                    string uid = drUser["uid"].ToString();
                    //判断用户活动资格start
                    if (!go_activity_userBusiness.valQualification(uid, tuanaid.ToString()))
                    {
                        //没有活动资格
                        go_activity_userBusiness.Qualification(uid, tuanaid.ToString());
                    }

                    if (!go_activity_userBusiness.valQualificationCount(uid, tuanaid.ToString()))
                    {
                        //已达到参与次数上限
                        continue;
                    }
                        string where = "Where 1=1 AND activity_id=" + tuanaid + " And uid=" + uid;
                        string from = "go_activity_user";
                        int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
                        if (count == 0)
                        {
                            go_activity_userBusiness.Qualification(uid, tuanaid.ToString());
                        }
                        where += " AND (count=0 OR count>alreadycount)";
                        count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
                        if (count == 0)
                        {
                            //已达到参与次数上限
                            continue;
                        }

                        JArray code = JsonConvert.DeserializeObject<JArray>(tuanEntity.Code_config_ids.ToString());
                        if (code!=null)
                        {
                            for (int i = 0; i < code.Count; i++)
                            {
                                if (code[i]["id"].ToString() != "0")
                                {
                                    go_code_configEntity codeconfigEntity = go_code_configBusiness.LoadEntity(Convert.ToInt32(code[i]["id"].ToString()));
                                    DataRow dr = dtuserredpack.NewRow();
                                    dr["activity_id"] = tuanaid;
                                    dr["code_config_id"] = int.Parse(code[i]["id"].ToString()); ;
                                    //dr["codetitle"] = code[i]["title"].ToString() ;
                                    dr["codetitle"] = code[i]["title"].ToString() + tuanEntity.Usercodeinfo;
                                    dr["channel_id"] = 0;
                                    dr["uid"] = uid;
                                    dr["mobile"] = "";
                                    dr["order_id"] = "";
                                    dr["starttime"] = DBNull.Value;
                                    dr["endtime"] = DBNull.Value;
                                    dr["raisetime"] = 0;
                                    dr["timespans"] = "";
                                    dr["use_range"] = "";
                                    dr["usetime"] = DBNull.Value;
                                    dr["descript"] = "";
                                    dr["d_type"] = 2;
                                    dr["amount"] = codeconfigEntity.Amount;
                                    dr["discount"] = codeconfigEntity.Discount;
                                    dr["state"] = 0;
                                    dr["from"] = 1;
                                    dr["addtime"] = DateTime.Now;
                                    dr["senttime"] = DateTime.Now;
                                    dr["overtime"] = DateTime.Now.AddDays(tuanEntity.Redpackday);
                                    dtuserredpack.Rows.Add(dr);
                                }

                            
                            IList<go_activity_userEntity> ilUser = go_activity_userBusiness.GetListEntity("uid=" + uid + " and activity_id=" + tuanaid, "*");
                            go_activity_userEntity eUser = ilUser[0];
                            eUser.Alreadycount++;
                            go_activity_userBusiness.SaveEntity(eUser, false);
                        }
                    }
                }
                CustomsBusiness.CommitDataTable(dtuserredpack, "select * from go_activity_code");
                    
                
            }
            //买爆款不中包赔活动发包end
            return true;
        }
    }
}
