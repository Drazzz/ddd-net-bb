using System;
using DDDNETBB.Core.Abstractions;
using DDDNETBB.Domain.Abstractions;
using DDDNETBB.Domain.Exceptions;

namespace DDDNETBB.Domain
{
    public abstract class Entity<TKey> : Entity<TKey, Guid>
        where TKey: Identity { }


    public abstract class Entity<TKey, TKeyType> : ITransientable
        where TKey : Identity<TKeyType>
    {
        private TKey _id;
        public TKey Id 
        {
            get => _id;
            protected set => _id = value;
        }


        public bool IsTransient() => Id.IsTransient();

        public void Check(IBusinessRule rule)
        {
            if (rule.IsBroken())
                throw new BusinessRuleValidationException(rule);
        }

        public static bool operator ==(Entity<TKey, TKeyType> left, Entity<TKey, TKeyType> right) => left?.Equals(right) ?? Equals(right, null);
        public static bool operator !=(Entity<TKey, TKeyType> left, Entity<TKey, TKeyType> right) => !(left == right);

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TKey, TKeyType>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Entity<TKey, TKeyType>)obj;
            return item.Id == _id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}