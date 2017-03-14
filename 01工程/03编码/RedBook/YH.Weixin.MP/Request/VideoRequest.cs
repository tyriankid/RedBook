using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Entity;

namespace YH.Weixin.MP.Request
{
    public class VideoRequest : AbstractRequest
    {
        public int MediaId { get; set; }

        public int ThumbMediaId { get; set; }
    }
}
