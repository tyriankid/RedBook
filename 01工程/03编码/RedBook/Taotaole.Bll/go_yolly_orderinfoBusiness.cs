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
	/// 永乐充值-业务操作类
	/// </summary>
	public class go_yolly_orderinfoBusiness
	{
		/// <summary>
		/// 根据主键加载永乐充值数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_yolly_orderinfoManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载永乐充值实体
		/// </summary>
		public static go_yolly_orderinfoEntity LoadEntity(string  ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_yolly_orderinfoManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询永乐充值数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_yolly_orderinfoManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询永乐充值首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_yolly_orderinfoManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询永乐充值数据实体
		/// </summary>
		public static IList<go_yolly_orderinfoEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_yolly_orderinfoManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除永乐充值
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_yolly_orderinfoManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除永乐充值
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_yolly_orderinfoManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存永乐充值
		/// </summary>
		public static bool SaveEntity(go_yolly_orderinfoEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_yolly_orderinfoManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
