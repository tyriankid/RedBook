using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Entity
{
    public class ReplyInfo
    {
        public ReplyInfo()
        {
            this.LastEditDate = DateTime.Now;
            this.MatchType =MatchType.Like;
            this.MessageType =MessageType.Text;
        }

        public int ActivityId { get; set; }

        public int Id { get; set; }

        public bool IsDisable { get; set; }

        public string Keys { get; set; }

        public DateTime LastEditDate { get; set; }

        public string LastEditor { get; set; }

        public YH.Weixin.MP.Entity.MatchType MatchType { get; set; }

        public YH.Weixin.MP.Entity.MessageType MessageType { get; set; }

        public string MessageTypeName
        {
            get
            {
                return this.MessageType.ToShowText();
            }
        }

        public YH.Weixin.MP.Entity.ReplyType ReplyType { get; set; }
    }
}
