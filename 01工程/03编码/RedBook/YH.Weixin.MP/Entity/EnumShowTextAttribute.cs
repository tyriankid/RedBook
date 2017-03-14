using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YH.Weixin.MP.Entity
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class EnumShowTextAttribute : Attribute
    {
        public EnumShowTextAttribute(string showTest)
        {
            this.ShowText = showTest;
        }

        public string ShowText { get; private set; }
    }
}
