using BackendAPIASP.DTOs.Authentication;

namespace BackendAPIASP.Interfaces.Services
{
    public interface IAccountService
    {
        // Trả về Token kèm thông tin sau khi Login thành công
        Task<AuthResponseDto?> AuthenticateAsync(LoginRequestDto loginRequest);
        Task<bool> RegisterUserAsync(RegisterRequestDto registerRequest);
    }
}
