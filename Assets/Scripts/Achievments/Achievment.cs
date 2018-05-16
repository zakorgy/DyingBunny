using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Core
{
    abstract class Achievement
    {
        protected string name;

        protected Achievement(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        }
    }
}

