[![NuGet version](https://img.shields.io/nuget/v/CurrencyCloud.svg)](https://www.nuget.org/packages/CurrencyCloud/) [![CodeQL](https://github.com/CurrencyCloud/currencycloud-net/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/CurrencyCloud/currencycloud-net/actions/workflows/github-code-scanning/codeql)
# Currencycloud
This is the official .NET SDK for v2 of Currencycloud's API. Additional documentation for each API endpoint can be found at [Currencycloud API documentation][introduction]. If you have any queries or you require support, please contact our development team at development@currencycloud.com

## Installation
The library is distributed on `NuGet`. To install the latest version, run the following command in the Package Manager Console: 
```sh
PM> Install-Package Currencycloud
```

## Supported .NET versions
The least supported .NET framework version is 4.5.

# Usage
The following example retrieves a list of all tradeable currencies:
```C#
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
More extensive examples can be found in the [Examples] solution or by checking the [Unit Tests][utests]

## Client
The object of class `CurrencyCloud.Client` is a single entry point to access all Currency Cloud's API functions.

Supported APIs are listed in the [Currency Cloud API overview][overview].

## Initialization
Prior to making API requests via the `CurrencyCloud.Client` object, it must be initialized by calling `InitializeAsync(ApiServer apiServer, string loginId, string apiKey)` method, that starts a new session for the API user with the given credentials. If an API function is invoked by a non-initialized client, `InvalidOperationException` is thrown.

`InitializeAsync` method retrieves authentication token, which is passed with all subsequent API calls. If a call fails due to token is expired, then re-authentication is performed, so that the token is refreshed and the failed request is retried.

When working with API is finished, it is recommended to close the session and reset the client by calling its `CloseAsync()` method.

## Passing parameters
Most of the API functions accept entity instances as parameters. Every entity has mandatory parameters required by constructor. And optional parameters as properties. Null value means, that optional parameter not passed.
```C#
CurrencyCloud.Client client = new CurrencyCloud.Client();

await client.InitializeAsync(ApiServer.Demo, "loginId", "ApiKey");

var beneficiary = new CurrencyCloud.Entity.BeneficiaryFindParameters
{
    BeneficiaryCountry = "GB",
    BeneficiaryEntityType = "company",
    BeneficiaryCompanyName = "Currencycloud",
    BeneficiaryFirstName = "John",
    BeneficiaryLastName = "Doe",
    BeneficiaryCity = "London",
    BeneficiaryPostcode = "E1 6FQ",
    BeneficiaryStateOrProvince = "LND",
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

## Exponential Backoff & Retry Strategy
Requests over the internet will fail on occasion for seemingly no apparent reason, and the SDK includes a comprehensive set of error handling capabilities to help troubleshoot those situations. Sometimes however, the best strategy is simply to retry. This is the case particularly with transient errors like **HTTP 429 - Too Many Requests**, but wrapping calls in for/while loops is discouraged as in some extreme cases this may trigger our anti-DoS defences.

As of version 2.4.3 we have introduced an Exponential Backoff with Jitter retry feature which we recommend you use to safely handle retries. Enabling Backoff Retries for all API calls is accomplished by setting `Retry.Enabled = true`

### Retry Parameters
* _Retry.Enabled_: Enables global API call retries on error. Default is `false`
* _Retry.NumRetries_: The maximum amount of retries. Default is `7`
* _Retry.MinWait_: The number of milliseconds before starting the first retry. Default is a random value between `100 and 1000 ms`
* _Retry.MaxWait_: The maximum number of milliseconds between two retries. Default is a random value between `60 and 90 sec`
* _Retry.Jitter_: The amount of random jitter to add on each retry. Default is a random value between `250 and 750 ms`
* _Retry.OnError_: HttpStatusCode errors to retry on. Default is an array of `Unauthorized, RequestTimeout, TooManyRequests InternalServerError, BadGateway, ServiceUnavailable and GatewayTimeout`

A typical use case is presented below. For more information see the [Cookbook examples][examples].
```C#
Retry.Enabled = true;

var client = new Client();
var retrieveBalance = await client.GetBalanceAsync("EUR");
Console.WriteLine("Retrieve Balance with default Retry Parameters: {0}\n", retrieveBalance.ToJSON());

Retry.NumRetries = 11;
Retry.MinWait = new Random().Next(500, 2000);
Retry.MaxWait = new Random().Next(30000, 45000);
Retry.Jitter = new Random().Next(0, 1500);
Retry.OnError = new []
            {
                HttpStatusCode.RequestTimeout,
                (HttpStatusCode)429,
                HttpStatusCode.ServiceUnavailable,
                HttpStatusCode.GatewayTimeout
            };

retrieveBalance = await client.GetBalanceAsync("EUR");
Console.WriteLine("Retrieve Balance with custom Retry Parameters: {0}\n", retrieveBalance.ToJSON());
```

## On Behalf Of
Some API calls can be executed on behalf of another user (e.g. someone who has a sub-account with the logged in user). To achieve this, the `optional` argument of the SDK function should include `OnBehalfOf` parameter with a value of corresponding contact id:
`OnBehalfOf(string id, Func<Task> function)` method is used to run many API calls for the given contact id:
```C#
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
```C#
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
  url: https://devapi.currencycloud.com/v2/balances/XYZ
response:
  status_code: 400
  date: Fri, 12 Feb 2017 12:23:59 GMT
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
* [Polly][polly]

## Contributing
**We welcome pull requests from everyone!** Please see [CONTRIBUTING][contr]
Our sincere thanks for [helping us][hof] create the best API for moving money anywhere around the world!

## Versioning
This project uses [semantic versioning][semver]. You can safely express a dependency on a major version and expect all minor and patch versions to be backwards compatible.

## Deprecation Policy
Technology evolves quickly and we are always looking for better ways to serve our customers. From time to time we need to make room for innovation by removing sections of code that are no longer necessary. We understand this can be disruptive and consequently we have designed a Deprecation Policy that protects our customers' investment and that allows us to take advantage of modern tools, frameworks and practices in developing software.

Deprecation means that we discourage the use of a feature, design or practice because it has been superseded or is no longer considered efficient or safe but instead of removing it immediately, we mark it as **[Obsolete]** to provide backwards compatibility and time for you to update your projects. While the deprecated feature remains in the SDK for a period of time, we advise that you replace it with the recommended alternative which is explained in the relevant section of the code.

We remove deprecated features after **three months** from the time of announcement.

The security of our customers' assets is of paramount importance to us and sometimes we have to deprecate features because they may pose a security threat or because new, more secure, ways are available. On such occasions we reserve the right to set a different deprecation period which may range from **immediate removal** to the standard **three months**. 

Once a feature has been marked as deprecated, we no longer develop the code or implement bug fixes. We only do security fixes.

### List of features being deprecated
```
```

# Support
We actively support the latest version of the SDK. We support the immediate previous version on best-efforts basis. All other versions are no longer supported nor maintained.

# Security Consideration
1. Authentication
    1. All data under [this folder](Source/CurrencyCloud.Tests/Mock) provide and return dummy credentials to verify that authentication workflows behave as expected.

# Copyright
Copyright (c) 2015-2019 Currencycloud. See [LICENSE][license] for details.

[introduction]: https://developer.currencycloud.com/guides/getting-started/introduction/
[overview]:     https://developer.currencycloud.com/api-reference/
[examples]:     Examples/Cookbook
[utests]:       Source/CurrencyCloud.Tests
[newtonsoft]:   https://www.nuget.org/packages/Newtonsoft.Json/
[polly]:        https://www.nuget.org/packages/polly/
[semver]:       http://semver.org/
[license]:      LICENSE.md
[contr]:	      CONTRIBUTING.md
[hof]:	        HALL_OF_FAME.md
