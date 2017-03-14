using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Domain.Menu
{
    public class SubMenu : BaseButton
    {
        public SubMenu()
        {
            this.sub_button = new List<SingleButton>();
        }

        public SubMenu(string name)
            : this()
        {
            base.name = name;
        }

        public List<SingleButton> sub_button { get; set; }
    }
}
