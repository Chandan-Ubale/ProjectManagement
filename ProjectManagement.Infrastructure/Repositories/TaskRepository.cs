using MongoDB.Driver;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<TaskItem> _tasks;

        public TaskRepository(MongoDbContext context)
        {
            _tasks = context.Tasks;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _tasks.Find(_ => true).ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(string id)
        {
            return await _tasks.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TaskItem>> FindAsync(Expression<Func<TaskItem, bool>> predicate)
        {
            return await _tasks.Find(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> FindAllAsync(Expression<Func<TaskItem, bool>> predicate)
        {
            return await _tasks.Find(predicate).ToListAsync();
        }

        public async Task CreateAsync(TaskItem entity)
        {
            await _tasks.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, TaskItem entity)
        {
            await _tasks.ReplaceOneAsync(t => t.Id == id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _tasks.DeleteOneAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(string projectId)
        {
            return await _tasks.Find(t => t.ProjectId == projectId).ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(string userId)
        {
            return await _tasks.Find(t => t.AssignedTo == userId).ToListAsync();
        }
    }
}
