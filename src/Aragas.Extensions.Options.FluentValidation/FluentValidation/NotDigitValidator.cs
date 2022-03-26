using FluentValidation;
using FluentValidation.Validators;

using System.Linq;

namespace Aragas.Extensions.Options.FluentValidation.FluentValidation
{
    public interface INotDigitValidator : IPropertyValidator { }

    public class NotDigitValidator<T> : PropertyValidator<T, string>, INotDigitValidator
    {
        public override string Name => "NotDigitValidator";

        public override bool IsValid(ValidationContext<T> context, string value) => !value.All(char.IsDigit);

        protected override string GetDefaultMessageTemplate(string errorCode) => "{PropertyName} is containing only digits!";
    }
}