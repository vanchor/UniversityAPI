using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityApi.Models
{
    public class Group
    {
        public int Id { get; set; }
        
        [Required]
        public string GradeName { get; set; } = null!;
        public string Department { get; set; } = null!;

        public int Semester { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<Subject>? Subjects { get; set; } = new List<Subject>();
    }
}
