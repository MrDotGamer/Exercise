using Exchange.Application.Handlers;
using Exchange.Application.Services;
using Exchange.Core.Data.Repository;
using Exchange.Services.Interfaces;
using Moq;
using System.Text;

namespace Exchange.Tests
{
    [TestFixture]
    public class CurrencyManagerTests
    {
        private CurrencyManager _currencyManager;
        private Mock<IGetAvailableRates>? _mockGetAvailableRates;
        private Mock<ICheckCountryCodeService>? _mockXmlService;

        [SetUp]
        public void Setup()
        {
            _mockGetAvailableRates = new Mock<IGetAvailableRates>();
            _mockXmlService = new Mock<ICheckCountryCodeService>();
            _currencyManager = new CurrencyManager(_mockGetAvailableRates.Object, _mockXmlService!.Object);
        }

        [Test]
        public async Task Exchange_ValidCurrenciesAndAmount_ReturnsCorrectExchangeAmount()
        {
            // Arrange
            string currencyFrom = "USD";
            string currencyTo = "EUR";
            decimal amount = 100;
            var rates = new Dictionary<string, decimal>
            {
                { "USD", 0.85m },
                { "EUR", 1.0m }
            };
            _mockGetAvailableRates!.Setup(r => r.GetRatesAsync()).ReturnsAsync(rates);

            // Act
            decimal result = await _currencyManager.ExchangeAsync(currencyFrom, currencyTo, amount);

            // Assert
            Assert.That(result, Is.EqualTo(85));
        }

        [Test]
        public async Task PrintResult_ValidArguments_PrintsCorrectResult()
        {
            // Arrange
            string[] args = ["USD", "EUR", "100"];
            decimal amount = 85;
            StringBuilder expectedOutput = new();
            expectedOutput.AppendLine("Exchanger change 100 of USD to 85 of EUR");
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            await _currencyManager.PrintResultAsync(args, amount);
            string result = consoleOutput.ToString().Trim();

            // Assert
            Assert.That(result, Is.EqualTo(expectedOutput.ToString().Trim()));
        }

        [Test]
        public async Task ValidateArguments_ShouldReturnValidatedArgs()
        {
            // Arrange
            var args = new string[] { "USD/EUR", "100" };
            var expectedArgs = new string[] { "USD", "EUR", "100" };

            var mockXmlService = new Mock<ICheckCountryCodeService>();
            mockXmlService.Setup(x => x.CheckCountryCodeAsync(It.IsAny<string[]>())).ReturnsAsync(true);

            var handler = new ArgumentsValidationHandler();
            handler.SetNext(new AlphabeticalCountryCodeValidationHandler())
                   .SetNext(new CurrencyArgumentsValidationHandler())
                   .SetNext(new CountryCodeValidationHandler(mockXmlService.Object));

            // Act
            var result = await handler.HandleAsync(args);

            // Assert
            Assert.That(result, Is.EqualTo(expectedArgs));
        }
    }
}
