using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Enums;

namespace YH.Weixin.MP.Request.Event
{
    public class UnSubscribeEventRequest : EventRequest
    {
        public override RequestEventType Event
        {
            get
            {
                return RequestEventType.UnSubscribe;
            }
            set
            {
            }
        }
    }
}
