using NUnit.Framework;
using FluentAssertions;
using DDDNETBB.Domain.Tests.Fakes;
using Newtonsoft.Json;

namespace DDDNETBB.Domain.Tests
{
    public sealed class EnumerationTests
    {
        [Test]
        public void Enumeration_should_be_equal_to_other_if_they_have_same_value_and_id()
        {
            //arrange
            var sut = FakeEnumeration.Draft;
            var otherEnumeration = FakeEnumeration.Draft;

            //act
            var areEqual = sut == otherEnumeration && sut.Equals(otherEnumeration);

            //assert
            areEqual.Should().BeTrue();
            sut.Id.Should().Be(otherEnumeration.Id);
            sut.Name.Should().Be(otherEnumeration.Name);
        }

        [TestCase("draft")]
        [TestCase("Draft")]
        [TestCase("DRAFT")]
        [TestCase("dRaFT")]
        public void Enumeration_should_be_able_to_provide_enum_instance_by_name(string name)
        {
            //act
            var @enum = FakeEnumeration.From(name);

            //assert
            @enum.Should().NotBeNull();
            @enum.Id.Should().Be(1);
            @enum.Name.Should().Be("DRAFT");
        }

        [Test]
        public void Enumeration_should_be_able_to_provide_enum_instance_by_id()
        {
            //arrange
            const int id = 2;
            const string expectedName = "PAID";

            //act
            var @enum = FakeEnumeration.From(id);

            //assert
            @enum.Should().NotBeNull();
            @enum.Id.Should().Be(id);
            @enum.Name.Should().Be(expectedName);
        }

        [Test]
        public void Enumeration_should_be_able_to_return_IReadOnlyCollection_of_its_items()
        {
            //act
            var @enums = FakeEnumeration.List();

            //assert
            @enums.Should().NotBeNull();
            @enums.Should().NotBeEmpty();
            @enums.Count.Should().Be(3);
        }

        [Test]
        public void Enumeration_should_support_serialization()
        {
            //arrange
            var sut = FakeEnumeration.Cancelled;

            //act
            var serializedEnum = JsonConvert.SerializeObject(sut);
            var deserializedEnum = JsonConvert.DeserializeObject<FakeEnumeration>(serializedEnum);

            //assert
            deserializedEnum.Should().Be(sut);
        }
    }
}