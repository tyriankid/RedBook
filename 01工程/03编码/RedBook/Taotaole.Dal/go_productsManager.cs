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
	/// 商品基本信息-数据库操作类
	/// </summary>
	public class go_productsManager
	{
		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_products Where productid={0}", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_products";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["productid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
        public static go_productsEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_products Where productid={0}", ID);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_productsEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
        public static DataTable SelectListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_products {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_products";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["productid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_products {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
        public static IList<go_productsEntity> SelectListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_products {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_productsEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_products Where productid={0}", ID);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_products {0}", where);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
        public static bool SaveEntity(go_productsEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
                string execSql = (isAdd) ?
                "Insert Into go_products(renqi,pos,status,categoryid,brandid,typeid,stock,operation,money,time,title,title2,keywords,description,thumb,picarr,contents,number,baokuan,tejia,youhui,newhand)values(@renqi,@pos,@status,@categoryid,@brandid,@typeid,@stock,@operation,@money,@time,@title,@title2,@keywords,@description,@thumb,@picarr,@contents,@number,@baokuan,@tejia,@youhui,@newhand)" :
                "Update go_products Set renqi=@renqi,pos=@pos,status=@status,categoryid=@categoryid,brandid=@brandid,typeid=@typeid,stock=@stock,operation=@operation,money=@money,time=@time,title=@title,title2=@title2,keywords=@keywords,description=@description,thumb=@thumb,picarr=@picarr,contents=@contents,number=@number,baokuan=@baokuan,tejia=@tejia,youhui=@youhui,newhand=@newhand  Where productid=@productid";
                SqlParameter[] para = new SqlParameter[]
                {
                new SqlParameter("@renqi",entity.Renqi),
                new SqlParameter("@pos",entity.Pos),
                new SqlParameter("@baokuan",entity.Baokuan),
                new SqlParameter("@tejia",entity.Tejia),
                new SqlParameter("@newhand",entity.NewHand),
                new SqlParameter("@youhui",entity.Youhui),
                new SqlParameter("@status",entity.Status),
                new SqlParameter("@productid",entity.Productid),
                new SqlParameter("@categoryid",entity.Categoryid),
                new SqlParameter("@brandid",entity.Brandid),
                new SqlParameter("@typeid",entity.Typeid),
                new SqlParameter("@stock",entity.Stock),
                new SqlParameter("@operation",entity.Operation),
                new SqlParameter("@money",entity.Money),
                (entity.Time==null || entity.Time==DateTime.MinValue)?new SqlParameter("@time",DBNull.Value):new SqlParameter("@time",entity.Time),
                (entity.Title==null)?new SqlParameter("@title",DBNull.Value):new SqlParameter("@title",entity.Title),
                (entity.Title2==null)?new SqlParameter("@title2",DBNull.Value):new SqlParameter("@title2",entity.Title2),
                (entity.Keywords==null)?new SqlParameter("@keywords",DBNull.Value):new SqlParameter("@keywords",entity.Keywords),
                (entity.Description==null)?new SqlParameter("@description",DBNull.Value):new SqlParameter("@description",entity.Description),
                (entity.Thumb==null)?new SqlParameter("@thumb",DBNull.Value):new SqlParameter("@thumb",entity.Thumb),
                (entity.Picarr==null)?new SqlParameter("@picarr",DBNull.Value):new SqlParameter("@picarr",entity.Picarr),
                (entity.Contents==null)?new SqlParameter("@contents",DBNull.Value):new SqlParameter("@contents",entity.Contents),
                (entity.Number==null)?new SqlParameter("@number",DBNull.Value):new SqlParameter("@number",entity.Number),
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
