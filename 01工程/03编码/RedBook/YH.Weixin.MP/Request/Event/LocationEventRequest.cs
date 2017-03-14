using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Enums;

namespace YH.Weixin.MP.Request.Event
{
    /// <summary>
    /// 定位请求
    /// </summary>
    public class LocationEventRequest : EventRequest
    {


        public override RequestEventType Event
        {

            get
            {
                return RequestEventType.Location;
            }
            set { }

        }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public float Precision { get; set; }


    }
}
