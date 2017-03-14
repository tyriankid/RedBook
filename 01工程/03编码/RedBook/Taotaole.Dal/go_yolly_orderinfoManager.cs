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
	/// 永乐充值-数据库操作类
	/// </summary>
	public class go_yolly_orderinfoManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_yolly_orderinfo Where serialid={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_yolly_orderinfo";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["serialid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_yolly_orderinfoEntity LoadEntity(string  ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_yolly_orderinfo Where serialid='{0}'", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_yolly_orderinfoEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_yolly_orderinfo {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_yolly_orderinfo";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["serialid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_yolly_orderinfo {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_yolly_orderinfoEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_yolly_orderinfo {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_yolly_orderinfoEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_yolly_orderinfo Where serialid={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_yolly_orderinfo {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_yolly_orderinfoEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
                "Insert Into go_yolly_orderinfo(serialid,usetype,status,issynchro,money,usetime,orderId,usenum,context,paytype)values(@serialid,@usetype,@status,@issynchro,@money,@usetime,@orderId,@usenum,@context,@paytype)" :
				"Update go_yolly_orderinfo Set usetype=@usetype,status=@status,issynchro=@issynchro,money=@money,usetime=@usetime,orderId=@orderId,usenum=@usenum,context=@context,paytype=@paytype Where serialid=@serialid";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@usetype",entity.Usetype),
					new SqlParameter("@status",entity.Status),
					new SqlParameter("@issynchro",entity.Issynchro),
					new SqlParameter("@money",entity.Money),
					(entity.Usetime==null || entity.Usetime==DateTime.MinValue)?new SqlParameter("@usetime",DBNull.Value):new SqlParameter("@usetime",entity.Usetime),
					(entity.Serialid==null)?new SqlParameter("@serialid",DBNull.Value):new SqlParameter("@serialid",entity.Serialid),
					(entity.OrderId==null)?new SqlParameter("@orderId",DBNull.Value):new SqlParameter("@orderId",entity.OrderId),
					(entity.Usenum==null)?new SqlParameter("@usenum",DBNull.Value):new SqlParameter("@usenum",entity.Usenum),
					(entity.Context==null)?new SqlParameter("@context",DBNull.Value):new SqlParameter("@context",entity.Context),
					(entity.Paytype==null)?new SqlParameter("@paytype",DBNull.Value):new SqlParameter("@paytype",entity.Paytype),
				};
				DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(execSql, para);
				return true;
			}
			catch(Exception ex)
			{
                //System.IO.File.AppendAllText(@"E:\WebSite\APITaotaolePlatformDB_\Debug_Logger.txt", string.Format("{0}:{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message));
				return false;
			}
		}

	}
}
