using ProjectManagement.Domain.DTOs;
using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.Interface
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project?> GetProjectByIdAsync(string id);
        Task<Project> CreateProjectAsync(ProjectCreateDto projectDto, string managerId);
        Task<bool> UpdateProjectAsync(ProjectUpdateDto projectDto);
        Task<bool> DeleteProjectAsync(string id);
    }
}
