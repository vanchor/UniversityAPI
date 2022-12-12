using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UniversityApi.Models
{
    public class University
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Info { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
