using System;
using Taotaole.Model;

namespace Taotaole.Bll
{
    /// <summary>
    /// 用户的动态捕捉器
    /// </summary>
    public class UserDynamic
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private string _dynamicType;
        public string DynamicType
        {
            get { return _dynamicType; }
            set { _dynamicType = value; }
        }
        private string _businessId;
        public string BusinessId
        {
            get { return _businessId; }
            set { _businessId = value; }
        }
        private DateTime _addTime;
        public DateTime AddTime
        {
            get { return _addTime; }
            set { _addTime = value; }
        }
        private string _businessType;
        public string BusinessType
        {
            get { return _businessType; }
            set { _businessType = value; }
        }

        public UserDynamic()
        {
            //本身的处理
            RB_User_DynamicEntity DE = new RB_User_DynamicEntity()
            {
                Id = Id,
                UserId = UserId,
                DynamicType = DynamicType ,
                BusinessId = BusinessId,
                AddTime = DateTime.Now
            };
            RB_User_DynamicBusiness.SaveEntity(DE, true);
        }


        /// <summary>
        /// 根据当前
        /// </summary>
        public bool Execute()
        {
            bool flag = false;
            switch (DynamicType)
            {
                case "Fabulous":
                    goFabulous();
                    break;
            }
            
            return flag;
        }

        private string goFabulous()
        {
            string result = "";
            RB_User_FabulousEntity rfEntity = new RB_User_FabulousEntity()
            {
                Id = Guid.NewGuid(),
                UserId = UserId,
                BusinessId = new Guid(BusinessId),
                BusinessType = BusinessType,
                AddTime = AddTime,
            };
            if(Fabulous.goFabulous(rfEntity))//点赞之后,更新数据,并获取当前点赞总数的返回值
            {
                int fabulousCount = Fabulous.addCount(BusinessType);
            }

            return result;
        }
        

        /// <summary>
        /// 回复类型枚举 Book：对文章进行的评论；Comment：对评论进行的评论
        /// </summary>
        public enum commentType
        {
            Book = 1,
            Comment = 2
        }


        /// <summary>
        /// 用户的动态类型枚举 新建文章:Book,点赞Fabulous,分享:Share,评论:Comment,收藏:Favorite,浏览：Browse
        /// </summary>
        public enum dynamicType
        {
            CreateBook = 1,
            Fabulous = 2,
            UnFabulous = 7,
            Share = 3,
            Comment = 4,
            Favorite = 5,
            Browse = 6
        }

        /// <summary>
        /// 用户浏览类型枚举 浏览文章:Book,浏览用户主页:User
        /// </summary>
        public enum browseType
        {
            Book = 1,
            User = 2
        }



    }
}
