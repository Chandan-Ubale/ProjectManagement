using ProjectManagement.Domain;
using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.Interface
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(string id);
        Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(string projectId);
        Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(string userId);
        Task<TaskItem> CreateTaskAsync(TaskDto taskDto);
        Task UpdateTaskAsync(string id, TaskDto taskDto);
        Task DeleteTaskAsync(string id);
    }
}
