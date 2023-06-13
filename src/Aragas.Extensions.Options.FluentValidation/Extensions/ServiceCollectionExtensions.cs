using Aragas.Extensions.Options.FluentValidation.Services;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using System;
using System.Diagnostics.CodeAnalysis;

namespace Aragas.Extensions.Options.FluentValidation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static OptionsBuilder<TOptions> AddValidatedOptions<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions, TOptionsValidator>(this IServiceCollection services)
            where TOptions : class where TOptionsValidator : class, IValidator<TOptions>
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddHostedService<OptionValidationService>();

            return services.AddOptions<TOptions>()
                .ValidateViaFluent<TOptions, TOptionsValidator>()
                .ValidateViaHostManager();
        }

        public static OptionsBuilder<TOptions> AddValidatedOptionsWithHttp<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]  TOptions, TOptionsValidator>(this IServiceCollection services, Action<IHttpClientBuilder>? httpClientBuilder = null)
            where TOptions : class where TOptionsValidator : class, IValidator<TOptions>
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddHostedService<OptionValidationService>();

            return services.AddOptions<TOptions>()
                .ValidateViaFluentWithHttp<TOptions, TOptionsValidator>(httpClientBuilder)
                .ValidateViaHostManager();
        }
    }
}