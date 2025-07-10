using RabbitMQ.Client;
using Moq;
using Rabbitmq.Producers;
using Rabbitmq.Shared;

namespace Rabbitmq.Tests
{
    [TestFixture]
    public class ProducerBookTests
    {
        private Mock<IModel> _channelMock;
        private ProducerBook _producer;

        [SetUp]
        public void Setup()
        {
            _channelMock = new Mock<IModel>();
            _producer = new ProducerBook(_channelMock.Object);
        }

        [Test]
        public void Init_DeclaresQueueAndExchange()
        {
            // Act - Init is called in constructor
            
            // Assert
            _channelMock.Verify(x => x.QueueDeclare(
                ExchangeContext.BookDirect,
                true,
                false,
                false,
                null), Times.Once);

            _channelMock.Verify(x => x.ExchangeDeclare(
                ExchangeContext.BookDirect,
                ExchangeType.Direct,
                false,
                false,
                null), Times.Once);
        }
       

        [Test]
        public void Publish_WithNullObject_ThrowsArgumentNullException()
        {
            // Arrange
            object nullObject = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _producer.Publish(nullObject));
        }

       
    }
}