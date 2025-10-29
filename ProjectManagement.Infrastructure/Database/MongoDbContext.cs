using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Infrastructure.Settings;

namespace ProjectManagement.Infrastructure.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> mongoSettings)
        {
            var settings = mongoSettings.Value;
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

       
        public IMongoDatabase Database => _database;

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Project> Projects => _database.GetCollection<Project>("Projects");
        public IMongoCollection<TaskItem> Tasks => _database.GetCollection<TaskItem>("Tasks");
    }
}
