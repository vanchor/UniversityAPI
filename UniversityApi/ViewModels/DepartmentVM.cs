using System.ComponentModel.DataAnnotations;
using UniversityApi.Models;

namespace UniversityApi.ViewModels
{
    public class DepartmentVM
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string UniversityId { get; set; } = null!;
        [Required]
        public Address address { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
