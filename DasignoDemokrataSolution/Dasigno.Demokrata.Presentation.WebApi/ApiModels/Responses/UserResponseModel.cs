namespace Dasigno.Demokrata.Presentation.WebApi.ApiModels.Responses
{
    public record UserResponseModel(int Id, string FirstName, string MiddleName, string LastName, string SurName, DateOnly BirthDate);
}
