namespace CurrencyCloud
{
    class Credentials
    {
        public Credentials(string loginId, string apiKey)
        {
            ApiKey = apiKey;
            LoginId = loginId;
        }

        public string ApiKey { get; }
        public string LoginId { get; }
    }
}
