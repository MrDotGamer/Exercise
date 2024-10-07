using Exchange.Application.Exceptions;
using Exchange.Application.Handlers;
using Exchange.Services.Interfaces;
using Moq;

namespace Exchange.Tests
{
    public class ExchangeExtensionsTests
    {
        [Test]
        public async Task ValidateArguments_ValidArgs_ReturnsExpectedResult()
        {
            // Arrange
            var args = new string[] { "DKK/USD", "100" };
            var expected = new string[] { "DKK/USD", "100" };
            var handler = new ArgumentsValidationHandler();

            // Act
            var result = await handler.HandleAsync(args);

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public async Task ValidateArgumentsCount_InvalidArgs_ThrowsValidationException()
        {
            // Arrange
            var args = new string[] { "DKK/USD", "100", "3" };
            var args1 = new string[] { "DKK/USD" };
            var handler = new ArgumentsValidationHandler();
            // Act
            async Task testDelegate() => await handler.HandleAsync(args);
            async Task testDelegate1() => await handler.HandleAsync(args1);

            // Assert
            Assert.ThrowsAsync<FluentValidation.ValidationException>(testDelegate);
            Assert.ThrowsAsync<FluentValidation.ValidationException>(testDelegate1);
        }

        [Test]
        public async Task ValidateArguments_InvalidCountryCode_ReturnsError()
        {
            // Arrange
            var args = new string[] { "XYZ/USD", "100" };
            var args1 = new string[] { "DKK/XYZ", "100" };

            var mockXmlService = new Mock<ICheckCountryCodeService>();
            mockXmlService.Setup(s => s.CheckCountryCodeAsync(It.IsAny<string[]>())).ReturnsAsync(false);

            var handler = new CountryCodeValidationHandler(mockXmlService.Object);

            // Act

            async Task testDelegate() => await handler.HandleAsync(args);
            async Task testDelegate1() => await handler.HandleAsync(args1);

            // Assert
            Assert.ThrowsAsync<ValidateCurrencyCountryCodeArgumentsException>(testDelegate);
            Assert.ThrowsAsync<ValidateCurrencyCountryCodeArgumentsException>(testDelegate1);
        }

        [Test]
        public async Task ValidateArguments_ValidCountryCode_ReturnsExpectedResult()
        {
            // Arrange
            var args = new string[] { "DKK", "USD", "100" };
            var expected = new string[] { "DKK", "USD", "100" };
            var mockXmlService = new Mock<ICheckCountryCodeService>();
            mockXmlService.Setup(s => s.CheckCountryCodeAsync(It.IsAny<string[]>())).ReturnsAsync(true);

            var handler = new CountryCodeValidationHandler(mockXmlService.Object);

            // Act
            var result = await handler.HandleAsync(args);

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public async Task AlphabeticalCountryCodeHandler_InvalidCurrencyCode_ReturnsError()
        {
            // Arrange
            var args = new string[] { "US", "XYZ" };
            string[] args1 = { "USD\\USD", "100" };
            string[] args2 = { "USD/US", "100" };
            string[] args3 = { "1US/USD", "100" };
            string[] args4 = { "US,USD3", "100" };
            string[] args5 = { "US|USD", "100" };
            string[] args6 = { "US-USD", "100" };

            var handler = new AlphabeticalCountryCodeValidationHandler();
            // Act

            async Task testDelegate() => await handler.HandleAsync(args);
            async Task testDelegate1() => await handler.HandleAsync(args1);
            async Task testDelegate2() => await handler.HandleAsync(args2);
            async Task testDelegate3() => await handler.HandleAsync(args3);
            async Task testDelegate4() => await handler.HandleAsync(args4);
            async Task testDelegate5() => await handler.HandleAsync(args5);
            async Task testDelegate6() => await handler.HandleAsync(args6);

            // Assert
            Assert.ThrowsAsync<AlphabeticCountryCodeValidationException>(testDelegate);
            Assert.ThrowsAsync<AlphabeticCountryCodeValidationException>(testDelegate1);
            Assert.ThrowsAsync<AlphabeticCountryCodeValidationException>(testDelegate2);
            Assert.ThrowsAsync<AlphabeticCountryCodeValidationException>(testDelegate3);
            Assert.ThrowsAsync<AlphabeticCountryCodeValidationException>(testDelegate4);
            Assert.ThrowsAsync<AlphabeticCountryCodeValidationException>(testDelegate5);
            Assert.ThrowsAsync<AlphabeticCountryCodeValidationException>(testDelegate6);
        }

        [Test]
        public async Task AlphabeticalCountryCodeHandler_ValidCurrencyCode()
        {
            // Arrange
            var args = new string[] { "DKK/USD", "100" };
            var expected = new string[] { "DKK", "USD", "100" };

            var handler = new AlphabeticalCountryCodeValidationHandler();

            // Act
            var result = await handler.HandleAsync(args);

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public async Task TryParseCurrencyArguments_ValidArgs_ArgumentUpdated()
        {
            // Arrange
            string[] args = { "USA", "USD", "100,50" };
            string[] args1 = { "USA", "USD", "000,50" };
            string[] args2 = { "USA", "USD", "1000,50" };
            string[] args3 = { "USA", "USD", "1,5" };
            string[] args4 = { "USA", "USD", "0,50" };
            string[] args5 = { "USA", "USD", ",50" };
            string[] args6 = { "USA", "USD", "50," };

            // Act
            var handler = new CurrencyArgumentsValidationHandler();

            var result = await handler.HandleAsync(args);
            var result1 = await handler.HandleAsync(args1);
            var result2 = await handler.HandleAsync(args2);
            var result3 = await handler.HandleAsync(args3);
            var result4 = await handler.HandleAsync(args4);
            var result5 = await handler.HandleAsync(args5);
            var result6 = await handler.HandleAsync(args6);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result[2], Is.EqualTo("100.50"));
                Assert.That(result1[2], Is.EqualTo("000.50"));
                Assert.That(result2[2], Is.EqualTo("1000.50"));
                Assert.That(result3[2], Is.EqualTo("1.5"));
                Assert.That(result4[2], Is.EqualTo("0.50"));
                Assert.That(result5[2], Is.EqualTo(".50"));
                Assert.That(result6[2], Is.EqualTo("50"));
            });
        }

        [Test]
        public async Task TryParseCurrencyArguments_InvalidValidArgs_ReturnsError()
        {
            // Arrange
            string[] args = { "USA", "USD", "1,000.50.00" };
            string[] args1 = { "USA", "USD", "50,0d" };
            string[] args2 = { "USA", "USD", "d,00" };

            // Act
            var handler = new CurrencyArgumentsValidationHandler();
            async Task testDelegate1() => await handler.HandleAsync(args1);
            async Task testDelegate2() => await handler.HandleAsync(args2);
            async Task testDelegate() => await handler.HandleAsync(args);

            // Act & Assert
            Assert.ThrowsAsync<ParseCurrencyArgumentsException>(testDelegate);
            Assert.ThrowsAsync<ParseCurrencyArgumentsException>(testDelegate1);
            Assert.ThrowsAsync<ParseCurrencyArgumentsException>(testDelegate2);
        }
    }
}
