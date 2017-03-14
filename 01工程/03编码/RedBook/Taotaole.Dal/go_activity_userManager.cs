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
    public class go_activity_userManager
    {
        private readonly static string dbServerName = null; //数据库服务名，为空时调用主服务器

        /// <summary>
        /// 根据主键查询数据集
        /// </summary>
        public static DataTable LoadData(int ID)
        {
            string selectSql = string.Format(@"Select * From go_activity_user Where id={0}", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_activity_user";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据主键查询数据实体
        /// </summary>
        public static go_activity_userEntity LoadEntity(int ID)
        {
            string selectSql = string.Format(@"Select * From go_activity_user Where id={0}", ID);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
            {
                return ReaderConvert.ReaderToModel<go_activity_userEntity>(reader);
            }
        }

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable SelectListData(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {2} {1} From go_activity_user {0}", where, selectFields, top == 0 ? "" : "top " + top);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_activity_user";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据条件查询首行首列
        /// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", string orderby = null)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {1} From go_activity_user {0}", where, selectFields);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            return DataAccessFactory.GetDataProvider(dbServerName).GetScalar(selectSql);
        }

        /// <summary>
        /// 根据条件查询数据实体
        /// </summary>
        public static IList<go_activity_userEntity> SelectListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {2} {1} From go_activity_user {0}", where, selectFields, top == 0 ? "" : "top " + top);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
            {
                return ReaderConvert.ReaderToList<go_activity_userEntity>(reader);
            }
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        public static void Del(int ID)
        {
            string deleteSql = string.Format(@"Delete From go_activity_user Where id={0}", ID);
            DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        public static void DelListData(string where = null)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string deleteSql = string.Format(@"Delete From go_activity_user {0}", where);
            DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public static bool SaveEntity(go_activity_userEntity entity, bool isAdd)
        {
            try
            {
                string execSql = (isAdd) ?
                "Insert Into go_activity_user(activity_id,a_type,channel_id,[count],alreadycount,sentcount,alreadysentcount,raisetime,amount,redpackday,starttime,endtime,addtime,timespans,use_range,uid,mobile)values(@activity_id,@a_type,@channel_id,@count,@alreadycount,@sentcount,@alreadysentcount,@raisetime,@amount,@redpackday,@starttime,@endtime,@addtime,@timespans,@use_range,@uid,@mobile)" :
                "Update go_activity_user Set activity_id=@activity_id,a_type=@a_type,channel_id=@channel_id,[count]=@count,alreadycount=@alreadycount,sentcount=@sentcount,alreadysentcount=@alreadysentcount,raisetime=@raisetime,amount=@amount,redpackday=@redpackday,starttime=@starttime,endtime=@endtime,addtime=@addtime,timespans=@timespans,use_range=@use_range,uid=@uid,mobile=@mobile Where id=@id";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@id",entity.Id),
					new SqlParameter("@activity_id",entity.Activity_id),
					new SqlParameter("@a_type",entity.A_type),
					new SqlParameter("@channel_id",entity.Channel_id),
					new SqlParameter("@count",entity.Count),
					new SqlParameter("@alreadycount",entity.Alreadycount),
					new SqlParameter("@sentcount",entity.Sentcount),
					new SqlParameter("@alreadysentcount",entity.Alreadysentcount),
					new SqlParameter("@raisetime",entity.Raisetime),
					new SqlParameter("@amount",entity.Amount),
					new SqlParameter("@redpackday",entity.Redpackday),
					(entity.Starttime==null || entity.Starttime==DateTime.MinValue)?new SqlParameter("@starttime",DBNull.Value):new SqlParameter("@starttime",entity.Starttime),
					(entity.Endtime==null || entity.Endtime==DateTime.MinValue)?new SqlParameter("@endtime",DBNull.Value):new SqlParameter("@endtime",entity.Endtime),
					(entity.Addtime==null || entity.Addtime==DateTime.MinValue)?new SqlParameter("@addtime",DBNull.Value):new SqlParameter("@addtime",entity.Addtime),
					(entity.Timespans==null)?new SqlParameter("@timespans",DBNull.Value):new SqlParameter("@timespans",entity.Timespans),
					(entity.Use_range==null)?new SqlParameter("@use_range",DBNull.Value):new SqlParameter("@use_range",entity.Use_range),
					(entity.Uid==null)?new SqlParameter("@uid",DBNull.Value):new SqlParameter("@uid",entity.Uid),
					(entity.Mobile==null)?new SqlParameter("@mobile",DBNull.Value):new SqlParameter("@mobile",entity.Mobile),
				};
                DataAccessFactory.GetDataProvider(dbServerName).Execute(execSql, para);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
