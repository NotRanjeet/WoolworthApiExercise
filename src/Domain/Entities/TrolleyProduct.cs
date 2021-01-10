using Woolworth.Domain.ValueObjects;

namespace Woolworth.Domain.Entities
{
    class TrolleyProduct
    {
        public ProductName Name { get; }
        public Price Price { get; }

        public TrolleyProduct(string name, decimal price)
        {
            Name = new ProductName(name);
            Price = new Price(price);
        }
    }
}
