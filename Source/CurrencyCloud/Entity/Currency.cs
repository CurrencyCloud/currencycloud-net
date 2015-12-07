namespace CurrencyCloud.Entity
{
    public class Currency : Entity
    {
        internal Currency() { }

        public string Code { get; set; }

        public int DecimalPlaces { get; set; }

        public string Name { get; set; }
    }
}
