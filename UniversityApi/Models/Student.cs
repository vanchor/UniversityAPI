using System.Text.Json;
using System.Text.Json.Serialization;

namespace UniversityApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Pasel { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int age { get; set; }

        public int GroupId { get; set; }
        [JsonIgnore]
        public Group? Group { get; set; }
    }
}
