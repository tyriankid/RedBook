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
	/// 推广提现明细-业务操作类
	/// </summary>
	public class go_channel_cash_outBusiness
	{
		/// <summary>
		/// 根据主键加载推广提现明细数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_channel_cash_outManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载推广提现明细实体
		/// </summary>
		public static go_channel_cash_outEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_channel_cash_outManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询推广提现明细数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_channel_cash_outManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询推广提现明细首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_channel_cash_outManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询推广提现明细数据实体
		/// </summary>
		public static IList<go_channel_cash_outEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_channel_cash_outManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除推广提现明细
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_channel_cash_outManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除推广提现明细
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_channel_cash_outManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存推广提现明细
		/// </summary>
		public static bool SaveEntity(go_channel_cash_outEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_channel_cash_outManager.SaveEntity(entity, isAdd, currDbName);
		}

          /// <summary>
        /// 获取所有给服务商或者渠道商的提现金额
        /// </summary>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectAllcashoutmoney(string from,string where=null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_cash_outManager.SelectAllcashoutmoney(from, where, currDbName);
        }
        
	}
}
