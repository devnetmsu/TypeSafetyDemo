using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    public class Seed<T> : ISeed where T : Fruit
    {
        public Seed(int timeToGrow)
        {
            this.TimeToGrow = timeToGrow;
        }

        public virtual decimal SellPrice
        {
            get
            {
                return 1;
            }
        }

        public int TimeToGrow { get; }
        public async Task<Fruit> Grow()
        {
            await Task.Delay(TimeToGrow);
            return typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { }) as Fruit;
        }
    }
}
