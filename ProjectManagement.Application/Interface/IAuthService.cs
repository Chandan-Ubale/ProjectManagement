using ProjectManagement.Domain;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Interface
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserRegisterDto registerDto);
        Task<string?> LoginAsync(UserLoginDto loginDto);
        Task<bool> IsEmailTakenAsync(string email);
    }
}
