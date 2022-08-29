namespace UniversityApi.ViewModels
{
    public class SubjectViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<int>? GroupsId { get; set; }
    }
}
