using MongoDB.Driver;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastructure.Database;
using System.Linq.Expressions;


namespace ProjectManagement.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMongoCollection<Project> _projects;

        public ProjectRepository(MongoDbContext context)
        {
            _projects = context.Projects;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _projects.Find(_ => true).ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(string id)
        {
            return await _projects.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Project>> FindAsync(Expression<Func<Project, bool>> predicate)
        {
            return await _projects.Find(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Project>> FindAllAsync(Expression<Func<Project, bool>> predicate)
        {
            return await _projects.Find(predicate).ToListAsync();
        }

        public async Task CreateAsync(Project entity)
        {
            await _projects.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, Project entity)
        {
            await _projects.ReplaceOneAsync(p => p.Id == id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _projects.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId)
        {
            return await _projects.Find(p => p.TeamMembers.Contains(userId)).ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsByManagerIdAsync(string managerId)
        {
            return await _projects.Find(p => p.ManagerId == managerId).ToListAsync();
        }
    }
}
