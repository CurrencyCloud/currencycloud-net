using NUnit.Framework;
using CurrencyCloud.Entity;
using CurrencyCloud.Tests.Mock.Data;
using CurrencyCloud.Entity.Pagination;
using CurrencyCloud.Tests.Mock.Http;
using CurrencyCloud.Environment;
using System.Threading.Tasks;
using System;

namespace CurrencyCloud.Tests
{
    [TestFixture]
    class ConversionsTest
    {
        Client client = new Client();
        Player player = new Player("/../../Mock/Http/Recordings/Conversions.json");

        [OneTimeSetUpAttribute]
        public void SetUp()
        {
            player.Start(ApiServer.Mock.Url);
            player.Play("SetUp");

            var credentials = Authentication.Credentials;

            client.InitializeAsync(Authentication.ApiServer, credentials.LoginId, credentials.ApiKey).Wait();
        }

        [OneTimeTearDownAttribute]
        public void TearDown()
        {
            player.Play("TearDown");

            client.CloseAsync().Wait();

            player.Close();
        }

        /// <summary>
        /// Successfully creates a conversion.
        /// </summary>
        [Test]
        public async Task Create()
        {
            player.Play("Create");

            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);

            Assert.AreEqual(conversion1.BuyCurrency, created.BuyCurrency);
            Assert.AreEqual(conversion1.SellCurrency, created.SellCurrency);
            Assert.AreEqual(conversion1.FixedSide, created.FixedSide);
        }

        /// <summary>
        /// Successfully gets a conversion.
        /// </summary>
        [Test]
        public async Task Get()
        {
            player.Play("Get");

            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            Conversion gotten = await client.GetConversionAsync(created.Id);

            Assert.AreEqual(gotten, created);
        }

        /// <summary>
        /// Successfully finds a conversion with search parameters.
        /// </summary>
        [Test]
        public async Task FindWithParams()
        {
            player.Play("FindWithParams");

            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            PaginatedConversions found = await client.FindConversionsAsync(new ConversionFindParameters
            {
                ConversionIds = new []
                {
                    created.Id
                },
                Order = "created_at",
                OrderAscDesc = FindParameters.OrderDirection.Desc,
                PerPage = 5
            });

            Assert.Contains(created, found.Conversions);
        }

        /// <summary>
        /// Successfully finds a conversion without search parameters.
        /// </summary>
        [Test]
        public async Task FindNoParams()
        {
            player.Play("FindNoParams");

            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            PaginatedConversions found = await client.FindConversionsAsync();

            Assert.Contains(created, found.Conversions);
        }

        /// <summary>
        /// Successfully quotes a conversion cancellation.
        /// </summary>
        [Test]
        public async Task QuoteCancel()
        {
            player.Play("QuoteCancel");
            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            ConversionCancellation cancelQuoted = await client.QuoteCancelConversionAsync(new ConversionCancellation
            {
                ConversionId = created.Id
            });

            Assert.AreEqual(cancelQuoted.Currency, created.BuyCurrency);
            Assert.IsNull(cancelQuoted.ConversionId);
            Assert.IsNull(cancelQuoted.ContactId);
            Assert.IsNull(cancelQuoted.AccountId);
            Assert.NotZero((decimal)cancelQuoted.Amount);
            Assert.IsNull(cancelQuoted.Notes);
            Assert.NotNull(cancelQuoted.EventDateTime);
            Assert.IsNull(cancelQuoted.EventAccountId);
            Assert.IsNull(cancelQuoted.EventContactId);
            Assert.IsNull(cancelQuoted.EventType);
        }

        /// <summary>
        /// Successfully cancels a conversion.
        /// </summary>
        [Test]
        public async Task Cancel()
        {
            player.Play("Cancel");
            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            ConversionCancellation cancelled = await client.CancelConversionsAsync(new ConversionCancellation
            {
                ConversionId = created.Id,
                Notes = "some notes"
            });

            Assert.AreEqual(cancelled.ConversionId, created.Id);
        }

        /// <summary>
        /// Successfully quotes a conversion date change.
        /// </summary>
        [Test]
        public async Task QuoteDateChange()
        {
            player.Play("QuoteDateChange");
            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);

            DateTime newSettlementDate = DateTime.Parse("2018-02-02T12:34:56+00:00");
            ConversionDateChange dateChangeQuoted = await client.QuoteDateChangeConversionAsync(new ConversionDateChange {
                ConversionId = created.Id,
                NewSettlementDate = newSettlementDate
            });

            Assert.AreEqual(dateChangeQuoted.ConversionId, created.Id);
            Assert.AreEqual(dateChangeQuoted.Currency, created.SellCurrency);
            Assert.NotZero((decimal)dateChangeQuoted.Amount);
            Assert.AreEqual(dateChangeQuoted.NewSettlementDate, newSettlementDate);
            Assert.NotNull(dateChangeQuoted.NewConversionDate);
            Assert.NotNull(dateChangeQuoted.OldConversionDate);
            Assert.NotNull(dateChangeQuoted.OldSettlementDate);
            Assert.NotNull(dateChangeQuoted.EventDateTime);
        }

        /// <summary>
        /// Successfully changes the date of a conversion.
        /// </summary>
        [Test]
        public async Task DateChange()
        {
            player.Play("DateChange");
            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);

            DateTime newSettlementDate = DateTime.Parse("2018-02-02T12:34:56+00:00");
            ConversionDateChange dateChanged = await client.DateChangeConversionAsync(new ConversionDateChange {
                ConversionId = created.Id,
                NewSettlementDate = newSettlementDate
            });

            Assert.AreEqual(dateChanged.ConversionId, created.Id);
            Assert.AreEqual(dateChanged.Currency, created.SellCurrency);
            Assert.NotZero((decimal)dateChanged.Amount);
            Assert.AreEqual(dateChanged.NewSettlementDate, newSettlementDate);
            Assert.NotNull(dateChanged.NewConversionDate);
            Assert.NotNull(dateChanged.OldConversionDate);
            Assert.NotNull(dateChanged.OldSettlementDate);
            Assert.NotNull(dateChanged.EventDateTime);
        }

        /// <summary>
        /// Successfully previews a conversion split.
        /// </summary>
        [Test]
        public async Task PreviewSplit()
        {
            player.Play("PreviewSplit");
            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            ConversionSplit splitPreviewed = await client.PreviewSplitConversionAsync(new Conversion
            {
                Id = created.Id,
                Amount = 9370
            });

            Assert.AreEqual(splitPreviewed.ParentConversion.Id, created.Id);
            Assert.IsNull(splitPreviewed.ChildConversion.Id);
            Assert.NotNull(splitPreviewed.ParentConversion.ShortReference);
            Assert.IsNull(splitPreviewed.ChildConversion.ShortReference);
            Assert.AreEqual(splitPreviewed.ParentConversion.SellCurrency, created.SellCurrency);
            Assert.AreEqual(splitPreviewed.ParentConversion.BuyCurrency, created.BuyCurrency);
            Assert.AreEqual(splitPreviewed.ChildConversion.BuyCurrency, splitPreviewed.ParentConversion.BuyCurrency);
            Assert.AreEqual(splitPreviewed.ChildConversion.SellCurrency, splitPreviewed.ParentConversion.SellCurrency);
            Assert.AreEqual(splitPreviewed.ParentConversion.ConversionDate, splitPreviewed.ChildConversion.ConversionDate);
            Assert.AreEqual(splitPreviewed.ParentConversion.SettlementDate, splitPreviewed.ChildConversion.SettlementDate);
            Assert.AreEqual(splitPreviewed.ParentConversion.Status, splitPreviewed.ChildConversion.Status);
            Assert.AreEqual(created.ClientBuyAmount, splitPreviewed.ParentConversion.BuyAmount + splitPreviewed.ChildConversion.BuyAmount);
            Assert.AreEqual(created.ClientSellAmount, splitPreviewed.ParentConversion.SellAmount + splitPreviewed.ChildConversion.SellAmount);
        }

        /// <summary>
        /// Successfully splits a conversion.
        /// </summary>
        [Test]
        public async Task Split()
        {
            player.Play("Split");
            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            ConversionSplit split = await client.SplitConversionAsync(new Conversion
            {
                Id = created.Id,
                Amount = 9370
            });

            Assert.AreEqual(split.ParentConversion.Id, created.Id);
            Assert.NotNull(split.ChildConversion.Id);
            Assert.NotNull(split.ParentConversion.ShortReference);
            Assert.NotNull(split.ChildConversion.ShortReference);
            Assert.AreEqual(split.ParentConversion.SellCurrency, created.SellCurrency);
            Assert.AreEqual(split.ParentConversion.BuyCurrency, created.BuyCurrency);
            Assert.AreEqual(split.ChildConversion.BuyCurrency, split.ParentConversion.BuyCurrency);
            Assert.AreEqual(split.ChildConversion.SellCurrency, split.ParentConversion.SellCurrency);
            Assert.AreEqual(split.ParentConversion.SettlementDate, split.ChildConversion.SettlementDate);
            Assert.AreEqual(split.ParentConversion.Status, split.ChildConversion.Status);
            Assert.AreEqual(created.ClientBuyAmount, split.ParentConversion.BuyAmount + split.ChildConversion.BuyAmount);
            Assert.AreEqual(created.ClientSellAmount, split.ParentConversion.SellAmount + split.ChildConversion.SellAmount);
        }

        /// <summary>
        /// Successfully splits a conversion.
        /// </summary>
        [Test]
        public async Task SplitHistory()
        {
            player.Play("SplitHistory");
            var conversion1 = Conversions.Conversion1;

            Conversion created = await client.CreateConversionAsync(conversion1);
            ConversionSplit splitConversion = await client.SplitConversionAsync(new Conversion
            {
                Id = created.Id,
                Amount = 9370
            });
            ConversionSplit splitChild = await client.SplitConversionAsync(new Conversion
            {
                Id = splitConversion.ChildConversion.Id,
                Amount = 6951
            });
            ConversionSplitHistory splitHistoryParent = await client.SplitHistoryConversionAsync(new Conversion
            {
                Id = splitConversion.ParentConversion.Id
            });
            ConversionSplitHistory splitHistoryChildChild = await client.SplitHistoryConversionAsync(new Conversion
            {
                Id = splitChild.ChildConversion.Id
            });

            Assert.AreEqual(splitConversion.ParentConversion.Id, created.Id);
            Assert.AreEqual(splitChild.ParentConversion.Id, splitConversion.ChildConversion.Id);
            Assert.AreEqual(splitConversion.ChildConversion.BuyAmount, splitChild.ParentConversion.BuyAmount + splitChild.ChildConversion.BuyAmount);
            Assert.NotNull(splitHistoryParent.ParentConversion);
            Assert.Null(splitHistoryParent.OriginConversion);
            Assert.IsNotEmpty(splitHistoryParent.ChildConversions);
            Assert.NotNull(splitHistoryChildChild.ParentConversion);
            Assert.NotNull(splitHistoryChildChild.OriginConversion);
            Assert.IsEmpty(splitHistoryChildChild.ChildConversions);
            Assert.AreEqual(splitHistoryChildChild.OriginConversion.Id, splitHistoryParent.ParentConversion.Id);
        }

        /// <summary>
        /// Returns an object that contains information related to actions on conversions that have generated profit or loss.
        /// </summary>
        [Test]
        public async Task FindProfitAndLosses()
        {
            player.Play("FindProfitAndLosses");

            PaginatedConversionProfitAndLosses profitAndLosses = await client.FindConversionProfitAndLossesAsync();

            foreach (ConversionProfitAndLoss element in profitAndLosses.ConversionProfitAndLosses)
            {
                Assert.NotNull(element.AccountId);
                Assert.NotNull(element.ContactId);
                Assert.NotNull(element.EventAccountId);
                Assert.NotNull(element.EventContactId);
                Assert.AreEqual(element.EventType, "self_service_roll");
                Assert.NotZero(element.Amount ?? 0);
                Assert.NotNull(element.Currency);
                Assert.NotNull(element.EventDateTime);
            }
            Assert.AreEqual(profitAndLosses.ConversionProfitAndLosses.Count, profitAndLosses.Pagination.TotalEntries);
        }
        
        /// <summary>
        /// Successfully creates a conversion with conversion date preference.
        /// </summary>
        [Test]
        public async Task CreateWithConversionDatePreference()
        {
            player.Play("CreateWithConversionDatePreference");

            var conversion =  new Conversion(
                "EUR",
                "GBP",
                "buy",
                1000.00m,
                true
            );
            conversion.ConversionDatePreference = "earliest";

            var created = await client.CreateConversionAsync(conversion);

            Assert.That(created, Is.Not.Null);
            Assert.AreEqual(805.90, created.ClientSellAmount);
            Assert.AreEqual(DateTime.Parse("2020-05-19T00:00:00+00:00"), created.ConversionDate);

        }
    }
}
