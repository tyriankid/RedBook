using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class go_orderQuery : Pagination
    {
        public string keyword { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public string order { get; set; }
        public string UserInfo { get; set; }
        public string ProductInfo { get; set; }

        public string user { get; set; }
        public string product { get; set; }
        public string iswon { get; set; }
       
        public string StarTime { get; set; }
        public string EndTime { get; set; }
    }
}