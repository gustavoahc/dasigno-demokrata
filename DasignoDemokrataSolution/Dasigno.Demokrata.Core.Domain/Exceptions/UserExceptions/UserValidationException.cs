using Dasigno.Demokrata.Core.Domain.Exceptions.Base;
using FluentValidation.Results;
using System.Net;

namespace Dasigno.Demokrata.Core.Domain.Exceptions.UserExceptions
{
    public class UserValidationException : Exception, IException
    {
        public HttpStatusCode HttpExceptionCode => HttpStatusCode.BadRequest;

        public string ExceptionMessage => "Validation errors";

        public List<string> ValidationErrors { get; }

        public UserValidationException(List<ValidationFailure> validationErrors)
        {
            ValidationErrors = new List<string>();
            foreach (var error in validationErrors)
            {
                ValidationErrors.Add(String.Format("Property {0}: {1}", error.PropertyName, error.ErrorMessage));
            }

        }
    }
}
