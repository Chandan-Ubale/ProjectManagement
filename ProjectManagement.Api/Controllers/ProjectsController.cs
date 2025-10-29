using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Interface;
using ProjectManagement.Domain.DTOs;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(string id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound("Project not found.");

            return Ok(project);
        }

     
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            var managerId = "temporary-manager-id";

            var createdProject = await _projectService.CreateProjectAsync(dto, managerId);
            if (createdProject == null)
                return BadRequest("Project creation failed.");

            return Ok(new
            {
                message = "Project created successfully",
                project = createdProject
            });
        }

      
        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _projectService.UpdateProjectAsync(dto);
            if (!updated)
                return BadRequest("Failed to update project.");

            return Ok(new { message = "Project updated successfully" });
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            var deleted = await _projectService.DeleteProjectAsync(id);
            if (!deleted)
                return BadRequest("Failed to delete project.");

            return Ok(new { message = "Project deleted successfully" });
        }
    }
}
