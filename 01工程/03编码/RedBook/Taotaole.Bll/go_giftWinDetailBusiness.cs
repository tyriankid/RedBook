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
	public class go_giftWinDetailBusiness
	{
		/// <summary>
		/// 根据主键加载数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_giftWinDetailManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载实体
		/// </summary>
		public static go_giftWinDetailEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_giftWinDetailManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_giftWinDetailManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

        /// <summary>
        /// 根据条件查询中奖数据集
        /// </summary>
        public static DataTable selectData(string where = null, string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_giftWinDetailManager.selectData(where, orderby, top, currDbName);
        }


		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_giftWinDetailManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_giftWinDetailEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_giftWinDetailManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_giftWinDetailManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_giftWinDetailManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存
		/// </summary>
		public static bool SaveEntity(go_giftWinDetailEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_giftWinDetailManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
