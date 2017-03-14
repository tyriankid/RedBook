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
    /// 推广佣金明细-业务操作类
    /// </summary>
    public class go_channel_recodesBusiness
    {
        /// <summary>
        /// 根据主键加载推广佣金明细数据集
        /// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (Globals.GetMasterSettings().OpenCacheServer)
            {
                return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
            }
            else
            {
                return go_channel_recodesManager.LoadData(ID, currDbName);
            }
        }

        /// <summary>
        /// 根据主键加载推广佣金明细实体
        /// </summary>
        public static go_channel_recodesEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_recodesManager.LoadEntity(ID, currDbName);
        }

        /// <summary>
        /// 根据条件查询推广佣金明细数据集
        /// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_recodesManager.SelectListData(where, selectFields, orderby, top, currDbName);
        }

        /// <summary>
        /// 根据条件查询推广佣金明细首行首列
        /// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_recodesManager.SelectScalar(where, selectFields, orderby, currDbName);
        }


        /// <summary>
        /// 根据服务商编号获取佣金金额
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectSumMoney(int uid, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_recodesManager.SelectSumMoney(uid, currDbName);
        }

       

        /// <summary>
        /// 根据充值记录添加渠道商或服务商佣金记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rechargemoney"></param>
        /// <param name="currDbName"></param>
        public static void AddRecodes(string OrderId, int uid, decimal rechargemoney, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {

            go_channel_recodesManager.AddRecodes(OrderId,uid, rechargemoney, currDbName);
        }
        /// <summary>
        /// 获取所有服务商或者渠道商佣金
        /// </summary>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectAllMoney(string from, string where = null, string channel = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_recodesManager.SelectAllMoney(from, where,channel, currDbName);
        }

        /// <summary>
        /// 获取所有给服务商或者渠道商的充值金额
        /// </summary>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectAllrechargemoney(string from, string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_recodesManager.SelectAllrechargemoney(from, where, currDbName);
        }
        /// <summary>
        /// 根据条件查询推广佣金明细数据实体
        /// </summary>
        public static IList<go_channel_recodesEntity> GetListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_recodesManager.SelectListEntity(where, selectFields, orderby, top, currDbName);
        }

        /// <summary>
        /// 根据主键删除推广佣金明细
        /// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            go_channel_recodesManager.Del(ID, currDbName);
        }

        /// <summary>
        /// 根据条件删除推广佣金明细
        /// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            go_channel_recodesManager.DelListData(where, currDbName);
        }

        /// <summary>
        /// 保存推广佣金明细
        /// </summary>
        public static bool SaveEntity(go_channel_recodesEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_recodesManager.SaveEntity(entity, isAdd, currDbName);
        }

    }
}
