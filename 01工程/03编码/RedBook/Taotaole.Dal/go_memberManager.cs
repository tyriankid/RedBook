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
	/// 会员-数据库操作类
	/// </summary>
	public class go_memberManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_member Where uid={0}", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_member";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["uid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
        public static go_memberEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From go_member Where uid={0}", ID);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_memberEntity>(reader);
			}
		}
		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
        public static DataTable SelectListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_member {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_member";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["uid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_member {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
        public static IList<go_memberEntity> SelectListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From go_member {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_memberEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From go_member Where uid={0}", ID);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_member {0}", where);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
        public static bool SaveEntity(go_memberEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            try
            {
                string execSql = (isAdd) ?
                            "Insert Into go_member(groupid,othercode,typeid,approuse,score,luckyb,jingyan,yaoqing,auto_user,servicecityid,servicechannelid,ceaseuser,money,tuanmoney,time,unionid,username,email,mobile,password,user_ip,img,band,headimg,wxid,telcode,appwxid,qianming,addgroup,emailcode,mobilecode,passcode,reg_key,zenPoint)values(@groupid,@othercode,@typeid,@approuse,@score,@luckyb,@jingyan,@yaoqing,@auto_user,@servicecityid,@servicechannelid,@ceaseuser,@money,@tuanmoney,@time,@unionid,@username,@email,@mobile,@password,@user_ip,@img,@band,@headimg,@wxid,@telcode,@appwxid,@qianming,@addgroup,@emailcode,@mobilecode,@passcode,@reg_key,@zenPoint)" :
                            "Update go_member Set groupid=@groupid,othercode=@othercode,typeid=@typeid,approuse=@approuse,score=@score,luckyb=@luckyb,jingyan=@jingyan,yaoqing=@yaoqing,auto_user=@auto_user,servicecityid=@servicecityid,servicechannelid=@servicechannelid,ceaseuser=@ceaseuser,money=@money,tuanmoney=@tuanmoney,time=@time,unionid=@unionid,username=@username,email=@email,mobile=@mobile,password=@password,user_ip=@user_ip,img=@img,band=@band,headimg=@headimg,wxid=@wxid,telcode=@telcode,appwxid=@appwxid,qianming=@qianming,addgroup=@addgroup,emailcode=@emailcode,mobilecode=@mobilecode,passcode=@passcode,reg_key=@reg_key,zenPoint=@zenPoint Where uid=@uid";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@groupid",entity.Groupid),
					new SqlParameter("@othercode",entity.Othercode),
					new SqlParameter("@typeid",entity.Typeid),
					new SqlParameter("@approuse",entity.Approuse),
					new SqlParameter("@uid",entity.Uid),
					new SqlParameter("@score",entity.Score),
					new SqlParameter("@luckyb",entity.Luckyb),
					new SqlParameter("@jingyan",entity.Jingyan),
					new SqlParameter("@yaoqing",entity.Yaoqing),
					new SqlParameter("@auto_user",entity.Auto_user),
					new SqlParameter("@servicecityid",entity.Servicecityid),
					new SqlParameter("@servicechannelid",entity.Servicechannelid),
					new SqlParameter("@ceaseuser",entity.Ceaseuser),
					(entity.Money==null)?new SqlParameter("@money",DBNull.Value):new SqlParameter("@money",entity.Money),
					(entity.Tuanmoney==null)?new SqlParameter("@tuanmoney",DBNull.Value):new SqlParameter("@tuanmoney",entity.Tuanmoney),
					(entity.Time==null || entity.Time==DateTime.MinValue)?new SqlParameter("@time",DBNull.Value):new SqlParameter("@time",entity.Time),
					(entity.Unionid==null)?new SqlParameter("@unionid",DBNull.Value):new SqlParameter("@unionid",entity.Unionid),
					(entity.Username==null)?new SqlParameter("@username",DBNull.Value):new SqlParameter("@username",entity.Username),
					(entity.Email==null)?new SqlParameter("@email",DBNull.Value):new SqlParameter("@email",entity.Email),
					(entity.Mobile==null)?new SqlParameter("@mobile",DBNull.Value):new SqlParameter("@mobile",entity.Mobile),
					(entity.Password==null)?new SqlParameter("@password",DBNull.Value):new SqlParameter("@password",entity.Password),
					(entity.User_ip==null)?new SqlParameter("@user_ip",DBNull.Value):new SqlParameter("@user_ip",entity.User_ip),
					(entity.Img==null)?new SqlParameter("@img",DBNull.Value):new SqlParameter("@img",entity.Img),
					(entity.Band==null)?new SqlParameter("@band",DBNull.Value):new SqlParameter("@band",entity.Band),
					(entity.Headimg==null)?new SqlParameter("@headimg",DBNull.Value):new SqlParameter("@headimg",entity.Headimg),
					(entity.Wxid==null)?new SqlParameter("@wxid",DBNull.Value):new SqlParameter("@wxid",entity.Wxid),
					(entity.Telcode==null)?new SqlParameter("@telcode",DBNull.Value):new SqlParameter("@telcode",entity.Telcode),
					(entity.Appwxid==null)?new SqlParameter("@appwxid",DBNull.Value):new SqlParameter("@appwxid",entity.Appwxid),
					(entity.Qianming==null)?new SqlParameter("@qianming",DBNull.Value):new SqlParameter("@qianming",entity.Qianming),
					(entity.Addgroup==null)?new SqlParameter("@addgroup",DBNull.Value):new SqlParameter("@addgroup",entity.Addgroup),
					(entity.Emailcode==null)?new SqlParameter("@emailcode",DBNull.Value):new SqlParameter("@emailcode",entity.Emailcode),
					(entity.Mobilecode==null)?new SqlParameter("@mobilecode",DBNull.Value):new SqlParameter("@mobilecode",entity.Mobilecode),
					(entity.Passcode==null)?new SqlParameter("@passcode",DBNull.Value):new SqlParameter("@passcode",entity.Passcode),
					(entity.Reg_key==null)?new SqlParameter("@reg_key",DBNull.Value):new SqlParameter("@reg_key",entity.Reg_key),
                    (entity.ZenPoint==null)?new SqlParameter("@zenPoint",DBNull.Value):new SqlParameter("@zenPoint",entity.ZenPoint),
				};
                DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(execSql, para);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 更新用户ip
        /// </summary>
        public static void UpdateLogin(go_memberEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string deleteSql = string.Format(@"update go_member set user_ip = '{0}' where uid = {1}", entity.User_ip,entity.Uid);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
        }

        /// <summary>
        /// 更新支付OPENID
        /// </summary>
        public static void UpdateBand(go_memberEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string deleteSql = string.Format(@"update go_member set Band = '{0}' where uid = {1}", entity.Band, entity.Uid);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
        }


        /// <summary>
        /// 充值成功后,更新用户余额
        /// </summary>
        /// <returns></returns>
        public static bool AddMemberRechargeInfo(int uid, go_member_addmoney_recordEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
                string updateSql = string.Format(@"update go_member set money = money + {0} where uid = {1}", entity.Money, uid);
                DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(updateSql);
                return true;
        }
        /// <summary>
        /// 充值成功后,更新用户积分
        /// </summary>
        /// <returns></returns>
        public static bool UpdateScoreInfo(decimal score,decimal money,int uid,DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string updateSql = string.Format(@"update go_member set score = score + {0},money = money + {1}  where uid = {2}", Convert.ToInt32(score),Convert.ToInt32(money), uid);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(updateSql);
            return true;
        }
        /// <summary>
        /// 充值成功后,更新用户团购余额
        /// </summary>
        /// <returns></returns>
        public static bool AddMemberTuanRechargeInfo(int uid, go_member_addmoney_recordEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string updateSql = string.Format(@"update go_member set tuanmoney = tuanmoney + {0} where uid = {1}", entity.Money, uid);
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(updateSql);
            return true;
        }

	}
}
