using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taotaole.Model
{
    public class NewsMsgInfo
    {
        public string Content { get; set; }

        public string Description { get; set; }

        public int Articleid { get; set; }

        public string Imgurl { get; set; }

        public NewsReplyInfo Reply { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}
