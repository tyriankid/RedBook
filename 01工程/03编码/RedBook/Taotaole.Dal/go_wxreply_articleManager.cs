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
	/// 图文回复-数据库操作类
	/// </summary>
	public class go_wxreply_articleManager
	{
		private readonly static string dbServerName = null; //数据库服务名，为空时调用主服务器

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID)
		{
			string selectSql = string.Format(@"Select * From go_wxreply_article Where articleid={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_wxreply_article";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["articleid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_wxreply_articleEntity LoadEntity(int ID)
		{
			string selectSql = string.Format(@"Select * From go_wxreply_article Where articleid={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_wxreply_articleEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*")
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_wxreply_article {0}", where, selectFields);
			DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_wxreply_article";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["articleid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*")
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_wxreply_article {0}", where, selectFields);
			return DataAccessFactory.GetDataProvider(dbServerName).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_wxreply_articleEntity> SelectListEntity(string where = null)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select * From go_wxreply_article {0}", where);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_wxreply_articleEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID)
		{
			string deleteSql = string.Format(@"Delete From go_wxreply_article Where articleid={0}", ID);
			DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_wxreply_article {0}", where);
			DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_wxreply_articleEntity entity, bool isAdd)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into go_wxreply_article(replyid,title,imgurl,url,description,content)values(@replyid,@title,@imgurl,@url,@description,@content)" :
				"Update go_wxreply_article Set replyid=@replyid,title=@title,imgurl=@imgurl,url=@url,description=@description,content=@content Where articleid=@articleid";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@replyid",entity.Replyid),
					new SqlParameter("@articleid",entity.Articleid),
					new SqlParameter("@title",entity.Title),
					new SqlParameter("@imgurl",entity.Imgurl),
					new SqlParameter("@url",entity.Url),
					new SqlParameter("@description",entity.Description),
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

	}
}
