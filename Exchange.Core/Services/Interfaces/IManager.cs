namespace Exchange.Services.Interfaces
{
    /// <summary>
    /// Represents a manager for handling currency exchange operations.
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// Validates the arguments passed to the exchange operation asynchronously.
        /// </summary>
        /// <param name="args">The arguments to validate.</param>
        /// <returns>An array of validation errors, if any.</returns>
        Task<string[]> ValidateArgumentsAsync(string[] args);

        /// <summary>
        /// Performs the currency exchange asynchronously.
        /// </summary>
        /// <param name="currencyFrom">The currency to exchange from.</param>
        /// <param name="currencyTo">The currency to exchange to.</param>
        /// <param name="amount">The amount to exchange.</param>
        /// <returns>The exchanged amount.</returns>
        Task<decimal> ExchangeAsync(string currencyFrom, string currencyTo, decimal amount);

        /// <summary>
        /// Prints the result of the exchange operation asynchronously.
        /// </summary>
        /// <param name="args">The arguments passed to the exchange operation.</param>
        /// <param name="result">The result of the exchange operation.</param>
        Task PrintResultAsync(string[] args, decimal result);
    }
}