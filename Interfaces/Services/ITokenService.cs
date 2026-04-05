using BackendAPIASP.Models;

namespace BackendAPIASP.Interfaces.Services
{
    public interface ITokenService
    {
        Task<String> CreateToken(Account account);
    }
}
