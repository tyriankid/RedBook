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
	/// 团购参团记录-业务操作类
	/// </summary>
	public class go_tuan_listinfoBusiness
	{
		/// <summary>
		/// 根据主键加载团购参团记录数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_tuan_listinfoManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载团购参团记录实体
		/// </summary>
		public static go_tuan_listinfoEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_tuan_listinfoManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询团购参团记录数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_tuan_listinfoManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询团购参团记录首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_tuan_listinfoManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询团购参团记录数据实体
		/// </summary>
		public static IList<go_tuan_listinfoEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_tuan_listinfoManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除团购参团记录
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_tuan_listinfoManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除团购参团记录
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_tuan_listinfoManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存团购参团记录
		/// </summary>
		public static bool SaveEntity(go_tuan_listinfoEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_tuan_listinfoManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
