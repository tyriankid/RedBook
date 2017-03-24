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
	public class RB_Book_CategoryManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From RB_Book_Category Where Id='{0}'", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "RB_Book_Category";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["Id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static RB_Book_CategoryEntity LoadEntity(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From RB_Book_Category Where Id='{0}'", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<RB_Book_CategoryEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From RB_Book_Category {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "RB_Book_Category";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["Id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From RB_Book_Category {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<RB_Book_CategoryEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From RB_Book_Category {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<RB_Book_CategoryEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From RB_Book_Category Where Id='{0}'", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From RB_Book_Category {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(RB_Book_CategoryEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into RB_Book_Category(Id,SortBaseNum,AddTime,CategoryName,CategoryIcon,Description)values(@Id,@SortBaseNum,@AddTime,@CategoryName,@CategoryIcon,@Description)" :
				"Update RB_Book_Category Set Id=@Id,SortBaseNum=@SortBaseNum,AddTime=@AddTime,CategoryName=@CategoryName,CategoryIcon=@CategoryIcon,Description=@Description Where Id=@Id";
				SqlParameter[] para = new SqlParameter[]
				{
					(entity.Id==null)?new SqlParameter("@Id",DBNull.Value):new SqlParameter("@Id",entity.Id),
					new SqlParameter("@SortBaseNum",entity.SortBaseNum),
					(entity.AddTime==null || entity.AddTime==DateTime.MinValue)?new SqlParameter("@AddTime",DBNull.Value):new SqlParameter("@AddTime",entity.AddTime),
					(entity.CategoryName==null)?new SqlParameter("@CategoryName",DBNull.Value):new SqlParameter("@CategoryName",entity.CategoryName),
					(entity.CategoryIcon==null)?new SqlParameter("@CategoryIcon",DBNull.Value):new SqlParameter("@CategoryIcon",entity.CategoryIcon),
					(entity.Description==null)?new SqlParameter("@Description",DBNull.Value):new SqlParameter("@Description",entity.Description),
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
