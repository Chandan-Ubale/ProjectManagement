using FluentValidation;
using ProjectManagement.Application.Interface;
using ProjectManagement.Domain;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IValidator<TaskDto> _taskValidator;

        public TaskService(ITaskRepository taskRepository, IValidator<TaskDto> taskValidator)
        {
            _taskRepository = taskRepository;
            _taskValidator = taskValidator;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync() =>
            await _taskRepository.GetAllAsync();

        public async Task<TaskItem?> GetTaskByIdAsync(string id) =>
            await _taskRepository.GetByIdAsync(id);

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(string projectId) =>
            await _taskRepository.GetTasksByProjectIdAsync(projectId);

        public async Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(string userId) =>
            await _taskRepository.GetTasksByUserIdAsync(userId);

        public async Task<TaskItem> CreateTaskAsync(TaskDto taskDto)
        {
            var validationResult = await _taskValidator.ValidateAsync(taskDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var task = new TaskItem
            {
                ProjectId = taskDto.ProjectId,
                Title = taskDto.Title,
                Description = taskDto.Description,
                AssignedTo = taskDto.AssignedUserId,
                CreatedAt = DateTime.UtcNow,
                DueDate = taskDto.DueDate,
                Status = taskDto.Status,
                Priority = taskDto.Priority
            };

            await _taskRepository.CreateAsync(task);
            return task;
        }

        public async Task UpdateTaskAsync(string id, TaskDto taskDto)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null)
                throw new KeyNotFoundException("Task not found.");

            var validationResult = await _taskValidator.ValidateAsync(taskDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.AssignedTo = taskDto.AssignedUserId;
            existingTask.DueDate = taskDto.DueDate;
            existingTask.Status = taskDto.Status;
            existingTask.Priority = taskDto.Priority;

            await _taskRepository.UpdateAsync(id, existingTask);
        }

        public async Task DeleteTaskAsync(string id)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null)
                throw new KeyNotFoundException("Task not found.");

            await _taskRepository.DeleteAsync(id);
        }
    }
}
