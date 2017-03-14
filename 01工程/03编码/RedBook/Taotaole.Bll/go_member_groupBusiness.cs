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
	/// 用户组-业务操作类
	/// </summary>
	public class go_member_groupBusiness
	{
		/// <summary>
		/// 根据主键加载用户组数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
                return go_member_groupManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载用户组实体
		/// </summary>
        public static go_member_groupEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_member_groupManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询用户组数据集
		/// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_member_groupManager.SelectListData(where, selectFields, orderby, top, currDbName);
		}

		/// <summary>
		/// 根据条件查询用户组首行首列
		/// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_member_groupManager.SelectScalar(where, selectFields, orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询用户组数据实体
		/// </summary>
        public static IList<go_member_groupEntity> GetListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_member_groupManager.SelectListEntity(where, selectFields, orderby, top, currDbName);
		}

		/// <summary>
		/// 根据主键删除用户组
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_member_groupManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除用户组
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_member_groupManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存用户组
		/// </summary>
        public static bool SaveEntity(go_member_groupEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_member_groupManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
