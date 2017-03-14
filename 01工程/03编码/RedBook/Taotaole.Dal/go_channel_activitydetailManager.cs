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
    /// -数据库操作类
    /// </summary>
    public class go_channel_activitydetailManager
    {
        private readonly static string dbServerName = null; //数据库服务名，为空时调用主服务器

        /// <summary>
        /// 根据主键查询数据集
        /// </summary>
        public static DataTable LoadData(int ID)
        {
            string selectSql = string.Format(@"Select * From go_channel_activitydetail Where adid={0}", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_channel_activitydetail";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["adid"] };
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据主键查询数据实体
        /// </summary>
        public static go_channel_activitydetailEntity LoadEntity(int ID)
        {
            string selectSql = string.Format(@"Select * From go_channel_activitydetail Where adid={0}", ID);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
            {
                return ReaderConvert.ReaderToModel<go_channel_activitydetailEntity>(reader);
            }
        }

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable SelectListData(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {2} {1} From go_channel_activitydetail {0}", where, selectFields, top == 0 ? "" : "top " + top);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_channel_activitydetail";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["adid"] };
            return ds.Tables[0];
        }
        /// <summary>
        /// 增加网吧渠道商记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="shopid"></param>
        /// <param name="currDbName"></param>
        public static void AddActivityDetails(int uid, int shopid,int activityID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            //用户的所属渠道商是网吧且未参与此活动
            go_memberEntity memberEntity = go_memberManager.LoadEntity(uid);

            go_channel_activitydetailEntity classify = new go_channel_activitydetailEntity();
            classify.Aid = activityID;
            classify.Cuid = memberEntity.Servicechannelid;
            classify.Uid = uid;
            classify.Createtime = DateTime.Now;
            classify.Exchange = 0;
            classify.Shopid = shopid;
            go_channel_activitydetailManager.SaveEntity(classify, true);
        }

        //private void WriteLog(string log)
        //{
        //    System.IO.StreamWriter writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("~/wx_login.txt"));

        //    writer.WriteLine(System.DateTime.Now);

        //    writer.WriteLine(log);

        //    writer.Flush();

        //    writer.Close();

        //}
        /// <summary>
        /// 根据条件查询首行首列
        /// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", string orderby = null)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {1} From go_channel_activitydetail {0}", where, selectFields);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            return DataAccessFactory.GetDataProvider(dbServerName).GetScalar(selectSql);
        }

        /// <summary>
        /// 根据条件查询数据实体
        /// </summary>
        public static IList<go_channel_activitydetailEntity> SelectListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {2} {1} From go_channel_activitydetail {0}", where, selectFields, top == 0 ? "" : "top " + top);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
            {
                return ReaderConvert.ReaderToList<go_channel_activitydetailEntity>(reader);
            }
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        public static void Del(int ID)
        {
            string deleteSql = string.Format(@"Delete From go_channel_activitydetail Where adid={0}", ID);
            DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
        }

        /// <summary>
        /// 根据渠道商编号更改兑换状态
        /// </summary>
        /// <param name="cuid"></param>
        public static void UpdateApplyExchange(int cuid)
        {
            try
            {

                string execSql = string.Format("update go_channel_activitydetail set exchange=2,settlementtime='{0}' Where cuid=@cuid and exchange=3 ",DateTime.Now);
                SqlParameter[] para = new SqlParameter[]
				{
					
					new SqlParameter("@cuid",cuid)
				};
                DataAccessFactory.GetDataProvider(dbServerName).Execute(execSql, para);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 根据渠道商编号更改兑换状态
        /// </summary>
        /// <param name="cuid"></param>
        public static void UpdateExchange(int cuid,string orderid)
        {
            try
            {

                string execSql = string.Format("update go_channel_activitydetail set orderid=@orderid Where cuid=@cuid and exchange=0");
                SqlParameter[] para = new SqlParameter[]
				{
					
					new SqlParameter("@cuid",cuid),
                    new SqlParameter("@orderid",orderid)
				};
                DataAccessFactory.GetDataProvider(dbServerName).Execute(execSql, para);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 根据条件删除
        /// </summary>
        public static void DelListData(string where = null)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string deleteSql = string.Format(@"Delete From go_channel_activitydetail {0}", where);
            DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public static bool SaveEntity(go_channel_activitydetailEntity entity, bool isAdd)
        {
            try
            {
                string execSql = (isAdd) ?
                                "Insert Into go_channel_activitydetail(aid,exchange,cuid,uid,shopid,createtime,mantime,settlementtime,applytime,orderid)values(@aid,@exchange,@cuid,@uid,@shopid,@createtime,@mantime,@settlementtime,@applytime,@orderid)" :
                                "Update go_channel_activitydetail Set aid=@aid,exchange=@exchange,cuid=@cuid,uid=@uid,shopid=@shopid,createtime=@createtime,mantime=@mantime,settlementtime=@settlementtime,applytime=@applytime,orderid=@orderid Where adid=@adid";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@aid",entity.Aid),
					new SqlParameter("@exchange",entity.Exchange),
					new SqlParameter("@adid",entity.Adid),
					new SqlParameter("@cuid",entity.Cuid),
					new SqlParameter("@uid",entity.Uid),
					new SqlParameter("@shopid",entity.Shopid),
					(entity.Createtime==null || entity.Createtime==DateTime.MinValue)?new SqlParameter("@createtime",DBNull.Value):new SqlParameter("@createtime",entity.Createtime),
					(entity.Mantime==null || entity.Mantime==DateTime.MinValue)?new SqlParameter("@mantime",DBNull.Value):new SqlParameter("@mantime",entity.Mantime),
					(entity.Settlementtime==null || entity.Settlementtime==DateTime.MinValue)?new SqlParameter("@settlementtime",DBNull.Value):new SqlParameter("@settlementtime",entity.Settlementtime),
					(entity.Applytime==null || entity.Applytime==DateTime.MinValue)?new SqlParameter("@applytime",DBNull.Value):new SqlParameter("@applytime",entity.Applytime),
					(entity.Orderid==null)?new SqlParameter("@orderid",DBNull.Value):new SqlParameter("@orderid",entity.Orderid),

				};
                DataAccessFactory.GetDataProvider(dbServerName).Execute(execSql, para);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 修改兑换状态
        /// </summary>
        public static bool Modify(go_channel_activitydetailEntity entity)
        {
            try
            {
                string execSql = "Update go_channel_activitydetail Set exchange=@exchange,createtime=@createtime Where uid=@uid and exchange=0";
                SqlParameter[] para = new SqlParameter[]
				{
					
					new SqlParameter("@exchange",entity.Exchange),
                    new SqlParameter("@createtime",entity.Mantime),
					new SqlParameter("@uid",entity.Uid)
				};
                DataAccessFactory.GetDataProvider(dbServerName).Execute(execSql, para);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
