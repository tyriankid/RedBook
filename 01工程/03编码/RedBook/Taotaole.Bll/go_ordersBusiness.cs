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
	/// 订单-业务操作类
	/// </summary>
	public class go_ordersBusiness
	{
		/// <summary>
		/// 根据主键加载订单数据集
		/// </summary>
        public static DataTable LoadData(string ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_ordersManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载订单实体
		/// </summary>
		public static go_ordersEntity LoadEntity(string ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_ordersManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询订单数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_ordersManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}
        /// <summary>
        /// 根据条件查询直购订单
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="top"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static DataTable SelectZhiData(string where = null, string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_ordersManager.SelectZhiData(where, orderby, top, currDbName);
		}

		/// <summary>
		/// 根据条件查询订单首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_ordersManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询订单数据实体
		/// </summary>
		public static IList<go_ordersEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_ordersManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除订单
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_ordersManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除订单
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_ordersManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存订单
		/// </summary>
		public static bool SaveEntity(go_ordersEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_ordersManager.SaveEntity(entity, isAdd, currDbName);
		}
        /// <summary>
        /// 根据主键加载订单数据集(一元购)
        /// </summary>
        public static DataTable LoadDataYuan(string ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (Globals.GetMasterSettings().OpenCacheServer)
            {
                return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
            }
            else
            {
                return go_ordersManager.LoadDataYuan(ID, currDbName);
            }
        }
        /// <summary>
        /// 根据主键加载订单数据集(一元购)
        /// </summary>
        public static DataTable LoadDataTuan(string ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (Globals.GetMasterSettings().OpenCacheServer)
            {
                return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
            }
            else
            {
                return go_ordersManager.LoadDataTuan(ID, currDbName);
            }
        }
        /// <summary>
        /// 根据主键加载订单数据集(一元购)
        /// </summary>
        public static DataTable LoadAllSend(string ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (Globals.GetMasterSettings().OpenCacheServer)
            {
                return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
            }
            else
            {
                return go_ordersManager.LoadAllSend(ID, currDbName);
            }
        }
	}
}
