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
    /// -业务操作类
    /// </summary>
    public class go_slidesBusiness
    {
        /// <summary>
        /// 根据主键加载数据集
        /// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (Globals.GetMasterSettings().OpenCacheServer)
            {
                return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
            }
            else
            {
                return go_slidesManager.LoadData(ID, currDbName);
            }
        }

        /// <summary>
        /// 根据主键加载实体
        /// </summary>
        public static go_slidesEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_slidesManager.LoadEntity(ID, currDbName);
        }





        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_slidesManager.SelectListData(where, selectFields, orderby, top, currDbName);
        }

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable getapp(int id, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_slidesManager.getapp(id, currDbName);

        }

        public static DataTable getapplist(int slidetype, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_slidesManager.getapplist(slidetype, currDbName);
        }

        /// <summary>
        /// 根据条件查询首行首列
        /// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_slidesManager.SelectScalar(where, selectFields, orderby, currDbName);
        }

        /// <summary>
        /// 根据条件查询数据实体
        /// </summary>
        public static IList<go_slidesEntity> GetListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_slidesManager.SelectListEntity(where, selectFields, orderby, top, currDbName);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            go_slidesManager.Del(ID, currDbName);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            go_slidesManager.DelListData(where, currDbName);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static bool UpdateEntity(go_slidesEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_slidesManager.UpdateEntity(entity, currDbName);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public static bool SaveEntity(go_slidesEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_slidesManager.SaveEntity(entity, isAdd, currDbName);
        }

    }
}
