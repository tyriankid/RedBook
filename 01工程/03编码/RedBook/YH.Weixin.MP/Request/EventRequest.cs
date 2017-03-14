using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Entity;
using YH.Weixin.MP.Enums;

namespace YH.Weixin.MP.Request
{
    public abstract class EventRequest : AbstractRequest
    {
        protected EventRequest()
        {
        }

        public virtual RequestEventType Event { get; set; }
    }
}
