using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Entity;

namespace YH.Weixin.MP.Request
{
    public class LocationRequest : AbstractRequest
    {
        public string Label { get; set; }

        public float Location_X { get; set; }

        public float Location_Y { get; set; }

        public int Scale { get; set; }
    }
}
