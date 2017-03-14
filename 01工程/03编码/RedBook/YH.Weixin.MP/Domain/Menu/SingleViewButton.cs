using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Domain.Menu
{
    public class SingleViewButton : SingleButton
    {
        public SingleViewButton(): base(ButtonType.view.ToString())
        {
        }

        public string url { get; set; }
    }
}
