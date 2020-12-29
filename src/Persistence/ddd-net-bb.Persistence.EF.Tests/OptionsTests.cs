using FluentAssertions;
using NUnit.Framework;
using DDDNETBB.Persistence.EF;
using Newtonsoft.Json;

namespace DDDNETBB.Persistence.EF.Tests
{
    [TestFixture]
    public sealed class OptionsTest
    {
        [Test]
        public void Options_should_support_serialization()
        {
            //arrange
            const string settingsJson = "{'ConnectionString':'test-connection-string','CommandTimeout':10,'Retry':{'EnableRetryOnFailure':true,'MaxRetryCount':3,'MaxRetryDelay':30}}";
            
            //act
            var options = JsonConvert.DeserializeObject<Options>(settingsJson);

            //assert
            options.Should().NotBeNull();
            options.ConnectionString.Should().Be("test-connection-string");
            options.CommandTimeout.Should().Be(10);
            options.Retry.Should().NotBeNull();
            options.Retry.EnableRetryOnFailure.Should().BeTrue();
            options.Retry.MaxRetryCount.Should().Be(3);
            options.Retry.MaxRetryDelay.Should().Be(30);
        }
    }
}