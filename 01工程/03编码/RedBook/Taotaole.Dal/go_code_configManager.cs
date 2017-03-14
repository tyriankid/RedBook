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
	/// 红包配置-数据库操作类
	/// </summary>
	public class go_code_configManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_code_config Where id={0}", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_code_config";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
        public static go_code_configEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_code_config Where id={0}", ID);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_code_configEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
        public static DataTable SelectListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_code_config {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_code_config";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_code_config {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
        public static IList<go_code_configEntity> SelectListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_code_config {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_code_configEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_code_config Where id={0}", ID);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_code_config {0}", where);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
        public static bool SaveEntity(go_code_configEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_code_config(whichweek,count,restcount,sentcount,amount,discount,rebate,rebatemax,state,starttime,endtime,sentraisetime,raisetime,timespans2,addtime,senttime,overtime,timespans,use_range,r_type,d_type,title,remark)values(@whichweek,@count,@restcount,@sentcount,@amount,@discount,@rebate,@rebatemax,@state,@starttime,@endtime,@sentraisetime,@raisetime,@timespans2,@addtime,@senttime,@overtime,@timespans,@use_range,@r_type,@d_type,@title,@remark)" :
				"Update go_code_config Set whichweek=@whichweek,count=@count,restcount=@restcount,sentcount=@sentcount,amount=@amount,discount=@discount,rebate=@rebate,rebatemax=@rebatemax,state=@state,starttime=@starttime,endtime=@endtime,sentraisetime=@sentraisetime,raisetime=@raisetime,timespans2=@timespans2,addtime=@addtime,senttime=@senttime,overtime=@overtime,timespans=@timespans,use_range=@use_range,r_type=@r_type,d_type=@d_type,title=@title,remark=@remark Where id=@id";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@id",entity.Id),
					new SqlParameter("@whichweek",entity.Whichweek),
					new SqlParameter("@count",entity.Count),
					new SqlParameter("@restcount",entity.Restcount),
					new SqlParameter("@sentcount",entity.Sentcount),
					new SqlParameter("@amount",entity.Amount),
					new SqlParameter("@discount",entity.Discount),
					new SqlParameter("@rebate",entity.Rebate),
					new SqlParameter("@rebatemax",entity.Rebatemax),
					new SqlParameter("@state",entity.State),
					(entity.Starttime==null || entity.Starttime==DateTime.MinValue)?new SqlParameter("@starttime",DBNull.Value):new SqlParameter("@starttime",entity.Starttime),
					(entity.Endtime==null || entity.Endtime==DateTime.MinValue)?new SqlParameter("@endtime",DBNull.Value):new SqlParameter("@endtime",entity.Endtime),
					(entity.Sentraisetime==null || entity.Sentraisetime==DateTime.MinValue)?new SqlParameter("@sentraisetime",DBNull.Value):new SqlParameter("@sentraisetime",entity.Sentraisetime),
					(entity.Raisetime==null || entity.Raisetime==DateTime.MinValue)?new SqlParameter("@raisetime",DBNull.Value):new SqlParameter("@raisetime",entity.Raisetime),
					(entity.Timespans2==null || entity.Timespans2==DateTime.MinValue)?new SqlParameter("@timespans2",DBNull.Value):new SqlParameter("@timespans2",entity.Timespans2),
					(entity.Addtime==null || entity.Addtime==DateTime.MinValue)?new SqlParameter("@addtime",DBNull.Value):new SqlParameter("@addtime",entity.Addtime),
					(entity.Senttime==null || entity.Senttime==DateTime.MinValue)?new SqlParameter("@senttime",DBNull.Value):new SqlParameter("@senttime",entity.Senttime),
					(entity.Overtime==null || entity.Overtime==DateTime.MinValue)?new SqlParameter("@overtime",DBNull.Value):new SqlParameter("@overtime",entity.Overtime),
					(entity.Timespans==null)?new SqlParameter("@timespans",DBNull.Value):new SqlParameter("@timespans",entity.Timespans),
					(entity.Use_range==null)?new SqlParameter("@use_range",DBNull.Value):new SqlParameter("@use_range",entity.Use_range),
					new SqlParameter("@r_type",entity.R_type),
					new SqlParameter("@d_type",entity.D_type),
					(entity.Title==null)?new SqlParameter("@title",DBNull.Value):new SqlParameter("@title",entity.Title),
					(entity.Remark==null)?new SqlParameter("@remark",DBNull.Value):new SqlParameter("@remark",entity.Remark),
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
