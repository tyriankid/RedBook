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
	public class go_card_rechargeManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_card_recharge Where id={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_card_recharge";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_card_rechargeEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_card_recharge Where id={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_card_rechargeEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_card_recharge {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_card_recharge";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_card_recharge {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_card_rechargeEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_card_recharge {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_card_rechargeEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_card_recharge Where id={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_card_recharge {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_card_rechargeEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_card_recharge(isrepeat,usetype,time,uid,money,rechargetime,usetime,code,codepwd,orderId,usebeizhu)values(@isrepeat,@usetype,@time,@uid,@money,@rechargetime,@usetime,@code,@codepwd,@orderId,@usebeizhu)" :
				"Update go_card_recharge Set isrepeat=@isrepeat,usetype=@usetype,time=@time,uid=@uid,money=@money,rechargetime=@rechargetime,usetime=@usetime,code=@code,codepwd=@codepwd,orderId=@orderId,usebeizhu=@usebeizhu Where id=@id";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@isrepeat",entity.Isrepeat),
					new SqlParameter("@usetype",entity.Usetype),
					new SqlParameter("@id",entity.Id),
					new SqlParameter("@time",entity.Time),
					new SqlParameter("@uid",entity.Uid),
					new SqlParameter("@money",entity.Money),
					(entity.Rechargetime==null || entity.Rechargetime==DateTime.MinValue)?new SqlParameter("@rechargetime",DBNull.Value):new SqlParameter("@rechargetime",entity.Rechargetime),
					(entity.Usetime==null || entity.Usetime==DateTime.MinValue)?new SqlParameter("@usetime",DBNull.Value):new SqlParameter("@usetime",entity.Usetime),
					(entity.Code==null)?new SqlParameter("@code",DBNull.Value):new SqlParameter("@code",entity.Code),
					(entity.Codepwd==null)?new SqlParameter("@codepwd",DBNull.Value):new SqlParameter("@codepwd",entity.Codepwd),
					(entity.OrderId==null)?new SqlParameter("@orderId",DBNull.Value):new SqlParameter("@orderId",entity.OrderId),
					(entity.Usebeizhu==null)?new SqlParameter("@usebeizhu",DBNull.Value):new SqlParameter("@usebeizhu",entity.Usebeizhu),
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
