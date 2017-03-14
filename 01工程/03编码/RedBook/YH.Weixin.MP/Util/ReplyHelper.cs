using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taotaole.Model;
using YH.Weixin.MP.Entity;

namespace YH.Weixin.MP.Util
{
    public class ReplyHelper
    {

        public static IList<ReplyInfo> GetReplies(ReplyType type)
        {
            //return new ReplyDao().GetReplies(type);
            return null;
        }

        public static ReplyInfo GetReply(int id)
        {
            //return new ReplyDao().GetReply(id);
            return null;
        }

        public static ReplyInfo GetSubscribeReply()
        {
            /*IList<ReplyInfo> replies = new ReplyDao().GetReplies(ReplyType.Subscribe);
            if ((replies != null) && (replies.Count > 0))
            {
                return replies[0];
            }*/
            return null;
        }

        public static bool HasReplyKey(string key)
        {
            //return new ReplyDao().HasReplyKey(key);
            return false;
        }

        public static bool SaveReply(ReplyInfo reply)
        {
            /*reply.LastEditDate = DateTime.Now;
            reply.LastEditor = ManagerHelper.GetCurrentManager().UserName;
            return new ReplyDao().SaveReply(reply);*/
            return false;
        }

        public static bool UpdateReply(ReplyInfo reply)
        {
            /*reply.LastEditDate = DateTime.Now;
            reply.LastEditor = ManagerHelper.GetCurrentManager().UserName;
            return new ReplyDao().UpdateReply(reply);*/
            return false;
        }

        public static bool UpdateReplyRelease(int id)
        {
            /*return new ReplyDao().UpdateReplyRelease(id);*/
            return false;
        }
    }
}
