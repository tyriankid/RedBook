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
	public class go_activityManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_activity Where id={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_activity";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_activityEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_activity Where id={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_activityEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_activity {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_activity";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_activity {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_activityEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_activity {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_activityEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_activity Where id={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_activity {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_activityEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_activity(a_type,sent_type,state,amount,count,sent_count,shopnum,shopnummin,shopnummax,redpackday,nextweeknum,starttime,endtime,addtime,qualification,title,content,timespans,code_ids_level,code_config_ids,use_range,usercodeinfo)values(@a_type,@sent_type,@state,@amount,@count,@sent_count,@shopnum,@shopnummin,@shopnummax,@redpackday,@nextweeknum,@starttime,@endtime,@addtime,@qualification,@title,@content,@timespans,@code_ids_level,@code_config_ids,@use_range,@usercodeinfo)" :
				"Update go_activity Set a_type=@a_type,sent_type=@sent_type,state=@state,amount=@amount,count=@count,sent_count=@sent_count,shopnum=@shopnum,shopnummin=@shopnummin,shopnummax=@shopnummax,redpackday=@redpackday,nextweeknum=@nextweeknum,starttime=@starttime,endtime=@endtime,addtime=@addtime,qualification=@qualification,title=@title,content=@content,timespans=@timespans,code_ids_level=@code_ids_level,code_config_ids=@code_config_ids,use_range=@use_range,usercodeinfo=@usercodeinfo Where id=@id";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@a_type",entity.A_type),
					new SqlParameter("@sent_type",entity.Sent_type),
					new SqlParameter("@state",entity.State),
					new SqlParameter("@id",entity.Id),
					new SqlParameter("@amount",entity.Amount),
					new SqlParameter("@count",entity.Count),
					new SqlParameter("@sent_count",entity.Sent_count),
					new SqlParameter("@shopnum",entity.Shopnum),
					new SqlParameter("@shopnummin",entity.Shopnummin),
					new SqlParameter("@shopnummax",entity.Shopnummax),
					new SqlParameter("@redpackday",entity.Redpackday),
					new SqlParameter("@nextweeknum",entity.Nextweeknum),
					(entity.Starttime==null || entity.Starttime==DateTime.MinValue)?new SqlParameter("@starttime",DBNull.Value):new SqlParameter("@starttime",entity.Starttime),
					(entity.Endtime==null || entity.Endtime==DateTime.MinValue)?new SqlParameter("@endtime",DBNull.Value):new SqlParameter("@endtime",entity.Endtime),
					(entity.Addtime==null || entity.Addtime==DateTime.MinValue)?new SqlParameter("@addtime",DBNull.Value):new SqlParameter("@addtime",entity.Addtime),
					new SqlParameter("@qualification",entity.Qualification),
					(entity.Title==null)?new SqlParameter("@title",DBNull.Value):new SqlParameter("@title",entity.Title),
					(entity.Content==null)?new SqlParameter("@content",DBNull.Value):new SqlParameter("@content",entity.Content),
					(entity.Timespans==null)?new SqlParameter("@timespans",DBNull.Value):new SqlParameter("@timespans",entity.Timespans),
					(entity.Code_ids_level==null)?new SqlParameter("@code_ids_level",DBNull.Value):new SqlParameter("@code_ids_level",entity.Code_ids_level),
					(entity.Code_config_ids==null)?new SqlParameter("@code_config_ids",DBNull.Value):new SqlParameter("@code_config_ids",entity.Code_config_ids),
					(entity.Use_range==null)?new SqlParameter("@use_range",DBNull.Value):new SqlParameter("@use_range",entity.Use_range),
					(entity.Usercodeinfo==null)?new SqlParameter("@usercodeinfo",DBNull.Value):new SqlParameter("@usercodeinfo",entity.Usercodeinfo),
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
