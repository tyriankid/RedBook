using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class go_redpackQuery : Pagination
    {
        public int id { get; set; }
        public string title { get; set; }
        public string amount { get; set; }
        public int discount { get; set; }
        public string keyword { get; set; }
    }
    public class go_userredpackQuery : Pagination
    {
        public int id { get; set; }
        public string codetitle { get; set; }
        public int uid { get; set; }
        public int discount { get; set; }
        public int amount { get; set; }
        public int order_id { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string state { get; set; }
        public string keyword { get; set; }
    }

}