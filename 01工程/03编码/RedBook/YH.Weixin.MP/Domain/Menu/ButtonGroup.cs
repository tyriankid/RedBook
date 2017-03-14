using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Domain.Menu
{
    public class ButtonGroup
    {
        public ButtonGroup()
        {
            this.button = new List<BaseButton>();
        }

        public List<BaseButton> button { get; set; }
    }
}
