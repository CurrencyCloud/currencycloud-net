using CurrencyCloud.Entity;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class FundsSimulations
    {
        private static SimulateFunds CreateFundingSimulation(SimulateFundingAction action)
        {
            return new SimulateFunds()
            {
                Id = "a93260b9-5035-4e4b-8964-178ad8758ae6",
                ReceiverAccountNumber = "13071472",
                ReceiverRoutingCode = "03027514",
                Amount = 15000.80m,
                Currency = "USD",
                Action = action,
                SenderName = "John Doe",
                SenderReference = "sender-ref",
                SenderCountry = "US",
                SenderAccountNumber = "123456789",
                SenderRoutingCode = "200606",
                SenderAddress = "9303 Roslyndale Ave."
            };
        }

        public static readonly SimulateFunds ApprovedFundingSimulation = CreateFundingSimulation(SimulateFundingAction.approve);

        public static readonly SimulateFunds RejectFundingSimulation = CreateFundingSimulation(SimulateFundingAction.reject);
    }
}