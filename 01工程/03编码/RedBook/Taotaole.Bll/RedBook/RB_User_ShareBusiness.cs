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
	/// -业务操作类
	/// </summary>
	public class RB_User_ShareBusiness
	{
		/// <summary>
		/// 根据主键加载数据集
		/// </summary>
		public static DataTable LoadData(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return RB_User_ShareManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载实体
		/// </summary>
		public static RB_User_ShareEntity LoadEntity(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return RB_User_ShareManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return RB_User_ShareManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return RB_User_ShareManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<RB_User_ShareEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return RB_User_ShareManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(Guid ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			RB_User_ShareManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			RB_User_ShareManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存
		/// </summary>
		public static bool SaveEntity(RB_User_ShareEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return RB_User_ShareManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
