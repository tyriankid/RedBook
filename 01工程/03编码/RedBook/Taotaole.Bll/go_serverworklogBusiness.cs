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
	/// 服务操作日志-业务操作类
	/// </summary>
	public class go_serverworklogBusiness
	{
		/// <summary>
		/// 根据主键加载服务操作日志数据集
		/// </summary>
		public static DataTable LoadData(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_serverworklogManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载服务操作日志实体
		/// </summary>
		public static go_serverworklogEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_serverworklogManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询服务操作日志数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_serverworklogManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询服务操作日志首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_serverworklogManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询服务操作日志数据实体
		/// </summary>
		public static IList<go_serverworklogEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_serverworklogManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除服务操作日志
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_serverworklogManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除服务操作日志
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_serverworklogManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存服务操作日志
		/// </summary>
		public static bool SaveEntity(go_serverworklogEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_serverworklogManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
