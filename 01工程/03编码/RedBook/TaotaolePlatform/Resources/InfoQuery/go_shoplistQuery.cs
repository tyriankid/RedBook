using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class go_productsQuery : Pagination
    {
        public int id { get; set; }
        public string title { get; set; }
        public string title2 { get; set; }
        public int categoryid { get; set; }
        public int brandid { get; set; }
        public string keyword { get; set; }
        public string time { get; set; }
        public string State { get; set; }

    }
    public class SlidesWechatQuery : Pagination
    {
        public int slideid { get; set; }
        public string slideorder { get; set; }
    }

    public class go_articleQuery : Pagination
    {
        public int id { get; set; }
        public string title { get; set; }
        public string categoryid { get; set; }
        public string keyword { get; set; }
        public string order { get; set; }
    }

    public class go_categoryQuery : Pagination
    {
        public int cateid { get; set; }
        public string name { get; set; }
        public string catdir { get; set; }
        public string keyword { get; set; }
        public string model { get; set; }
        public string order { get; set; }
        
    }
    public class go_brandQuery : Pagination
    {
        public int id { get; set; }
        public string name { get; set; }
        public string keyword { get; set; }
        public string order { get; set; }
    }


    public class go_cardRechargeQuery : Pagination
    {
        public string code { get; set; }
        public decimal money { get; set; }
        public string username { get; set; }
        public int isrepeat { get; set; }
        public int usetype { get; set; }
        public string orderId { get; set; }
    }

}