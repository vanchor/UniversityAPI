using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityApi.Models
{
    public class Group
    {
        public int Id { get; set; }
        
        [Required]
        public string GradeName { get; set; }
        public string Section { get; set; }

        [JsonIgnore]
        public ICollection<Student>? Students { get; set; }

        [JsonIgnore]
        public ICollection<Subject>? Subjects { get; set; } = new List<Subject>();
    }
}
