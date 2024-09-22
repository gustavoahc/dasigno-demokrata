using System.Net;

namespace Dasigno.Demokrata.Core.Domain.Exceptions.Base
{
    public interface IException
    {
        HttpStatusCode HttpExceptionCode { get; }

        string ExceptionMessage { get; }
    }
}
