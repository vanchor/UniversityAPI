using System.Text.Json.Serialization;

namespace UniversityApi.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Group>? Groups { get; set; } = new List<Group>();
    }
}
