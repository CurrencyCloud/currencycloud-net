[![NuGet version](https://img.shields.io/nuget/v/CurrencyCloud.svg)](https://www.nuget.org/packages/CurrencyCloud/) [![Travis](https://img.shields.io/travis/CurrencyCloud/currencycloud-net.svg)](https://travis-ci.org/CurrencyCloud/currencycloud-net)

# Currencycloud

This is the official .NET SDK for v2 of Currencycloud's API. Additional documentation for each API endpoint can be found at [Currencycloud API documentation][introduction]. If you have any queries or you require support, please contact our sales team at sales@currencycloud.com.

## Installation

The library is distributed on `NuGet`. To install the latest version, run the following command in the Package Manager Console: 

``` sh
PM> Install-Package CurrencyCloud
```

## Supported .NET versions

The least supported .NET framework version is 4.5.

# Usage

The following example retrieves a list of all tradeable currencies:

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

try
{
  await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

  Console.WriteLine("Available currencies: ");
  var res = await client.GetAvailableCurrenciesAsync();
  Console.WriteLine(string.Join(Environment.NewLine, res.Currencies));
  
  await client.CloseAsync();
}
catch (CurrencyCloud.Exception.ApiException ex)
{
  Console.WriteLine(ex.ToYamlString());
}
catch (Exception ex)
{
  Console.WriteLine(ex.Message);
}
```
More extensive examples can be found in the [Examples] solution.

## Client

The object of class `CurrencyCloud.Client` is a single entry point to access all Currency Cloud's API functions.

Supported APIs are listed in the [Currency Cloud API overview][overview].

## Initialization

Prior to making API requests via the `CurrencyCloud.Client` object, it must be initialized by calling `InitializeAsync(ApiServer apiServer, string loginId, string apiKey)` method, that starts a new session for the API user with the given credentials. If an API function is invoked by a non-initialized client, `InvalidOperationException` is thrown.

`InitializeAsync` method retrieves authentication token, which is passed with all subsequent API calls. If a call fails due to token is expired, then re-authentication is performed, so that the token is refreshed and the failed request is retried.

When working with API is finished, it is recommended to close the session and reset the client by calling its `CloseAsync()` method.

## Passing parameters

Most of the API functions accept entity instances as parameters. Every entity has mandatory parameters required by constructor. And optional parameters as properties. Null value means, that optional parameter not passed.

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

var beneficiary = new CurrencyCloud.Entity.BeneficiaryFindParameters
{
    BeneficiaryCountry = "GB",
    BeneficiaryEntityType = "company",
    BeneficiaryCompanyName = "JD Company LLC",
    BeneficiaryFirstName = "John",
    BeneficiaryLastName = "Doe",
    BeneficiaryCity = "London",
    BeneficiaryPostcode = "W11 2BQ",
    BeneficiaryStateOrProvince = "TX",
    BeneficiaryDateOfBirth = new DateTime(1990, 7, 20),
    Page = 1,
    PerPage = 5,
    Order = "name",
    OrderAscDesc = FindParameters.OrderDirection.Asc
};


var beneficiaries = await client.FindBeneficiariesAsync(beneficiary);
```

## Asynchrony

Every API function is an asynchronous non-blocking operation implementing the Task-based Asynchronous Pattern.

## On Behalf Of

Some API calls can be executed on behalf of another user (e.g. someone who has a sub-account with the logged in user). To achieve this, the `optional` argument of the SDK function should include `OnBehalfOf` parameter with a value of corresponding contact id:
`OnBehalfOf(string id, Func<Task> function)` method is used to run a bunch of API calls for the given contact id:

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

await client.OnBehalfOf("5c4716dc-42dd-4571-b4bf-0aa299fff928", async () =>
{
  var beneficiary = await client.CreateBeneficiaryAsync(new Beneficiary("Martin McFly", "FR", "EUR", "Employee Funds"));
  var conversion = await client.CreateConversionAsync(new ConversionCreate("EUR", "GBP", "buy", 10000, "Invoice Payment", true));
  var payment = await client.CreatePaymentAsync(new Payment("EUR", beneficiary.Id, 10000, "Invoice Payment", "Invoice 1234")
  {
    ConversionId = conversion.Id,
    PaymentType = "regular"
  });

  Console.WriteLine(payment);
});
```
## Errors

If an API call fails, the SDK function throws an exception, which derives from `APIException` class and represents some specific type of server error, e.g. `AuthenticationException` or `BadRequestException`.
Base `APIException` class converts the error to human-readable YAML string:

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

try
{
  var balance = await client.GetBalanceAsync("XYZ");
}
catch (ApiException ex)
{
  Console.WriteLine(ex.Message);
}

/* outputs
BadRequestException
---
platform: .NET 4.6 or later
request:
  parameters: {}
  verb: GET
  url: https://devapi.thecurrencycloud.com/v2/balances/XYZ
response:
  status_code: 400
  date: Fri, 12 Feb 2016 12:23:59 GMT
  request_id: 2984400063350753512
errors:
- field: currency
  code: invalid_currency
  message: XYZ is not a valid ISO 4217 currency code
  params:
    currency: XYZ
*/
```
# Development

## Dependencies

* [Newtonsoft.Json][newtonsoft]

## Versioning

This project uses [semantic versioning][semver]. You can safely express a dependency on a major version and expect all minor and patch versions to be backwards compatible.

# Copyright

Copyright (c) 2016 Currencycloud. See [LICENSE][license] for details.

[introduction]: https://developer.currencycloud.com/documentation/getting-started/introduction
[overview]:     https://developer.currencycloud.com/documentation/api-docs/overview/
[examples]:     Examples
[newtonsoft]:   https://www.nuget.org/packages/Newtonsoft.Json/
[semver]:       http://semver.org/
[license]:      LICENSE.md
