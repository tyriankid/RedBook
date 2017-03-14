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
	/// 商品基本信息-业务操作类
	/// </summary>
	public class go_productsBusiness
	{
		/// <summary>
		/// 根据主键加载商品基本信息数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
                return go_productsManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载商品基本信息实体
		/// </summary>
        public static go_productsEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_productsManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询商品基本信息数据集
		/// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_productsManager.SelectListData(where, selectFields, orderby, top, currDbName);
		}

		/// <summary>
		/// 根据条件查询商品基本信息首行首列
		/// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_productsManager.SelectScalar(where, selectFields, orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询商品基本信息数据实体
		/// </summary>
        public static IList<go_productsEntity> GetListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_productsManager.SelectListEntity(where, selectFields, orderby, top, currDbName);
		}

		/// <summary>
		/// 根据主键删除商品基本信息
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_productsManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除商品基本信息
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_productsManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存商品基本信息
		/// </summary>
        public static bool SaveEntity(go_productsEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_productsManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
