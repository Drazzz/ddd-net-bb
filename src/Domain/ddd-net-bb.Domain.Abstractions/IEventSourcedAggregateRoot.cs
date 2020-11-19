using System.Collections.Generic;

namespace DDDNETBB.Domain.Abstractions
{
    public interface IEventSourcedAggregateRoot : IEventedAggregateRoot
    {
        int Version {get;}
        void LoadFromHistory(IEnumerable<IDomainEvent> history);
    }
}