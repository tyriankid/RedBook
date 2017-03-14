using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Domain.Menu
{
    public class SingleClickButton : SingleButton
    {
        public SingleClickButton(): base(ButtonType.click.ToString())
        {
        }

        public string key { get; set; }
    }
}
