using BackendAPIASP.DTOs.User_Profile;

namespace BackendAPIASP.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetUserProfileAsync(int userId);

        // Cập nhật thông tin Profile (Xử lý ảnh Avatar từ Flutter gửi lên)
        Task<bool> UpdateProfileAsync(int userId, UserProfileDto profileUpdate);
    }
}
