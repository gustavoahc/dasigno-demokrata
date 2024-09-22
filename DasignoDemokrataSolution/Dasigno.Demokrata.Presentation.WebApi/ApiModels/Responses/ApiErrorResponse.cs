using Microsoft.AspNetCore.Mvc;

namespace Dasigno.Demokrata.Presentation.WebApi.ApiModels.Responses
{
    public class ApiErrorResponse
    {
        public int Status { get; set; }

        public string? Title { get; set; }

        public string? Instance { get; set; }

        public Object? Errores { get; set; }

        public ApiErrorResponse(ProblemDetails problemDetails, Object errores)
        {
            Status = (int)problemDetails.Status!;
            Title = problemDetails.Title;
            Instance = problemDetails.Instance;
            Errores = errores;
        }
    }
}
