using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Entity
{
    public class AbstractRequest
    {
        public DateTime CreateTime { get; set; }

        public string FromUserName { get; set; }

        public long MsgId { get; set; }

        public RequestMsgType MsgType { get; set; }

        public string ToUserName { get; set; }
    }
}
