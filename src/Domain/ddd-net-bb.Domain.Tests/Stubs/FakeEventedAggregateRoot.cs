using System;
using Newtonsoft.Json;

namespace DDDNETBB.Domain.Tests.Fakes
{
    internal sealed class FakeEventedAggregateRoot : EventedAggregateRoot<FakeEntityId>
    {
        public string Name {get; private set;}
        public int Age {get; private set; }
        public bool IsFake {get; private set;} = true;

        [JsonConstructor]
        private FakeEventedAggregateRoot(FakeEntityId id, string name, int age, bool isFake)
        {
            Id = id;
            Name = name;
            Age = age;
            IsFake = isFake;
        }
        public static FakeEventedAggregateRoot Default()
            => new FakeEventedAggregateRoot(new FakeEntityId(Guid.NewGuid()), "test name", 111, true);

        public void ChangePersonalData(string name, int age)
        {
            Check(BusinessRuleToCheck.For(age));

            IsFake = false;
            Name = name;
            Age = age;

            AddDomainEvent(new FakeDomainEvent());
        }
    }
}