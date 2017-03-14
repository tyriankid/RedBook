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
	/// 永乐对账-业务操作类
	/// </summary>
	public class go_yolly_orderinfo_apiBusiness
	{
		/// <summary>
		/// 根据主键加载永乐对账数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_yolly_orderinfo_apiManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载永乐对账实体
		/// </summary>
		public static go_yolly_orderinfo_apiEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_yolly_orderinfo_apiManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询永乐对账数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_yolly_orderinfo_apiManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询永乐对账首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_yolly_orderinfo_apiManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询永乐对账数据实体
		/// </summary>
		public static IList<go_yolly_orderinfo_apiEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_yolly_orderinfo_apiManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除永乐对账
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_yolly_orderinfo_apiManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除永乐对账
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_yolly_orderinfo_apiManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存永乐对账
		/// </summary>
		public static bool SaveEntity(go_yolly_orderinfo_apiEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_yolly_orderinfo_apiManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
