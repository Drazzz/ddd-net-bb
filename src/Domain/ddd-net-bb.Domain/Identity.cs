using System;
using DDDNETBB.Core.Abstractions;

namespace DDDNETBB.Domain
{
    public abstract record Identity(Guid Value) : Identity<Guid>(Value);

    public abstract record Identity<TValue>(TValue Value) : SingleValueObject<TValue>(Value), ITransientable
    {
        public abstract bool IsTransient();

        public override string ToString() => Value.ToString();
    }
}