using Woolworth.Domain.ValueObjects;

namespace Woolworth.Domain.Entities
{
    class TrolleyQuantity
    {
        public ProductName Name { get; }
        public Quantity Quantity { get; }

        public TrolleyQuantity(string name, int amount)
        {
            Name = new ProductName(name);
            Quantity = new Quantity(amount);
        }
    }
}
