using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Domain.Enum;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IUserRepository userRepository, ILogger<EmployeeController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _userRepository.GetAllAsync();
            return Ok(employees);
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var employee = await _userRepository.GetByIdAsync(id);
            if (employee == null)
                return NotFound(new { message = "Employee not found." });

            return Ok(employee);
        }

        // PUT: api/Employee/{id}/role
        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateEmployeeRole(string id, [FromBody] UpdateRoleRequest request)
        {
            if (!Enum.TryParse<Role>(request.NewRole, true, out var newRole))
                return BadRequest(new { message = "Invalid role specified." });

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "Employee not found." });

            user.Role = newRole;
            await _userRepository.UpdateAsync(user.Id, user);

            _logger.LogInformation($"Admin updated {user.FullName}'s role to {user.Role}");

            return Ok(new { message = $"Role updated successfully to {user.Role}" });
        }
    }

    public class UpdateRoleRequest
    {
        public string NewRole { get; set; } = null!;
    }
}
