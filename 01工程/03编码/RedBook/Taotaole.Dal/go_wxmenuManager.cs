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
	public class go_wxmenuManager
	{
		private readonly static string dbServerName = null; //数据库服务名，为空时调用主服务器

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID)
		{
			string selectSql = string.Format(@"Select * From go_wxmenu Where menuid={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_wxmenu";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["menuid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static go_wxmenuEntity LoadEntity(int ID)
		{
			string selectSql = string.Format(@"Select * From go_wxmenu Where menuid={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<go_wxmenuEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*")
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_wxmenu {0}", where, selectFields);
			DataSet ds = DataAccessFactory.GetDataProvider(dbServerName).GetDataset(selectSql);
			ds.Tables[0].TableName = "go_wxmenu";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["menuid"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*")
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From go_wxmenu {0}", where, selectFields);
			return DataAccessFactory.GetDataProvider(dbServerName).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_wxmenuEntity> SelectListEntity(string where = null)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select * From go_wxmenu {0}", where);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<go_wxmenuEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID)
		{
			string deleteSql = string.Format(@"Delete From go_wxmenu Where menuid={0}", ID);
			DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From go_wxmenu {0}", where);
			DataAccessFactory.GetDataProvider(dbServerName).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(go_wxmenuEntity entity, bool isAdd)
		{
			try
			{
				string execSql = (isAdd) ?
                "Insert Into go_wxmenu(parentid,type,name,url,sort)values(@parentid,@type,@name,@url,@sort)" :
                "Update go_wxmenu Set parentid=@parentid,type=@type,name=@name,url=@url,sort=@sort Where menuid=@menuid";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@menuid",entity.Menuid),
					new SqlParameter("@parentid",entity.Parentid),
					new SqlParameter("@type",entity.Type),
					new SqlParameter("@name",entity.Name),
					new SqlParameter("@url",entity.Url),
                    new SqlParameter("@sort",entity.Sort == null?"99":entity.Sort)
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
        /// 获取微信一级菜单
        /// </summary>
        /// <returns></returns>
        public static IList<go_wxmenuEntity> GetTopMenus()
        {
            string selectSql = "SELECT * FROM go_wxmenu WHERE ParentId = 0 ";
            selectSql += " ORDER BY sort ASC";
            using (IDataReader reader = DataAccessFactory.GetDataProvider(dbServerName).GetReader(selectSql))
            {
                return ReaderConvert.ReaderToList<go_wxmenuEntity>(reader);
            }
        }

	}
}
