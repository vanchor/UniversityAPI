using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UniversityApi.Models
{
    public class Educator
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<int>? SubjectsId { get; set; }
    }
}
