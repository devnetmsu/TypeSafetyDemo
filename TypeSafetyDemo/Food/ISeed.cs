using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    public interface ISeed : ILeftover
    {
        Task<Fruit> Grow();
    }
}
