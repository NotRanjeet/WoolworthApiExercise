using System;
using Woolworth.Domain.Exceptions;

namespace Woolworth.Domain.ValueObjects
{
    public readonly struct Price : IEquatable<Price>
    {
        public decimal Money { get; }

        public bool Equals(Price other)
        {
            return this.Money == other.Money;
        }

        
        public Price(decimal value)
        {
            if (value < 0) throw new DomainException("Invalid value for price");
            Money = value;
        }

    }
}
