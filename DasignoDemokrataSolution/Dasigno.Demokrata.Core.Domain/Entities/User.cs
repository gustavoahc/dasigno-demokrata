namespace Dasigno.Demokrata.Core.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = String.Empty;

        public string MiddleName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string SurName { get; set; } = String.Empty;

        public DateOnly BirthDate { get; set; }

        public int Salary { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
