# Dwolla V2 C#

Dwolla V2 cross-platform C# client.

[API Documentation](https://docsv2.dwolla.com)

## Installation

```
Install-Package Dwolla.Client
```

### Basic usage

Follow the [guide](https://developers.dwolla.com/guides/sandbox-setup/) to create a Sandbox Dwolla
application, set DWOLLA_APP_KEY and DWOLLA_APP_SECRET environment variables, and take a look at the
[Example Application](https://github.com/Dwolla/dwolla-v2-csharp/tree/master/ExampleApp).

Only a handful of Models are included right now, but you can either create your own or add them to 
this library and open a Pull Request so we can merge them in.

## Contributing

Bug reports and pull requests are welcome on GitHub at https://github.com/Dwolla/dwolla-v2-csharp.

## Changelog

- **5.0.15** Upgrade dependencies and Dwolla.Client.Tests and ExampleApp to `netcoreapp2.0`. Breaking changes:
  - DwollaClient no longer throws on API errors, they should be properly deserialized into RestResponse.Error instead
  - DwollaException, RestException, and RestResponse.Exception are removed
  - Use `EmptyResponse` instead of `object` in DwollaClient inteface 
- **4.0.14** Ignore null values in JSON POST requests
- **4.0.13** Add Beneficial Owner models and examples
- **4.0.12** Add Controller models
- **4.0.11** Add document failure reason
- **4.0.10** Add Micro Deposit models
- **4.0.9** Add Document models, support transfer fees
- **4.0.8** Add Transfer models, expose raw response on RestResponse
- **4.0.7** Add Micro Deposit and Balance models
- **4.0.6** Breaking change: Remove CreateCustomerRequest.Status. Add UpdateCustomerRequest
- **3.0.5** Breaking change: CreateCustomerRequest.DateOfBirth `string` -> `DateTime?`. Create base responses, refactor ExampleApp to tasks, add Funding Source models
- **2.0.4** Add Webhook Subscription models
- **2.0.3** Breaking change: CustomerEmbed -> CustomersEmbed. Thanks to @ithielnor for adding Business Classification models and a CLI
- **1.0.2** Lower VisualStudioVersion, add more properties to Customer
- **1.0.1** Include deserialized error in DwollaException
- **1.0.0** Initial release
