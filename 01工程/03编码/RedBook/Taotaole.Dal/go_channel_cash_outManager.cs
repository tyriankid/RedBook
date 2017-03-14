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
	/// 推广提现明细-数据库操作类
	/// </summary>
	public class go_channel_cash_outManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_channel_cash_out Where rid={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_channel_cash_out";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["rid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_channel_cash_outEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_channel_cash_out Where rid={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_channel_cash_outEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_channel_cash_out {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_channel_cash_out";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["rid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_channel_cash_out {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_channel_cash_outEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_channel_cash_out {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_channel_cash_outEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_channel_cash_out Where rid={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_channel_cash_out {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_channel_cash_outEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_channel_cash_out(uid,reviewtime,money,auditstatus,procefees,username,bankname,branch,time,banknumber,linkphone,reason)values(@uid,@reviewtime,@money,@auditstatus,@procefees,@username,@bankname,@branch,@time,@banknumber,@linkphone,@reason)" :
				"Update go_channel_cash_out Set uid=@uid,reviewtime=@reviewtime,money=@money,auditstatus=@auditstatus,procefees=@procefees,username=@username,bankname=@bankname,branch=@branch,time=@time,banknumber=@banknumber,linkphone=@linkphone,reason=@reason Where rid=@rid";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@rid",entity.Rid),
					new SqlParameter("@uid",entity.Uid),
					(entity.Reviewtime==null || entity.Reviewtime==DateTime.MinValue)?new SqlParameter("@reviewtime",DBNull.Value):new SqlParameter("@reviewtime",entity.Reviewtime),
					new SqlParameter("@money",entity.Money),
					new SqlParameter("@auditstatus",entity.Auditstatus),
					new SqlParameter("@procefees",entity.Procefees),
					(entity.Username==null)?new SqlParameter("@username",DBNull.Value):new SqlParameter("@username",entity.Username),
					(entity.Bankname==null)?new SqlParameter("@bankname",DBNull.Value):new SqlParameter("@bankname",entity.Bankname),
					(entity.Branch==null)?new SqlParameter("@branch",DBNull.Value):new SqlParameter("@branch",entity.Branch),
					(entity.Time==null)?new SqlParameter("@time",DBNull.Value):new SqlParameter("@time",entity.Time),
					(entity.Banknumber==null)?new SqlParameter("@banknumber",DBNull.Value):new SqlParameter("@banknumber",entity.Banknumber),
					(entity.Linkphone==null)?new SqlParameter("@linkphone",DBNull.Value):new SqlParameter("@linkphone",entity.Linkphone),
					(entity.Reason==null)?new SqlParameter("@reason",DBNull.Value):new SqlParameter("@reason",entity.Reason),
				};
				DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(execSql, para);
				return true;
			}
			catch
			{
				return false;
			}
		}

        /// <summary>
        /// 获取所有给服务商体现金额
        /// </summary>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectAllcashoutmoney(string from, string where=null,DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
        
            string selectSql = "select sum(money) from  " + from;
            selectSql += " where ";
            if (where != null)
            {
                selectSql +=  where;
            }
            selectSql += "  and auditstatus=1 ";
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }



	}
}
