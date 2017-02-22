using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafetyDemo
{
    public abstract class Food : IConsumable
    {
        public abstract IEnumerable<ILeftover> Consume();
        public virtual bool IsPoison => false;
        public abstract int FillValue { get; }
    }
}
