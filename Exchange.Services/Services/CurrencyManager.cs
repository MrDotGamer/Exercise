﻿using Exchange.Application.Handlers;
using Exchange.Core.Data.Repository;
using Exchange.Services.Interfaces;
using System.Text;

namespace Exchange.Application.Services
{
    /// <summary>
    /// Represents a manager for handling currency exchange operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CurrencyManager"/> class.
    /// </remarks>
    /// <param name="getAvailableRates">The service for getting available rates.</param>
    /// <param name="xmlService">The service for checking country codes.</param>
    public class CurrencyManager(IGetAvailableRates getAvailableRates, ICheckCountryCodeService xmlService) : IManager
    {
        private readonly IGetAvailableRates _getAvailableRates = getAvailableRates;
        private readonly ICheckCountryCodeService _xmlService = xmlService;

        /// <summary>
        /// Validates the arguments passed to the exchange operation asynchronously.
        /// </summary>
        /// <param name="args">The arguments to validate.</param>
        /// <returns>An array of validation errors, if any.</returns>
        public async Task<string[]> ValidateArgumentsAsync(string[] args)
        {
            var handler = new ArgumentsValidationHandler();
            handler.SetNext(new AlphabeticalCountryCodeValidationHandler())
                   .SetNext(new CurrencyArgumentsValidationHandler())
                   .SetNext(new CountryCodeValidationHandler(_xmlService));

            return await handler.HandleAsync(args);
        }

        /// <summary>
        /// Performs the currency exchange operation asynchronously.
        /// </summary>
        /// <param name="currencyFrom">The currency to exchange from.</param>
        /// <param name="currencyTo">The currency to exchange to.</param>
        /// <param name="amount">The amount to exchange.</param>
        /// <returns>The exchanged amount.</returns>
        public async Task<decimal> ExchangeAsync(string currencyFrom, string currencyTo, decimal amount)
        {
            var list = await _getAvailableRates.GetRatesAsync();
            var from = list[currencyFrom];
            var to = list[currencyTo];
            return from / to * amount;
        }

        /// <summary>
        /// Prints the result of the exchange operation asynchronously.
        /// </summary>
        /// <param name="args">The arguments passed to the exchange operation.</param>
        /// <param name="result">The result of the exchange operation.</param>
        public async Task PrintResultAsync(string[] args, decimal result)
        {
            StringBuilder builder = new();

            builder.AppendLine($"Exchanger change {args[2]} of {args[0]} to {result} of {args[1]}");

            Console.WriteLine(builder.ToString());
        }
    }
}
