using BackendAPIASP.Models;

namespace BackendAPIASP.Interfaces.Repository
{
    public interface IAccountRepository
    {
        Task<Account?> GetByUsernameAsync(string username);

        // Lấy tài khoản theo ID (phục vụ việc lấy thông tin Profile sau này)
        Task<Account?> GetByIdAsync(int accountId);

        // Lưu tài khoản mới vào Database
        Task<bool> AddAsync(Account account);

        // Kiểm tra Username đã tồn tại chưa để đảm bảo tính [Unique]
        Task<bool> IsUsernameExistsAsync(string username);

        // Cập nhật trạng thái IsActive (Khóa/Mở khóa tài khoản)
        Task<bool> UpdateStatusAsync(int accountId, bool isActive);
    }
}