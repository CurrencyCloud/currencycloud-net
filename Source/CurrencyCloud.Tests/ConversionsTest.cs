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
        Player player = new Player("Mock/Http/Recordings/Conversions.json");

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
            conversion1.QuoteId = "3c25ce4a-3552-45bb-869e-406c795052aa";

            Conversion created = await client.CreateConversionAsync(conversion1);

            Assert.That(created.BuyCurrency, Is.EqualTo(conversion1.BuyCurrency));
            Assert.That(created.SellCurrency, Is.EqualTo(conversion1.SellCurrency));
            Assert.That(created.FixedSide, Is.EqualTo(conversion1.FixedSide));
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

            Assert.That(created, Is.EqualTo(gotten));
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

            Assert.That(found.Conversions, Does.Contain(created));
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

            Assert.That(found.Conversions, Does.Contain(created));
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

            Assert.That(created.BuyCurrency, Is.EqualTo(cancelQuoted.Currency));
            Assert.That(cancelQuoted.ConversionId, Is.Null);
            Assert.That(cancelQuoted.ContactId, Is.Null);
            Assert.That(cancelQuoted.AccountId, Is.Null);
            Assert.That(cancelQuoted.Amount, Is.Not.Zero);
            Assert.That(cancelQuoted.Notes, Is.Null);
            Assert.That(cancelQuoted.EventDateTime, Is.Not.Null);
            Assert.That(cancelQuoted.EventAccountId, Is.Null);
            Assert.That(cancelQuoted.EventContactId, Is.Null);
            Assert.That(cancelQuoted.EventType, Is.Null);
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

            Assert.That(created.Id, Is.EqualTo(cancelled.ConversionId));
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

            Assert.That(created.Id, Is.EqualTo(dateChangeQuoted.ConversionId));
            Assert.That(created.SellCurrency, Is.EqualTo(dateChangeQuoted.Currency));
            Assert.That(dateChangeQuoted.Amount, Is.Not.Zero);
            Assert.That(newSettlementDate, Is.EqualTo(dateChangeQuoted.NewSettlementDate));
            Assert.That(dateChangeQuoted.NewConversionDate, Is.Not.Null);
            Assert.That(dateChangeQuoted.OldConversionDate, Is.Not.Null);
            Assert.That(dateChangeQuoted.OldSettlementDate, Is.Not.Null);
            Assert.That(dateChangeQuoted.EventDateTime, Is.Not.Null);
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

            Assert.That(created.Id, Is.EqualTo(dateChanged.ConversionId));
            Assert.That(created.SellCurrency, Is.EqualTo(dateChanged.Currency));
            Assert.That(dateChanged.Amount, Is.Not.Zero);
            Assert.That(newSettlementDate, Is.EqualTo(dateChanged.NewSettlementDate));
            Assert.That(dateChanged.NewConversionDate, Is.Not.Null);
            Assert.That(dateChanged.OldConversionDate, Is.Not.Null);
            Assert.That(dateChanged.OldSettlementDate, Is.Not.Null);
            Assert.That(dateChanged.EventDateTime, Is.Not.Null);
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

            Assert.That(created.Id, Is.EqualTo(splitPreviewed.ParentConversion.Id));
            Assert.That(splitPreviewed.ChildConversion.Id, Is.Null);
            Assert.That(splitPreviewed.ParentConversion.ShortReference, Is.Not.Null);
            Assert.That(splitPreviewed.ChildConversion.ShortReference, Is.Null);
            Assert.That(created.SellCurrency, Is.EqualTo(splitPreviewed.ParentConversion.SellCurrency));
            Assert.That(created.BuyCurrency, Is.EqualTo(splitPreviewed.ParentConversion.BuyCurrency));
            Assert.That(splitPreviewed.ParentConversion.BuyCurrency, Is.EqualTo(splitPreviewed.ChildConversion.BuyCurrency));
            Assert.That(splitPreviewed.ParentConversion.SellCurrency, Is.EqualTo(splitPreviewed.ChildConversion.SellCurrency));
            Assert.That(splitPreviewed.ChildConversion.ConversionDate, Is.EqualTo(splitPreviewed.ParentConversion.ConversionDate));
            Assert.That(splitPreviewed.ChildConversion.SettlementDate, Is.EqualTo(splitPreviewed.ParentConversion.SettlementDate));
            Assert.That(splitPreviewed.ChildConversion.Status, Is.EqualTo(splitPreviewed.ParentConversion.Status));
            Assert.That(splitPreviewed.ParentConversion.BuyAmount + splitPreviewed.ChildConversion.BuyAmount, Is.EqualTo(created.ClientBuyAmount));
            Assert.That(splitPreviewed.ParentConversion.SellAmount + splitPreviewed.ChildConversion.SellAmount, Is.EqualTo(created.ClientSellAmount));
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

            Assert.That(created.Id, Is.EqualTo(split.ParentConversion.Id));
            Assert.That(split.ChildConversion.Id, Is.Not.Null);
            Assert.That(split.ParentConversion.ShortReference, Is.Not.Null);
            Assert.That(split.ChildConversion.ShortReference, Is.Not.Null);
            Assert.That(created.SellCurrency, Is.EqualTo(split.ParentConversion.SellCurrency));
            Assert.That(created.BuyCurrency, Is.EqualTo(split.ParentConversion.BuyCurrency));
            Assert.That(split.ParentConversion.BuyCurrency, Is.EqualTo(split.ChildConversion.BuyCurrency));
            Assert.That(split.ParentConversion.SellCurrency, Is.EqualTo(split.ChildConversion.SellCurrency));
            Assert.That(split.ChildConversion.SettlementDate, Is.EqualTo(split.ParentConversion.SettlementDate));
            Assert.That(split.ChildConversion.Status, Is.EqualTo(split.ParentConversion.Status));
            Assert.That(split.ParentConversion.BuyAmount + split.ChildConversion.BuyAmount, Is.EqualTo(created.ClientBuyAmount));
            Assert.That(split.ParentConversion.SellAmount + split.ChildConversion.SellAmount, Is.EqualTo(created.ClientSellAmount));
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

            Assert.That(created.Id, Is.EqualTo(splitConversion.ParentConversion.Id));
            Assert.That(splitConversion.ChildConversion.Id, Is.EqualTo(splitChild.ParentConversion.Id));
            Assert.That(splitChild.ParentConversion.BuyAmount + splitChild.ChildConversion.BuyAmount, Is.EqualTo(splitConversion.ChildConversion.BuyAmount));
            Assert.That(splitHistoryParent.ParentConversion, Is.Not.Null);
            Assert.That(splitHistoryParent.OriginConversion, Is.Null);
            Assert.That(splitHistoryParent.ChildConversions, Is.Not.Empty);
            Assert.That(splitHistoryChildChild.ParentConversion, Is.Not.Null);
            Assert.That(splitHistoryChildChild.OriginConversion, Is.Not.Null);
            Assert.That(splitHistoryChildChild.ChildConversions, Is.Empty);
            Assert.That(splitHistoryParent.ParentConversion.Id, Is.EqualTo(splitHistoryChildChild.OriginConversion.Id));
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
                Assert.That(element.AccountId, Is.Not.Null);
                Assert.That(element.ContactId, Is.Not.Null);
                Assert.That(element.EventAccountId, Is.Not.Null);
                Assert.That(element.EventContactId, Is.Not.Null);
                Assert.That(element.EventType, Is.EqualTo("self_service_roll"));
                Assert.That(element.Amount ?? 0, Is.Not.Zero);
                Assert.That(element.Currency, Is.Not.Null);
                Assert.That(element.EventDateTime, Is.Not.Null);
            }
            Assert.That(profitAndLosses.Pagination.TotalEntries, Is.EqualTo(profitAndLosses.ConversionProfitAndLosses.Count));
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
            Assert.That(created.ClientSellAmount, Is.EqualTo(805.90));
            Assert.That(created.ConversionDate, Is.EqualTo(DateTime.Parse("2020-05-19T00:00:00+00:00")));

        }
    }
}
