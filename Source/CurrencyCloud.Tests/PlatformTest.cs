using CurrencyCloud.Environment;
using NUnit.Framework;

namespace CurrencyCloud.Tests;

[TestFixture]
public class PlatformTest
{
    [Test]
    public void Platform_Version_Should_Not_Be_Null_Or_Empty()
    {
        Assert.That(Platform.Version, Is.Not.Null.And.Not.Empty);
    }
}
