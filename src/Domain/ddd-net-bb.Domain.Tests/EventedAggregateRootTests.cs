using DDDNETBB.Domain.Tests.Fakes;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DDDNETBB.Domain.Tests
{
    public sealed class EventedAggregateRootTests
    {
        [Test]
        public void EventedAggregateRoot_should_be_able_to_add_domainEvents()
        {
            //arrange
            var sut = FakeEventedAggregateRoot.Default();

            //act
            sut.ChangePersonalData("new name", 999);

            //assert
            sut.GetUncommittedChanges().Should().NotBeEmpty();
            sut.GetUncommittedChanges().Count.Should().Be(1);
        }

        [Test]
        public void EventedAggregateRoot_should_clear_UncommittedChanges_when_marking_them_as_commited()
        {
            //arrange
            var sut = FakeEventedAggregateRoot.Default();

            //act
            sut.ChangePersonalData("new name", 999);
            sut.GetUncommittedChanges().Should().NotBeEmpty();
            sut.MarkChangesAsCommitted();

            //assert
            sut.GetUncommittedChanges().Should().BeEmpty();
        }

        [Test]
        public void EventedAggregateRoot_should_update_aggregate_Version_when_its_marking_changes_as_commited()
        {
            //arrange
            var sut = FakeEventedAggregateRoot.Default();

            //act
            sut.ChangePersonalData("new name", 999);
            sut.MarkChangesAsCommitted();

            //assert
            sut.Version.Should().Be(1);
        }

        [Test]
        public void EventedAggregateRoot_should_support_serialization()
        {
            //arrange
            var sut = FakeEventedAggregateRoot.Default();

            //act
            var serialized = JsonConvert.SerializeObject(sut);
            var deserialized = JsonConvert.DeserializeObject<FakeEventedAggregateRoot>(serialized);

            //assert
            deserialized.Should().Be(sut);
        }
    }
}