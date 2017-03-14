using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Taotaole.Business;
using Taotaole.Common;
using Taotaole.Dal;
using Taotaole.Model;
using YH.Utility;

namespace Taotaole.Bll
{
	/// <summary>
	/// 团购商品-业务操作类
	/// </summary>
	public class go_tuanBusiness
	{
		/// <summary>
		/// 根据主键加载团购商品数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_tuanManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载团购商品实体
		/// </summary>
		public static go_tuanEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_tuanManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询团购商品数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_tuanManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询团购商品首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return go_tuanManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询团购商品数据实体
		/// </summary>
		public static IList<go_tuanEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return go_tuanManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除团购商品
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_tuanManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除团购商品
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			go_tuanManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存团购商品
		/// </summary>
		public static bool SaveEntity(go_tuanEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            bool b = go_tuanManager.SaveEntity(entity, isAdd, currDbName);
            if(isAdd)
            {
                int maxTID = int.Parse(go_tuanBusiness.GetScalar("", "tId", "tId desc").ToString());
                string TableName = "" + go_yiyuanManager.GetAndRefreshCodetable(entity.Max_sell * entity.Total_num, Globals.CodeTablemMax) + "";
                /*string insertSql = string.Format(@"insert into {2} (codetype,businessid,code,status) Select 'tuan'as codetype,{0} as businessId,code,0 as status From (Select top {1} * From go_shopcode Where codetype='moban')T Order by NEWID()"
                    , maxTID, entity.Max_sell * entity.Total_num, TableName);*/
                string insertSql = string.Format(@"
                                                                    if object_id('{2}') is null
                                                                    Begin
	                                                                    select * into {2} from go_shopcode where 1=2;insert into {2} (codetype,businessid,code,status) Select 'tuan'as codetype,{0} as businessId,code,0 as status From (Select top {1} * From go_shopcode Where codetype='moban')T Order by NEWID()
                                                                    End
                                                                    else
                                                                    Begin
	                                                                    insert into {2} (codetype,businessid,code,status) Select 'tuan'as codetype,{0} as businessId,code,0 as status From (Select top {1} * From go_shopcode Where codetype='moban')T Order by NEWID()
                                                                    End"
                  , maxTID, entity.Max_sell * entity.Total_num, TableName);
                CustomsBusiness.ExecuteSql(insertSql);
                string updateSql = string.Format(@"Update go_tuan set codetable = '{0}' where tId = {1}", TableName, maxTID);
                CustomsBusiness.ExecuteSql(updateSql);
            }
            return b;
		}
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="currDbName"></param>
        /// <returns></returns>
        public static bool UpdateEntity(go_tuanEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_tuanManager.UpdateEntity(entity, currDbName);
        }

	}
}
