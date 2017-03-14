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
	public class go_giftWinDetailManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_giftWinDetail Where giftWinID={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_giftWinDetail";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["giftWinID"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_giftWinDetailEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_giftWinDetail Where giftWinID={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_giftWinDetailEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_giftWinDetail {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_giftWinDetail";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["giftWinID"] };
			return ds.Tables[0];
		}

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable selectData(string where = null, string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"select  PD.title,od.status,PD.thumb,WD.*from go_giftWinDetail WD  left  join go_gifts G on   WD.giftId=g.giftId  left join  go_products PD on G.productid=PD.productid  left  join  go_orders  OD  on  WD.orderId=OD.orderId {0}", where, top == 0 ? "" : "top " + top);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_giftWinDetail";
            return ds.Tables[0];
        }

        
        /// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_giftWinDetail {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_giftWinDetailEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_giftWinDetail {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_giftWinDetailEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_giftWinDetail Where giftWinID={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_giftWinDetail {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_giftWinDetailEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_giftWinDetail(uid,giftId,isWon,costScore,addtime,orderId)values(@uid,@giftId,@isWon,@costScore,@addtime,@orderId)" :
				"Update go_giftWinDetail Set uid=@uid,giftId=@giftId,isWon=@isWon,costScore=@costScore,addtime=@addtime,orderId=@orderId Where giftWinID=@giftWinID";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@giftWinID",entity.GiftWinID),
					new SqlParameter("@uid",entity.Uid),
					new SqlParameter("@giftId",entity.GiftId),
					new SqlParameter("@isWon",entity.IsWon),
					new SqlParameter("@costScore",entity.CostScore),
					(entity.Addtime==null || entity.Addtime==DateTime.MinValue)?new SqlParameter("@addtime",DBNull.Value):new SqlParameter("@addtime",entity.Addtime),
					(entity.OrderId==null)?new SqlParameter("@orderId",DBNull.Value):new SqlParameter("@orderId",entity.OrderId),
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
