using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafetyDemo
{
    public interface IConsumable
    {
        bool IsPoison { get; }
        int FillValue { get; }
        IEnumerable<ILeftover> Consume();        
    }
}
