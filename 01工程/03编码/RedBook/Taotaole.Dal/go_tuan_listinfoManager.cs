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
	/// 团购参团记录-数据库操作类
	/// </summary>
	public class go_tuan_listinfoManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_tuan_listinfo Where tuanlistId={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_tuan_listinfo";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["tuanlistId"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_tuan_listinfoEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_tuan_listinfo Where tuanlistId={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_tuan_listinfoEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_tuan_listinfo {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_tuan_listinfo";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["tuanlistId"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_tuan_listinfo {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_tuan_listinfoEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_tuan_listinfo {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_tuan_listinfoEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_tuan_listinfo Where tuanlistId={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_tuan_listinfo {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_tuan_listinfoEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_tuan_listinfo(status,tuanId,uid,total_num,involvement_num,remain_num,winners_id,assign_winners,addtime,endtime,q_end_time,codetable,winning_code,q_showtime)values(@status,@tuanId,@uid,@total_num,@involvement_num,@remain_num,@winners_id,@assign_winners,@addtime,@endtime,@q_end_time,@codetable,@winning_code,@q_showtime)" :
				"Update go_tuan_listinfo Set status=@status,tuanId=@tuanId,uid=@uid,total_num=@total_num,involvement_num=@involvement_num,remain_num=@remain_num,winners_id=@winners_id,assign_winners=@assign_winners,addtime=@addtime,endtime=@endtime,q_end_time=@q_end_time,codetable=@codetable,winning_code=@winning_code,q_showtime=@q_showtime Where tuanlistId=@tuanlistId";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@status",entity.Status),
					new SqlParameter("@tuanlistId",entity.TuanlistId),
					new SqlParameter("@tuanId",entity.TuanId),
					new SqlParameter("@uid",entity.Uid),
					new SqlParameter("@total_num",entity.Total_num),
					new SqlParameter("@involvement_num",entity.Involvement_num),
					new SqlParameter("@remain_num",entity.Remain_num),
					new SqlParameter("@winners_id",entity.Winners_id),
					new SqlParameter("@assign_winners",entity.Assign_winners),
					(entity.Addtime==null || entity.Addtime==DateTime.MinValue)?new SqlParameter("@addtime",DBNull.Value):new SqlParameter("@addtime",entity.Addtime),
					(entity.Endtime==null || entity.Endtime==DateTime.MinValue)?new SqlParameter("@endtime",DBNull.Value):new SqlParameter("@endtime",entity.Endtime),
					(entity.Q_end_time==null || entity.Q_end_time==DateTime.MinValue)?new SqlParameter("@q_end_time",DBNull.Value):new SqlParameter("@q_end_time",entity.Q_end_time),
					(entity.Codetable==null)?new SqlParameter("@codetable",DBNull.Value):new SqlParameter("@codetable",entity.Codetable),
					(entity.Winning_code==null)?new SqlParameter("@winning_code",DBNull.Value):new SqlParameter("@winning_code",entity.Winning_code),
					(entity.Q_showtime==null)?new SqlParameter("@q_showtime",DBNull.Value):new SqlParameter("@q_showtime",entity.Q_showtime),
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
