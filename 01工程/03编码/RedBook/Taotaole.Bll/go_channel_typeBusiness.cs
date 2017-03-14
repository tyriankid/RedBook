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
	/// 渠道推广类型-业务操作类
	/// </summary>
	public class go_channel_typeBusiness
	{
		/// <summary>
		/// 根据主键加载渠道推广类型数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_channel_typeManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载渠道推广类型实体
		/// </summary>
		public static go_channel_typeEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_channel_typeManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询渠道推广类型数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_channel_typeManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询渠道推广类型首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_channel_typeManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询渠道推广类型数据实体
		/// </summary>
		public static IList<go_channel_typeEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_channel_typeManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除渠道推广类型
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_channel_typeManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除渠道推广类型
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_channel_typeManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存渠道推广类型
		/// </summary>
		public static bool SaveEntity(go_channel_typeEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_channel_typeManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
