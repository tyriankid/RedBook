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
	/// 用户地址-业务操作类
	/// </summary>
	public class go_member_dizhiBusiness
	{
		/// <summary>
		/// 根据主键加载用户地址数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_member_dizhiManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载用户地址实体
		/// </summary>
		public static go_member_dizhiEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_member_dizhiManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询用户地址数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_member_dizhiManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询用户地址首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_member_dizhiManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询用户地址数据实体
		/// </summary>
		public static IList<go_member_dizhiEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_member_dizhiManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除用户地址
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_member_dizhiManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除用户地址
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_member_dizhiManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存用户地址
		/// </summary>
		public static bool SaveEntity(go_member_dizhiEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_member_dizhiManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
