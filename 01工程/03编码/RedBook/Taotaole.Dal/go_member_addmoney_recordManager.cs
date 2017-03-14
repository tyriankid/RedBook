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
	/// 用户充值记录-数据库操作类
	/// </summary>
	public class go_member_addmoney_recordManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_member_addmoney_record Where id={0}", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_member_addmoney_record";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
        public static go_member_addmoney_recordEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_member_addmoney_record Where id={0}", ID);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_member_addmoney_recordEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
        public static DataTable SelectListData(string where = null, string selectFields = "*", DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_member_addmoney_record {0}", where, selectFields);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_member_addmoney_record";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_member_addmoney_record {0}", where, selectFields);
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
        public static IList<go_member_addmoney_recordEntity> SelectListEntity(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select * From go_member_addmoney_record {0}", where);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_member_addmoney_recordEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_member_addmoney_record Where id={0}", ID);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_member_addmoney_record {0}", where);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
        public static bool SaveEntity(go_member_addmoney_recordEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				string execSql = (isAdd) ?
				"Insert Into go_member_addmoney_record(tuanid,tuanlistid,time,uid,money,score,buy_type,code,pay_type,status,memo)values(@tuanid,@tuanlistid,@time,@uid,@money,@score,@buy_type,@code,@pay_type,@status,@memo)" :
				"Update go_member_addmoney_record Set tuanid=@tuanid,tuanlistid=@tuanlistid,time=@time,uid=@uid,money=@money,score=@score,buy_type=@buy_type,code=@code,pay_type=@pay_type,status=@status,memo=@memo Where id=@id";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@id",entity.Id),
					new SqlParameter("@tuanid",entity.Tuanid),
					new SqlParameter("@tuanlistid",entity.Tuanlistid),
					new SqlParameter("@time",entity.Time),
					new SqlParameter("@uid",entity.Uid),
					new SqlParameter("@money",entity.Money),
					new SqlParameter("@score",entity.Score),
					new SqlParameter("@buy_type",entity.Buy_type),
					new SqlParameter("@code",entity.Code),
					new SqlParameter("@pay_type",entity.Pay_type),
					new SqlParameter("@status",entity.Status),
                    new SqlParameter("@memo",entity.Memo)
				};
                DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(execSql, para);
				return true;
		}

        public static void SaveStatus(string code, string status, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string updateSql = "update go_member_addmoney_record Set status='{0}' Where score=1 And code='{1}'";
            updateSql = string.Format(updateSql, status,code.Replace("'",""));
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(updateSql);
        }

	}
}
