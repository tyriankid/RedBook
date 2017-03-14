using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Entity
{

    public enum RequestMsgType
    {
        Text,
        Image,
        Voice,
        Video,
        Location,
        Link,
        Event,
        transfer_customer_service
    }
}
