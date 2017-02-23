using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo.Misc
{
    public class Grease : ILeftover
    {
        public decimal SellPrice
        {
            get
            {
                return 5;
            }
        }
    }
}
