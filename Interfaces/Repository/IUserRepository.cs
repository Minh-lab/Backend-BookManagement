using BackendAPIASP.DTOs.Book;
using BackendAPIASP.Models;

namespace BackendAPIASP.Interfaces.Repository
{
    public interface IUserRepository
    {
         Task<User?> GetUserProfileAsync(int userId);

        Task<bool> UpdateProfileAsync(User user);

        Task<bool> DeleteUserAsync(int userId);

    }
}
