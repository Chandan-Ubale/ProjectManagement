using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ProjectManagement.Domain.Entities
{
    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("ProjectId")]
        public string ProjectId { get; set; } = null!;
        [BsonElement("Title")]
        public string Title { get; set; } = null!;
        [BsonElement("Description")]
        public string Description { get; set; } = null!;
        [BsonElement("AssignedTo")]
        public string AssignedTo { get; set; } = null!;
        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [BsonElement("DueDate")]
        public DateTime? DueDate { get; set; }
        [BsonElement("Status")]
        public string Status { get; set; } = "ToDo";
        [BsonElement("Priority")]
        public int Priority { get; set; } 
    }
}
