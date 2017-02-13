using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafetyDemo
{
    public class Watermelon : Fruit
    {
        public override ConsoleColor Color => ConsoleColor.Green;

        public override int TimeToGrow => 3000;

        public override int FillValue => 5;

        public override IEnumerable<ISeed> GetSeeds()
        {
            var seeds = new List<Seed<Watermelon>>();
            var r = new Random();
            var numSeeds = 300;
            for (int i = 0; i < numSeeds; i++)
            {
                seeds.Add(new Seed<Watermelon>(TimeToGrow));
            }
            return seeds;
        }
    }
}
