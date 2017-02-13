using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    public class Farmer : Person
    {
        public override int Decisiveness => 90;

        public override async Task<IEnumerable<ILeftover>> Eat(IConsumable consumable)
        {
            var leftovers = await base.Eat(consumable);

            // Plant the leftovers, if they're seeds
            var tasks = new List<Task<Fruit>>();
            foreach (ISeed item in leftovers.Where(x => x is ISeed))
            {
                tasks.Add(item.Grow());
            }

            // Add the grown fruit to the inventory
            foreach (var item in await Task.WhenAll(tasks))
            {
                Inventory.Add(item);
            }

            return leftovers.Where(x => !(x is ISeed));
        }
    }
}
