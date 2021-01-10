using System.Collections.Generic;
using Woolworth.Domain.ValueObjects;

namespace Woolworth.Domain.Entities
{
    public class Trolley
    {
        private List<Trolley> _products;
        private List<TrolleySpecial> _specials;
        private List<TrolleyQuantity> _quantities;

        public Price GetTotal()
        {
            //TODO: Asked for detailed requirements
            return new Price(0);
        }
    }
}
