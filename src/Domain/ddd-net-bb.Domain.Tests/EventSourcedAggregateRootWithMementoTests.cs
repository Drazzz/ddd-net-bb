using System;
using DDDNETBB.Domain.Tests.Fakes;
using FluentAssertions;
using NUnit.Framework;

namespace DDDNETBB.Domain.Tests
{
    public sealed class EventSourcedAggregateRootWithMementoTests 
    {
        [Test]
        public void EventSourcedAggregateRootWithMemento_should_apply_snapshot_data_to_properties()
        {
            //arrange
            var sut = FakeEventSourcedAggregateRootWithMemento.Default();

            var newAge = new Birth(5, DateTime.UtcNow.AddYears(-5));
            var snapshot = new FakeAggregateSnapshot
            {
                Id = new FakeEntityId(Guid.NewGuid()),
                Age = newAge,
                Amount = 4.44m,
                Name = "Snapshot"
            };

            //act
            sut.ApplySnapshot(snapshot, 1);

            //assert
            sut.Name.Should().Be("Snapshot");
            sut.Amount.Should().Be(4.44m);
            sut.Age.Should().Be(newAge);
        }

        [Test]
        public void EventSourcedAggregateRootWithMemento_should_be_able_to_take_a_snapshot_of_its_state()
        {
            //arrange
            var sut = FakeEventSourcedAggregateRootWithMemento.Default();

            //act
            var snapshot = sut.TakeSnapshot();
            var mapped = snapshot.snapshot as FakeAggregateSnapshot;

            //assert
            snapshot.Should().NotBeNull();
            snapshot.snapshot.Should().NotBeNull();
            snapshot.snapshotVersion.Should().Be(0);

            mapped.Should().NotBeNull();
        }
    }
}