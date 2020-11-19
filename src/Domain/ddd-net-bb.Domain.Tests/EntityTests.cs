using NUnit.Framework;
using FluentAssertions;
using DDDNETBB.Domain.Tests.Fakes;
using DDDNETBB.Domain.Exceptions;
using System;
using Newtonsoft.Json;

namespace DDDNETBB.Domain.Tests
{
    public sealed class EntityTests
    {
        [Test]
        public void Entity_should_have_generated_id()
        {
            //arrange
            var entity = FakeEntity.New();

            //act & assert
            entity.Id.Should().NotBeNull();
            entity.Id.Value.Should().NotBeEmpty();
            entity.IsTransient().Should().BeTrue();
        }

        [Test]
        public void Entity_should_be_able_to_handle_id_generated_from_different_source()
        {
            //arrange
            var guid = Guid.NewGuid();
            var entity = FakeEntity.For(guid);

            //act & assert
            entity.Id.Value.Should().Be(guid);
            entity.IsTransient().Should().BeTrue();
        }

        [Test]
        public void Entity_AsyncMethodThrows_BusinessRuleValidationException_method_should_throw_BusinessRuleValidationException_when_its_checking_the_rule()
        {
            //arrange
            var entity = FakeEntity.New();

            //act & assert
            Assert.ThrowsAsync<BusinessRuleValidationException>(() => entity.AsyncMethodThrows_BusinessRuleValidationException(0));
        }

        [Test]
        public void Entity_should_be_equal_to_another_Entity_with_the_same_identity()
        {
            //arrange
            var guid = Guid.NewGuid();
            var entity = FakeEntity.For(guid);
            var otherEntity = FakeEntity.For(guid);

            //act
            var areEqual = entity == otherEntity && entity.Equals(otherEntity);

            //assert
            areEqual.Should().BeTrue();
            entity.Should().Be(otherEntity);
        }

        [Test]
        public void Entity_should_support_Json_serialization()
        {
            //arrange
            var guid = Guid.NewGuid();
            var entity = FakeEntity.For(guid);

            //act
            var serializedEntity = JsonConvert.SerializeObject(entity);
            var deserializedEntity = JsonConvert.DeserializeObject<FakeEntity>(serializedEntity);

            //assert
            deserializedEntity.Should().Be(entity);
            deserializedEntity.Id.Should().Be(entity.Id);
            deserializedEntity.Name.Should().Be(entity.Name);
        }
    }
}