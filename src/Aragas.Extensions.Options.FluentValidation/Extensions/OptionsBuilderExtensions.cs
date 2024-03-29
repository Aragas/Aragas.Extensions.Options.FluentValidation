﻿using Aragas.Extensions.Options.FluentValidation.FluentValidation;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using System;
using System.Diagnostics.CodeAnalysis;

using ValidatorOptions = Aragas.Extensions.Options.FluentValidation.Options.ValidatorOptions;

namespace Aragas.Extensions.Options.FluentValidation.Extensions
{
    public static class OptionsBuilderExtensions
    {
        public static OptionsBuilder<TOptions> ValidateViaFluent<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TOptions, TOptionsValidator>(this OptionsBuilder<TOptions> optionsBuilder)
            where TOptions : class
            where TOptionsValidator : class, IValidator<TOptions>
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            optionsBuilder.Services.AddTransient<TOptionsValidator>();
            optionsBuilder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IValidator<TOptions>, TOptionsValidator>(sp => sp.GetRequiredService<TOptionsValidator>()));
            optionsBuilder.Services.AddTransient<IValidateOptions<TOptions>, FluentValidateOptions<TOptions>>();

            return optionsBuilder;
        }

        public static OptionsBuilder<TOptions> ValidateViaFluentWithHttp<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TOptions, TOptionsValidator>(this OptionsBuilder<TOptions> optionsBuilder, Action<IHttpClientBuilder>? httpClientBuilder = null)
            where TOptions : class
            where TOptionsValidator : class, IValidator<TOptions>
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            var builder = optionsBuilder.Services.AddHttpClient<TOptionsValidator>();
            if (httpClientBuilder is not null) httpClientBuilder(builder);
            optionsBuilder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IValidator<TOptions>, TOptionsValidator>(sp => sp.GetRequiredService<TOptionsValidator>()));
            optionsBuilder.Services.AddTransient<IValidateOptions<TOptions>, FluentValidateOptions<TOptions>>();

            return optionsBuilder;
        }

        /// <summary>
        /// Enforces options validation check on start rather than in runtime.
        /// </summary>
        /// <typeparam name="TOptions">The type of options.</typeparam>
        /// <param name="optionsBuilder">The <see cref="OptionsBuilder{TOptions}"/> to configure options instance.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that additional calls can be chained.</returns>
        [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2091", Justification = "Workaround for https://github.com/mono/linker/issues/1416. Outer method has been annotated with DynamicallyAccessedMembers.")]
        public static OptionsBuilder<TOptions> ValidateViaHostManager<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] TOptions>(this OptionsBuilder<TOptions> optionsBuilder)
            where TOptions : class
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            optionsBuilder.Services.AddOptions<ValidatorOptions>().Configure<IOptionsMonitor<TOptions>>((vo, options) =>
            {
                // This adds an action that resolves the options value to force evaluation
                // We don't care about the result as duplicates are not important
                vo.Validators[typeof(TOptions)] = () => options.Get(optionsBuilder.Name);
            });

            return optionsBuilder;
        }
    }
}