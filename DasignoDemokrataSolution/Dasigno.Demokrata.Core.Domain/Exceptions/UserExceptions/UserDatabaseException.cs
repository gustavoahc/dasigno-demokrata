using Dasigno.Demokrata.Core.Domain.Exceptions.Base;
using System.Net;

namespace Dasigno.Demokrata.Core.Domain.Exceptions.UserExceptions
{
    public class UserDatabaseException : Exception, IException
    {
        public HttpStatusCode HttpExceptionCode => HttpStatusCode.InternalServerError;

        public string ExceptionMessage => GetExceptionMessage();

        public UserDatabaseException(string message)
        {
            _exceptionMessage = message;
        }

        private readonly string _exceptionMessage;

        private string GetExceptionMessage()
        {
            return _exceptionMessage;
        }
    }
}
