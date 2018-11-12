using System;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class Transactions
    {
        public static readonly Entity.SenderDetails SenderDetails1 = new Entity.SenderDetails
        {
             Id = "0c5c75f6-70ea-4f04-92ee-6148860c3b2b",
            Amount = new decimal(10000.23f),
            Currency = "GBP",
            AdditionalInformation = "GBTRD-0001",
            ValueDate = new DateTime(2018, 01, 01),
            Sender = "FR7615589290001234567890113, CMBRFR2BARK, Debtor, FR, Centre ville",
            ReceivingAccountIban = "GB99OXPH94665099600083",
            CreatedAt = new DateTime(2018, 01, 01, 12, 34, 56),
            UpdatedAt = new DateTime(2018, 01, 01, 12, 34, 56)
        };
    }
}