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
	/// 用户充值记录-业务操作类
	/// </summary>
	public class go_member_addmoney_recordBusiness
	{
		/// <summary>
		/// 根据主键加载用户充值记录数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
                return go_member_addmoney_recordManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载用户充值记录实体
		/// </summary>
        public static go_member_addmoney_recordEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_member_addmoney_recordManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询用户充值记录数据集
		/// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_member_addmoney_recordManager.SelectListData(where, selectFields, currDbName);
		}

		/// <summary>
		/// 根据条件查询用户充值记录首行首列
		/// </summary>
        public static object SelectScalar(string where = null, string selectFields = "*", DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_member_addmoney_recordManager.SelectScalar(where, selectFields, currDbName);
		}

		/// <summary>
		/// 根据条件查询用户充值记录数据实体
		/// </summary>
        public static IList<go_member_addmoney_recordEntity> GetListEntity(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_member_addmoney_recordManager.SelectListEntity(where, currDbName);
		}

		/// <summary>
		/// 根据主键删除用户充值记录
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_member_addmoney_recordManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除用户充值记录
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_member_addmoney_recordManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存用户充值记录
		/// </summary>
        public static bool SaveEntity(go_member_addmoney_recordEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            if (entity.Money <= 0) return true;
            return go_member_addmoney_recordManager.SaveEntity(entity, isAdd, currDbName);
		}

        /// <summary>
        /// 更新充值订单状态(团购)
        /// </summary>
        public static void SaveStatus(string code, string status, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            go_member_addmoney_recordManager.SaveStatus(code, status, currDbName);
        }

	}
}
