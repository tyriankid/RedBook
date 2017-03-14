using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Taotaole.Common;
using Taotaole.Dal;
using Taotaole.Model;

namespace Taotaole.Bll
{
	/// <summary>
	/// 微信自定义回复-业务操作类
	/// </summary>
	public class go_wxreplyBusiness
	{
		/// <summary>
		/// 根据主键加载微信自定义回复数据集
		/// </summary>
		public static DataTable LoadData(int ID)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_wxreplyManager.LoadData(ID);
			}
		}

		/// <summary>
		/// 根据主键加载微信自定义回复实体
		/// </summary>
		public static go_wxreplyEntity LoadEntity(int ID)
		{
				return go_wxreplyManager.LoadEntity(ID);
		}

		/// <summary>
		/// 根据条件查询微信自定义回复数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*")
		{
			return go_wxreplyManager.SelectListData(where,selectFields);
		}

		/// <summary>
		/// 根据条件查询微信自定义回复首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*")
		{
			return go_wxreplyManager.SelectScalar(where,selectFields);
		}

		/// <summary>
		/// 根据条件查询微信自定义回复数据实体
		/// </summary>
		public static IList<go_wxreplyEntity> GetListEntity(string where = null)
		{
				return go_wxreplyManager.SelectListEntity(where);
		}

		/// <summary>
		/// 根据主键删除微信自定义回复
		/// </summary>
		public static void Del(int ID)
		{
			go_wxreplyManager.Del(ID);
		}

		/// <summary>
		/// 根据条件删除微信自定义回复
		/// </summary>
		public static void DelListData(string where = null)
		{
			go_wxreplyManager.DelListData(where);
		}

		/// <summary>
		/// 保存微信自定义回复
		/// </summary>
		public static bool SaveEntity(go_wxreplyEntity entity, bool isAdd)
		{
			return go_wxreplyManager.SaveEntity(entity, isAdd);
		}

        public static bool HasReplyKeyword(string keyword)
        {
            return go_wxreplyManager.HasReplyKeyword(keyword);
        }

        /// <summary>
        /// 保存reply信息,包括单图文和多图文
        /// </summary>
        public static bool SaveReplyInfo(go_wxreplyEntity reply)
        {
            return go_wxreplyManager.SaveReplyInfo(reply);
        }

        /// <summary>
        /// 保存reply信息,包括单图文和多图文
        /// </summary>
        public static bool UpdateReplyInfo(go_wxreplyEntity reply)
        {
            return go_wxreplyManager.UpdateReplyInfo(reply);
        }

        /// <summary>
        /// 判断关注时回复是否已经存在
        /// </summary>
        /// <returns>replyid</returns>
        public static bool isSubscribeReplyExist(int replyid)
        {
            IList<go_wxreplyEntity> list = go_wxreplyManager.SelectListEntity("replytype = 2 and isdisable = 0");
            if (list.Count > 0 && list[0].Replyid != replyid)//如果存在关注时回复并不等于当前replyid,返回true
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断无匹配回复是否已经存在,返回存在的id,如果当前页面id正好是被占用的id,可以不用隐藏
        /// </summary>
        /// <returns>replyid</returns>
        public static bool isMismatchReplyExist(int replyid)
        {
            IList<go_wxreplyEntity> list = go_wxreplyManager.SelectListEntity("replytype = 3 and isdisable = 0");
            if (list.Count > 0 && list[0].Replyid != replyid)//如果存在无匹配回复并不等于当前replyid,返回true
            {
                return true;
            }
            else
            {
                return false;
            }
        }

	}
}
