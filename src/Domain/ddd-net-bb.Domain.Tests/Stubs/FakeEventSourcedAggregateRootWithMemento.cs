using System;
using Newtonsoft.Json;

namespace DDDNETBB.Domain.Tests.Fakes
{
    internal sealed class FakeAggregateSnapshot
    {
        internal FakeEntityId Id {get; set;}
        internal decimal Amount {get; set;}
        internal string Name {get; set;}
        internal Birth Age {get; set;}        
    }

    internal sealed record Birth(int Age, DateTime birthDate) : ValueObject;

    internal sealed class FakeEventSourcedAggregateRootWithMemento : EventSourcedAggregateRoot<FakeEntityId, Guid, FakeAggregateSnapshot>
    {
        public decimal Amount {get; private set;}
        public string Name {get; private set;}
        public Birth Age {get; private set;}


        [JsonConstructor]
        private FakeEventSourcedAggregateRootWithMemento(FakeEntityId id, decimal amount, string name, Birth age)
        {
            Id = id;
            Amount = amount;
            Name = name;
            Age = age;
        }
        public static FakeEventSourcedAggregateRootWithMemento Default()
            => new FakeEventSourcedAggregateRootWithMemento(new FakeEntityId(Guid.NewGuid()), 3.14m, "default name", new Birth(1, DateTime.UtcNow.AddYears(-1)));


        protected override FakeAggregateSnapshot CreateMemento()
            => this.ToFakeAggregateSnapshot();

        protected override void SetMemento(FakeAggregateSnapshot memento)
        {
            Id = memento.Id;
            Amount = memento.Amount;
            Name = memento.Name;
            Age = memento.Age;
        }
    }


    internal static class FakeEventSourcedAggregateRootWithMementoExtensions
    {
        internal static FakeAggregateSnapshot ToFakeAggregateSnapshot(this FakeEventSourcedAggregateRootWithMemento aggegate)
            => new FakeAggregateSnapshot {
                Id = aggegate.Id,
                Amount = aggegate.Amount,
                Name = aggegate.Name,
                Age = aggegate.Age
            };
    }
}