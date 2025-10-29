using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProjectManagement.Domain.Enum;
using System;

namespace ProjectManagement.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("FullName")]
        public string FullName { get; set; } = null!;
        [BsonElement("Email")]
        public string Email { get; set; } = null!;
        [BsonElement("PasswordHash")]
        public string PasswordHash { get; set; } = null!;
        [BsonElement("Role")]
        [BsonRepresentation(BsonType.String)]
        public Role Role { get; set; } = Role.Member;

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
