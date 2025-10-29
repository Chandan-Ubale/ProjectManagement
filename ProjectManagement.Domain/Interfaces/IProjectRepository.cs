using ProjectManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Domain.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId);
        Task<IEnumerable<Project>> GetProjectsByManagerIdAsync(string managerId);
    }
}
