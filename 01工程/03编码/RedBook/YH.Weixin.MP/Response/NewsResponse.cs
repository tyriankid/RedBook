using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.Weixin.MP.Domain;
using YH.Weixin.MP.Entity;

namespace YH.Weixin.MP.Response
{
    public class NewsResponse : AbstractResponse
    {
        public int ArticleCount
        {
            get
            {
                return ((this.Articles == null) ? 0 : this.Articles.Count);
            }
        }

        public IList<Article> Articles { get; set; }

        public override ResponseMsgType MsgType
        {
            get
            {
                return ResponseMsgType.News;
            }
        }
    }
}
