using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafetyDemo
{
    /// <summary>
    /// A food that can be grown
    /// </summary>
    public abstract class Fruit : Food
    {
        /// <summary>
        /// The color of the fruit
        /// </summary>
        public abstract ConsoleColor Color { get; }

        /// <summary>
        /// Amount of time, in ticks, that it takes to grow the fruit
        /// </summary>
        public abstract int TimeToGrow { get; }

        /// <summary>
        /// Gets seeds that can be used to plant the fruit
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<ISeed> GetSeeds();

        public override IEnumerable<ILeftover> Consume()
        {
            return GetSeeds();
        }
    }
}
