using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafetyDemo
{
    public class Banana : Fruit
    {
        public override ConsoleColor Color => ConsoleColor.Yellow;

        public override int TimeToGrow => 2000;

        public override int FillValue => 10;

        public override IEnumerable<ISeed> GetSeeds()
        {
            var seeds = new List<Seed<Banana>>();
            var r = new Random();
            var numSeeds = (new Random()).Next(15, 62);
            for (int i = 0; i < numSeeds; i++)
            {
                seeds.Add(new Seed<Banana>(TimeToGrow));
            }
            return seeds;
        }
    }
}
