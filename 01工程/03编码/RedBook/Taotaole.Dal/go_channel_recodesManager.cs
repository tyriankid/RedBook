using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Taotaole.Model;
using YH.DataAccess;
using YH.Utility;

namespace Taotaole.Dal
{
    /// <summary>
    /// 推广佣金明细-数据库操作类
    /// </summary>
    public class go_channel_recodesManager
    {

        /// <summary>
        /// 根据主键查询数据集
        /// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = string.Format(@"Select * From go_channel_recodes Where rid={0}", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_channel_recodes";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["rid"] };
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据主键查询数据实体
        /// </summary>
        public static go_channel_recodesEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = string.Format(@"Select * From go_channel_recodes Where rid={0}", ID);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
            {
                return ReaderConvert.ReaderToModel<go_channel_recodesEntity>(reader);
            }
        }

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable SelectListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {2} {1} From go_channel_recodes {0}", where, selectFields, top == 0 ? "" : "top " + top);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_channel_recodes";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["rid"] };
            return ds.Tables[0];
        }


        /// <summary>
        /// 根据服务商编号获取佣金金额
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectSumMoney(int uid, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = string.Format(@"select sum(money) from go_channel_recodes where uid={0}", uid);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 根据充值记录添加渠道商或服务商佣金记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rechargemoney"></param>
        /// <param name="currDbName"></param>
        public static void AddRecodes(string OrderId,int uid, decimal rechargemoney, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {

            go_memberEntity classify = go_memberManager.LoadEntity(uid);
            if (classify != null)
            {
                if (classify.Servicecityid != 0) //城市服务商佣金记录
                {
                    go_channel_userEntity user = go_channel_userManager.LoadEntity(classify.Servicecityid);
                    string state = user.Frozenstate.ToString();
                    go_channel_recodesEntity recodes = new go_channel_recodesEntity();
                    recodes.Uid = classify.Servicecityid;
                    recodes.Cid = uid;
                    recodes.Content = "微信充值";
                    recodes.Rechargemoney = rechargemoney;
                    recodes.State = 0;
                    recodes.Orderid = OrderId;
                    if (user.Rebateratio != 0)
                        recodes.Money = rechargemoney * user.Rebateratio / 100;
                    recodes.Time = DateTime.Now;

                    if (state == "1")   //如果被冻结判断如果服务商佣金就是0
                        recodes.Money = 0;
                    if (go_channel_recodesManager.SaveEntity(recodes, true))
                    {

                        if (state != "1")   //如果不是被冻结 才可以增加可提现金额
                        {
                            user.Nowithdrawcash = user.Nowithdrawcash + recodes.Money;
                            user.Usercountmoney = user.Usercountmoney + rechargemoney;
                        }
                        go_channel_userManager.SaveEntity(user, false);
                    }
                }



                if (classify.Servicechannelid != 0) //渠道商佣金记录
                {
                    go_channel_userEntity user = go_channel_userManager.LoadEntity(classify.Servicechannelid);
                    string state = user.Frozenstate.ToString();

                    go_channel_recodesEntity recodes = new go_channel_recodesEntity();
                    recodes.Uid = classify.Servicechannelid;
                    recodes.Cid = uid;
                    recodes.Content = "微信充值";
                    recodes.Rechargemoney = rechargemoney;
                    recodes.State = 0;
                    recodes.Orderid = OrderId;
                    if (user.Rebateratio != 0)
                        recodes.Money = rechargemoney * user.Rebateratio / 100;
                    recodes.Time = DateTime.Now;

                    if (state == "1")   //如果被冻结判断如果服务商佣金就是0
                        recodes.Money = 0;

                    if (go_channel_recodesManager.SaveEntity(recodes, true))
                    {
                        if (state != "1")   //如果不是被冻结 才可以增加可提现金额
                        {
                            user.Nowithdrawcash = user.Nowithdrawcash + recodes.Money;
                            user.Usercountmoney = user.Usercountmoney + rechargemoney;
                        }
                        go_channel_userManager.SaveEntity(user, false); //更改可提现金额

                    }
                }
            }
        }

        /// <summary>
        /// 获取所有给服务商或者渠道商的充值金额
        /// </summary>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectAllrechargemoney(string from, string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = "select sum(rechargemoney) from  " + from;

            if (where != null)
            {
                selectSql += " where " + where;
            }
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 获取所有服务商或者渠道商佣金
        /// </summary>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectAllMoney(string from, string where = null, string channel = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = "select sum(money) from  " + from;
            if (channel != null)
            {
                selectSql = "select sum(m) from  " + from;
            }
            if (where != null)
            {
                selectSql += " where " + where;
            }
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }


        /// <summary>
        /// 根据条件查询首行首列
        /// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {1} From go_channel_recodes {0}", where, selectFields);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
        }

        /// <summary>
        /// 根据条件查询数据实体
        /// </summary>
        public static IList<go_channel_recodesEntity> SelectListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {2} {1} From go_channel_recodes {0}", where, selectFields, top == 0 ? "" : "top " + top);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
            {
                return ReaderConvert.ReaderToList<go_channel_recodesEntity>(reader);
            }
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string deleteSql = string.Format(@"Delete From go_channel_recodes Where rid={0}", ID);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string deleteSql = string.Format(@"Delete From go_channel_recodes {0}", where);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public static bool SaveEntity(go_channel_recodesEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            try
            {
                string execSql = (isAdd) ?
                                "Insert Into go_channel_recodes(state,uid,cid,money,rechargemoney,time,settlementtime,content,orderid)values(@state,@uid,@cid,@money,@rechargemoney,@time,@settlementtime,@content,@OrderId)" :
                                "Update go_channel_recodes Set state=@state,uid=@uid,cid=@cid,money=@money,rechargemoney=@rechargemoney,time=@time,settlementtime=@settlementtime,content=@content,orderid=@OrderId Where rid=@rid";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@state",entity.State),
					new SqlParameter("@rid",entity.Rid),
					new SqlParameter("@uid",entity.Uid),
					new SqlParameter("@cid",entity.Cid),
                    new SqlParameter("@OrderId",entity.Orderid),
					(entity.Money==null)?new SqlParameter("@money",DBNull.Value):new SqlParameter("@money",entity.Money),
					(entity.Rechargemoney==null)?new SqlParameter("@rechargemoney",DBNull.Value):new SqlParameter("@rechargemoney",entity.Rechargemoney),
					(entity.Time==null || entity.Time==DateTime.MinValue)?new SqlParameter("@time",DBNull.Value):new SqlParameter("@time",entity.Time),
					(entity.Settlementtime==null || entity.Settlementtime==DateTime.MinValue)?new SqlParameter("@settlementtime",DBNull.Value):new SqlParameter("@settlementtime",entity.Settlementtime),
					(entity.Content==null)?new SqlParameter("@content",DBNull.Value):new SqlParameter("@content",entity.Content),
				};
                DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(execSql, para);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
