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
    /// 渠道推广用户-业务操作类
    /// </summary>
    public class go_channel_userBusiness
    {
        /// <summary>
        /// 根据主键加载渠道推广用户数据集
        /// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (Globals.GetMasterSettings().OpenCacheServer)
            {
                return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
            }
            else
            {
                return go_channel_userManager.LoadData(ID, currDbName);
            }
        }

        /// <summary>
        /// 根据主键加载渠道推广用户实体
        /// </summary>
        public static go_channel_userEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_userManager.LoadEntity(ID, currDbName);
        }

        /// <summary>
        /// 根据条件查询渠道推广用户数据集
        /// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_userManager.SelectListData(where, selectFields, orderby, top, currDbName);
        }

        /// <summary>
        /// 根据服务商编号获取渠道商数量
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectChannelCount(int uid, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {

            return go_channel_userManager.SelectChannelCount(uid, currDbName);
        }

        /// <summary>
        /// 根据上级编号获取城市服务商名
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectChannelName(int parentid, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_userManager.SelectChannelName(parentid, currDbName);
        }

        /// <summary>
        /// 获取所有服务商推广用户
        /// </summary>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectAllUserCount(string from, string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_userManager.SelectAllUserCount(  from,  where,currDbName);
        }

        /// <summary>
        /// 获取所有服务商推广充值
        /// </summary>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static string SelectAllUserMoney(string from, string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_userManager.SelectAllUserMoney(from, where, currDbName);
        }
        /// <summary>
        /// 根据条件查询渠道推广用户首行首列
        /// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_userManager.SelectScalar(where, selectFields, orderby, currDbName);
        }

        /// <summary>
        /// 根据条件查询渠道推广用户数据实体
        /// </summary>
        public static IList<go_channel_userEntity> GetListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_channel_userManager.SelectListEntity(where, selectFields, orderby, top, currDbName);
        }

        /// <summary>
        /// 根据主键删除渠道推广用户
        /// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            go_channel_userManager.Del(ID, currDbName);
        }

        /// <summary>
        /// 根据条件删除渠道推广用户
        /// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            go_channel_userManager.DelListData(where, currDbName);
        }

        /// <summary>
        /// 保存渠道推广用户
        /// </summary>
        public static bool SaveEntity(go_channel_userEntity entity, bool isAdd)
        {
            return go_channel_userManager.SaveEntity(entity, isAdd);
        }

        /// <summary>
        /// 增加推广用户数
        /// </summary>
        /// <param name="uid">推广人ID</param>
        /// <param name="type">推广人类型(0渠道商 1城市服务商)</param>
        public static void AddUsercount(int uid, int type = 0)
        {
            if (uid == 0) return;
            go_channel_userEntity user = go_channel_userBusiness.LoadEntity(uid);
            if (user == null) return;
            switch (type)
            { 
                case 0:
                    user.Usercount += 1;
                    go_channel_userEntity userParent = go_channel_userBusiness.LoadEntity(user.Parentid);
                    if (userParent != null)
                    {
                        userParent.Usercount += 1;
                        go_channel_userBusiness.SaveEntity(userParent, false);
                    }
                    break;
                case 1:
                    user.Usercount += 1;
                    break;
            }
            go_channel_userBusiness.SaveEntity(user, false);//对应更改渠道推广用户数
        }
    }
}
