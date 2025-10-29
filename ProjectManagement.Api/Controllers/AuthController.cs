using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Interface;
using ProjectManagement.Domain;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<UserRegisterDto> _userValidator;

        public AuthController(IAuthService authService, IValidator<UserRegisterDto> userValidator)
        {
            _authService = authService;
            _userValidator = userValidator;
        }

       
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            var validationResult = await _userValidator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var user = await _authService.RegisterAsync(registerDto);
            if (user == null)
                return BadRequest("Email already registered.");

            return Ok(new
            {
                message = "User registered successfully",
                user.Id,
                user.Email,
                user.FullName,
                user.Role
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            if (token == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new
            {
                token,
                message = "Login successful"
            });
        }

      
        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmail([FromQuery] string email)
        {
            var isTaken = await _authService.IsEmailTakenAsync(email);
            return Ok(new { email, isTaken });
        }
    }
}
