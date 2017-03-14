using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
	/// 运营活动-业务操作类
	/// </summary>
	public class go_activityBusiness
	{
		/// <summary>
		/// 根据主键加载运营活动数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
                return go_activityManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载运营活动实体
		/// </summary>
        public static go_activityEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_activityManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询运营活动数据集
		/// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_activityManager.SelectListData(where, selectFields, orderby, top, currDbName);
		}

		/// <summary>
		/// 根据条件查询运营活动首行首列
		/// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_activityManager.SelectScalar(where, selectFields, orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询运营活动数据实体
		/// </summary>
        public static IList<go_activityEntity> GetListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_activityManager.SelectListEntity(where, selectFields, orderby, top, currDbName);
		}

		/// <summary>
		/// 根据主键删除运营活动
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_activityManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除运营活动
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_activityManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存运营活动
		/// </summary>
        public static bool SaveEntity(go_activityEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_activityManager.SaveEntity(entity, isAdd, currDbName);
		}
        /// <summary>
        /// 判断是否在活动时间内
        /// </summary>
        public static bool valActivityDate(string aid)
        {
            go_activityEntity ae = go_activityBusiness.LoadEntity(int.Parse(aid));
            JArray timespans = JsonConvert.DeserializeObject<JArray>(ae.Timespans.ToString());
            //判断是否在活动时间内start
            if (timespans != null)
            {
                for (int i = 0; i < timespans.Count; i++)
                {

                    if (DateTime.Now > DateTime.Parse(timespans[i]["starttime"].ToString()) && DateTime.Now < DateTime.Parse(timespans[i]["endtime"].ToString()))
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }
            return false;
        }
	}
}
