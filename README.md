# Dwolla SDK for C#

This repository contains the source code for Dwolla's C#-based SDK, which allows developers to
interact with Dwolla's server-side API via a C# API. Any action that can be performed via an HTTP
request can be made using this SDK when executed within a server-side environment.

## Table of Contents

- [Getting Started](#getting-started)
  - [Installation](#installation)
  - [Initialization](#initialization)
    - [Tokens](#tokens)
- [Making Requests](#making-requests)
  - [Low-Level Requests](#low-level-requests)
    - [Setting Headers](#setting-headers)
    - [GET](#get)
    - [POST](#post)
    - [DELETE](#delete)
  - [Example App](#example-app)
    - [Docker](#docker)
- [Changelog](#changelog)
- [Community](#community)
- [Additional Resources](#additional-resources)

## Getting Started

### Installation

To begin using this SDK, you will first need to download it to your machine. We use
[NuGet](https://www.nuget.org/packages/Dwolla.Client) to distribute this package. Check out the
[Microsoft](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio)
documentation for more information on how to install and manage packages from Nuget using Visual
Studio.

Here's an example using the
[Package Manager Console](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-powershell?view=vsmac-2022)

```shell
$ Install-Package Dwolla.Client -Version 5.2.2
```

### Initialization

Before any API requests can be made, you must first determine which environment you will be using,
as well as fetch the application key and secret. To fetch your application key and secret, please
visit one of the following links:

- Production: https://dashboard.dwolla.com/applications
- Sandbox: https://dashboard-sandbox.dwolla.com/applications

Finally, you can create an instance of `DwollaClient` by specifying which environment you will be
using—Production or Sandbox—via the `isSandbox` boolean flag.

```csharp
var client = DwollaClient.Create(isSandbox: true);
```

#### Tokens

Application access tokens are used to authenticate against the API on behalf of an application.
Application tokens can be used to access resources in the API that either belong to the application
itself (`webhooks`, `events`, `webhook-subscriptions`) or the Dwolla Account that owns the
application (`accounts`, `customers`, `funding-sources`, etc.). Application tokens are obtained by
using the [`client_credentials`](https://tools.ietf.org/html/rfc6749#section-4.4) OAuth grant type:

```csharp
var tokenRes = await client.PostAuthAsync<AppTokenRequest, TokenResponse>(
    new Uri($"{client.AuthBaseAddress}/token"),
    new AppTokenRequest {Key = "...", Secret = "..."});
```

_Application access tokens are short-lived: 1 hour. They do not include a `refresh_token`. When it
expires, generate a new one using `AppTokenRequest`._

## Making Requests

Once you've created a `DwollaClient`, currently, you can make low-level HTTP requests.

### Low-Level Requests

To make low-level HTTP requests, you can use the `GetAsync()`, `PostAsync()`, `UploadAsync()` and
`DeleteAsync()` methods with the available
[request models](https://github.com/Dwolla/dwolla-v2-csharp/blob/main/Dwolla.Client/Models/Requests).
These methods will return responses that can be mapped to one of the available
[response models](https://github.com/Dwolla/dwolla-v2-csharp/blob/main/Dwolla.Client/Models/Responses).

#### Setting Headers

To specify headers for a request (e.g., `Authorization`), you can pass a `Headers` object as the
last argument.

```csharp
var headers = new Headers {{"Authorization", $"Bearer {tokenRes.Content.Token}"}};
client.GetAsync<GetCustomersResponse>(url, headers);
```

#### `GET`

```csharp
// GET api.dwolla.com/customers
var url = new Uri("https://api.dwolla.com/customers");
client.GetAsync<GetCustomersResponse>(url);
```

#### `POST`

```csharp
// POST api.dwolla.com/customers
var url = new Uri("https://api.dwolla.com/customers/");
var request = new CreateCustomerRequest
{
  FirstName = "Jane",
  LastName = "Doe",
  Email = "jane.doe@email.com"
};
var res = await PostAsync<CreateCustomerRequest, EmptyResponse>(url, request, headers);
//res.Response.Headers.Location => "https://api-sandbox.dwolla.com/customers/fc451a7a-ae30-4404-aB95-e3553fcd733f

// POST api.dwolla.com/customers/{id}/documents multipart/form-data foo=...
var url = new Uri("https://api-sandbox.dwolla.com/customers/{id}/documents");
var request = new UploadDocumentRequest
{
    DocumentType = "idCard",
    Document = new File
    {
        ContentType = "image/png",
        Filename = "filename.jpg",
        Stream = fileStream
    }
};
client.UploadAsync<UploadDocumentRequest, EmptyResponse>(url, request, headers);
```

#### `DELETE`

```csharp
// DELETE api.dwolla.com/resource
var url = "https://api.dwolla.com/labels/{id}"
client.DeleteAsync<object>(url, null);
```

### Example App

Take a look at the
[Example Application](https://github.com/Dwolla/dwolla-v2-csharp/tree/main/ExampleApp) for examples
on how to use the available C# models to call the Dwolla API. Before you can begin using the app,
however, you will need to specify a `DWOLLA_APP_KEY` and `DWOLLA_APP_SECRET` environment variable.

#### Docker

If you prefer to use Docker to run ExampleApp locally, a Dockerfile file is included in the root
directory. You can either build the Docker image with your API key and secret (by passing the values
via CLI), or you can specify the values for the `app_key` and `app_secret` build arguments in
Dockerfile. Finally, you will need to build and run the Docker image. More information on this topic
can be found on [Docker's website](https://docs.docker.com/build/hellobuild/), or you can find some
example commands below.

##### Building Docker Container

```shell
# Building container by specifying build arguments.
# In this configuration, you will not need to modify Dockerfile. All of the
# necessary arguments are passed via Docker's `--build-arg` option.
$ docker build \
    --build-arg app_key=YOUR_API_KEY \
    --build-arg app_secret=YOUR_APP_SECRET \
    -t dwolla/csharp-example-app:latest .

# Building container without specifying build arguments.
# In this configuration, you will need to specify your account API key and
# secret (retrieved from Dwolla) in the Dockerfile file.
$ docker build -t dwolla/csharp-example-app:latest .
```

##### Running Container Instance

```shell
# Running Docker container in interactive shell
$ docker run --init -it dwolla/csharp-example-app:latest
```

## Changelog

- [**6.0.0**](https://github.com/Dwolla/dwolla-v2-csharp/releases/tag/6.0.0)
  - Fix issue [#41](https://github.com/Dwolla/dwolla-v2-csharp/issues/47) reported by,
    [@waynebrantley](https://github.com/waynebrantley)
    - Upgrade the target framework to `netstandard2.0` in line with Microsoft's recommendation to
      avoid targeting `netstandard1.x`. Users of the SDK will need to update their applications to
      target `netstandard2.0` or a later version to use the updated SDK.
    - Replace `Newtonsoft.Json` with `System.Text.Json` to remove external dependencies.
  - Configure static `HttpClient` in `DwollaClient` to adhere to
    [Microsoft's HttpClient guidelines for .NET](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use).
  - Special thanks to [@natehitz](https://github.com/natehitze) and
    [@IsaiahDahlberg](https://github.com/IsaiahDahlberg) for their contributions to this release!
    :raised_hands:
- [**5.4.0**](https://github.com/Dwolla/dwolla-v2-csharp/releases/tag/5.4.0)
  - Fix issue with deserialization of `200 Ok` response with an `_embedded` error on GET customer.
    Issue [#47](https://github.com/Dwolla/dwolla-v2-csharp/issues/47). (Thanks,
    [@dahlbyk](https://github.com/dahlbyk)!)
  - Add missing fields to Customer response schema - `Embedded`, `CorrelationID`, `BusinessType`,
    `BusinessClassification`.
- [**5.3.0**](https://github.com/Dwolla/dwolla-v2-csharp/releases/tag/5.3.0)
  - Add API models and examples for Exchanges
  - Add Trace ID under AchDetails object
- **5.2.2** Update `Newtonsoft.Json` to version 13.0.1
- **5.2.1** Add Masspayment models and examples, support RTP transfers
- **5.2.0** Change Token URLs and Add Labels models and examples
- **5.1.1** Update Content-Type and Accept headers for Token URLs
- **5.1.0** Change Token URLs
- **5.0.16** Add missing `using` to ExampleApp
- **5.0.15** Upgrade dependencies and Dwolla.Client.Tests and ExampleApp to `netcoreapp2.0`.
  Breaking changes:
  - DwollaClient no longer throws on API errors, they should be properly deserialized into
    RestResponse.Error instead
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
- **3.0.5** Breaking change: CreateCustomerRequest.DateOfBirth `string` -> `DateTime?`. Create base
  responses, refactor ExampleApp to tasks, add Funding Source models
- **2.0.4** Add Webhook Subscription models
- **2.0.3** Breaking change: CustomerEmbed -> CustomersEmbed. Thanks to @ithielnor for adding
  Business Classification models and a CLI
- **1.0.2** Lower VisualStudioVersion, add more properties to Customer
- **1.0.1** Include deserialized error in DwollaException
- **1.0.0** Initial release

## Community

- If you have any feedback, please reach out to us on [our forums](https://discuss.dwolla.com/) or
  by [creating a GitHub issue](https://github.com/Dwolla/dwolla-v2-csharp/issues/new).
- If you would like to contribute to this library,
  [bug reports](https://github.com/Dwolla/dwolla-v2-csharp/issues) and
  [pull requests](https://github.com/Dwolla/dwolla-v2-csharp/pulls) are always appreciated!

## Additional Resources

To learn more about Dwolla and how to integrate our product with your application, please consider
visiting the following resources and becoming a member of our community!

- [Dwolla](https://www.dwolla.com/)
- [Dwolla Developers](https://developers.dwolla.com/)
- [SDKs and Tools](https://developers.dwolla.com/sdks-tools)
  - [Dwolla SDK for Kotlin](https://github.com/Dwolla/dwolla-v2-kotlin)
  - [Dwolla SDK for Node](https://github.com/Dwolla/dwolla-v2-node)
  - [Dwolla SDK for PHP](https://github.com/Dwolla/dwolla-swagger-php)
  - [Dwolla SDK for Python](https://github.com/Dwolla/dwolla-v2-python)
  - [Dwolla SDK for Ruby](https://github.com/Dwolla/dwolla-v2-ruby)
- [Developer Support Forum](https://discuss.dwolla.com/)
