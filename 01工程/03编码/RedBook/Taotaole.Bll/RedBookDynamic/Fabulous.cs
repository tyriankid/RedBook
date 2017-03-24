using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Taotaole.Model;

namespace Taotaole.Bll
{
    public class Fabulous:UserDynamic
    {

        /// <summary>
        /// 用户的点赞类型名 Book 或者Comment
        /// </summary>
        public enum fabulousType
        {
            Book = 1,
            Comment = 2
        }



        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool goFabulous(RB_User_FabulousEntity entity)
        {
            bool flag = false;
            bool isAdd = false;
            //自动判断是关注还是取消
            DataTable existEntity = RB_User_FabulousBusiness.GetListData(" BusinessId = '" + entity.BusinessId + "' and UserId = "+entity.UserId+" ","Id,Status");//判断当前用户在当前文章下面是否点赞过

            //有数据,分为点赞过,或取消了点赞
            if (existEntity.Rows.Count > 0) 
            {
                entity.Id = new Guid(existEntity.Rows[0]["Id"].ToString());
                entity.Status = existEntity.Rows[0]["Status"].ToString() == "0" ? 1 : 0;
                isAdd = false;
            }
            else//没数据,则为没有点赞过,增加一条点赞数据
            {
                entity.Status = 0;
                isAdd = true;
            }
            flag = RB_User_FabulousBusiness.SaveEntity(entity, isAdd);

            return flag;
        }
        /// <summary>
        /// 获取当前点赞总数,并增加被点赞对象的点赞数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int addCount(string businessType)
        {
            int result = 0;

            return result;
        }



    }
}
