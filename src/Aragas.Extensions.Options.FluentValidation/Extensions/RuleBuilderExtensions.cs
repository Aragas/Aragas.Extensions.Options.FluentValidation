using Aragas.Extensions.Options.FluentValidation.FluentValidation;

using FluentValidation;

using System;
using System.Net.Http;

namespace Aragas.Extensions.Options.FluentValidation.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> NotDigitValidator<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException(nameof(ruleBuilder));
            }

            return ruleBuilder.SetValidator(new NotDigitValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> NotNumberValidator<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException(nameof(ruleBuilder));
            }

            return ruleBuilder.SetValidator(new NotNumberValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> NotBoolean<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException(nameof(ruleBuilder));
            }

            return ruleBuilder.SetValidator(new NotBooleanValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> IsUri<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException(nameof(ruleBuilder));
            }

            return ruleBuilder.SetValidator(new IsUriValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> IsUriAvailable<T>(this IRuleBuilder<T, string> ruleBuilder, HttpClient httpClient)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException(nameof(ruleBuilder));
            }

            return ruleBuilder.SetValidator(new IsUriAvailableValidator<T>(httpClient));
        }

        public static IRuleBuilderOptions<T, string> IsIPAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException(nameof(ruleBuilder));
            }

            return ruleBuilder.SetValidator(new IsIPAddressValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> IsIPEndPoint<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException(nameof(ruleBuilder));
            }

            return ruleBuilder.SetValidator(new IsIPEndPointValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> IsUrlTcpEndpointAvailable<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            if (ruleBuilder == null)
            {
                throw new ArgumentNullException(nameof(ruleBuilder));
            }

            return ruleBuilder.SetValidator(new IsUrlTcpEndpointAvailableValidator<T>());
        }
    }
}