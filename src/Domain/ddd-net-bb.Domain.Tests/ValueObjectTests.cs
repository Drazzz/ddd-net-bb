using NUnit.Framework;
using FluentAssertions;
using DDDNETBB.Domain.Tests.Fakes;
using DDDNETBB.Domain.Exceptions;

namespace DDDNETBB.Domain.Tests
{
    public sealed class ValueObjectTests
    {
        [Test]
        public void ValueObject_should_be_compared_by_values_not_by_references()
        {
            //arrange
            var vo = new FakeValueObject("test", 1, false);
            var fvo = new FakeValueObject("test", 1, false);

            //assert
            vo.Should().Be(fvo);
            vo.Should().NotBeSameAs(fvo);
        }

        [Test]
        public void ValueObject_should_throw_BusinessRuleValidationException_when_its_checking_the_rule()
        {
            //arrange
            var vo = new FakeValueObject("Name", 1, false);

            //act & assert
            Assert.ThrowsAsync<BusinessRuleValidationException>(() => vo.AsyncMethodThrows_BusinessRuleValidationException(0));
        }
    }
}