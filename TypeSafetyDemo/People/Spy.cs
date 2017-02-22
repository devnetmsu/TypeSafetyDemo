using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    public class Spy : Person
    {
        public override int Decisiveness
        {
            get
            {
                // As a spy, you have to be able to make quick decisions
                return 100;
            }
        }

        public override async Task<IConsumable> ChooseConsumable()
        {
            // As a spy, a common occupational hazard is being poisoned

            var item = await base.ChooseConsumable();

            // Check to see if an item was chosen
            if (item == null)
            {
                return null;
            }

            // Keep choosing (and discarding items) if they're poisoned
            while (item.IsPoison)
            {
                
                item = await base.ChooseConsumable();
            }
            return item;
        }
    }
}
