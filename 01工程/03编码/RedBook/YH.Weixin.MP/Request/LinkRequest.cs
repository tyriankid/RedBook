using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Entity;

namespace YH.Weixin.MP.Request
{
    public class LinkRequest : AbstractRequest
    {
        public string Description { get; set; }

        public int Title { get; set; }

        public string Url { get; set; }
    }
}
