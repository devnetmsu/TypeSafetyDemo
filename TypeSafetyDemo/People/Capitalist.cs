using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo.People
{
    public class Capitalist : Person
    {

        /// <summary>
        /// The amount of money in the capitalist's wallet
        /// </summary>
        public decimal HeldMoney { get; set; }

        public override int Decisiveness
        {
            get
            {
                return 90;
            }
        }

        public override async Task<IEnumerable<ILeftover>> Eat(IConsumable consumable)
        {
            var leftovers = await base.Eat(consumable);

            // The capitalist sells his leftovers
            // He markets his literal garbage as treasure and marks up the cost by 100%.
            foreach (var item in leftovers)
            {
                HeldMoney += item.SellPrice * 2;
            }
            return Enumerable.Empty<ILeftover>();
        }
    }
}
