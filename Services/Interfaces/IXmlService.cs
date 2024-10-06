namespace Exchange.Services.Interfaces
{
    public interface IXmlService
    {
        Task<bool> CheckCountryCodeAsync(string[] codes);
    }
}
