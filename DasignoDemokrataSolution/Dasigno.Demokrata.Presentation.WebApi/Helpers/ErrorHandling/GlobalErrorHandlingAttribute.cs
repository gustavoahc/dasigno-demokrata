using Dasigno.Demokrata.Core.Domain.Exceptions.Base;
using Dasigno.Demokrata.Core.Domain.Exceptions.UserExceptions;
using Dasigno.Demokrata.Presentation.WebApi.ApiModels.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Dasigno.Demokrata.Presentation.WebApi.Helpers.ErrorHandling
{
    public class GlobalErrorHandlingAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalErrorHandlingAttribute> _logger;

        public GlobalErrorHandlingAttribute(ILogger<GlobalErrorHandlingAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            int errorCode = (int)HttpStatusCode.InternalServerError;
            string errorMessage = String.Empty;
            Object errors = new Object();
            string errorMessageLog = String.Empty;

            switch (context.Exception)
            {
                case UserValidationException:
                    errorCode = (int)((IException)context.Exception).HttpExceptionCode;
                    errorMessage = ((IException)context.Exception).ExceptionMessage;
                    errorMessageLog = errorMessage;
                    errors = ((UserValidationException)context.Exception).ValidationErrors;
                    break;
                case IException:
                    errorCode = (int)((IException)context.Exception).HttpExceptionCode;
                    errorMessage = ((IException)context.Exception).ExceptionMessage;
                    errorMessageLog = errorMessage;
                    break;
                default:
                    errorMessage = "An error has occurred";
                    errorMessageLog = String.Format("{0}. {1}", context.Exception.Message
                        , ((context.Exception.InnerException != null) ? context.Exception.InnerException.Message : String.Empty));
                    break;
            }

            //Log error
            _logger.LogError(errorMessageLog);

            //Return error to client
            var problemDetails = new ProblemDetails
            {
                Status = errorCode,
                Title = errorMessage,
                Instance = context.HttpContext.Request.Path
            };

            ApiErrorResponse apiResponseError = new ApiErrorResponse(problemDetails, errors);
            ObjectResult result = new ObjectResult(apiResponseError);
            result.StatusCode = errorCode;
            context.HttpContext.Response.ContentType = "application/problem+json";
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
