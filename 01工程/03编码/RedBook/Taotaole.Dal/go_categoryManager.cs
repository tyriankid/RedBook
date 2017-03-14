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
	/// 商品分类-数据库操作类
	/// </summary>
	public class go_categoryManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_category Where cateid={0}", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_category";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["cateid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
        public static go_categoryEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_category Where cateid={0}", ID);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_categoryEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
        public static DataTable SelectListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_category {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_category";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["cateid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_category {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
        public static IList<go_categoryEntity> SelectListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_category {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_categoryEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_category Where cateid={0}", ID);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_category {0}", where);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
        public static bool SaveEntity(go_categoryEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_category(parentid,info,channel,model,orders,name,catdir,pic_url,url)values(@parentid,@info,@channel,@model,@orders,@name,@catdir,@pic_url,@url)" :
				"Update go_category Set parentid=@parentid,info=@info,channel=@channel,model=@model,orders=@orders,name=@name,catdir=@catdir,pic_url=@pic_url,url=@url Where cateid=@cateid";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@parentid",entity.Parentid),
					new SqlParameter("@cateid",entity.Cateid),
					(entity.Info==null)?new SqlParameter("@info",DBNull.Value):new SqlParameter("@info",entity.Info),
					new SqlParameter("@channel",entity.Channel),
					new SqlParameter("@model",entity.Model),
					new SqlParameter("@orders",entity.Orders),
					(entity.Name==null)?new SqlParameter("@name",DBNull.Value):new SqlParameter("@name",entity.Name),
					(entity.Catdir==null)?new SqlParameter("@catdir",DBNull.Value):new SqlParameter("@catdir",entity.Catdir),
					(entity.Pic_url==null)?new SqlParameter("@pic_url",DBNull.Value):new SqlParameter("@pic_url",entity.Pic_url),
					(entity.Url==null)?new SqlParameter("@url",DBNull.Value):new SqlParameter("@url",entity.Url),
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
