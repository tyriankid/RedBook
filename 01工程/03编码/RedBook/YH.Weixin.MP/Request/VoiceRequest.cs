using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Entity;

namespace YH.Weixin.MP.Request
{
    public class VoiceRequest : AbstractRequest
    {
        public string Format { get; set; }

        public int MediaId { get; set; }
    }
}
