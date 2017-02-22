using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    /// <summary>
    /// A variant of an apple seed that kills
    /// </summary>
    public class PoisonAppleSeed : ISeed, ILeftover
    {
        public int FillValue
        {
            get
            {
                return 1;
            }
        }

        public bool IsPoison
        {
            get
            {
                return true;
            }
        }

        public IEnumerable<ILeftover> Consume()
        {
            return Enumerable.Empty<ILeftover>();
        }

        public Task<Fruit> Grow()
        {
            throw new NotImplementedException();
        }
    }
}
