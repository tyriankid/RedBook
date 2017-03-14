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
	/// -业务操作类
	/// </summary>
	public class go_wxmenuBusiness
	{
		/// <summary>
		/// 根据主键加载数据集
		/// </summary>
		public static DataTable LoadData(int ID)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return go_wxmenuManager.LoadData(ID);
			}
		}

		/// <summary>
		/// 根据主键加载实体
		/// </summary>
		public static go_wxmenuEntity LoadEntity(int ID)
		{
				return go_wxmenuManager.LoadEntity(ID);
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*")
		{
			return go_wxmenuManager.SelectListData(where,selectFields);
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*")
		{
			return go_wxmenuManager.SelectScalar(where,selectFields);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<go_wxmenuEntity> GetListEntity(string where = null)
		{
				return go_wxmenuManager.SelectListEntity(where);
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID)
		{
			go_wxmenuManager.Del(ID);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null)
		{
			go_wxmenuManager.DelListData(where);
		}

		/// <summary>
		/// 保存
		/// </summary>
		public static bool SaveEntity(go_wxmenuEntity entity, bool isAdd)
		{
			return go_wxmenuManager.SaveEntity(entity, isAdd);
		}

        public static IList<go_wxmenuEntity> GetInitMenus()
        {
            IList<go_wxmenuEntity> topMenus = go_wxmenuManager.GetTopMenus();
            foreach (go_wxmenuEntity info in topMenus)
            {
                info.Chilren = go_wxmenuManager.SelectListEntity(" parentid = " + info.Menuid);
                if (info.Chilren == null)
                {
                    info.Chilren = new List<go_wxmenuEntity>();
                }
            }
            return topMenus;
        }


	}
}
