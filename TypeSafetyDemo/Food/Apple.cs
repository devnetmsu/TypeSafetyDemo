using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeSafetyDemo
{
    public class Apple : Fruit
    {
        public override ConsoleColor Color => ConsoleColor.Red;

        public override int TimeToGrow => 3000;

        public override int FillValue => 15;

        public override IEnumerable<ISeed> GetSeeds()
        {
            var seeds = new List<Seed<Apple>>();
            for (int i = 0; i < 5; i++)
            {
                seeds.Add(new Seed<Apple>(TimeToGrow));
            }
            return seeds;
        }
    }
}
