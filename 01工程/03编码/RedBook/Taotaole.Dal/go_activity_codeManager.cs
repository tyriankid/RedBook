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
    public class go_activity_codeManager
    {
        private readonly static string dbServerName = null; //数据库服务名，为空时调用主服务器

        /// <summary>
        /// 根据主键查询数据集
        /// </summary>
        public static DataTable LoadData(int ID)
        {
            string selectSql = string.Format(@"Select * From go_activity_code Where id={0}", ID);
            DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_activity_code";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据主键查询数据实体
        /// </summary>
        public static go_activity_codeEntity LoadEntity(int ID)
        {
            string selectSql = string.Format(@"Select * From go_activity_code Where id={0}", ID);
            using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
            {
                return ReaderConvert.ReaderToModel<go_activity_codeEntity>(reader);
            }
        }

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable SelectListData(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {2} {1} From go_activity_code {0}", where, selectFields, top == 0 ? "" : "top " + top);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
            ds.Tables[0].TableName = "go_activity_code";
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["id"] };
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据条件查询首行首列
        /// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", string orderby = null)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {1} From go_activity_code {0}", where, selectFields);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            return DataAccessFactory.GetDataProvider(dbServerName).GetScalar(selectSql);
        }

        /// <summary>
        /// 根据条件查询数据实体
        /// </summary>
        public static IList<go_activity_codeEntity> SelectListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {2} {1} From go_activity_code {0}", where, selectFields, top == 0 ? "" : "top " + top);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
            {
                return ReaderConvert.ReaderToList<go_activity_codeEntity>(reader);
            }
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        public static void Del(int ID)
        {
            string deleteSql = string.Format(@"Delete From go_activity_code Where id={0}", ID);
            DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        public static void DelListData(string where = null)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string deleteSql = string.Format(@"Delete From go_activity_code {0}", where);
            DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public static bool SaveEntity(go_activity_codeEntity entity, bool isAdd)
        {
            try
            {
                string execSql = (isAdd) ?
                "Insert Into go_activity_code(activity_id,code_config_id,channel_id,raisetime,d_type,amount,discount,state,[from],starttime,endtime,usetime,addtime,overtime,senttime,timespans,use_range,codetitle,uid,mobile,order_id,descript)values(@activity_id,@code_config_id,@channel_id,@raisetime,@d_type,@amount,@discount,@state,@from,@starttime,@endtime,@usetime,@addtime,@overtime,@senttime,@timespans,@use_range,@codetitle,@uid,@mobile,@order_id,@descript)" :
                "Update go_activity_code Set activity_id=@activity_id,code_config_id=@code_config_id,channel_id=@channel_id,raisetime=@raisetime,d_type=@d_type,amount=@amount,discount=@discount,state=@state,[from]=@from,starttime=@starttime,endtime=@endtime,usetime=@usetime,addtime=@addtime,overtime=@overtime,senttime=@senttime,timespans=@timespans,use_range=@use_range,codetitle=@codetitle,uid=@uid,mobile=@mobile,order_id=@order_id,descript=@descript Where id=@id";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@id",entity.Id),
					new SqlParameter("@activity_id",entity.Activity_id),
					new SqlParameter("@code_config_id",entity.Code_config_id),
					new SqlParameter("@channel_id",entity.Channel_id),
					new SqlParameter("@raisetime",entity.Raisetime),
					new SqlParameter("@d_type",entity.D_type),
					new SqlParameter("@amount",entity.Amount),
					new SqlParameter("@discount",entity.Discount),
					new SqlParameter("@state",entity.State),
					new SqlParameter("@from",entity.From),
					(entity.Starttime==null || entity.Starttime==DateTime.MinValue)?new SqlParameter("@starttime",DBNull.Value):new SqlParameter("@starttime",entity.Starttime),
					(entity.Endtime==null || entity.Endtime==DateTime.MinValue)?new SqlParameter("@endtime",DBNull.Value):new SqlParameter("@endtime",entity.Endtime),
					(entity.Usetime==null || entity.Usetime==DateTime.MinValue)?new SqlParameter("@usetime",DBNull.Value):new SqlParameter("@usetime",entity.Usetime),
					(entity.Addtime==null || entity.Addtime==DateTime.MinValue)?new SqlParameter("@addtime",DBNull.Value):new SqlParameter("@addtime",entity.Addtime),
					(entity.Overtime==null || entity.Overtime==DateTime.MinValue)?new SqlParameter("@overtime",DBNull.Value):new SqlParameter("@overtime",entity.Overtime),
					(entity.Senttime==null || entity.Senttime==DateTime.MinValue)?new SqlParameter("@senttime",DBNull.Value):new SqlParameter("@senttime",entity.Senttime),
					(entity.Timespans==null)?new SqlParameter("@timespans",DBNull.Value):new SqlParameter("@timespans",entity.Timespans),
					(entity.Use_range==null)?new SqlParameter("@use_range",DBNull.Value):new SqlParameter("@use_range",entity.Use_range),
					(entity.Codetitle==null)?new SqlParameter("@codetitle",DBNull.Value):new SqlParameter("@codetitle",entity.Codetitle),
					(entity.Uid==null)?new SqlParameter("@uid",DBNull.Value):new SqlParameter("@uid",entity.Uid),
					(entity.Mobile==null)?new SqlParameter("@mobile",DBNull.Value):new SqlParameter("@mobile",entity.Mobile),
					(entity.Order_id==null)?new SqlParameter("@order_id",DBNull.Value):new SqlParameter("@order_id",entity.Order_id),
					(entity.Descript==null)?new SqlParameter("@descript",DBNull.Value):new SqlParameter("@descript",entity.Descript),
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
