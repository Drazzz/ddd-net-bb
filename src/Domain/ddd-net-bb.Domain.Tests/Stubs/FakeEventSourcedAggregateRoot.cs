using System;
using Newtonsoft.Json;

namespace DDDNETBB.Domain.Tests.Fakes
{
    internal sealed class FakeEventSourcedAggregateRoot : EventSourcedAggregateRoot<FakeEntityId>
    {
        public string Name {get; private set;}


        [JsonConstructor]
        private FakeEventSourcedAggregateRoot(FakeEntityId id, string name)
        {
            Id = id;
            Name = name;
        }
        public static FakeEventSourcedAggregateRoot For(string name)
            => new FakeEventSourcedAggregateRoot(new FakeEntityId(Guid.NewGuid()), name);
        public static FakeEventSourcedAggregateRoot For(Guid id, string name)
            => new FakeEventSourcedAggregateRoot(new FakeEntityId(id), name);

        
        private void Apply(FakeDomainEvent @event)
            => Name = @event.Name;
    }
}