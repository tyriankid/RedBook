using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Entity;

namespace YH.Weixin.MP.Request
{
    public class TextRequest : AbstractRequest
    {
        public string Content { get; set; }
    }
}
