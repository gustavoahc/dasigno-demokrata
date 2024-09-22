namespace Dasigno.Demokrata.Presentation.WebApi.ApiModels.Requests
{
    public record UserUpdateRequestModel(int Id, string FirstName, string MiddleName, string LastName, string SurName, DateOnly BirthDate, int Salary);
}
