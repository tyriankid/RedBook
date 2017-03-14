using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Domain.Menu
{
    public class Menu
    {
        public Menu()
        {
            this.menu = new ButtonGroup();
        }

        public ButtonGroup menu { get; set; }
    }
}
