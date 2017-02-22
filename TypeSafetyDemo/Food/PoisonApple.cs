using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    public class PoisonApple : Apple
    {
        public override bool IsPoison
        {
            get
            {
                return true;
            }
        }

        public override IEnumerable<ISeed> GetSeeds()
        {
            var seeds = new List<PoisonAppleSeed>();
            for (int i = 0; i < 5; i++)
            {
                seeds.Add(new PoisonAppleSeed());
            }
            return seeds;
        }
    }
}
