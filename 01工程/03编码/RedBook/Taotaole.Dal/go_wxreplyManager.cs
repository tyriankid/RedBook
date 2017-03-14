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
	/// 微信自定义回复-数据库操作类
	/// </summary>
	public class go_wxreplyManager
	{
		private readonly static string dbServerName = null; //数据库服务名，为空时调用主服务器

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID)
		{
			string selectSql = string.Format(@"Select * From go_wxreply Where replyid={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_wxreply";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["replyid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_wxreplyEntity LoadEntity(int ID)
		{
			string selectSql = string.Format(@"Select * From go_wxreply Where replyid={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_wxreplyEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*")
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_wxreply {0}", where, selectFields);
			DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_wxreply";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["replyid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*")
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_wxreply {0}", where, selectFields);
			return DataAccessFactory.GetDataProvider(dbServerName).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_wxreplyEntity> SelectListEntity(string where = null)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select * From go_wxreply {0}", where);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_wxreplyEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID)
		{
			string deleteSql = string.Format(@"Delete From go_wxreply Where replyid={0}", ID);
			DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_wxreply {0}", where);
			DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_wxreplyEntity entity, bool isAdd)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_wxreply(matchtype,replytype,messagetype,isdisable,keyword,content)values(@matchtype,@replytype,@messagetype,@isdisable,@keyword,@content)" :
				"Update go_wxreply Set matchtype=@matchtype,replytype=@replytype,messagetype=@messagetype,isdisable=@isdisable,keyword=@keyword,content=@content Where replyid=@replyid";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@replyid",entity.Replyid),
					new SqlParameter("@matchtype",entity.Matchtype),
					new SqlParameter("@replytype",entity.Replytype),
					new SqlParameter("@messagetype",entity.Messagetype),
					new SqlParameter("@isdisable",entity.Isdisable),
					new SqlParameter("@keyword",entity.Keyword),
					new SqlParameter("@content",entity.Content)
				};
				DataAccessFactory.GetDataProvider(dbServerName).Execute(execSql, para);
				return true;
			}
			catch
			{
				return false;
			}
		}

        /// <summary>
        /// 判断关键字是否重复,true重复
        /// </summary>
        public static bool HasReplyKeyword(string keyword)
        {
            string selectSql = string.Format("SELECT COUNT(*) FROM go_wxreply WHERE keyword = @keyword");
            SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@keyword",keyword),
            };
            return Convert.ToInt32(DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(DbServers.DbServerName.LatestDB)).GetScalar(selectSql, para).ToString()) > 0;
        }

        public static bool UpdateReplyInfo(go_wxreplyEntity reply)
        {
            bool flag = false;

            switch (reply.Messagetype) //1:文本 2:单图文 3:多图文
            {
                case 1:
                    return SaveEntity(reply, false);
                case 2:
                case 3:
                    return UpdateNewsReply(reply as NewsReplyInfo);
                /*
                case (MessageType.News | MessageType.Text):
                    return flag;
                 */
            }
            return flag;
        }


        public static bool SaveReplyInfo(go_wxreplyEntity reply)
        {
            bool flag = false;
            
            switch (reply.Messagetype) //1:文本 2:单图文 3:多图文
            {
                case 1:
                    return SaveEntity(reply,true);
                case 2:
                case 3:
                    return SaveNewsReply(reply as NewsReplyInfo);
                /*
                case (MessageType.News | MessageType.Text):
                    return flag;
                 */ 
            }
            return flag;
        }

        
        private static bool SaveNewsReply(NewsReplyInfo model)
        {
            int num;
            string execSql = "Insert Into go_wxreply(matchtype,replytype,messagetype,isdisable,keyword,content)values(@matchtype,@replytype,@messagetype,@isdisable,@keyword,@content)";
            execSql += ";select @@IDENTITY";
            SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@matchtype",model.Matchtype),
				new SqlParameter("@replytype",model.Replytype),
				new SqlParameter("@messagetype",model.Messagetype),
				new SqlParameter("@isdisable",model.Isdisable),
				new SqlParameter("@keyword",model.Keyword),
				new SqlParameter("@content","")
			};
            if (int.TryParse(DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(DbServers.DbServerName.LatestDB)).GetScalar(execSql, para).ToString(), out num))
            {
                foreach (NewsMsgInfo info in model.NewsMsg)
                {
                    string execsqlArticle = "Insert Into go_wxreply_article(replyid,title,imgurl,url,description,content)values(@replyid,@title,@imgurl,@url,@description,@content)";
                    SqlParameter[] paraArticle = new SqlParameter[]
			        {
				        new SqlParameter("@replyid",num),
				        new SqlParameter("@articleid",info.Articleid),
				        new SqlParameter("@title",info.Title),
				        new SqlParameter("@imgurl",info.Imgurl),
				        new SqlParameter("@url",info.Url),
				        new SqlParameter("@description",info.Description),
				        new SqlParameter("@content",info.Content)
			        };
                    DataAccessFactory.GetDataProvider(dbServerName).Execute(execsqlArticle, paraArticle);
                }
            }
            return true;
        }

        private static bool UpdateNewsReply(NewsReplyInfo model)
        {
            string execSql = "Update go_wxreply Set matchtype=@matchtype,replytype=@replytype,messagetype=@messagetype,isdisable=@isdisable,keyword=@keyword,content=@content Where replyid=@replyid";
            SqlParameter[] para = new SqlParameter[]
			{
                new SqlParameter("@replyid",model.Replyid),
				new SqlParameter("@matchtype",model.Matchtype),
				new SqlParameter("@replytype",model.Replytype),
				new SqlParameter("@messagetype",model.Messagetype),
				new SqlParameter("@isdisable",model.Isdisable),
				new SqlParameter("@keyword",model.Keyword),
				new SqlParameter("@content","")
			};
            DataAccessFactory.GetDataProvider(dbServerName).Execute(execSql, para);
            //先删
            string deleteSql = "delete from go_wxreply_article where replyid = " + model.Replyid;
            DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
            //再增加
            foreach (NewsMsgInfo info in model.NewsMsg)
            {
                string execsqlArticle = "Insert Into go_wxreply_article(replyid,title,imgurl,url,description,content)values(@replyid,@title,@imgurl,@url,@description,@content)";
                SqlParameter[] paraArticle = new SqlParameter[]
			        {
				        new SqlParameter("@replyid",model.Replyid),
				        new SqlParameter("@title",info.Title),
				        new SqlParameter("@imgurl",info.Imgurl),
				        new SqlParameter("@url",info.Url),
				        new SqlParameter("@description",info.Description),
				        new SqlParameter("@content",info.Content)
			        };
                DataAccessFactory.GetDataProvider(dbServerName).Execute(execsqlArticle, paraArticle);
            }
            
            return true;
        }
        
	}
}
