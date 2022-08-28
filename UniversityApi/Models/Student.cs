using System.Text.Json.Serialization;

namespace UniversityApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int age { get; set; }

        [JsonIgnore]
        public Group? Group { get; set; }
        public int GroupId { get; set; }
    }
}
