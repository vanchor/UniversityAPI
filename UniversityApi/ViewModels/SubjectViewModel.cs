namespace UniversityApi.ViewModels
{
    public class SubjectViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DepartmentID { get; set; }
        public int numberOfHours { get; set; }
        public int ECTS { get; set; }

        public List<int>? GroupsId { get; set; }
    }
}
