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
	/// 用户地址-数据库操作类
	/// </summary>
	public class go_member_dizhiManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_member_dizhi Where id={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_member_dizhi";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_member_dizhiEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_member_dizhi Where id={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_member_dizhiEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_member_dizhi {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_member_dizhi";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_member_dizhi {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_member_dizhiEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_member_dizhi {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_member_dizhiEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_member_dizhi Where id={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_member_dizhi {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_member_dizhiEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_member_dizhi([shifoufahuo],[uid],[youbian],[time],[sheng],[shi],[xian],[jiedao],[shouhuoren],[mobile],[tell],[isdefault],[qq])values(@shifoufahuo,@uid,@youbian,@time,@sheng,@shi,@xian,@jiedao,@shouhuoren,@mobile,@tell,@default,@qq)" :
                "Update go_member_dizhi Set [shifoufahuo]=@shifoufahuo,uid=@uid,youbian=@youbian,[time]=@time,sheng=@sheng,shi=@shi,xian=@xian,jiedao=@jiedao,shouhuoren=@shouhuoren,mobile=@mobile,tell=@tell,[isdefault]=@default,[qq]=@qq Where id=@id";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@shifoufahuo",entity.Shifoufahuo),
					new SqlParameter("@id",entity.Id),
					new SqlParameter("@uid",entity.Uid),
					new SqlParameter("@youbian",entity.Youbian),
					(entity.Time==null || entity.Time==DateTime.MinValue)?new SqlParameter("@time",DBNull.Value):new SqlParameter("@time",entity.Time),
					(entity.Sheng==null)?new SqlParameter("@sheng",DBNull.Value):new SqlParameter("@sheng",entity.Sheng),
					(entity.Shi==null)?new SqlParameter("@shi",DBNull.Value):new SqlParameter("@shi",entity.Shi),
					(entity.Xian==null)?new SqlParameter("@xian",DBNull.Value):new SqlParameter("@xian",entity.Xian),
					(entity.Jiedao==null)?new SqlParameter("@jiedao",DBNull.Value):new SqlParameter("@jiedao",entity.Jiedao),
					(entity.Shouhuoren==null)?new SqlParameter("@shouhuoren",DBNull.Value):new SqlParameter("@shouhuoren",entity.Shouhuoren),
					(entity.Mobile==null)?new SqlParameter("@mobile",DBNull.Value):new SqlParameter("@mobile",entity.Mobile),
					(entity.Tell==null)?new SqlParameter("@tell",DBNull.Value):new SqlParameter("@tell",entity.Tell),
					(entity.isDefault==null)?new SqlParameter("@default",DBNull.Value):new SqlParameter("@default",entity.isDefault),
					(entity.Qq==null)?new SqlParameter("@qq",DBNull.Value):new SqlParameter("@qq",entity.Qq),
				};
				DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(execSql, para);
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

	}
}
