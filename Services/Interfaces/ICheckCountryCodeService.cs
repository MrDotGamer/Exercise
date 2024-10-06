namespace Exchange.Services.Interfaces
{

    /// <summary>
    /// Represents a service for checking country codes.
    /// </summary>
    public interface ICheckCountryCodeService
    {
        /// <summary>
        /// Asynchronously checks if the given country codes are valid.
        /// </summary>
        /// <param name="codes">An array of country codes to check.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating if the country codes are valid.</returns>
        Task<bool> CheckCountryCodeAsync(string[] codes);
    }
}
