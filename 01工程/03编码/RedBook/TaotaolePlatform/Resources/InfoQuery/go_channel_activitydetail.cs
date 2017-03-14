using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace TaotaolePlatform.Resources.InfoQuery
{
    public class go_channel_activitydetail : Pagination
    {
        public int adid { get; set; }
        public string atitle { get; set; }
        public string createtime { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
    }
}