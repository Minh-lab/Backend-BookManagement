using BackendAPIASP.Data;
using BackendAPIASP.DTOs.User_Profile;
using BackendAPIASP.Interfaces.Repository;
using BackendAPIASP.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPIASP.Repositores
{
    public class UserRepository : IUserRepository
    {
        AppDbContext _db;
        public UserRepository(AppDbContext db) { this._db = db; }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                User? user = await _db.Users.Include(e => e.Account).FirstOrDefaultAsync(e => e.UserId == userId);
                if (user == null) return false;
                if (user.Account == null) return false;
                user.Account.IsActive = false;

                return await _db.SaveChangesAsync() > 0;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<User?> GetUserProfileAsync(int userId)
        {
            try
            {
                User? user = await _db.Users.Include(e => e.Account).Include(u => u.UserBooks).FirstOrDefaultAsync(e => e.UserId == userId);
                return user;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<bool> UpdateProfileAsync(User user)
        {
            try
            {
                User? userDb = await _db.Users.FindAsync(user.UserId);
                if (userDb == null) return false;
                _db.Entry(userDb).CurrentValues.SetValues(user);
                return await _db.SaveChangesAsync() > 0;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
