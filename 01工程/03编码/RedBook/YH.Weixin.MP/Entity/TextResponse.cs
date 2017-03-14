using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Entity
{
    public class TextResponse : AbstractResponse
    {
        public string Content { get; set; }

        public override ResponseMsgType MsgType
        {
            get
            {
                return ResponseMsgType.Text;
            }
        }
    }
}
