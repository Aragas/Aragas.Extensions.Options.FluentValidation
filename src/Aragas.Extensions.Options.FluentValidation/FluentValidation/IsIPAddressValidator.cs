using FluentValidation;
using FluentValidation.Validators;

using System.Net;

namespace Aragas.Extensions.Options.FluentValidation.FluentValidation
{
    public interface IIsIPAddressValidator : IPropertyValidator { }

    public class IsIPAddressValidator<T> : PropertyValidator<T, string>, IIsIPAddressValidator
    {
        public override string Name => "IsIPAddressValidator";

        public override bool IsValid(ValidationContext<T> context, string value) => value switch
        {
            { } s when IPAddress.TryParse(s, out _) => true,
            _ => false
        };

        protected override string GetDefaultMessageTemplate(string errorCode) => "{PropertyName} is not an IP Address!";
    }
}