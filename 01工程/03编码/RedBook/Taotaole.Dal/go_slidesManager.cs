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
	public class go_slidesManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_slides Where slideid={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_slides";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["slideid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_slidesEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_slides Where slideid={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_slidesEntity>(reader);
			}
		}
      
		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_slides {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_slides";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["slideid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_slides {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_slidesEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_slides {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_slidesEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_slides Where slideid={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_slides {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}



        /// <summary>
        /// 修改数据
        /// </summary>
        public static bool UpdateEntity(go_slidesEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            try
            {

                string updateSql = @"Update go_slides Set 
                                     slideorder=@slideorder
                                     Where Slideid=@slideid";
                SqlParameter[] para = new SqlParameter[]
				{
                    new SqlParameter("@slideorder",entity.Slideorder),
					
                    new SqlParameter("@slideid",entity.Slideid),

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
		public static bool SaveEntity(go_slidesEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_slides(slidetype,slidestate,slideorder,activityid,slidelastupdatetime,slidelink,slideimg)values(@slidetype,@slidestate,@slideorder,@activityid,@slidelastupdatetime,@slidelink,@slideimg)" :
				"Update go_slides Set slidetype=@slidetype,slidestate=@slidestate,slideorder=@slideorder,activityid=@activityid,slidelastupdatetime=@slidelastupdatetime,slidelink=@slidelink,slideimg=@slideimg Where slideid=@slideid";
				SqlParameter[] para = new SqlParameter[]
				{
					(entity.Slidetype==null)?new SqlParameter("@slidetype",DBNull.Value):new SqlParameter("@slidetype",entity.Slidetype),
					(entity.Slidestate==null)?new SqlParameter("@slidestate",DBNull.Value):new SqlParameter("@slidestate",entity.Slidestate),
					new SqlParameter("@slideid",entity.Slideid),
					new SqlParameter("@slideorder",entity.Slideorder),
					new SqlParameter("@activityid",entity.Activityid),
					(entity.Slidelastupdatetime==null || entity.Slidelastupdatetime==DateTime.MinValue)?new SqlParameter("@slidelastupdatetime",DBNull.Value):new SqlParameter("@slidelastupdatetime",entity.Slidelastupdatetime),
					(entity.Slidelink==null)?new SqlParameter("@slidelink",DBNull.Value):new SqlParameter("@slidelink",entity.Slidelink),
					(entity.Slideimg==null)?new SqlParameter("@slideimg",DBNull.Value):new SqlParameter("@slideimg",entity.Slideimg),
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
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable getapp(int id, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {

            string selectSql = @" select a.activityid,a.slidelink,a.slideimg  ,b.title  from
                       go_slides a left join go_activity b on a.activityid=b.id  where  a.slideid=" + id;
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_slides";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["slideid"] };
            return ds.Tables[0];
        }

          /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable getapplist(int slidetype, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {

            string selectSql = @" select b.id,a.slideid,a.slideimg  ,b.title ,b.timespans from
                       go_slides a left join go_activity b on a.activityid=b.id  where  a.slidetype=" + slidetype;
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_slides";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["slideid"] };
            return ds.Tables[0];
        }

        
	}
}
