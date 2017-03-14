using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class go_channellistQuery : Pagination
    {
        public int uid { get; set; }
        public string realname { get; set; }
        public string contacts { get; set; }
        public string usermobile { get; set; }
        public string useremail { get; set; }
        public string keyword { get; set; }
        public string type { get; set; }
        public string order { get; set; }
        
    }

    public class go_channelrecodesQuery : Pagination
    {
        public int uid { get; set; }
        public int cid { get; set; }
        public char time { get; set; }
        public int state { get; set; }
        public decimal money { get; set; }
        public decimal rechargemoney { get; set; }
        public string keyword { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
    }

    public class go_channelcashoutQuery : Pagination
    {
        public string username { get; set; }
        public string bankname { get; set; }
        public string branch { get; set; }
        public decimal money { get; set; }
        public char reviewtime { get; set; }
        public string banknumber { get; set; }
        public string linkphone { get; set; }
        public string keyword { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string auditstatus { get; set; }
    }
}