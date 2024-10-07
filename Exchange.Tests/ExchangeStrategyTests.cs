using Exchange.Application.Services;
using Exchange.Services.Interfaces;
using Moq;

namespace Exchange.Tests
{
    public class ExchangeStrategyTests
    {
        private Mock<IManager> _mockManager;
        private ExchangeStrategy _exchangeStrategy;

        [SetUp]
        public void Setup()
        {
            _mockManager = new Mock<IManager>();
            _exchangeStrategy = new ExchangeStrategy(_mockManager.Object);
        }

        [Test]
        public async Task Execute_ValidArguments_CallsManagerMethods()
        {
            // Arrange
            var args = new string[] { "Exchange", "USD", "EUR", "100" };
            var validArgs = new string[] { "USD", "EUR", "100" };
            var amount = 85m;

            _mockManager.Setup(m => m.ValidateArgumentsAsync(It.IsAny<string[]>())).ReturnsAsync(validArgs);
            _mockManager.Setup(m => m.ExchangeAsync(validArgs[0], validArgs[1], decimal.Parse(validArgs[2]))).ReturnsAsync(amount);

            // Act
            await _exchangeStrategy.ExecuteAsync(args);

            // Assert
            _mockManager.Verify(m => m.ValidateArgumentsAsync(It.Is<string[]>(a => a[0] == "USD" && a[1] == "EUR")), Times.Once);
            _mockManager.Verify(m => m.ExchangeAsync("USD", "EUR", 100m), Times.Once);
            _mockManager.Verify(m => m.PrintResultAsync(validArgs, amount), Times.Once);
        }

        [Test]
        public async Task Execute_InvalidArguments_ThrowsArgumentException()
        {
            // Arrange
            var args = new object();

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _exchangeStrategy.ExecuteAsync(args));
        }
    }
}
