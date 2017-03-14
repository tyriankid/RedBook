using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP
{
    public class WeiXinOAuthAttribute : Attribute
    {
        public WeiXinOAuthAttribute(WeiXinOAuthPage page)
        {
            m_WeiXinOAuthPage = page;
        }

        WeiXinOAuthPage m_WeiXinOAuthPage;
        public WeiXinOAuthPage WeiXinOAuthPage
        {
            get { return m_WeiXinOAuthPage; }
            set { m_WeiXinOAuthPage = value; }
        }
    }
}
