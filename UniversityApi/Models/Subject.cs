namespace UniversityApi.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
