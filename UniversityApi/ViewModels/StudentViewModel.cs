namespace UniversityApi.ViewModels
{
    public class StudentViewModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Pasel { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int age { get; set; }

        public int GroupId { get; set; }
    }
}
