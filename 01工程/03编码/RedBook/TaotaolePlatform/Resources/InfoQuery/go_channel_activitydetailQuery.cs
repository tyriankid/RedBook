using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class go_channel_activitydetailQuery : Pagination
    {
        public int username { get; set; }
        public int unexchangenumber { get; set; }
        public string realname { get; set; }
        public string createtime { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public int exchange { get; set; }
        public string keyword { get; set; }
    }
}