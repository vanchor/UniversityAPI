using Microsoft.EntityFrameworkCore;

namespace UniversityApi.Models
{
    public class UniversityContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {

        }
    }
}
