using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Enums;

namespace YH.Weixin.MP.Request.Event
{
    public class ScanEventRequest : EventRequest
    {
        public override RequestEventType Event
        {
            get
            {
                return RequestEventType.Scan;
            }
            set
            {
            }
        }

        public string EventKey { get; set; }

        public string Ticket { get; set; }
    }
}
