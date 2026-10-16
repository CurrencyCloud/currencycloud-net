using System.Collections.Generic;
using NUnit.Framework;
using CurrencyCloud.Entity;

namespace CurrencyCloud.Tests
{
    /// <summary>
    /// Fork behaviour: an entity's Equals compares its lists null-safely. Upstream's SequenceEqual threw when either side was null.
    /// </summary>
    [TestFixture]
    class EntityEqualsTest
    {
        [Test]
        public void TreatsTwoNullListsAsEqual()
        {
            Assert.That(new Beneficiary(), Is.EqualTo(new Beneficiary()));
            Assert.That(new Conversion(), Is.EqualTo(new Conversion()));
            Assert.That(new Payer(), Is.EqualTo(new Payer()));
            Assert.That(new SettlementAccount(), Is.EqualTo(new SettlementAccount()));
            Assert.That(new PaymentTrackingInfo(), Is.EqualTo(new PaymentTrackingInfo()));
        }

        [Test]
        public void TreatsOneNullListAsDifferent()
        {
            var withAddress = new SettlementAccount { BankAddress = new List<string> { "1 High Street" } };

            Assert.That(withAddress, Is.Not.EqualTo(new SettlementAccount()));
            Assert.That(new SettlementAccount(), Is.Not.EqualTo(withAddress));
        }

        [Test]
        public void ComparesListsByTheirElements()
        {
            var first = new Conversion { PaymentIds = new List<string> { "a", "b" } };

            Assert.That(first, Is.EqualTo(new Conversion { PaymentIds = new List<string> { "a", "b" } }));
            Assert.That(first, Is.Not.EqualTo(new Conversion { PaymentIds = new List<string> { "b", "a" } }));
        }
    }
}
