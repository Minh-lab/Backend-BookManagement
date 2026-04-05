using BackendAPIASP.DTOs.Account_Activity;
using BackendAPIASP.DTOs.Book;
using BackendAPIASP.Models;

namespace BackendAPIASP.Interfaces.Services
{
    public interface IUserBookService
    {
        //danh sách các cuốn sách mà một người dùng cụ thể (userId) đang sở hữu, được lọc theo trạng thái (status) 
        Task<IEnumerable<UserBookActivityDto>> GetActivityAsync(int userId, string status);

        // Thêm sách vào danh sách (Nút "Add to Reading")
        Task<bool> AddBookToUserAsync(int userId, int bookId);

        // Đánh dấu đã đọc xong (Chuyển Status sang Finished)
        Task<bool> MarkAsFinishedAsync(int userId, int bookId);
        Task<IEnumerable<BookDto>> GetUserLibrary(int userId);

    }
}
