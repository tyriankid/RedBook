using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class go_yolly_orderinfoQuery : Pagination
    {
        public string serialid { get; set; }
        public string keyword { get; set; }
        public string status { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public string StarTime { get; set; }
        public string EndTime { get; set; }
    }
}