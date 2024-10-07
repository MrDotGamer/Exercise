using Exchange.Core.Services;
using Exchange.Services.Interfaces;

namespace Exchange.Tests
{
    class ExchangeStrategyContextTests
    {
        private ExchangeStrategyContext _context;
        private MockStrategy _mockStrategy;

        [SetUp]
        public void Setup()
        {
            _context = new ExchangeStrategyContext();
            _mockStrategy = new MockStrategy();
        }

        [Test]
        public async Task AddStrategy_ShouldAddStrategyToDictionary()
        {
            // Arrange
            var key = "TestStrategy";

            // Act
            _context.AddStrategy(key, _mockStrategy);

            // Assert
            Assert.DoesNotThrowAsync(async () => await _context.ExecuteStrategyAsync(key, null));
        }

        [Test]
        public void ExecuteStrategy_ShouldExecuteAddedStrategy()
        {
            // Arrange
            var key = "TestStrategy";
            _context.AddStrategy(key, _mockStrategy);

            // Act
            Task task = _context.ExecuteStrategyAsync(key, null);

            // Assert
            Assert.That(_mockStrategy.Executed, Is.True);
        }

        [Test]
        public async Task ExecuteStrategy_ShouldThrowArgumentException_WhenStrategyNotFound()
        {
            // Arrange
            var key = "NonExistentStrategy";

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _context.ExecuteStrategyAsync(key, null));
            Assert.That(ex.Message, Is.EqualTo($"Strategy not found for key: {key}"));
        }

        private class MockStrategy : IStrategy
        {
            public string Name => "MockStrategy";
            public bool Executed { get; private set; }

            public async Task ExecuteAsync(object args)
            {
                Executed = true;
            }
        }
    }
}
