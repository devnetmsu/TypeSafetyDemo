using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    public class Bacon : Food
    {
        public override int FillValue
        {
            get
            {
                return 10;
            }
        }

        public override IEnumerable<ILeftover> Consume()
        {
            return Enumerable.Empty<ILeftover>();
        }
    }
}
