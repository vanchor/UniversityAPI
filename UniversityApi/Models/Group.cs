namespace UniversityApi.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Specialization { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
