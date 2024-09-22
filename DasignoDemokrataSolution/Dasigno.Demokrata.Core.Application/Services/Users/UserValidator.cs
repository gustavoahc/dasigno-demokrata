using Dasigno.Demokrata.Core.Domain.Entities;
using FluentValidation;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Dasigno.Demokrata.Core.Application.Services.Users
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(e => e.FirstName)
                .NotEmpty()
                .NotNull().WithMessage("Please enter {PropertyName}")
                .Length(1, 50).WithMessage("{PropertyName} must be between 1 and 50 characters")
                .Must(ValidateMandatoryName).WithMessage("{PropertyName} must not contain numbers");

            RuleFor(e => e.MiddleName)
                .Length(0, 50).WithMessage("{PropertyName} must not be greater than 50 characters")
                .Must(ValidateOptionalName).WithMessage("{PropertyName} must not contain numbers");
            RuleFor(e => e.LastName)
                .NotEmpty()
                .NotNull().WithMessage("Please enter {PropertyName}")
                .Length(1, 50).WithMessage("{PropertyName} must be between 1 and 50 characters")
                .Must(ValidateMandatoryName).WithMessage("{PropertyName} must not contain numbers");

            RuleFor(e => e.SurName)
                .Length(0, 50).WithMessage("{PropertyName} must not be greater than 50 characters")
                .Must(ValidateOptionalName).WithMessage("{PropertyName} must not contain numbers");

            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .NotNull().WithMessage("Please enter {PropertyName}")
                .Must(ValidateBirthDateFormat);

            RuleFor(e => e.Salary)
                .NotNull().WithMessage("Please enter {PropertyName}")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than $0");
        }

        private static bool ValidateBirthDateFormat(DateOnly birthDate)
        {
            DateOnly birthDateValidated;
            string birthDateString = birthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            bool isValidDate = DateOnly.TryParseExact(birthDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDateValidated);

            return isValidDate;
        }

        private static bool ValidateOptionalName(string name)
        {
            string pattern = @"^[^\d]*$";
            Match match = Regex.Match(name, pattern);
            return match.Success;
        }

        private static bool ValidateMandatoryName(string name)
        {
            string pattern = @"^[^\d]+$";
            Match match = Regex.Match(name, pattern);
            return match.Success;
        }
    }
}