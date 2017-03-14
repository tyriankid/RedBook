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
	/// 用户组-数据库操作类
	/// </summary>
	public class go_member_groupManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_member_group Where groupid={0}", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_member_group";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["groupid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
        public static go_member_groupEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_member_group Where groupid={0}", ID);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_member_groupEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
        public static DataTable SelectListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_member_group {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_member_group";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["groupid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_member_group {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
        public static IList<go_member_groupEntity> SelectListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_member_group {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_member_groupEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_member_group Where groupid={0}", ID);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_member_group {0}", where);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
        public static bool SaveEntity(go_member_groupEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_member_group(jingyan_end,jingyan_start,name,icon,type)values(@jingyan_end,@jingyan_start,@name,@icon,@type)" :
				"Update go_member_group Set jingyan_end=@jingyan_end,jingyan_start=@jingyan_start,name=@name,icon=@icon,type=@type Where groupid=@groupid";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@groupid",entity.Groupid),
					new SqlParameter("@jingyan_end",entity.Jingyan_end),
					new SqlParameter("@jingyan_start",entity.Jingyan_start),
					(entity.Name==null)?new SqlParameter("@name",DBNull.Value):new SqlParameter("@name",entity.Name),
					(entity.Icon==null)?new SqlParameter("@icon",DBNull.Value):new SqlParameter("@icon",entity.Icon),
					(entity.Type==null)?new SqlParameter("@type",DBNull.Value):new SqlParameter("@type",entity.Type),
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
