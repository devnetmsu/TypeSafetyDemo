using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    public class Vegan : Person
    {

        public Vegan() : base()
        {
            Inventory = new List<Fruit>();
        }

        public override int Decisiveness
        {
            get
            {
                return 90;
            }
        }

        /// <remarks>
        /// The vegan will never keep animal goods (like <see cref="Bacon"/>) in his or her inventory.
        /// </remarks>
        public new IList<Fruit> Inventory { get; set; }

        public override Task<IConsumable> ChooseConsumable()
        {
            var item = Inventory[0];
            Inventory.RemoveAt(0);
            return Task.FromResult<IConsumable>(item);
        }
    }
}
