using FluentValidation;
using FluentValidation.Validators;

using System.Linq;

namespace Aragas.Extensions.Options.FluentValidation.FluentValidation
{
    public interface INotNumberValidator : IPropertyValidator { }

    public class NotNumberValidator<T> : PropertyValidator<T, string>, INotNumberValidator
    {
        public override string Name => "NotNumberValidator";

        public override bool IsValid(ValidationContext<T> context, string value) => !value.All(char.IsNumber);

        protected override string GetDefaultMessageTemplate(string errorCode) => "{PropertyName} is containing only numbers!";
    }
}