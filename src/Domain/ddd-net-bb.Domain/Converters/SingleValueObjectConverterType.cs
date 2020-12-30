using DDDNETBB.Core;
using Newtonsoft.Json;
using DDDNETBB.Domain.Abstractions;

namespace DDDNETBB.Domain.Converters
{
    internal interface ISingleValueObjectNamingPolicy : IPolicy<string> { }

    internal sealed record SingleValueObjectConverterPolicy : ISingleValueObjectNamingPolicy
    {
        public string Execute() => "Value";
    }

    public sealed class SingleValueObjectConverterType : Enumeration
    {
        [JsonConstructor]
        private SingleValueObjectConverterType(int id, string name) : base(id, name){}

        public static SingleValueObjectConverterType SingleValue = new SingleValueObjectConverterType(2, nameof(SingleValue));
    }
}