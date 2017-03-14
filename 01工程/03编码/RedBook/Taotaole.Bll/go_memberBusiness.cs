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
	/// 会员-业务操作类
	/// </summary>
	public class go_memberBusiness
	{
		/// <summary>
		/// 根据主键加载会员数据集
		/// </summary>
        public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
                return go_memberManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载会员实体
		/// </summary>
        public static go_memberEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_memberManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询会员数据集
		/// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_memberManager.SelectListData(where, selectFields, orderby, top, currDbName);
		}

		/// <summary>
		/// 根据条件查询会员首行首列
		/// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_memberManager.SelectScalar(where, selectFields, orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询会员数据实体
		/// </summary>
        public static IList<go_memberEntity> GetListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_memberManager.SelectListEntity(where, selectFields, orderby, top, currDbName);
		}

		/// <summary>
		/// 根据主键删除会员
		/// </summary>
        public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_memberManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除会员
		/// </summary>
        public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            go_memberManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存会员
		/// </summary>
        public static bool SaveEntity(go_memberEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
            return go_memberManager.SaveEntity(entity, isAdd, currDbName);
        }

        public static void UpdateLogin(go_memberEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            go_memberManager.UpdateLogin(entity, currDbName);
        }

        /// <summary>
        /// 更新支付OPENID
        /// </summary>
        public static void UpdateBand(go_memberEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            go_memberManager.UpdateBand(entity, currDbName);
        }

        public static go_memberEntity GetCurrentMember()
        {
            return GetMember(Globals.GetCurrentMemberUserId());
        }

        public static go_memberEntity GetMember(int userId, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            go_memberEntity member = go_memberManager.LoadEntity(userId, currDbName);
            return member;
        }

        public static bool UpdateScoreInfo(decimal score,decimal  money,int uid, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return go_memberManager.UpdateScoreInfo(score, money, uid, currDbName);
        }

        /// <summary>
        /// 充值成功后,更新对应的充值信息,并更新用户余额
        /// </summary>
        /// <returns></returns>
        public static bool SaveMemberRechargeInfo(int uid, go_member_addmoney_recordEntity entity, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (go_member_addmoney_recordManager.SaveEntity(entity, false, currDbName))//更新充值信息
            {
                if (entity.Memo == "tuan")
                {
                    return go_memberManager.AddMemberTuanRechargeInfo(uid, entity, currDbName);//更新用户团购余额
                }
                else
                {
                    return go_memberManager.AddMemberRechargeInfo(uid, entity, currDbName);//更新用户余额
                }
                
            }
            return false;
        }


	}
}
