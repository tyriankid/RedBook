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
	/// 晒单回复-业务操作类
	/// </summary>
	public class go_shaidan_huifuBusiness
	{
		/// <summary>
		/// 根据主键加载晒单回复数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_shaidan_huifuManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载晒单回复实体
		/// </summary>
		public static go_shaidan_huifuEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_shaidan_huifuManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询晒单回复数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_shaidan_huifuManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询晒单回复首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_shaidan_huifuManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询晒单回复数据实体
		/// </summary>
		public static IList<go_shaidan_huifuEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_shaidan_huifuManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除晒单回复
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_shaidan_huifuManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除晒单回复
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_shaidan_huifuManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存晒单回复
		/// </summary>
		public static bool SaveEntity(go_shaidan_huifuEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_shaidan_huifuManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
