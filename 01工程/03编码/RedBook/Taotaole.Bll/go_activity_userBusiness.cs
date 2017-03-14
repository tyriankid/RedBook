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
    /// -业务操作类
    /// </summary>
    public class go_activity_userBusiness
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
                return go_activity_userManager.LoadData(ID);
            }
        }

        /// <summary>
        /// 根据主键加载实体
        /// </summary>
        public static go_activity_userEntity LoadEntity(int ID)
        {
            return go_activity_userManager.LoadEntity(ID);
        }

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            return go_activity_userManager.SelectListData(where, selectFields, orderby, top);
        }

        /// <summary>
        /// 根据条件查询首行首列
        /// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null)
        {
            return go_activity_userManager.SelectScalar(where, selectFields, orderby);
        }

        /// <summary>
        /// 根据条件查询数据实体
        /// </summary>
        public static IList<go_activity_userEntity> GetListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            return go_activity_userManager.SelectListEntity(where, selectFields, orderby, top);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        public static void Del(int ID)
        {
            go_activity_userManager.Del(ID);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        public static void DelListData(string where = null)
        {
            go_activity_userManager.DelListData(where);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public static bool SaveEntity(go_activity_userEntity entity, bool isAdd)
        {
            return go_activity_userManager.SaveEntity(entity, isAdd);
        }
         /// <summary>
        /// 获取活动资格
        /// </summary>
        public static int Qualification(string uid,string aid)
        {
            string where = "Where 1=1 AND activity_id=" + aid + " And uid=" + uid;
            string from = "go_activity_user";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            //Globals.DebugLogger("count" + count);
            if (count > 0)
            {
                   //已有该活动资格，无法重复领取
                return 3;
            }
            go_activityEntity activityEntity = go_activityBusiness.LoadEntity(int.Parse(aid));
            go_activity_userEntity userEntity = new go_activity_userEntity();
            userEntity.Activity_id = int.Parse(aid);
            userEntity.A_type = 0;
            userEntity.Channel_id = 0;
            userEntity.Uid = uid;
            userEntity.Mobile = "";
            userEntity.Count = activityEntity.Count;
            userEntity.Alreadycount = 0;
            userEntity.Alreadysentcount = 0;
            userEntity.Sentcount = 0;
            userEntity.Starttime = activityEntity.Starttime;
            userEntity.Endtime = activityEntity.Endtime;
            userEntity.Raisetime = 0;
            userEntity.Use_range = activityEntity.Use_range;
            userEntity.Amount = activityEntity.Amount;
            userEntity.Addtime = DateTime.Now;
            userEntity.Redpackday = activityEntity.Redpackday;
            bool res = go_activity_userBusiness.SaveEntity(userEntity, true);
            if (res)
            {
                return 0;    //领取成功

            }
            else
            {
                return 2;   //领取失败
            }
        }

        /// <summary>
        /// 判断用户是否有活动资格
        /// </summary>
        public static bool valQualification(string uid, string aid)
        {
            string where = "Where 1=1 AND activity_id=" + aid + " And uid=" + uid;
            string from = "go_activity_user";
            int count = CustomsBusiness.GetDataCount(from, where, DbServers.DbServerName.LatestDB);
            if (count > 0)
            {
                //已有该活动资格，无法重复领取
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断用户是否有活动资格并未达到参与上限
        /// </summary>
        public static bool valQualificationCount(string uid, string aid)
        {
            IList<go_activity_userEntity> ilUser = go_activity_userBusiness.GetListEntity("uid=" + uid + " and activity_id=" + aid, "*");
            if (ilUser.Count == 0)
            {
                //未领取资格
                return false;
            }
            go_activity_userEntity eUser = ilUser[0];
            go_activityEntity aEntity = go_activityBusiness.LoadEntity(int.Parse(aid));
            if (aEntity.Count != 0 && eUser.Alreadycount >= aEntity.Count)
            {
                //已达到参与次数上限
                return false;
            }
                //未达到参与上限
                return true;
           
        }
    }
}
