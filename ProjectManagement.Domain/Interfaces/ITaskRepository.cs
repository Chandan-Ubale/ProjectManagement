using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Domain.Interfaces
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(string projectId);
        Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(string userId);
    }
}
