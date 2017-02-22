using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    public abstract class Person
    {
        public Person()
        {
            IsAlive = true;
            Belly = 100;
            Inventory = new List<IConsumable>();
            Leftovers = new List<ILeftover>();
        }

        /// <summary>
        /// The food that the person has
        /// </summary>
        public IList<IConsumable> Inventory { get; }

        public IList<ILeftover> Leftovers { get; }

        /// <summary>
        /// A value from 0 to 100 indicating how full the person's belly is
        /// </summary>
        public int Belly
        {
            get
            {
                return _belly;
            }
            private set
            {
                _belly = value;
                if (_belly < 1)
                {
                    IsAlive = false;
                }
            }
        }
        int _belly;

        /// <summary>
        /// A value from 0 to 100 indicating how decisive a person is.  0 means the person can't make decisions and 100 means decisions are instant.
        /// </summary>
        public abstract int Decisiveness { get; }

        /// <summary>
        /// Whether or not the person is alive
        /// </summary>
        public bool IsAlive { get; set; }

        /// <summary>
        /// Chooses a random food item from the inventory
        /// </summary>
        /// <returns>The consumable item that was chosen, or null if there was no consumable item to choose</returns>
        public virtual async Task<IConsumable> ChooseConsumable()
        {
            if (Inventory.Any())
            {
                // No food to choose from :(
                return null;
            }
            else
            {
                // Wait for a decision
                var decisionDelay = (decimal)4000 / Decisiveness;
                await Task.Run(() => Thread.Sleep((int)Math.Ceiling(decisionDelay)));

                // Decide
                var choice = Inventory[(new Random()).Next(0, Inventory.Count - 1)];
                Inventory.Remove(choice);
                return choice;
            }            
        }

        /// <summary>
        /// Tells the person to eat the given item
        /// </summary>
        /// <param name="consumable">The item to eat</param>
        /// <returns>The leftovers</returns>
        public virtual Task<IEnumerable<ILeftover>> Eat(IConsumable consumable)
        {
            if (consumable.IsPoison)
            {
                // Poison kills
                this.IsAlive = false;
            }
            else
            {
                // Fill the belly without exceeding maximum value
                this.Belly = Math.Min(consumable.FillValue + Belly, 100);                
            }

            // Return the leftover
            return Task.FromResult(consumable.Consume());
        }

        /// <summary>
        /// Tells the person to eat the given item, then store the leftovers
        /// </summary>
        /// <param name="consumable">The item to eat</param>
        public async Task EatAndStoreLeftovers(IConsumable consumable)
        {
            var leftovers = await Eat(consumable);
            foreach (var item in leftovers)
            {
                this.Leftovers.Add(item);
            }
        }

        /// <summary>
        /// Increments or decrements the belly (<see cref="Belly"/>)by a given amount.  This function is thread-safe.
        /// </summary>
        /// <param name="amount">The amount to increment or decrement</param>
        public void IncrementBelly(int amount)
        {
            if (amount > 0)
            {
                for (int i = 0; i < amount; i += 1)
                {
                    if (_belly <= 100)
                    {
                        Interlocked.Increment(ref _belly);
                    }                    
                }
            }
            else
            {
                for (int i = 0; i > amount; i -= 1)
                {
                    if (_belly > 0)
                    {
                        Interlocked.Decrement(ref _belly);
                    }
                    else
                    {
                        IsAlive = false;
                    }                    
                }
            }
                       
        }
    }
}
