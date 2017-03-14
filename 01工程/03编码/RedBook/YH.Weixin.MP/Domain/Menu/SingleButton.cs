using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Domain.Menu
{
    public abstract class SingleButton : BaseButton
    {
        public SingleButton(string theType)
        {
            this.type = theType;
        }

        public string type { get; set; }
    }
}
