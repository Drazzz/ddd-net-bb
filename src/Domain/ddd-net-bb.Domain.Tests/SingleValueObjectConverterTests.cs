using System;
using FluentAssertions;
using NUnit.Framework;
using Newtonsoft.Json;
using DDDNETBB.Domain.Converters;
using DDDNETBB.Domain.Tests.Fakes;

namespace DDDNETBB.Domain.Tests
{
    public sealed class SingleValueObjectConverterTests
    {
        [Test]
        public void SingleValueObjectConverterType_should_serialize_Identity_using_converter()
        {
            //arrange
            var guid = Guid.NewGuid();
            var identity = new FakeEntityId(guid);

            var serializerSettings = SettingsFor(SingleValueObjectConverterType.SingleValue);      
            var identityValueJson = JsonConvert.SerializeObject(identity.Value, serializerSettings);     

            //act            
            var identityJson = JsonConvert.SerializeObject(identity, serializerSettings);

            //assert
            identityJson.Should().Be(identityValueJson);
        }

        [Test]
        public void SingleValueObjectConverterType_should_deserialize_Identity_using_converter()
        {
            //arrange
            var guid = Guid.NewGuid();

            var serializerSettings = SettingsFor(SingleValueObjectConverterType.SingleValue);      
            var identityValueJson = JsonConvert.SerializeObject(guid, serializerSettings);          

            //act
            var identity = JsonConvert.DeserializeObject<FakeEntityId>(identityValueJson, serializerSettings);

            //assert
            identity.Value.Should().Be(guid);
        }

        private JsonSerializerSettings SettingsFor(SingleValueObjectConverterType serializationType)
            => new JsonSerializerSettings
            {
                Converters = new[] {SingleValueObjectConverter.For(serializationType)}
            };
    }
    
}