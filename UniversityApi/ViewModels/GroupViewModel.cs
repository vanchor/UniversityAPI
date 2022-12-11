using System.ComponentModel.DataAnnotations;

namespace UniversityApi.ViewModels
{
    public class GroupViewModel
    {
        [Required]
        public string GradeName { get; set; } = null!;
        public string Department { get; set; } = null!;
        public int Semester { get; set; }
    }
}
