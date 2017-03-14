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
	public class go_articleManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_article Where id={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_article";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_articleEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_article Where id={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_articleEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_article {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_article";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_article {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_articleEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_article {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_articleEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_article Where id={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_article {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}


        /// <summary>
        /// 修改数据
        /// </summary>
        public static bool UpdateEntity(go_articleEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            try
            {
              
                string updateSql = "Update go_article Set ordersn=@ordersn Where id=@id";
                SqlParameter[] para = new SqlParameter[]
				{
                    new SqlParameter("@ordersn",entity.Ordersn),
					new SqlParameter("@id",entity.Id),
				};
                DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(updateSql, para);
                return true;
            }
            catch
            {
                return false;
            }
        }
		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_articleEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_article(hit,ordersn,posttime,cateid,author,title,title_style,thumb,picarr,keywords,description,contents)values(@hit,@ordersn,@posttime,@cateid,@author,@title,@title_style,@thumb,@picarr,@keywords,@description,@contents)" :
				"Update go_article Set hit=@hit,ordersn=@ordersn,posttime=@posttime,cateid=@cateid,author=@author,title=@title,title_style=@title_style,thumb=@thumb,picarr=@picarr,keywords=@keywords,description=@description,contents=@contents Where id=@id";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@id",entity.Id),
					new SqlParameter("@hit",entity.Hit),
					new SqlParameter("@ordersn",entity.Ordersn),
					(entity.Posttime==null || entity.Posttime==DateTime.MinValue)?new SqlParameter("@posttime",DBNull.Value):new SqlParameter("@posttime",entity.Posttime),
					(entity.Cateid==null)?new SqlParameter("@cateid",DBNull.Value):new SqlParameter("@cateid",entity.Cateid),
					(entity.Author==null)?new SqlParameter("@author",DBNull.Value):new SqlParameter("@author",entity.Author),
					(entity.Title==null)?new SqlParameter("@title",DBNull.Value):new SqlParameter("@title",entity.Title),
					(entity.Title_style==null)?new SqlParameter("@title_style",DBNull.Value):new SqlParameter("@title_style",entity.Title_style),
					(entity.Thumb==null)?new SqlParameter("@thumb",DBNull.Value):new SqlParameter("@thumb",entity.Thumb),
					(entity.Picarr==null)?new SqlParameter("@picarr",DBNull.Value):new SqlParameter("@picarr",entity.Picarr),
					(entity.Keywords==null)?new SqlParameter("@keywords",DBNull.Value):new SqlParameter("@keywords",entity.Keywords),
					(entity.Description==null)?new SqlParameter("@description",DBNull.Value):new SqlParameter("@description",entity.Description),
					(entity.Contents==null)?new SqlParameter("@contents",DBNull.Value):new SqlParameter("@contents",entity.Contents),
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
