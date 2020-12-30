using System.Threading;
using System.Threading.Tasks;
using DDDNETBB.Persistence.MongoDB.Exceptions;
using DDDNETBB.Persistence.MongoDB.Factories;
using DDDNETBB.Persistence.MongoDB.Tests.Stubs;
using FluentAssertions;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace DDDNETBB.Persistence.MongoDB.Tests
{
    [TestFixture]
    public class MongoDbSessionFactoryTests
    {
        private IMongoDbSessionFactory _sut;


        [TearDown]
        public void TearDown() => _sut = null;
        
        
        [Test]
        public void Ctor_should_throw_PersistenceMongoDBConfigurationException_if_mongoClient_arg_is_null()
            => Assert.Throws<PersistenceMongoDBConfigurationException>(() => new MongoDbSessionFactory(null));

        [TestCase(false)]
        [TestCase(true)]
        public async Task Create_method_should_call_mongoClient_in_order_to_StartSession(bool useOptions)
        {
            //arrange
            var options = useOptions ? new ClientSessionOptions() : null;
            var clientMock = new Mock<IMongoClient>();
            clientMock.Setup(c => c.StartSessionAsync(options, It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new FakeMongoDbClientSession())
                .Verifiable("The StartSessionAsync wasn't called");

            _sut = new MongoDbSessionFactory(clientMock.Object);

            //act
            var session = await (options is null ? _sut.Create() : _sut.Create(options));

            //assert
            session.Should().NotBeNull("Factory should create new instance");
            clientMock.Verify(c => c.StartSessionAsync(options, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}