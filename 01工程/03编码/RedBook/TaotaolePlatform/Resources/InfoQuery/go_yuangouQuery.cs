using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class go_yuangouQuery : Pagination
    {
        public string keyword { get; set; }
        public int yid { get; set; }
        public string title { get; set; }
        public int qishu { get; set; }
        public DateTime time { get; set; }
        public int q_uid { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public string multiple { get; set; }
    }
}