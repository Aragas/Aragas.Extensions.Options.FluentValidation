using FluentValidation;
using FluentValidation.Validators;

using System.Linq;

namespace Aragas.Extensions.Options.FluentValidation.FluentValidation
{
    public interface IIsLetterValidator : IPropertyValidator { }

    public class IsLetterValidator<T> : PropertyValidator<T, string>, IIsLetterValidator
    {
        public override string Name => "IsLetterValidator";

        public override bool IsValid(ValidationContext<T> context, string value) => value.All(char.IsLetter);

        protected override string GetDefaultMessageTemplate(string errorCode) => "{PropertyName} is a number!";
    }
}