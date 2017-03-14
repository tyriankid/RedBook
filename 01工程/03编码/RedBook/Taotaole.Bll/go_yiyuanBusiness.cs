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
	/// 一元购-业务操作类
	/// </summary>
	public class go_yiyuanBusiness
	{
		/// <summary>
		/// 根据主键加载一元购数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
                return go_yiyuanManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载一元购实体
		/// </summary>
        public static go_yiyuanEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_yiyuanManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询一元购数据集
		/// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_yiyuanManager.SelectListData(where, selectFields, currDbName);
		}

		/// <summary>
		/// 根据条件查询一元购首行首列
		/// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_yiyuanManager.SelectScalar(where, selectFields, orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询一元购数据实体
		/// </summary>
        public static IList<go_yiyuanEntity> GetListEntity(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_yiyuanManager.SelectListEntity(where, currDbName);
		}

		/// <summary>
		/// 根据主键删除一元购
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_yiyuanManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除一元购
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_yiyuanManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存一元购
		/// </summary>
        public static bool SaveEntity(go_yiyuanEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_yiyuanManager.SaveEntity(entity, isAdd, currDbName);
		}
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static bool UpdateEntity(go_yiyuanEntity entity,DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_yiyuanManager.UpdateEntity(entity, currDbName);
        }

        public static int AddEntity(go_yiyuanEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_yiyuanManager.AddEntityId(entity, currDbName);
        }
        /// <summary>
        /// 更新商品对应的云购码table字段
        /// </summary>
        public static void UpdateCodetable(int yid, string codetable, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            go_yiyuanManager.UpdateCodetable(yid, codetable, currDbName);
        }

        /// <summary>
        /// 获取云购码table
        /// </summary>
        /// <param name="yid">一元购主键id</param>
        /// <param name="zongrenshu">所需总人数</param>
        /// <returns>成功或失败</returns>
        public static bool CreateShopCode(int zongrenshu, int yid)
        {
            return go_yiyuanManager.CreateShopCode(zongrenshu, yid, Globals.CodeTablemMax);
        }

        /// <summary>
        /// 将一元购商品倒计时状态设置为关闭 showtime=0
        /// </summary>
        /// <param name="yid"></param>
        public static void refreshLotteryShow(int yid)
        {
            go_yiyuanManager.refreshLotteryShow(yid);
        }

	}
}
