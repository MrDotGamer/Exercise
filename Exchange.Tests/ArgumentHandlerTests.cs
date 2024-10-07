using Exchange.Application.Services;

namespace Exchange.Tests;

public class ArgumentHandlerTests
{
    private TestArgumentHandler _handler;
    private TestArgumentHandler _nextHandler;

    [SetUp]
    public void Setup()
    {
        _handler = new TestArgumentHandler();
        _nextHandler = new TestArgumentHandler();
    }

    [Test]
    public void SetNext_ShouldSetNextHandler()
    {
        // Act
        var result = _handler.SetNext(_nextHandler);

        // Assert
        Assert.That(result, Is.EqualTo(_nextHandler));
    }

    [Test]
    public async Task Handle_ShouldCallNextHandler_WhenNextHandlerIsSet()
    {
        // Arrange
        var args = new string[] { "arg1", "arg2" };
        _handler.SetNext(_nextHandler);

        // Act
        var result = await _handler.HandleAsync(args);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(_nextHandler.HandleCalled, Is.True);
            Assert.That(result, Is.EqualTo(args));
        });
    }

    [Test]
    public async Task Handle_ShouldReturnArgs_WhenNextHandlerIsNotSet()
    {
        // Arrange
        var args = new string[] { "arg1", "arg2" };

        // Act
        var result = await _handler.HandleAsync(args);

        // Assert
        Assert.That(result, Is.EqualTo(args));
    }

    private class TestArgumentHandler : ArgumentHandlerService
    {
        public bool HandleCalled { get; private set; }

        public override async Task<string[]> HandleAsync(string[] args)
        {
            HandleCalled = true;
            return await base.HandleAsync(args);
        }
    }
}
