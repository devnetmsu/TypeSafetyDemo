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
        public int TimeToGrow { get; }
        public async Task<Fruit> Grow()
        {
            await Task.Run(() => Thread.Sleep(TimeToGrow));
            return typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { }) as Fruit;
        }
    }
}
