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
	/// 积分商城-数据库操作类
	/// </summary>
	public class go_giftsManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_gifts Where giftId={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_gifts";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["giftId"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_giftsEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_gifts Where giftId={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_giftsEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_gifts {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_gifts";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["giftId"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_gifts {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_giftsEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_gifts {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_giftsEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_gifts Where giftId={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_gifts {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_giftsEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
                "Insert Into go_gifts(productid,needScore,scope,probability,addtime,title,stock,prizeNumber,giftsuse,sumCode,winCode,codeCount)values(@productid,@needScore,@scope,@probability,@addtime,@title,@stock,@prizeNumber,@giftsuse,@sumCode,@winCode,@codeCount)" :
                "Update go_gifts Set productid=@productid,needScore=@needScore,scope=@scope,probability=@probability,addtime=@addtime,title=@title,stock=@stock,prizeNumber=@prizeNumber,giftsuse=@giftsuse,sumCode=@sumCode,winCode=@winCode,codeCount=@codeCount Where giftId=@giftId";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@giftId",entity.GiftId),
					new SqlParameter("@productid",entity.Productid),
					new SqlParameter("@needScore",entity.NeedScore),
					new SqlParameter("@scope",entity.Scope),
					new SqlParameter("@probability",entity.Probability),
					(entity.Addtime==null || entity.Addtime==DateTime.MinValue)?new SqlParameter("@addtime",DBNull.Value):new SqlParameter("@addtime",entity.Addtime),
					(entity.Title==null)?new SqlParameter("@title",DBNull.Value):new SqlParameter("@title",entity.Title),
					(entity.Stock==null)?new SqlParameter("@stock",DBNull.Value):new SqlParameter("@stock",entity.Stock),
					(entity.PrizeNumber==null)?new SqlParameter("@prizeNumber",DBNull.Value):new SqlParameter("@prizeNumber",entity.PrizeNumber),
					(entity.Giftsuse==null)?new SqlParameter("@giftsuse",DBNull.Value):new SqlParameter("@giftsuse",entity.Giftsuse),
                    (entity.sumCode==null)?new SqlParameter("@sumCode",DBNull.Value):new SqlParameter("@sumCode",entity.sumCode),
                    (entity.winCode==null)?new SqlParameter("@winCode",DBNull.Value):new SqlParameter("@winCode",entity.winCode),
                    (entity.codeCount==null)?new SqlParameter("@codeCount",DBNull.Value):new SqlParameter("@codeCount",entity.codeCount),
				};
				DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(execSql, para);
				return true;
			}
			catch
			{
				return false;
			}
		}

        //根据积分和游戏方式判断游戏是否开启
        public static bool isGameOpen(string score, string giftUse, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            bool isopen = true;
            string whereScore = "";
            string whereGiftUse = "";
            string where = "where 1=1";
            if (!string.IsNullOrEmpty(score))
            {
                whereScore = " and scope=" + score;
            }

            if (!string.IsNullOrEmpty(giftUse))
            {
                whereGiftUse = " and giftsuse=" +"'"+giftUse+"'";
            }
            string sql = string.Format("select productid,*from go_gifts {0}{1}{2}", where, whereScore, whereGiftUse);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(sql);
            if (ds.Tables[0].Rows.Count<1)
            {
                isopen =false;
            }
            return isopen;
        }

        /// <summary>
        /// 抽奖 跑马灯和大转盘
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="gameType"></param>
        /// <param name="minScore"></param>
        /// <param name="maxScore"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static int[] goGiftDraw(string orderid,int uid,string gameType, int minScore,int maxScore,DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            int state = -1;
            int winGiftId = -1;
            SqlParameter[] para ={
                    new SqlParameter("@orderid",SqlDbType.VarChar),  
                    new SqlParameter("@uid",SqlDbType.Int),  
                    new SqlParameter("@gameType",SqlDbType.VarChar),  
                    new SqlParameter("@minScore",SqlDbType.Int),  
                    new SqlParameter("@maxScore",SqlDbType.Int),
                    new SqlParameter("@state",SqlDbType.Int),
                    new SqlParameter("@winGiftId",SqlDbType.Int)
            };
            para[0].Value = orderid;
            para[1].Value = uid;
            para[2].Value = gameType;
            para[3].Value = minScore;
            para[4].Value = maxScore;
            para[5].Direction = ParameterDirection.Output; 
            para[6].Direction = ParameterDirection.Output; 
            using (SqlCommand command = CreateDbCommand(DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).ConnectionString, "goGiftDraw", para, CommandType.StoredProcedure))
            {
                command.Connection.Open();
                object result = command.ExecuteScalar();
                state = int.Parse(command.Parameters[5].Value.ToString());
                winGiftId = int.Parse(command.Parameters[6].Value.ToString());
                command.Connection.Close();
            }
            return new[] { state, winGiftId };
        }

        /// <summary>
        /// 刮刮乐抽奖
        /// </summary>
        /// <returns></returns>
        public static int[] goGiftGua(string orderid,int uid, int giftid, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            int state = -1;
            int isWon = -1;
            SqlParameter[] para ={  
                    new SqlParameter("@orderid",SqlDbType.VarChar),
                    new SqlParameter("@uid",SqlDbType.Int),  
                    new SqlParameter("@giftid",SqlDbType.Int),  
                    new SqlParameter("@state",SqlDbType.Int),
                    new SqlParameter("@isWon",SqlDbType.Int)
            };
            para[0].Value = orderid;
            para[1].Value = uid;
            para[2].Value = giftid;
            para[3].Direction = ParameterDirection.Output;
            para[4].Direction = ParameterDirection.Output;
            using (SqlCommand command = CreateDbCommand(DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).ConnectionString, "goGiftGua", para, CommandType.StoredProcedure))
            {
                command.Connection.Open();
                object result = command.ExecuteScalar();
                state = int.Parse(command.Parameters[3].Value.ToString());
                isWon = int.Parse(command.Parameters[4].Value.ToString());
                command.Connection.Close();
            }
            return new[] { state, isWon };
        }

        #region åå»ºä¸ä¸ªSqlCommandå¯¹è±¡
        /// <summary>
        /// åå»ºä¸ä¸ªSqlCommandå¯¹è±¡
        /// </summary>
        /// <param name="connStr">æ°æ®åºé¾æ¥å­ç¬¦ä¸²</param>   
        /// <param name="sql">è¦æ§è¡çæ¥è¯¢è¯­å¥</param>   
        /// <param name="parameters">æ§è¡SQLæ¥è¯¢è¯­å¥æéè¦çåæ°</param>
        /// <param name="commandType">æ§è¡çSQLè¯­å¥çç±»å</param>
        private static SqlCommand CreateDbCommand(string connStr, string sql, SqlParameter[] parameters, CommandType commandType)
        {
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandType = commandType;
            command.Connection = connection;
            if (!(parameters == null || parameters.Length == 0))
            {
                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }
        #endregion

        
	}

	
}
