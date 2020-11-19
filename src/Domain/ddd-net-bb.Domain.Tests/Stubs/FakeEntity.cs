using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DDDNETBB.Domain.Tests.Fakes
{
    internal sealed class FakeEntity: Entity<FakeEntityId>
    {
        public string Name {get; private set;}


        [JsonConstructor]
        private FakeEntity(FakeEntityId id)
        {
            Id = id;
            Name = "Test Name";
        }
        public static FakeEntity For(Guid id) => new FakeEntity(new FakeEntityId(id));
        public static FakeEntity New() => new FakeEntity(new FakeEntityId(Guid.NewGuid()));


        public async Task AsyncMethodThrows_BusinessRuleValidationException(int someData)
            => await Task.Run(() => Check(BusinessRuleToCheck.For(someData)));
    }
}