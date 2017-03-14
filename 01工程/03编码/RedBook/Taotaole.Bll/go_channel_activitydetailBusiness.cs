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
    public class go_channel_activitydetailBusiness
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
                return go_channel_activitydetailManager.LoadData(ID);
            }
        }

        /// <summary>
        /// 根据主键加载实体
        /// </summary>
        public static go_channel_activitydetailEntity LoadEntity(int ID)
        {
            return go_channel_activitydetailManager.LoadEntity(ID);
        }

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable GetListData(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            return go_channel_activitydetailManager.SelectListData(where, selectFields, orderby, top);
        }

        /// <summary>
        /// 根据渠道商编号更改兑换状态
        /// </summary>
        /// <param name="cuid"></param>
        public static void UpdateApplyExchange(int cuid)
        {
            go_channel_activitydetailManager.UpdateApplyExchange(cuid);
        }

        /// <summary>
        /// 根据渠道商编号更改兑换状态
        /// </summary>
        /// <param name="cuid"></param>
        public static void UpdateExchange(int cuid,string orderid)
        {
            go_channel_activitydetailManager.UpdateExchange(cuid,orderid);
        }
        /// <summary>
        /// 增加网吧渠道商记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="shopid"></param>
        /// <param name="currDbName"></param>
        public static void AddActivityDetails(int uid, int shopid,string orderid=null,  DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            //用户的所属渠道商是网吧且未参与此活动
            go_memberEntity memberEntity = go_memberManager.LoadEntity(uid);
            if (memberEntity == null || memberEntity.Servicechannelid == 0) return;

            go_channel_userEntity channel_userEntity = go_channel_userManager.LoadEntity(memberEntity.Servicechannelid);
            if (channel_userEntity == null || channel_userEntity.Typeid != 1) return;

            int count = int.Parse(go_channel_activitydetailManager.SelectScalar(string.Format("uid={0}", uid), "count(*)").ToString());
            if (count > 0) return;
            go_channel_activitydetailManager.AddActivityDetails(uid, shopid,ChannelActivity.Internet_Bar_Activity, currDbName);
            CustomsBusiness.ExecuteSql("update  go_orders set recordcode='W'  where  orderId='" + orderid + "'");
        }
        /// <summary>
        /// 根据条件查询首行首列
        /// </summary>
        public static object GetScalar(string where = null, string selectFields = "*", string orderby = null)
        {
            return go_channel_activitydetailManager.SelectScalar(where, selectFields, orderby);
        }

        /// <summary>
        /// 根据条件查询数据实体
        /// </summary>
        public static IList<go_channel_activitydetailEntity> GetListEntity(string where = null, string selectFields = "*", string orderby = null, int top = 0)
        {
            return go_channel_activitydetailManager.SelectListEntity(where, selectFields, orderby, top);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        public static void Del(int ID)
        {
            go_channel_activitydetailManager.Del(ID);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        public static void DelListData(string where = null)
        {
            go_channel_activitydetailManager.DelListData(where);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public static bool SaveEntity(go_channel_activitydetailEntity entity, bool isAdd)
        {
            return go_channel_activitydetailManager.SaveEntity(entity, isAdd);
        }

         public static bool Modify(go_channel_activitydetailEntity entity)
        {
            return go_channel_activitydetailManager.Modify(entity);
        }
        

    }
}
