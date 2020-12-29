using System;
using DDDNETBB.Core.Abstractions.Dependencies;
using DDDNETBB.Persistence.EF.Exceptions;
using DDDNETBB.Persistence.EF.Tests.Stubs;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace DDDNETBB.Persistence.EF.Tests
{
    [TestFixture]
    public class DomainDBContextFactoryTests
    {
        private IDomainDBContextFactory _sut;
        
        private Mock<IResolver> _resolverMock;
        private ILogger<DomainDBContextFactory> _loggerMock => new NullLogger<DomainDBContextFactory>();

        
        [SetUp]
        public void SetUp()
        {
            _resolverMock = new Mock<IResolver>();
            _sut = new DomainDBContextFactory(_resolverMock.Object, _loggerMock);
        }

        [TearDown]
        public void TearDown()
        {
            _resolverMock = null;
            _sut = null;
        }

        [Test]
        public void Ctor_should_throw_ArgumentNullException_if_loggerFactory_arg_is_null()
            => Assert.Throws<ArgumentNullException>(() => new DomainDBContextFactory(_resolverMock.Object, null));

        [Test]
        public void Ctor_should_throw_ArgumentNullException_if_resolver_arg_is_null()
            => Assert.Throws<ArgumentNullException>(() => new DomainDBContextFactory(null, _loggerMock));


        #region CreateDBContext

        [Test]
        public void
            CreateDBContext_method_should_throw_PersistenceEntityFrameworkConfigurationException_if_resolved_dbProvider_is_null()
        {
            //arrange
            _resolverMock.Setup(r => r.Resolve<It.IsAnyType>())
                .Returns(() => null)
                .Verifiable("The 'Resolve' method wasn't called");

            //act & assert
            Assert.Throws<PersistenceEntityFrameworkConfigurationException>(() => _sut.CreateDBContext());
        }
        
        [Test]
        public void CreateDBContext_method_should_return_instance_of_EntityFrameworkDbContext_created_by_dbProvider()
        {
            //arrange
            _resolverMock.Setup(r => r.Resolve<IDatabaseProvider>())
                .Returns(() => new FakeDBProvider())
                .Verifiable("The 'Resolve' method wasn't called");

           //act
            var result = _sut.CreateDBContext();

            //assert
            result.Should().NotBeNull();
            _resolverMock.Verify(r => r.Resolve<IDatabaseProvider>(), Times.Once);
        }

        #endregion
    }
}