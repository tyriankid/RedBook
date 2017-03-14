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
	/// 品牌-业务操作类
	/// </summary>
	public class go_brandBusiness
	{
		/// <summary>
		/// 根据主键加载品牌数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
                return go_brandManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载品牌实体
		/// </summary>
        public static go_brandEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_brandManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询品牌数据集
		/// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_brandManager.SelectListData(where, selectFields, orderby, top, currDbName);
		}

		/// <summary>
		/// 根据条件查询品牌首行首列
		/// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_brandManager.SelectScalar(where, selectFields, orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询品牌数据实体
		/// </summary>
        public static IList<go_brandEntity> GetListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_brandManager.SelectListEntity(where, selectFields, orderby, top, currDbName);
		}

		/// <summary>
		/// 根据主键删除品牌
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_brandManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除品牌
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_brandManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存品牌
		/// </summary>
        public static bool SaveEntity(go_brandEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_brandManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
