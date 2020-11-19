using System;
using DDDNETBB.Domain.Tests.Fakes;
using FluentAssertions;
using NUnit.Framework;

namespace DDDNETBB.Domain.Tests
{
    public sealed class DomainEventTests
    {
        [Test]
        public void DomainEvent_by_default_should_generate_its_own_metadata()
        {
            //arrange
            var @event = new FakeDomainEvent();

            //act && assert
            @event.Metadata.Should().NotBeNull();
            @event.Id.Should().NotBeEmpty();
            @event.OccurredOn.Should().BeSameDateAs(DateTime.UtcNow.Date);
        }

        [Test]
        public void DomainEvent_should_be_able_to_adapt_provided_arguments_as_metadata()
        {
            //arrange
            var id = Guid.NewGuid();
            var date = DateTime.UtcNow;

            var @event = new FakeDomainEvent(DomainEventMetadata.For(id, date));

            //act && assert
            @event.Metadata.Should().NotBeNull();
            @event.Id.Should().Be(id);
            @event.OccurredOn.Should().Be(date);
        }
    }
}