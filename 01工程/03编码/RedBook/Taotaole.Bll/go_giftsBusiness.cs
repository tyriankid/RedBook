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
	/// 积分商城-业务操作类
	/// </summary>
	public class go_giftsBusiness
	{
		/// <summary>
		/// 根据主键加载积分商城数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_giftsManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载积分商城实体
		/// </summary>
		public static go_giftsEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_giftsManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询积分商城数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_giftsManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询积分商城首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_giftsManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询积分商城数据实体
		/// </summary>
		public static IList<go_giftsEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_giftsManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除积分商城
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_giftsManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除积分商城
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_giftsManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存积分商城
		/// </summary>
		public static bool SaveEntity(go_giftsEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_giftsManager.SaveEntity(entity, isAdd, currDbName);
		}

        public static int[] goGiftDraw(string orderid,int uid, string gameType, int minScore, int maxScore)
        {
            return go_giftsManager.goGiftDraw(orderid,uid, gameType, minScore, maxScore);
        }

        public static int[] goGiftGua(string orderid,int uid, int giftId)
        {
            return go_giftsManager.goGiftGua(orderid,uid, giftId);
        }

        public static bool isGameOpen(string score, string giftUse, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_giftsManager.isGameOpen(score, giftUse);
        } 

	}
}
