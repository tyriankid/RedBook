﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taotaole.Model;

namespace YH.Weixin.MP.Entity
{
    public class NewsReplyInfo : go_wxreplyEntity
    {
        public NewsReplyInfo()
        {
            base.Messagetype = 3;//多图文
        }

        public IList<NewsMsgInfo> NewsMsg { get; set; }
    }
}
