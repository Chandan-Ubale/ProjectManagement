using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ProjectManagement.Domain.Entities
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime? EndDate { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; }

        [BsonElement("teamMembers")]
        public List<string> TeamMembers { get; set; } = new();
        [BsonElement("managerId")]
        public string ManagerId { get; set; } = null!;
    }
}
