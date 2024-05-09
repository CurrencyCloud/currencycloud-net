using System;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class FundingTransactions
    {
        public static readonly Entity.FundingTransaction FundingTransaction1 = new Entity.FundingTransaction
        {
            Id = "0c5c75f6-70ea-4f04-92ee-6148860c3b2b",
            Amount = new decimal(1.11f),
            Currency = "USD",
            Rail = "SEPA",
            AdditionalInformation = "CFST20231016143117",
            ValueDate = new DateTime(2023, 10, 16),
            ReceivingAccountNumber = "8302996933",
            CreatedAt = new DateTime(2023, 10, 16, 13, 31, 18),
            UpdatedAt = new DateTime(2023, 10, 16, 13, 31, 18),
            Sender = new Entity.FundingTransactionSender {
                SenderId = "5c675fa4-fdf0-4ee6-b5bb-156b36765433",
                SenderAddress = "test",
                SenderCountry = "GB",
                SenderName = "test"
            }
        };
    }
}