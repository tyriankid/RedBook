using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class baiducfgQuery: Pagination
    {
        public string cfgid { get; set; }
        public string isdelete { get; set; }
        public string keyword { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string title { get; set; }
    }
}