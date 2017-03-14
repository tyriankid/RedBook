using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class go_zengDetailQuery : Pagination
    {
        public string keyword { get; set; }
        public string StarTime { get; set; }
        public string EndTime { get; set; }
    }
}