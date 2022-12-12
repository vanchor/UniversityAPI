using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UniversityApi.Models
{
    // Katedra
    public class Department
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string UniversityId { get; set; } = null!;
        public Address address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public IEnumerable<Educator> Educators { get; set; } = new List<Educator>();
    }
}
