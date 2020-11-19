using System.Collections.Generic;
using Newtonsoft.Json;

namespace DDDNETBB.Domain.Tests.Fakes
{
    internal sealed class FakeEnumeration: Enumeration
    {
        [JsonConstructor]
        private FakeEnumeration(int id, string name) : base(id, name){}


        public static FakeEnumeration Draft = new FakeEnumeration(1, nameof(Draft).ToUpperInvariant());
        public static FakeEnumeration Paid = new FakeEnumeration(2, nameof(Paid).ToUpperInvariant());
        public static FakeEnumeration Cancelled = new FakeEnumeration(3, nameof(Cancelled).ToUpperInvariant());


        public static FakeEnumeration From(string name)
            => Enumeration.FromDisplayName<FakeEnumeration>(name?.ToUpperInvariant());
        public static FakeEnumeration From(int id)
            => Enumeration.FromValue<FakeEnumeration>(id);

        public static IReadOnlyCollection<FakeEnumeration> List()
            => new[] {Draft, Paid, Cancelled};
    }
}