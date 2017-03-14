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
	/// 团购商品-数据库操作类
	/// </summary>
	public class go_tuanManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_tuan Where tId={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_tuan";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["tId"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_tuanEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_tuan Where tId={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_tuanEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_tuan {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_tuan";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["tId"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_tuan {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_tuanEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_tuan {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_tuanEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_tuan Where tId={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_tuan {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_tuanEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_tuan(is_delete,status,productid,total_num,max_sell,sort,reachtuan_num,sell_num,per_price,lose_time,start_time,end_time,deadline,time,title,prize_num)values(@is_delete,@status,@productid,@total_num,@max_sell,@sort,@reachtuan_num,@sell_num,@per_price,@lose_time,@start_time,@end_time,@deadline,@time,@title,@prize_num)" :
				"Update go_tuan Set is_delete=@is_delete,status=@status,productid=@productid,total_num=@total_num,max_sell=@max_sell,sort=@sort,reachtuan_num=@reachtuan_num,sell_num=@sell_num,per_price=@per_price,lose_time=@lose_time,start_time=@start_time,end_time=@end_time,deadline=@deadline,time=@time,title=@title,prize_num=@prize_num Where tId=@tId";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@is_delete",entity.Is_delete),
					new SqlParameter("@status",entity.Status),
					new SqlParameter("@tId",entity.TId),
					new SqlParameter("@productid",entity.Productid),
					new SqlParameter("@total_num",entity.Total_num),
					new SqlParameter("@max_sell",entity.Max_sell),
					new SqlParameter("@sort",entity.Sort),
					new SqlParameter("@reachtuan_num",entity.Reachtuan_num),
                    new SqlParameter("@prize_num",entity.Prize_num),
					new SqlParameter("@sell_num",entity.Sell_num),
					new SqlParameter("@per_price",entity.Per_price),
					(entity.Lose_time==null || entity.Lose_time==DateTime.MinValue)?new SqlParameter("@lose_time",DBNull.Value):new SqlParameter("@lose_time",entity.Lose_time),
					(entity.Start_time==null || entity.Start_time==DateTime.MinValue)?new SqlParameter("@start_time",DBNull.Value):new SqlParameter("@start_time",entity.Start_time),
					(entity.End_time==null || entity.End_time==DateTime.MinValue)?new SqlParameter("@end_time",DBNull.Value):new SqlParameter("@end_time",entity.End_time),
					(entity.Deadline==null || entity.Deadline==DateTime.MinValue)?new SqlParameter("@deadline",DBNull.Value):new SqlParameter("@deadline",entity.Deadline),
					(entity.Time==null || entity.Time==DateTime.MinValue)?new SqlParameter("@time",DBNull.Value):new SqlParameter("@time",entity.Time),
					(entity.Title==null)?new SqlParameter("@title",DBNull.Value):new SqlParameter("@title",entity.Title),
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
        /// 修改数据
        /// </summary>
        public static bool UpdateEntity(go_tuanEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            try
            {
                string updateSql = "Update go_tuan Set  sort=@sort ,title=@title,is_delete=@is_delete,total_num=@total_num,per_price=@per_price,max_sell=@max_sell,start_time=@start_time,end_time=@end_time  Where tId=@tId";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@tId",entity.TId),
					new SqlParameter("@title",entity.Title),
					new SqlParameter("@sort",entity.Sort),
                    new SqlParameter("@is_delete",entity.Is_delete),
                    new SqlParameter("@total_num",entity.Total_num),
                    new SqlParameter("@per_price",entity.Per_price),
                    new SqlParameter("@max_sell",entity.Max_sell),
                    new SqlParameter("@start_time",entity.Start_time),
                    new SqlParameter("@end_time",entity.End_time),
				};
                DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(updateSql, para);
                return true;
            }
            catch
            {
                return false;
            }
        }

	}
}
