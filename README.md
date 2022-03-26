# Aragas.Extensions.Options.FluentValidation

[![Publish](https://github.com/Aragas/Aragas.Extensions.Options.FluentValidation/actions/workflows/publish.yml/badge.svg)](https://github.com/Aragas/Aragas.Extensions.Options.FluentValidation/actions/workflows/publish.yml)
[![Daily Code Format Check](https://github.com/Aragas/Aragas.Extensions.Options.FluentValidation/actions/workflows/dotnet-format-daily.yml/badge.svg)](https://github.com/Aragas/Aragas.Extensions.Options.FluentValidation/actions/workflows/dotnet-format-daily.yml)
[![NuGet](http://img.shields.io/nuget/v/Aragas.Extensions.Options.FluentValidation.svg?style=flat)](https://www.nuget.org/packages/Aragas.Extensions.Options.FluentValidation/) 
  
Validates ASP NET Core IOptions at startup via FluentValidation. If the options are not valid,
will throw an exception detailing what's wrong with the options and stop the app's execution.

## Installation
Aragas.Extensions.Options.FluentValidation can be installed using the Nuget Package Manager or the dotnet CLI.
```
# NuGet Package Manager
Install-Package Aragas.Extensions.Options.FluentValidation
```
```
# dotnet CLI
dotnet add package Aragas.Extensions.Options.FluentValidation
```

## Usage
### Standard Usage
Simply change the code you most likely used to do like this:
```csharp
    public sealed class SomeOptionsValidator : AbstractValidator<SomeOptions>
    {
        public SomeOptionsValidator()
        {
            RuleFor(x => x.Host).IsUri().IsUrlTcpEndpointAvailable().When(x => x.Enabled);
        }
    }

    public sealed record SomeOptions
    {
        public bool Enabled { get; init; } = default!;
        public string Host { get; init; } = default!;
    }


// Old code
services.Configure<SomeOptions>(ctx.Configuration.GetSection("SomeOptions"));
// New code
services.AddValidatedOptions<SomeOptions, SomeOptionsValidator>(ctx.Configuration.GetSection("SomeOptions"));
```

### HttpClient Usage
You can inject and configure a HttpClient in the validator
```csharp
    public sealed class SomeHttpOptionsValidator : AbstractValidator<SomeHttpOptions>
    {
        public SomeOptionsValidator(HttpClient client)
        {
            RuleFor(x => x.Url).IsUri().IsUrlAvailable(client).When(x => x.Enabled);
        }
    }

    public sealed record SomeHttpOptions
    {
        public bool Enabled { get; init; } = default!;
        public string Url { get; init; } = default!;
    }


// Old code
services.Configure<SomeOptions>(ctx.Configuration.GetSection("SomeHttpOptions"));
// New code
services.AddValidatedOptionsWithHttp<SomeOptions, SomeOptionsValidator>(ctx.Configuration.GetSection("SomeHttpOptions"), httpBuilder =>
{
    httpBuilder.ConfigureHttpClient(client =>
    {
        client.Timeout = TimeSpan.FromSeconds(30);
    });
});
```
