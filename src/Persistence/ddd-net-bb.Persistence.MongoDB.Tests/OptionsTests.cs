using NUnit.Framework;
using System.Text.Json;
using DDDNETBB.Persistence.MongoDB;
using FluentAssertions;

namespace DDDNETBB.Persistence.MongoDB.Tests
{
    [TestFixture]
    public sealed class OptionsTests
    {
        [Test]
        public void Options_should_support_serialization()
        {
            //arrange
            const string settingsJson = @"{""ConnectionString"":""test-connection-string"", ""DatabaseName"":""test-db-name"",""CollectionName"":""test-collection-name"", ""Seed"":true,""SetRandomDatabaseSuffix"":false}";
            
            //act
            var options = JsonSerializer.Deserialize<Options>(settingsJson);

            //assert
            options.Should().NotBeNull();
            options.ConnectionString.Should().Be("test-connection-string");
            options.DatabaseName.Should().Be("test-db-name");
            options.CollectionName.Should().Be("test-collection-name");
            options.Seed.Should().BeTrue();
            options.SetRandomDatabaseSuffix.Should().BeFalse();
        }
    }
}