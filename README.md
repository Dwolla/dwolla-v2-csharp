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

- **2.0.3** Breaking change: CustomerEmbed -> CustomersEmbed. Thanks to @ithielnor for adding Business Classification models and a CLI
- **1.0.2** Lower VisualStudioVersion, add more properties to Customer
- **1.0.1** Include deserialized error in DwollaException
- **1.0.0** Initial release
