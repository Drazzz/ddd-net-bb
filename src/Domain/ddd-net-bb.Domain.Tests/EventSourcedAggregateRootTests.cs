using DDDNETBB.Domain.Tests.Fakes;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DDDNETBB.Domain.Tests
{
    public sealed class EventSourcedAggregateRoot
    {
        [Test]
        public void EventSourcedAggregateRoot_should_support_serialization()
        {
            //arrange
            var sut = FakeEventSourcedAggregateRoot.For("test name");

            //act
            var serialized = JsonConvert.SerializeObject(sut);
            var deserialized = JsonConvert.DeserializeObject<FakeEventSourcedAggregateRoot>(serialized);

            //assert
            deserialized.Should().Be(sut);
        }

        [Test]
        public void EventSourcedAggregateRoot_should_invoke_event_specific_apply_methods_when_load_them_from_history()
        {
            //arrange
            var sut = FakeEventSourcedAggregateRoot.For("test event-sourced name");
            var domainEvents = new[] {new FakeDomainEvent(), new FakeDomainEvent(DomainEventMetadata.Default(), "new name")};
            
            //act
            sut.LoadFromHistory(domainEvents);

            //assert
            sut.Name.Should().Be("new name");
        }

        [Test]
        public void EventSourcedAggregateRoot_should_invoke_event_specific_apply_methods_when_load_them_from_history_in_provided_order()
        {
            //arrange
            var sut = FakeEventSourcedAggregateRoot.For("test event-sourced name");
            var domainEvents = new[] {new FakeDomainEvent(DomainEventMetadata.Default(), "new name"), new FakeDomainEvent()};

            //act
            sut.LoadFromHistory(domainEvents);

            //assert
            sut.Name.Should().BeNull();
        }

        [Test]
        public void EventSourcedAggregateRoot_should_invoke_event_specific_apply_methods_when_load_them_from_history_and_update_aggregate_version()
        {
            //arrange
            var sut = FakeEventSourcedAggregateRoot.For("test event-sourced name");
            var domainEvents = new[] {new FakeDomainEvent(), new FakeDomainEvent(DomainEventMetadata.Default(), "new name"), new FakeDomainEvent()};

            //act
            sut.LoadFromHistory(domainEvents);

            //assert
            sut.Version.Should().Be(3);
        }
    }
}