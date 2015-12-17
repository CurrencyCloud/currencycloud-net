[![NuGet version](https://img.shields.io/nuget/v/CurrencyCloud.svg)](https://www.nuget.org/packages/CurrencyCloud/) [![Travis](https://img.shields.io/travis/rust-lang/rust.svg)](https://github.com/CurrencyCloud/currencycloud-net)

# Currency Cloud

This is the official .NET SDK for v2 of Currency Cloud's API. Additional documentation for each API endpoint can be found at [Currency Cloud API documentation][introduction]. If you have any queries or you require support, please contact our implementation team at implementation@currencycloud.com.

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

Most of the API functions accept both required and optional parameters. To simplify creation of optional parameter sets of variable length `ParamsObject` class is introduced in the SDK. It is an implementation of the dynamic object concept that enables accessing properties via dot notation and adding or removing them in runtime:

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

dynamic beneficiary = new CurrencyCloud.ParamsObject();
beneficiary.BeneficiaryCountry = "GB";
beneficiary.BeneficiaryEntityType = "company";
beneficiary.BeneficiaryCompanyName = "JD Company LLC";
beneficiary.BeneficiaryFirstName = "John";
beneficiary.BeneficiaryLastName = "Doe";
beneficiary.BeneficiaryCity = "London";
beneficiary.BeneficiaryPostcode = "W11 2BQ";
beneficiary.BeneficiaryStateOrProvince = "TX";
beneficiary.BeneficiaryDateOfBirth = new DateTime(1990, 7, 20);

dynamic pagination = new CurrencyCloud.ParamsObject(new
{
  Page = 1,
  PerPage = 5,
  Order = "name",
  OrderAscDesc = "asc "
});

var beneficiaries = await client.FindBeneficiariesAsync(beneficiary + pagination);
```
`ParamsObject` class expects dynamic parameter names to follow PascalCase naming convention.

## Asynchrony

Every API function is an asynchronous non-blocking operation implementing the Task-based Asynchronous Pattern.

## On Behalf Of

Some API calls can be executed on behalf of another user (e.g. someone who has a sub-account with the logged in user). To achieve this, the `optional` argument of the SDK function should include `OnBehalfOf` parameter with a value of corresponding contact id:

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

var rate = await client.GetRateAsync("SEK", "GBP", "buy", 900, new ParamsObject(new
{
  OnBehalfOf = "8f639ab2-2b85-4327-9eb1-01ee4f0c77bc"
}));
```
Alternatively, `OnBehalfOf(string id, Func<Task> function)` method can be used to run a bunch of API calls for the given contact id:

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

await client.OnBehalfOf("5c4716dc-42dd-4571-b4bf-0aa299fff928", async () =>
{
  var beneficiary = await client.CreateBeneficiaryAsync("Martin McFly", "FR", "EUR", "Employee Funds");
  var conversion = await client.CreateConversionAsync("EUR", "GBP", "buy", 10000, "Invoice Payment", true);
  var payment = await client.CreatePaymentAsync("EUR", beneficiary.Id, 10000, "Invoice Payment", "Invoice 1234", new ParamsObject(new
  {
      ConversionId = conversion.Id,
      PaymentType = "regular"
  }));
  Console.WriteLine(payment);
});
```
## Errors

If an API call fails, the SDK function throws an exception, which derives from `APIException` class and represents some specific type of server error, e.g. `AuthenticationException` or `BadRequestException`.
Base `APIException` class exposes `ToYamlString()` serialization method to convert the error to human-readable YAML string:

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

try
{
  var balance = await client.GetBalanceAsync("XYZ");
}
catch (ApiException ex)
{
  Console.WriteLine(ex.ToYamlString());
}
catch (Exception ex)
{
  Console.WriteLine(ex.Message);
}

/* outputs
BadRequestException
---
Platform: .NET 4.6 or later
Request:
  Parameters: {}
[![NuGet version](https://img.shields.io/nuget/v/CurrencyCloud.svg)](https://www.nuget.org/packages/CurrencyCloud/) [![Travis](https://img.shields.io/travis/rust-lang/rust.svg)](https://github.com/CurrencyCloud/currencycloud-net)

# Currency Cloud

This is the official .NET SDK for v2 of Currency Cloud's API. Additional documentation for each API endpoint can be found at [Currency Cloud API documentation][introduction]. If you have any queries or you require support, please contact our implementation team at implementation@currencycloud.com.

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

Most of the API functions accept both required and optional parameters. To simplify creation of optional parameter sets of variable length `ParamsObject` class is introduced in the SDK. It is an implementation of the dynamic object concept that enables accessing properties via dot notation and adding or removing them in runtime:

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

dynamic beneficiary = new CurrencyCloud.ParamsObject();
beneficiary.BeneficiaryCountry = "GB";
beneficiary.BeneficiaryEntityType = "company";
beneficiary.BeneficiaryCompanyName = "JD Company LLC";
beneficiary.BeneficiaryFirstName = "John";
beneficiary.BeneficiaryLastName = "Doe";
beneficiary.BeneficiaryCity = "London";
beneficiary.BeneficiaryPostcode = "W11 2BQ";
beneficiary.BeneficiaryStateOrProvince = "TX";
beneficiary.BeneficiaryDateOfBirth = new DateTime(1990, 7, 20);

dynamic pagination = new CurrencyCloud.ParamsObject(new
{
  Page = 1,
  PerPage = 5,
  Order = "name",
  OrderAscDesc = "asc"
});

var beneficiaries = await client.FindBeneficiariesAsync(beneficiary + pagination);
```
`ParamsObject` class expects dynamic parameter names to follow PascalCase naming convention.

## Asynchrony

Every API function is an asynchronous operation implementing the Task-based Asynchronous Pattern.

## On Behalf Of

Some API calls can be executed on behalf of another user (e.g. someone who has a sub-account with the logged in user). To achieve this, the `optional` argument of the SDK function should include `OnBehalfOf` parameter with a value of corresponding contact id:

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

var rate = await client.GetRateAsync("SEK", "GBP", "buy", 900, new ParamsObject(new
{
  OnBehalfOf = "8f639ab2-2b85-4327-9eb1-01ee4f0c77bc"
}));
```
Alternatively, `OnBehalfOf(string id, Func<Task> function)` method can be used to run a bunch of API calls for the given contact id:

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

await client.OnBehalfOf("5c4716dc-42dd-4571-b4bf-0aa299fff928", async () =>
{
  var beneficiary = await client.CreateBeneficiaryAsync("Martin McFly", "FR", "EUR", "Employee Funds");
  var conversion = await client.CreateConversionAsync("EUR", "GBP", "buy", 10000, "Invoice Payment", true);
  var payment = await client.CreatePaymentAsync("EUR", beneficiary.Id, 10000, "Invoice Payment", "Invoice 1234", new ParamsObject(new
  {
      ConversionId = conversion.Id,
      PaymentType = "regular"
  }));
  Console.WriteLine(payment);
});
```
## Errors

If an API call fails, the SDK function throws an exception, which derives from `APIException` class and represents some specific type of server error, e.g. `AuthenticationException` or `BadRequestException`.
Base `APIException` class exposes `ToYamlString()` serialization method to convert the error to human-readable YAML string:

```
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

try
{
  var balance = await client.GetBalanceAsync("XYZ");
}
catch (ApiException ex)
{
  Console.WriteLine(ex.ToYamlString());
}
catch (Exception ex)
{
  Console.WriteLine(ex.Message);
}

/* outputs
BadRequestException
---
Platform: .NET 4.6 or later
Request:
  Parameters: {}
  Verb: GET
  Url: https://devapi.thecurrencycloud.com/v2/balances/XYZ
Response:
  StatusCode: 400
  Date: Thu, 17 Dec 2015 09:06:11 GMT
  RequestId: 2914269054259094430
Errors:
- Field: currency
  Code: invalid_currency
  Message: XYZ is not a valid ISO 4217 currency code
  Params:
    currency: XYZ
*/
```
# Development

## Dependencies

* [Newtonsoft.Json][newtonsoft]

## Versioning

This project uses [semantic versioning][semver]. You can safely express a dependency on a major version and expect all minor and patch versions to be backwards compatible.

# Copyright

Copyright (c) 2015 Currency Cloud. See [LICENSE][license] for details.

[introduction]: https://developer.currencycloud.com/documentation/getting-started/introduction
[overview]:     https://developer.currencycloud.com/documentation/api-docs/overview/
[examples]:     Examples
[newtonsoft]:   https://www.nuget.org/packages/Newtonsoft.Json/
[semver]:       http://semver.org/
[license]:      LICENSE.md