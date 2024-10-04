namespace Exchange.Exceptions
{
    public class CountryCodeDoesNotExistException : Exception
    {
        public CountryCodeDoesNotExistException(string message) : base(message)
        {
        }
    }
}
