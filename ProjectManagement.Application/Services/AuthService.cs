using ProjectManagement.Application.Interface;
using ProjectManagement.Domain;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastructure.Security;


namespace ProjectManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<User?> RegisterAsync(UserRegisterDto registerDto)
        {
            if (await _userRepository.IsEmailTakenAsync(registerDto.Email))
                return null;

            var user = new User
            {
                FullName = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = Domain.Enum.Role.Member,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);
            return user;
        }

        public async Task<string?> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null) return null;

            bool validPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!validPassword) return null;

            return _jwtTokenGenerator.GenerateToken(user);
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _userRepository.IsEmailTakenAsync(email);
        }
    }
}
