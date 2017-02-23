using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    public class Vegan : Person
    {
        public override int Decisiveness
        {
            get
            {
                return 90;
            }
        }

        /// <remarks>
        /// The vegan will never keep animal goods (like <see cref="Bacon"/>) in his or her inventory.
        /// 
        /// </remarks>
        public new IList<IConsumable> Inventory
        {
            get
            {
                // Discard all non-fruit
                base.Inventory = base.Inventory.Where(x => x is Fruit).ToList();
                return base.Inventory;
            }
            set
            {
                base.Inventory = value.Where(x => x is Fruit).ToList();
            }
        }
    }
}
