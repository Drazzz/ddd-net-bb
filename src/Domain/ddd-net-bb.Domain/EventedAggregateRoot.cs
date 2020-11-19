using System;
using System.Collections.Generic;
using DDDNETBB.Core.Abstractions;
using DDDNETBB.Domain.Abstractions;
using DDDNETBB.Domain.Exceptions;

namespace DDDNETBB.Domain
{
    public abstract class EventedAggregateRoot<TKey> : EventedAggregateRoot<TKey, Guid>
        where TKey : Identity {}

    public abstract class EventedAggregateRoot<TKey, TKeyType> : Entity<TKey, TKeyType>, IEventedAggregateRoot
        where TKey : Identity<TKeyType>
    {
        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();


        public int Version {get; protected set;}


        public IReadOnlyCollection<IEvent> GetUncommittedChanges() => _domainEvents?.AsReadOnly();

        public void MarkChangesAsCommitted()
        {
            Version += _domainEvents.Count;
            ClearAllDomainEvents();
        }

        protected void AddDomainEvent(IDomainEvent @event)
        {
            if (@event is null)
                throw new InvalidDomainEventArgumentException(nameof(@event));

            _domainEvents ??= new List<IDomainEvent>();
            _domainEvents.Add(@event);
        }

        private void ClearAllDomainEvents() => _domainEvents?.Clear();

        protected void RemoveDomainEvent(IDomainEvent @event) => _domainEvents?.Remove(@event);
    }
}