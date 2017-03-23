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
	public class RB_BookManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From RB_Book Where Id='{0}'", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "RB_Book";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["Id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static RB_BookEntity LoadEntity(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From RB_Book Where Id='{0}'", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<RB_BookEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From RB_Book {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "RB_Book";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["Id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From RB_Book {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<RB_BookEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From RB_Book {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<RB_BookEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From RB_Book Where Id='{0}'", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From RB_Book {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(RB_BookEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into RB_Book(Id,CategoryId,UserId,FabulousCount,FavoriteCount,WatchCount,SortBaseNum,Status,AddTime,MainTitle,SubTitle,BackImgUrl,UserName,Keyword,Description,ProductIds,Memo)values(@Id,@CategoryId,@UserId,@FabulousCount,@FavoriteCount,@WatchCount,@SortBaseNum,@Status,@AddTime,@MainTitle,@SubTitle,@BackImgUrl,@UserName,@Keyword,@Description,@ProductIds,@Memo)" :
				"Update RB_Book Set Id=@Id,CategoryId=@CategoryId,UserId=@UserId,FabulousCount=@FabulousCount,FavoriteCount=@FavoriteCount,WatchCount=@WatchCount,SortBaseNum=@SortBaseNum,Status=@Status,AddTime=@AddTime,MainTitle=@MainTitle,SubTitle=@SubTitle,BackImgUrl=@BackImgUrl,UserName=@UserName,Keyword=@Keyword,Description=@Description,ProductIds=@ProductIds,Memo=@Memo Where Id=@Id";
				SqlParameter[] para = new SqlParameter[]
				{
					(entity.Id==null)?new SqlParameter("@Id",DBNull.Value):new SqlParameter("@Id",entity.Id),
					(entity.CategoryId==null)?new SqlParameter("@CategoryId",DBNull.Value):new SqlParameter("@CategoryId",entity.CategoryId),
					new SqlParameter("@UserId",entity.UserId),
					new SqlParameter("@FabulousCount",entity.FabulousCount),
					new SqlParameter("@FavoriteCount",entity.FavoriteCount),
					new SqlParameter("@WatchCount",entity.WatchCount),
					new SqlParameter("@SortBaseNum",entity.SortBaseNum),
					new SqlParameter("@Status",entity.Status),
					(entity.AddTime==null || entity.AddTime==DateTime.MinValue)?new SqlParameter("@AddTime",DBNull.Value):new SqlParameter("@AddTime",entity.AddTime),
					(entity.MainTitle==null)?new SqlParameter("@MainTitle",DBNull.Value):new SqlParameter("@MainTitle",entity.MainTitle),
					(entity.SubTitle==null)?new SqlParameter("@SubTitle",DBNull.Value):new SqlParameter("@SubTitle",entity.SubTitle),
					(entity.BackImgUrl==null)?new SqlParameter("@BackImgUrl",DBNull.Value):new SqlParameter("@BackImgUrl",entity.BackImgUrl),
					(entity.UserName==null)?new SqlParameter("@UserName",DBNull.Value):new SqlParameter("@UserName",entity.UserName),
					(entity.Keyword==null)?new SqlParameter("@Keyword",DBNull.Value):new SqlParameter("@Keyword",entity.Keyword),
					(entity.Description==null)?new SqlParameter("@Description",DBNull.Value):new SqlParameter("@Description",entity.Description),
					(entity.ProductIds==null)?new SqlParameter("@ProductIds",DBNull.Value):new SqlParameter("@ProductIds",entity.ProductIds),
					(entity.Memo==null)?new SqlParameter("@Memo",DBNull.Value):new SqlParameter("@Memo",entity.Memo),
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
