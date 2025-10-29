using MongoDB.Driver;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastructure.Database;
using System.Linq.Expressions;



namespace ProjectManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDbContext context)
        {
            _users = context.Users;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _users.Find(predicate).ToListAsync();
        }

    
        public async Task<IEnumerable<User>> FindAllAsync(Expression<Func<User, bool>> predicate)
        {
            return await _users.Find(predicate).ToListAsync();
        }

        public async Task CreateAsync(User entity)
        {
            await _users.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, User entity)
        {
            await _users.ReplaceOneAsync(u => u.Id == id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _users.DeleteOneAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _users.Find(u => u.Email == email).AnyAsync();
        }
    }
}
