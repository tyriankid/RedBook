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
	public class RB_User_DynamicManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From RB_User_Dynamic Where Id='{0}'", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "RB_User_Dynamic";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["Id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static RB_User_DynamicEntity LoadEntity(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From RB_User_Dynamic Where Id='{0}'", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<RB_User_DynamicEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From RB_User_Dynamic {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "RB_User_Dynamic";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["Id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From RB_User_Dynamic {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<RB_User_DynamicEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From RB_User_Dynamic {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<RB_User_DynamicEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From RB_User_Dynamic Where Id='{0}'", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From RB_User_Dynamic {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(RB_User_DynamicEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into RB_User_Dynamic(Id,AddTime,UserId,DynamicType,BusinessId)values(@Id,@AddTime,@UserId,@DynamicType,@BusinessId)" :
				"Update RB_User_Dynamic Set Id=@Id,AddTime=@AddTime,UserId=@UserId,DynamicType=@DynamicType,BusinessId=@BusinessId Where Id=@Id";
				SqlParameter[] para = new SqlParameter[]
				{
					(entity.Id==null)?new SqlParameter("@Id",DBNull.Value):new SqlParameter("@Id",entity.Id),
					(entity.AddTime==null)?new SqlParameter("@AddTime",DBNull.Value):new SqlParameter("@AddTime",entity.AddTime),
					new SqlParameter("@UserId",entity.UserId),
					(entity.DynamicType==null)?new SqlParameter("@DynamicType",DBNull.Value):new SqlParameter("@DynamicType",entity.DynamicType),
					(entity.BusinessId==null)?new SqlParameter("@BusinessId",DBNull.Value):new SqlParameter("@BusinessId",entity.BusinessId),
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
