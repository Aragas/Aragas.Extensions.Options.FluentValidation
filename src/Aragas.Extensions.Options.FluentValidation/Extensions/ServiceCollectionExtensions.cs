using Aragas.Extensions.Options.FluentValidation.Services;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Diagnostics.CodeAnalysis;

namespace Aragas.Extensions.Options.FluentValidation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValidatedOptions<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions, TOptionsValidator>(this IServiceCollection services)
            where TOptions : class where TOptionsValidator : class, IValidator<TOptions>
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddHostedService<OptionValidationService>();

            services.AddOptions<TOptions>()
                .ValidateViaFluent<TOptions, TOptionsValidator>()
                .ValidateViaHostManager();

            return services;
        }

        public static IServiceCollection AddValidatedOptionsWithHttp<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]  TOptions, TOptionsValidator>(this IServiceCollection services, Action<IHttpClientBuilder>? httpClientBuilder = null)
            where TOptions : class where TOptionsValidator : class, IValidator<TOptions>
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddHostedService<OptionValidationService>();

            services.AddOptions<TOptions>()
                .ValidateViaFluentWithHttp<TOptions, TOptionsValidator>(httpClientBuilder)
                .ValidateViaHostManager();

            return services;
        }
    }
}