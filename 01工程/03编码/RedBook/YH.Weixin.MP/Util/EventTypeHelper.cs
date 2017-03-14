using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using YH.Weixin.MP.Enums;

namespace YH.Weixin.MP.Util
{
    public static class EventTypeHelper
    {
        public static RequestEventType GetEventType(string str)
        {
            return (RequestEventType)Enum.Parse(typeof(RequestEventType), str, true);
        }

        public static RequestEventType GetEventType(XDocument doc)
        {
            return GetEventType(doc.Root.Element("Event").Value);
        }
    }
}
