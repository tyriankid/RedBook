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
	/// 渠道推广用户-数据库操作类
	/// </summary>
	public class go_channel_userManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_channel_user Where uid={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_channel_user";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["uid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_channel_userEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_channel_user Where uid={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_channel_userEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_channel_user {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_channel_user";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["uid"] };
			return ds.Tables[0];
		}
        /// <summary>
        /// 根据服务商编号获取渠道商数量
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectChannelCount(int uid, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = string.Format(@"select count(*) from go_channel_user where parentid={0}", uid);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 根据上级编号获取城市服务商名
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectChannelName(int parentid, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = string.Format(@"select realname from go_channel_user where uid={0}", parentid);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 获取所有服务商推广用户
        /// </summary>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectAllUserCount(string from, string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
       
            string selectSql = "select sum(usercount) from  " + from;

            if (where != null)
            {
                selectSql += " where " + where;
            }
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }
        /// <summary>
        /// 获取所有服务商推广充值
        /// </summary>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectAllUserMoney(string from, string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
           
            string selectSql = "select sum(usercountmoney) from  " + from;

            if (where != null)
            {
                selectSql += " where " + where;
            }
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }
		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_channel_user {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_channel_userEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_channel_user {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_channel_userEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_channel_user Where uid={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_channel_user {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

        /// <summary>
        /// 保存数据
        /// </summary>
        public static bool SaveEntity(go_channel_userEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            try
            {
                string execSql = (isAdd) ?
                "Insert Into go_channel_user(typeid,frozenstate,parentid,usercount,nowithdrawcash,withdrawcash,usercountmoney,settlementprice,addtime,logintime,rebateratio,gatheringaccount,username,userpass,realname,contacts,usermobile,useremail,provinceid,cityid,remark,loginip)values(@typeid,@frozenstate,@parentid,@usercount,@nowithdrawcash,@withdrawcash,@usercountmoney,@settlementprice,@addtime,@logintime,@rebateratio,@gatheringaccount,@username,@userpass,@realname,@contacts,@usermobile,@useremail,@provinceid,@cityid,@remark,@loginip)" :
                "Update go_channel_user Set typeid=@typeid,frozenstate=@frozenstate,parentid=@parentid,usercount=@usercount,nowithdrawcash=@nowithdrawcash,withdrawcash=@withdrawcash,usercountmoney=@usercountmoney,settlementprice=@settlementprice,addtime=@addtime,logintime=@logintime,rebateratio=@rebateratio,gatheringaccount=@gatheringaccount,username=@username,userpass=@userpass,realname=@realname,contacts=@contacts,usermobile=@usermobile,useremail=@useremail,provinceid=@provinceid,cityid=@cityid,remark=@remark,loginip=@loginip Where uid=@uid";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@typeid",entity.Typeid),
					new SqlParameter("@frozenstate",entity.Frozenstate),
					new SqlParameter("@uid",entity.Uid),
					new SqlParameter("@parentid",entity.Parentid),
					new SqlParameter("@usercount",entity.Usercount),
					new SqlParameter("@nowithdrawcash",entity.Nowithdrawcash),
					new SqlParameter("@withdrawcash",entity.Withdrawcash),
					new SqlParameter("@usercountmoney",entity.Usercountmoney),
					new SqlParameter("@settlementprice",entity.Settlementprice),
					(entity.Addtime==null || entity.Addtime==DateTime.MinValue)?new SqlParameter("@addtime",DBNull.Value):new SqlParameter("@addtime",entity.Addtime),
					(entity.Logintime==null || entity.Logintime==DateTime.MinValue)?new SqlParameter("@logintime",DBNull.Value):new SqlParameter("@logintime",entity.Logintime),
					(entity.Rebateratio==null)?new SqlParameter("@rebateratio",DBNull.Value):new SqlParameter("@rebateratio",entity.Rebateratio),
					(entity.Gatheringaccount==null)?new SqlParameter("@gatheringaccount",DBNull.Value):new SqlParameter("@gatheringaccount",entity.Gatheringaccount),
					(entity.Username==null)?new SqlParameter("@username",DBNull.Value):new SqlParameter("@username",entity.Username),
					(entity.Userpass==null)?new SqlParameter("@userpass",DBNull.Value):new SqlParameter("@userpass",entity.Userpass),
					(entity.Realname==null)?new SqlParameter("@realname",DBNull.Value):new SqlParameter("@realname",entity.Realname),
					(entity.Contacts==null)?new SqlParameter("@contacts",DBNull.Value):new SqlParameter("@contacts",entity.Contacts),
					(entity.Usermobile==null)?new SqlParameter("@usermobile",DBNull.Value):new SqlParameter("@usermobile",entity.Usermobile),
					(entity.Useremail==null)?new SqlParameter("@useremail",DBNull.Value):new SqlParameter("@useremail",entity.Useremail),
					(entity.Provinceid==null)?new SqlParameter("@provinceid",DBNull.Value):new SqlParameter("@provinceid",entity.Provinceid),
					(entity.Cityid==null)?new SqlParameter("@cityid",DBNull.Value):new SqlParameter("@cityid",entity.Cityid),
					(entity.Remark==null)?new SqlParameter("@remark",DBNull.Value):new SqlParameter("@remark",entity.Remark),
					(entity.Loginip==null)?new SqlParameter("@loginip",DBNull.Value):new SqlParameter("@loginip",entity.Loginip),
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
