using BackendAPIASP.DTOs.User_Profile;
using BackendAPIASP.Interfaces.Repository;
using BackendAPIASP.Interfaces.Services;
using BackendAPIASP.Models;

namespace BackendAPIASP.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _repo;
        public UserService(IUserRepository repo) { this._repo = repo; }
        public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
        {
            try
            {
                User? user = await _repo.GetUserProfileAsync(userId);
                if (user == null) return null;
                return new UserProfileDto
                {
                    Email = user.Email,
                    Avatar = user.Avatar,
                    FullName = user.FullName,
                    BooksReadCount = user.UserBooks?.Count ?? 0,
                    MemberSince = user.MemberSince.ToString("MMM yyy"),
                    SavedBooksCount = user.UserBooks?.Count(ub => ub.Status == BookStatus.Finished)
                    ?? 0
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> UpdateProfileAsync(int userId, UserProfileDto profileUpdate)
        {
            try
            {
                User? user = await _repo.GetUserProfileAsync(userId);
                if (user == null) return false;
                user.FullName = profileUpdate.FullName;
               
                user.Email = profileUpdate.Email;
                return await _repo.UpdateProfileAsync(user);
                
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
