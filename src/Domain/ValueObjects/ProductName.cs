using System;
using Woolworth.Domain.Exceptions;

namespace Woolworth.Domain.ValueObjects
{
    public readonly struct ProductName : IEquatable<ProductName>
    {
        public string Name { get; }
        public bool Equals(ProductName other)
        {
            return Name.ToUpperInvariant() == other.Name.ToUpperInvariant();
        }

        public ProductName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("Invalid empty string for the product name");
            }

            if (name.Length <2)
            {
                throw new DomainException("Product name should be atleast 2 characters long");
            }
            this.Name = name;
        }
    }
}
