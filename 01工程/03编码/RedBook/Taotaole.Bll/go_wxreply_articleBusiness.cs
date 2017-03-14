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
	/// 图文回复-业务操作类
	/// </summary>
	public class go_wxreply_articleBusiness
	{
		/// <summary>
		/// 根据主键加载图文回复数据集
		/// </summary>
		public static DataTable LoadData(int ID)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_wxreply_articleManager.LoadData(ID);
			}
		}

		/// <summary>
		/// 根据主键加载图文回复实体
		/// </summary>
		public static go_wxreply_articleEntity LoadEntity(int ID)
		{
				return go_wxreply_articleManager.LoadEntity(ID);
		}

		/// <summary>
		/// 根据条件查询图文回复数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*")
		{
			return go_wxreply_articleManager.SelectListData(where,selectFields);
		}

		/// <summary>
		/// 根据条件查询图文回复首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*")
		{
			return go_wxreply_articleManager.SelectScalar(where,selectFields);
		}

		/// <summary>
		/// 根据条件查询图文回复数据实体
		/// </summary>
		public static IList<go_wxreply_articleEntity> GetListEntity(string where = null)
		{
				return go_wxreply_articleManager.SelectListEntity(where);
		}

		/// <summary>
		/// 根据主键删除图文回复
		/// </summary>
		public static void Del(int ID)
		{
			go_wxreply_articleManager.Del(ID);
		}

		/// <summary>
		/// 根据条件删除图文回复
		/// </summary>
		public static void DelListData(string where = null)
		{
			go_wxreply_articleManager.DelListData(where);
		}

		/// <summary>
		/// 保存图文回复
		/// </summary>
		public static bool SaveEntity(go_wxreply_articleEntity entity, bool isAdd)
		{
			return go_wxreply_articleManager.SaveEntity(entity, isAdd);
		}

	}
}
