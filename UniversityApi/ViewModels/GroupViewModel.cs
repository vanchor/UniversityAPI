using System.ComponentModel.DataAnnotations;

namespace UniversityApi.ViewModels
{
    public class GroupViewModel
    {
        [Required]
        public string GradeName { get; set; }
        public string Section { get; set; }
    }
}
