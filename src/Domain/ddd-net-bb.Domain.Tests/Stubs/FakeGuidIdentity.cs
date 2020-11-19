using System;
using Newtonsoft.Json;

namespace DDDNETBB.Domain.Tests.Fakes
{
    internal record FakeEntityId : Identity
    {
        [JsonConstructor]
        public FakeEntityId(Guid Id) : base(Id) { }

        public override bool IsTransient() => Value != Guid.Empty;
    }
}