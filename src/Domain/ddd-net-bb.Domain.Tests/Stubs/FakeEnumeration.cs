using System.Collections.Generic;
using DDDNETBB.Core;
using Newtonsoft.Json;

namespace DDDNETBB.Domain.Tests.Fakes
{
    internal sealed class FakeEnumeration: Enumeration
    {
        [JsonConstructor]
        private FakeEnumeration(int id, string name) : base(id, name){}


        public static FakeEnumeration Draft = new(1, nameof(Draft).ToUpperInvariant());
        public static FakeEnumeration Paid = new(2, nameof(Paid).ToUpperInvariant());
        public static FakeEnumeration Cancelled = new(3, nameof(Cancelled).ToUpperInvariant());


        public static FakeEnumeration From(string name) => FromDisplayName<FakeEnumeration>(name?.ToUpperInvariant());
        public static FakeEnumeration From(int id) => FromValue<FakeEnumeration>(id);
        public static IReadOnlyCollection<FakeEnumeration> List() => new[] {Draft, Paid, Cancelled};
    }
}