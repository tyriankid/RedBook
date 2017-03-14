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
	/// 晒单-数据库操作类
	/// </summary>
	public class go_shaidanManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_shaidan Where sd_id={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_shaidan";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["sd_id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_shaidanEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_shaidan Where sd_id={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_shaidanEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_shaidan {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_shaidan";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["sd_id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_shaidan {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_shaidanEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_shaidan {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_shaidanEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_shaidan Where sd_id={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_shaidan {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_shaidanEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_shaidan(sd_userid,sd_qishu,sd_zan,sd_ping,sd_time,sd_content,sd_photolist,sd_orderid,sd_ip,sd_title,sd_thumbs)values(@sd_userid,@sd_qishu,@sd_zan,@sd_ping,@sd_time,@sd_content,@sd_photolist,@sd_orderid,@sd_ip,@sd_title,@sd_thumbs)" :
				"Update go_shaidan Set sd_userid=@sd_userid,sd_qishu=@sd_qishu,sd_zan=@sd_zan,sd_ping=@sd_ping,sd_time=@sd_time,sd_content=@sd_content,sd_photolist=@sd_photolist,sd_orderid=@sd_orderid,sd_ip=@sd_ip,sd_title=@sd_title,sd_thumbs=@sd_thumbs Where sd_id=@sd_id";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@sd_id",entity.Sd_id),
					new SqlParameter("@sd_userid",entity.Sd_userid),
					new SqlParameter("@sd_qishu",entity.Sd_qishu),
					new SqlParameter("@sd_zan",entity.Sd_zan),
					new SqlParameter("@sd_ping",entity.Sd_ping),
					(entity.Sd_time==null || entity.Sd_time==DateTime.MinValue)?new SqlParameter("@sd_time",DBNull.Value):new SqlParameter("@sd_time",entity.Sd_time),
					(entity.Sd_content==null)?new SqlParameter("@sd_content",DBNull.Value):new SqlParameter("@sd_content",entity.Sd_content),
					(entity.Sd_photolist==null)?new SqlParameter("@sd_photolist",DBNull.Value):new SqlParameter("@sd_photolist",entity.Sd_photolist),
					(entity.Sd_orderid==null)?new SqlParameter("@sd_orderid",DBNull.Value):new SqlParameter("@sd_orderid",entity.Sd_orderid),
					(entity.Sd_ip==null)?new SqlParameter("@sd_ip",DBNull.Value):new SqlParameter("@sd_ip",entity.Sd_ip),
					(entity.Sd_title==null)?new SqlParameter("@sd_title",DBNull.Value):new SqlParameter("@sd_title",entity.Sd_title),
					(entity.Sd_thumbs==null)?new SqlParameter("@sd_thumbs",DBNull.Value):new SqlParameter("@sd_thumbs",entity.Sd_thumbs),
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
