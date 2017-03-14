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
	/// 订单-数据库操作类
	/// </summary>
	public class go_ordersManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
        public static DataTable LoadData(string ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_orders Where orderId='{0}'", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_orders";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["orderId"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_ordersEntity LoadEntity(string ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_orders Where orderId='{0}'", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_ordersEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_orders {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_orders";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["orderId"] };
			return ds.Tables[0];
		}
        /// <summary>
        /// 根据条件查直购订单
        /// </summary>
        /// <param name="where"></param>
        /// <param name="selectFields"></param>
        /// <param name="orderby"></param>
        /// <param name="top"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static DataTable SelectZhiData(string where = null, string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"select PD.title,PD.thumb,OD.*from   go_orders  OD  left  join go_zhigou gz on od.businessid = gz.quanid left join go_products     PD  on  gz.productid=PD.productid {0}", where, top == 0 ? "" : "top " + top);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_orders";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["orderId"] };
            return ds.Tables[0];
        }


		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_orders {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_ordersEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_orders {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_ordersEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_orders Where orderId='{0}'", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_orders {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_ordersEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_orders(iswon,card_use_type,uid,businessId,quantity,money,time,sendtime,ordertype,status,express_company,express_code,order_address_info,phone,goucode,pay_type,ip)values(@iswon,@card_use_type,@uid,@businessId,@quantity,@money,@time,@sendtime,@ordertype,@status,@express_company,@express_code,@order_address_info,@phone,@goucode,@pay_type,@ip)" :
				"Update go_orders Set iswon=@iswon,card_use_type=@card_use_type,uid=@uid,businessId=@businessId,quantity=@quantity,money=@money,time=@time,sendtime=@sendtime,ordertype=@ordertype,status=@status,express_company=@express_company,express_code=@express_code,order_address_info=@order_address_info,phone=@phone,goucode=@goucode,pay_type=@pay_type,ip=@ip Where orderId=@orderId";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@iswon",entity.Iswon),
					new SqlParameter("@card_use_type",entity.Card_use_type),
					new SqlParameter("@uid",entity.Uid),
					new SqlParameter("@businessId",entity.BusinessId),
					new SqlParameter("@quantity",entity.Quantity),
					new SqlParameter("@money",entity.Money),
					(entity.Time==null || entity.Time==DateTime.MinValue)?new SqlParameter("@time",DBNull.Value):new SqlParameter("@time",entity.Time),
					(entity.Sendtime==null || entity.Sendtime==DateTime.MinValue)?new SqlParameter("@sendtime",DBNull.Value):new SqlParameter("@sendtime",entity.Sendtime),
					(entity.Ordertype==null)?new SqlParameter("@ordertype",DBNull.Value):new SqlParameter("@ordertype",entity.Ordertype),
					(entity.Status==null)?new SqlParameter("@status",DBNull.Value):new SqlParameter("@status",entity.Status),
					(entity.Express_company==null)?new SqlParameter("@express_company",DBNull.Value):new SqlParameter("@express_company",entity.Express_company),
					(entity.Express_code==null)?new SqlParameter("@express_code",DBNull.Value):new SqlParameter("@express_code",entity.Express_code),
					(entity.Order_address_info==null)?new SqlParameter("@order_address_info",DBNull.Value):new SqlParameter("@order_address_info",entity.Order_address_info),
					(entity.Phone==null)?new SqlParameter("@phone",DBNull.Value):new SqlParameter("@phone",entity.Phone),
					(entity.OrderId==null)?new SqlParameter("@orderId",DBNull.Value):new SqlParameter("@orderId",entity.OrderId),
					(entity.Goucode==null)?new SqlParameter("@goucode",DBNull.Value):new SqlParameter("@goucode",entity.Goucode),
					(entity.Pay_type==null)?new SqlParameter("@pay_type",DBNull.Value):new SqlParameter("@pay_type",entity.Pay_type),
					(entity.Ip==null)?new SqlParameter("@ip",DBNull.Value):new SqlParameter("@ip",entity.Ip),
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
        /// 根据主键查询数据集(一元购)
        /// </summary>
        public static DataTable LoadDataYuan(string ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = string.Format(@"Select T.title,GM.username,PD.productid,t.qishu,t.maxqishu,GM.username,t.zongrenshu,PD.money as  pdmoney,GM.email,O.* From go_orders O inner Join go_member GM on o.uid=Gm.uid Inner Join  (Select ordertype='yuan',yId as businessid,title,productid,qishu,maxqishu,zongrenshu From go_yiyuan union all Select ordertype='tuan',tl.tuanlistId as businessid,t.title,t.productid,t.end_time,T.start_time,t.prize_num From go_tuan_listinfo tl Inner Join go_tuan t on tl.tuanId=t.tId  )T ON O.ordertype=T.ordertype And O.businessId=T.businessid  Inner Join go_products PD  on  PD.productid=t.productid WHERE O.ordertype ='yuan' and orderId='{0}'", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_orders";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["orderId"] };
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据主键查询数据集(团购)
        /// </summary>
        public static DataTable LoadDataTuan(string ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = string.Format(@"Select T.title as ProductName,T.end_time,T.start_time,T.total_num,T.per_price,GM.username,PD.money as  pdmoney,GM.email,  O.* From go_orders O inner Join go_member GM on o.uid=Gm.uid Inner Join  (Select ordertype='tuan',tl.tuanlistId as businessid,title,productid,end_time,start_time,T.total_num,per_price From go_tuan_listinfo tl Inner Join go_tuan t on tl.tuanId=t.tId  union all Select ordertype='yuan',yId as businessid,title,productid,qishu,maxqishu,zongrenshu,q_uid From go_yiyuan)T ON O.ordertype=T.ordertype And O.businessId=T.businessid  Inner Join go_products PD  on  PD.productid=t.productid WHERE O.ordertype ='tuan' AND orderId='{0}'", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_orders";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["orderId"] };
            return ds.Tables[0];
        }
        public static DataTable LoadAllSend(string ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = string.Format(@"Select O.card_use_type as  typeid,PD.thumb,T.title,GM.typeid as mtypeid,GM.username,PD.productid,O.* From go_orders O left Join go_member GM on o.uid=Gm.uid left Join  (Select ordertype='yuan',yId as businessid,title,productid From go_yiyuan union all Select ordertype='tuan',tl.tuanlistId as businessid,t.title,t.productid From go_tuan_listinfo tl left Join go_tuan t on tl.tuanId=t.tId  )T ON O.ordertype=T.ordertype And O.businessId=T.businessid  left Join go_products PD  on  PD.productid=t.productid  where iswon=1 and O.status='待发货'  and  orderId  in ({0})", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_orders";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["orderId"] };
            return ds.Tables[0];
        }
        

	}
}
