using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Models
{
    public class GroupCreate
    {
        [Required]
        public string GradeName { get; set; }
        public string Section { get; set; }
    }
}
