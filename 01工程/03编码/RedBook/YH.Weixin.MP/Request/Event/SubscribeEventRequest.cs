using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Enums;

namespace YH.Weixin.MP.Request.Event
{
    public class SubscribeEventRequest : EventRequest
    {
        public override RequestEventType Event
        {
            get
            {
                return RequestEventType.Subscribe;
            }
            set
            {
            }
        }

        public string EventKey { get; set; }

    }
}
