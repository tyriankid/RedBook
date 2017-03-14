using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class go_tuangouQuery : Pagination
    {
        public string keyword { get; set; }
        public string  title { get; set; }
        public int tId { get; set; }
        public DateTime time { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
    }
}