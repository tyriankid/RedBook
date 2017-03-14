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
	/// 一元购-数据库操作类
	/// </summary>
	public class go_yiyuanManager
	{
		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_yiyuan Where yId={0}", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_yiyuan";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["yId"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
        public static go_yiyuanEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_yiyuan Where yId={0}", ID);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_yiyuanEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
        public static DataTable SelectListData(string where = null, string selectFields = "*", DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_yiyuan {0}", where, selectFields);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_yiyuan";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["yId"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {1} From go_yiyuan {0}", where, selectFields);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " order by " + orderby;
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
        }

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
        public static IList<go_yiyuanEntity> SelectListEntity(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select * From go_yiyuan {0}", where);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_yiyuanEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_yiyuan Where yId={0}", ID);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_yiyuan {0}", where);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}
        /// <summary>
        /// 增加yiyuan商品数据后返回id
        /// </summary>
        public static int AddEntityId(go_yiyuanEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string execSql = "Insert Into go_yiyuan(productid,zongrenshu,canyurenshu,shengyurenshu,qishu,maxqishu,hadConfirm,yunjiage,zongjiage,time,title,pricerange,orders,recover,originalid)values(@productid,@zongrenshu,@canyurenshu,@shengyurenshu,@qishu,@maxqishu,@hadConfirm,@yunjiage,@zongjiage,@time,@title,@pricerange,@order,@recover,@originalid);select @@identity";
            SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@productid",entity.Productid),
					new SqlParameter("@zongrenshu",entity.Zongrenshu),
					new SqlParameter("@canyurenshu",entity.Canyurenshu),
					new SqlParameter("@shengyurenshu",entity.Shengyurenshu),
					new SqlParameter("@qishu",entity.Qishu),
					new SqlParameter("@maxqishu",entity.Maxqishu),
					new SqlParameter("@hadConfirm",entity.HadConfirm),
					new SqlParameter("@yunjiage",entity.Yunjiage),
					new SqlParameter("@time",entity.Time),
					new SqlParameter("@title",entity.Title),
					new SqlParameter("@pricerange",entity.Pricerange),
                    new SqlParameter("@zongjiage",entity.Zongjiage),
                    new SqlParameter("@order",entity.Orders),
                    new SqlParameter("@recover",entity.recover),
                    new SqlParameter("@originalid",entity.originalid),
                    
				};
            return Convert.ToInt32(DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(execSql, para).ToString());
        }

        public static void UpdateCodetable(int yid, string codetable, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string deleteSql = string.Format(@"Update go_yiyuan set codetable = '{0}' From go_yiyuan where yid = {1}", codetable,yid);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        public static bool UpdateEntity(go_yiyuanEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            try
            {
                string updateSql = "Update go_yiyuan Set  maxqishu=@maxqishu ,title=@title,pricerange=@pricerange,orders=@order,recover=@recover Where yId=@yId";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@yId",entity.YId),
					new SqlParameter("@maxqishu",entity.Maxqishu),
					new SqlParameter("@title",entity.Title),
					new SqlParameter("@pricerange",entity.Pricerange),
                    new SqlParameter("@order",entity.Orders),
                    new SqlParameter("@recover",entity.recover)
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
        public static bool SaveEntity(go_yiyuanEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
                "Insert Into go_yiyuan(productid,zongrenshu,canyurenshu,shengyurenshu,qishu,maxqishu,q_uid,zhiding,hadConfirm,yunjiage,time,title,q_code,q_counttime,pricerange,q_end_time,codetable,orders)values(@productid,@zongrenshu,@canyurenshu,@shengyurenshu,@qishu,@maxqishu,@q_uid,@zhiding,@hadConfirm,@yunjiage,@time,@title,@q_code,@q_counttime,@pricerange,@q_end_time,@codetable,@order)" :
                "Update go_yiyuan Set productid=@productid,zongrenshu=@zongrenshu,canyurenshu=@canyurenshu,shengyurenshu=@shengyurenshu,qishu=@qishu,maxqishu=@maxqishu,q_uid=@q_uid,zhiding=@zhiding,hadConfirm=@hadConfirm,yunjiage=@yunjiage,time=@time,title=@title,q_code=@q_code,q_counttime=@q_counttime,pricerange=@pricerange,q_end_time=@q_end_time,orders=@order Where yId=@yId";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@yId",entity.YId),
					new SqlParameter("@productid",entity.Productid),
					new SqlParameter("@zongrenshu",entity.Zongrenshu),
					new SqlParameter("@canyurenshu",entity.Canyurenshu),
					new SqlParameter("@shengyurenshu",entity.Shengyurenshu),
					new SqlParameter("@qishu",entity.Qishu),
					new SqlParameter("@maxqishu",entity.Maxqishu),
					new SqlParameter("@q_uid",entity.Q_uid),
					new SqlParameter("@zhiding",entity.Zhiding),
					new SqlParameter("@hadConfirm",entity.HadConfirm),
					new SqlParameter("@yunjiage",entity.Yunjiage),
					new SqlParameter("@time",entity.Time),
					new SqlParameter("@title",entity.Title),
					new SqlParameter("@q_code",entity.Q_code),
					new SqlParameter("@q_counttime",entity.Q_counttime),
					new SqlParameter("@pricerange",entity.Pricerange),
					new SqlParameter("@q_end_time",entity.Q_end_time),
                    new SqlParameter("@codetable",entity.Codetable),
                    new SqlParameter("@order",entity.Orders)
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
        /// 生成云购码并插入到最新的go_shopcode表内,
        /// </summary>
        public static bool CreateShopCode(int zongrenshu,int yid,int codetablemax)
        {
            //云购码生成规则:根据单期开奖总人数生成,30个人数,生成30个编码,顺序打乱插入go_shopcode表内
            //业务代号为 yuan
            try
            {
                /*
                //生成云购码数组,并插入到datatable内
                int seed =10000001;
                int[] codeList = GetRandomArray(zongrenshu, seed, seed + zongrenshu - 1);

                DataTable dtCodeList = new DataTable();
                dtCodeList.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("codetype"),
                    new DataColumn("businessId"),
                    new DataColumn("code"),
                });
                for (int i = 0; i < zongrenshu; i++)
                {
                    dtCodeList.Rows.Add("yuan", yid, codeList[i]);
                }
                 */

                
                //插入云购码
                string TableName =""+GetAndRefreshCodetable(zongrenshu, codetablemax)+"";
                string insertSql = string.Format(@"
                                                                    if object_id('{2}') is null
                                                                    Begin
	                                                                    select * into {2} from go_shopcode where 1=2;insert into {2} (codetype,businessid,code,status) Select 'yuan'as codetype,{0} as businessId,code,0 as status From (Select top {1} * From go_shopcode Where codetype='moban')T Order by NEWID()
                                                                    End
                                                                    else
                                                                    Begin
	                                                                    insert into {2} (codetype,businessid,code,status) Select 'yuan'as codetype,{0} as businessId,code,0 as status From (Select top {1} * From go_shopcode Where codetype='moban')T Order by NEWID()
                                                                    End", yid, zongrenshu, TableName);
                DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(DbServers.DbServerName.LatestDB)).Execute(insertSql);
                //将最新的tablename更新
                string updateSql = string.Format(@"Update go_yiyuan set codetable = '{0}' From go_yiyuan where yid = {1}", TableName, yid);
                DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(DbServers.DbServerName.LatestDB)).Execute(updateSql);
                return true;
            }
            catch(Exception ex)
            {
                return  false;
            }
        }

        /// <summary>
        /// 获取最新的codetable,如果当前总人数+当前表的行数超过了300000,创建一个新的表
        /// </summary>
        /// <param name="zongrenshu"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static string GetAndRefreshCodetable(int zongrenshu, int codetablemax, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            bool isFirst=false;
            //获取最新的表名
            string codetableSql = string.Format("select top 1 codetable from go_yiyuan where codetable is not null order by yId desc ; select top 1 codetable from go_tuan where codetable is not null order by tid desc");
            DataSet dsCodetable = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(codetableSql);
            string yuanCodetable = (dsCodetable.Tables[0].Rows.Count == 0 || DBNull.Value == dsCodetable.Tables[0].Rows[0]["codetable"]) ? "go_shopcode" : dsCodetable.Tables[0].Rows[0]["codetable"].ToString();
            string tuanCodetable = (dsCodetable.Tables[1].Rows.Count == 0 || DBNull.Value == dsCodetable.Tables[1].Rows[0]["codetable"])? "go_shopcode" : dsCodetable.Tables[1].Rows[0]["codetable"].ToString();
            if (yuanCodetable == "go_shopcode" && tuanCodetable == "go_shopcode") isFirst = true;

            bool hasNewTable = false;//是否已经跳过了新的表,如果是的,做了replace处理之后的表名,要加上#
            if (yuanCodetable.IndexOf("#") > 0 || tuanCodetable.IndexOf("#") > 0)
            {
                hasNewTable = true;
                yuanCodetable = yuanCodetable.Replace("#", "");
                tuanCodetable = tuanCodetable.Replace("#", "");
            }
            int yuanTableOrderNum = Convert.ToInt32(yuanCodetable.Replace("go_shopcode", "0"));
            int tuanTableOrderNum = Convert.ToInt32(tuanCodetable.Replace("go_shopcode", "0"));

            string tablename = yuanTableOrderNum >= tuanTableOrderNum? yuanCodetable : tuanCodetable;
            int maxTableOrderNum = yuanTableOrderNum >= tuanTableOrderNum? yuanTableOrderNum : tuanTableOrderNum;

            if (string.IsNullOrEmpty(tablename)) tablename = "go_shopcode";//设置第一个表名

            

            //查询云购码表行数
            string selectSql = string.Format(@"select count(*) c from {0}", hasNewTable ? "go_shopcode" + "#" + maxTableOrderNum : tablename);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            int c = Convert.ToInt32(ds.Tables[0].Rows[0]["c"]);
            //如果超过300000条,创建新表
            int tableNum = Convert.ToInt32(tablename.Replace("go_shopcode", "0"));
            string newTableName = "";
            if (zongrenshu + c >= codetablemax)
            {
                if (hasNewTable) newTableName = "go_shopcode" + "#" + (tableNum + 1);
                //string deleteSql = string.Format("select * into {0} from go_shopcode  where 1=2", newTableName);
                //DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
            }
            else
            {
                newTableName = "go_shopcode" + "#" + (tableNum);//"#"+tablename;
                if (isFirst) newTableName = newTableName.Replace("#", "");
            }
            return newTableName;
        }

        /// <summary>
        /// 生成指定范围内的不重复随机数
        /// </summary>
        /// <param name="Number">随机数个数</param>
        /// <param name="minNum">随机数下限</param>
        /// <param name="maxNum">随机数上限</param>
        /// <returns></returns>
        public static int[] GetRandomArray(int Number, int minNum, int maxNum)
        {
            int j;
            int[] b = new int[Number];
            Random r = new Random();
            for (j = 0; j < Number; j++)
            {
                int i = r.Next(minNum, maxNum + 1);
                int num = 0;
                for (int k = 0; k < j; k++)
                {
                    if (b[k] == i)
                    {
                        num = num + 1;
                    }
                }
                if (num == 0)
                {
                    b[j] = i;
                }
                else
                {
                    j = j - 1;
                }
            }
            return b;
        }


        /// <summary>
        /// 将一元购商品倒计时状态设置为关闭 showtime=0
        /// </summary>
        /// <param name="yid"></param>
        public static void refreshLotteryShow(int yid, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string execSql = string.Format("update go_yiyuan set q_showtime = 0 where yId={0}",yid);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(execSql);
        }
	}
}
