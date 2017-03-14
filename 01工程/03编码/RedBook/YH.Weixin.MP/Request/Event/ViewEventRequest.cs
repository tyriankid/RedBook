using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Enums;

namespace YH.Weixin.MP.Request.Event
{
    public class ViewEventRequest : EventRequest
    {
        public override RequestEventType Event
        {
            get
            {
                return RequestEventType.VIEW;
            }
            set
            {
            }
        }

        public string EventKey { get; set; }

        //public string Encrypt { get; set; }
    }
}
