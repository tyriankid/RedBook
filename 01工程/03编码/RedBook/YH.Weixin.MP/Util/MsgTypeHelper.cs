using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using YH.Weixin.MP.Entity;

namespace YH.Weixin.MP.Util
{
    public static class MsgTypeHelper
    {
        public static RequestMsgType GetMsgType(string str)
        {
            return (RequestMsgType) Enum.Parse(typeof(RequestMsgType), str, true);
        }

        public static RequestMsgType GetMsgType(XDocument doc)
        {
            return GetMsgType(doc.Root.Element("MsgType").Value);
        }
    }
}
