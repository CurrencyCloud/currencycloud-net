namespace CurrencyCloud.Entity
{
    public class IbanFindParameters : FindParameters
    {
        /// <summary>
        /// Currency
        /// </summary>
        [Param]
        public string Currency { get; set; }

    }
}