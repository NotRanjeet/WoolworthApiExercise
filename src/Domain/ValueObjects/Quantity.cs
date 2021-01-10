using System;
using Woolworth.Domain.Exceptions;

namespace Woolworth.Domain.ValueObjects
{
    public readonly struct Quantity : IEquatable<Quantity>
    {
        public int Amount { get; }

        public bool Equals(Quantity other)
        {
            return this.Amount == other.Amount;
        }

        public Quantity(int value)
        {
            if (value < 0) throw new DomainException("Invalid value for Quantity");
            Amount = value;
        }
    }
}
