using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class go_adminQuery : Pagination
    {
        public int uid { get; set; }
        public string keyword { get; set; }
        public string status { get; set; }
        public string StarTime{ get; set; }
        public string EndTime{ get; set; }
        public string type { get; set; }

    }


    public class go_adminLogsQuery : Pagination
    {
       
        public string keyword { get; set; }
        public string action { get; set; }
        public string StarTime { get; set; }
        public string EndTime { get; set; }
        public string Username { get; set; }

    }
}