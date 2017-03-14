using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Entity
{
    public enum MessageType
    {
        [EnumShowText("多图文")]
        List = 4,
        [EnumShowText("单图文")]
        News = 2,
        [EnumShowText("文本")]
        Text = 1
    }
}
