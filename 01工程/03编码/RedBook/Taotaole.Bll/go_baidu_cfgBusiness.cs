using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Taotaole.Common;
using Taotaole.Dal;
using Taotaole.Model;
using YH.Utility;

namespace Taotaole.Bll
{
	/// <summary>
	/// 百度推广-业务操作类
	/// </summary>
	public class go_baidu_cfgBusiness
	{
		/// <summary>
		/// 根据主键加载百度推广数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_baidu_cfgManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载百度推广实体
		/// </summary>
		public static go_baidu_cfgEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_baidu_cfgManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询百度推广数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_baidu_cfgManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询百度推广首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_baidu_cfgManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询百度推广数据实体
		/// </summary>
		public static IList<go_baidu_cfgEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_baidu_cfgManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除百度推广
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_baidu_cfgManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除百度推广
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_baidu_cfgManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存百度推广
		/// </summary>
		public static bool SaveEntity(go_baidu_cfgEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_baidu_cfgManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
