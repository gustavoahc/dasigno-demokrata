namespace Dasigno.Demokrata.Presentation.WebApi.ApiModels.Requests
{
    public record UserRequestModel(string FirstName, string MiddleName, string LastName, string SurName, DateOnly BirthDate);
}
