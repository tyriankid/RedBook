using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taotaole.Model;

namespace RedBookPlatform.Resources.InfoQuery
{
    public class RB_Book_CategoryQuery : Pagination
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public DateTime Addtime { get; set; }
        public int SortBaseNum { get; set; }
    }

    public class RB_BookQuery : Pagination
    {
        public Guid Id { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Keyword { get; set; }
        public int SortBaseNum { get; set; }
    }
    

}