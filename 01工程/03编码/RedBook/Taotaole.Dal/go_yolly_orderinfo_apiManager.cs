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
	/// 永乐对账-数据库操作类
	/// </summary>
	public class go_yolly_orderinfo_apiManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_yolly_orderinfo_api Where serialid={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_yolly_orderinfo_api";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["serialid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_yolly_orderinfo_apiEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_yolly_orderinfo_api Where serialid={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_yolly_orderinfo_apiEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_yolly_orderinfo_api {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_yolly_orderinfo_api";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["serialid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_yolly_orderinfo_api {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_yolly_orderinfo_apiEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_yolly_orderinfo_api {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_yolly_orderinfo_apiEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_yolly_orderinfo_api Where serialid={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_yolly_orderinfo_api {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_yolly_orderinfo_apiEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_yolly_orderinfo_api(usetime,orderId,money,usenum,usetype,status,context)values(@usetime,@orderId,@money,@usenum,@usetype,@status,@context)" :
				"Update go_yolly_orderinfo_api Set usetime=@usetime,orderId=@orderId,money=@money,usenum=@usenum,usetype=@usetype,status=@status,context=@context Where serialid=@serialid";
				SqlParameter[] para = new SqlParameter[]
				{
					(entity.Usetime==null || entity.Usetime==DateTime.MinValue)?new SqlParameter("@usetime",DBNull.Value):new SqlParameter("@usetime",entity.Usetime),
					(entity.Serialid==null)?new SqlParameter("@serialid",DBNull.Value):new SqlParameter("@serialid",entity.Serialid),
					(entity.OrderId==null)?new SqlParameter("@orderId",DBNull.Value):new SqlParameter("@orderId",entity.OrderId),
					(entity.Money==null)?new SqlParameter("@money",DBNull.Value):new SqlParameter("@money",entity.Money),
					(entity.Usenum==null)?new SqlParameter("@usenum",DBNull.Value):new SqlParameter("@usenum",entity.Usenum),
					(entity.Usetype==null)?new SqlParameter("@usetype",DBNull.Value):new SqlParameter("@usetype",entity.Usetype),
					(entity.Status==null)?new SqlParameter("@status",DBNull.Value):new SqlParameter("@status",entity.Status),
					(entity.Context==null)?new SqlParameter("@context",DBNull.Value):new SqlParameter("@context",entity.Context),
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
