using System.Collections.Generic;
using Woolworth.Domain.ValueObjects;

namespace Woolworth.Domain.Entities
{
    class TrolleySpecial
    {
        public IEnumerable<TrolleyQuantity> Quantities { get; } 

        public Price Total { get; }

        public TrolleySpecial(IEnumerable<TrolleyQuantity> quantities, decimal price)
        {
            Quantities = quantities;
            Total = new Price(price);
        }

    }
}
