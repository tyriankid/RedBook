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
	/// 游戏地址-数据库操作类
	/// </summary>
	public class go_member_dizhi_gameManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_member_dizhi_game Where id={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_member_dizhi_game";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_member_dizhi_gameEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_member_dizhi_game Where id={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_member_dizhi_gameEntity>(reader);
			}
		}
		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_member_dizhi_game {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_member_dizhi_game";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_member_dizhi_game {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_member_dizhi_gameEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_member_dizhi_game {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_member_dizhi_gameEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_member_dizhi_game Where id={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_member_dizhi_game {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_member_dizhi_gameEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_member_dizhi_game(uid,time,shifoufahuo,gamename,gamearea,gameserver,gametype,gameusercode,shouhuoren,mobile,tell,isdefault,qq)values(@uid,@time,@shifoufahuo,@gamename,@gamearea,@gameserver,@gametype,@gameusercode,@shouhuoren,@mobile,@tell,@isdefault,@qq)" :
				"Update go_member_dizhi_game Set uid=@uid,time=@time,shifoufahuo=@shifoufahuo,gamename=@gamename,gamearea=@gamearea,gameserver=@gameserver,gametype=@gametype,gameusercode=@gameusercode,shouhuoren=@shouhuoren,mobile=@mobile,tell=@tell,isdefault=@isdefault,qq=@qq Where id=@id";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@id",entity.Id),
					new SqlParameter("@uid",entity.Uid),
					(entity.Time==null || entity.Time==DateTime.MinValue)?new SqlParameter("@time",DBNull.Value):new SqlParameter("@time",entity.Time),
					new SqlParameter("@shifoufahuo",entity.Shifoufahuo),
					(entity.Gamename==null)?new SqlParameter("@gamename",DBNull.Value):new SqlParameter("@gamename",entity.Gamename),
					(entity.Gamearea==null)?new SqlParameter("@gamearea",DBNull.Value):new SqlParameter("@gamearea",entity.Gamearea),
					(entity.Gameserver==null)?new SqlParameter("@gameserver",DBNull.Value):new SqlParameter("@gameserver",entity.Gameserver),
					(entity.Gametype==null)?new SqlParameter("@gametype",DBNull.Value):new SqlParameter("@gametype",entity.Gametype),
					(entity.Gameusercode==null)?new SqlParameter("@gameusercode",DBNull.Value):new SqlParameter("@gameusercode",entity.Gameusercode),
					(entity.Shouhuoren==null)?new SqlParameter("@shouhuoren",DBNull.Value):new SqlParameter("@shouhuoren",entity.Shouhuoren),
					(entity.Mobile==null)?new SqlParameter("@mobile",DBNull.Value):new SqlParameter("@mobile",entity.Mobile),
					(entity.Tell==null)?new SqlParameter("@tell",DBNull.Value):new SqlParameter("@tell",entity.Tell),
					(entity.Isdefault==null)?new SqlParameter("@isdefault",DBNull.Value):new SqlParameter("@isdefault",entity.Isdefault),
					(entity.Qq==null)?new SqlParameter("@qq",DBNull.Value):new SqlParameter("@qq",entity.Qq),
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
