using DDDNETBB.Domain.Abstractions;
using ReflectionMagic;

namespace DDDNETBB.Domain
{
    public abstract class EmitApplyAggregateRoot<TKey, TKeyType> : EventedAggregateRoot<TKey, TKeyType>
        where TKey : Identity<TKeyType>
    {
        protected void Emit(IDomainEvent @event) => ApplyChanges(@event, true);

        internal void ApplyChanges(IDomainEvent @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            
            if(isNew) AddDomainEvent(@event);
            else Version++; 
        }
    }
}