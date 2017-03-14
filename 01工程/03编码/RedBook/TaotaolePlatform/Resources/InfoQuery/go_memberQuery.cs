using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class go_memberQuery : Pagination
    {
        public string username { get; set; }
        public string mobile { get; set; }
        public string keyword { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
    }
}