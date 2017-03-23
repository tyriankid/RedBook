using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Utility
{
    /// <summary>
    /// 用户的动态捕捉器
    /// </summary>
    public class UserDynamic
    {
        public DateTime AddTime { get; set; }

        public int UserId { get; set; }

        public string DynamicType { get; set; }

        public string BusinessId { get; set; }


        /// <summary>
        /// 回复类型枚举 Book：对文章进行的评论；Comment：对评论进行的评论
        /// </summary>
        public enum commentType
        {
            Book = 1,
            Comment = 2
        }

        /// <summary>
        /// 用户的点赞类型名 Book 或者Comment
        /// </summary>
        public enum fabulousType
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
