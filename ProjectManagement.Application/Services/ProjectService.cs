using Microsoft.Extensions.Logging;
using ProjectManagement.Application.Interface;
using ProjectManagement.Domain.DTOs;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(string id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task<Project> CreateProjectAsync(ProjectCreateDto projectDto, string managerId)
        {
            var project = new Project
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                TeamMembers = projectDto.AssignedUserIds ?? new List<string>(),
                Status = "Not Started",
                ManagerId = managerId
            };

            await _projectRepository.CreateAsync(project);
            return project;
        }

        public async Task<bool> UpdateProjectAsync(ProjectUpdateDto projectDto)
        {
            var existingProject = await _projectRepository.GetByIdAsync(projectDto.Id);
            if (existingProject == null)
                return false;

            if (!string.IsNullOrEmpty(projectDto.Name))
                existingProject.Name = projectDto.Name;

            if (!string.IsNullOrEmpty(projectDto.Description))
                existingProject.Description = projectDto.Description;

            if (projectDto.EndDate.HasValue)
                existingProject.EndDate = projectDto.EndDate;

            if (projectDto.AssignedUserIds != null)
                existingProject.TeamMembers = projectDto.AssignedUserIds;

            await _projectRepository.UpdateAsync(projectDto.Id, existingProject);
            return true;
        }

        public async Task<bool> DeleteProjectAsync(string id)
        {
            var existing = await _projectRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _projectRepository.DeleteAsync(id);
            return true;
        }
    }
}
