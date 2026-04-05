using BackendAPIASP.DTOs.Book;
using BackendAPIASP.Models;

namespace BackendAPIASP.Interfaces.Repository
{
    public interface IUserBookRepository
    {
        Task<IEnumerable<UserBook>> GetUserBooksByStatusAsync(int userId, BookStatus status);

        // Thêm một cuốn sách vào danh sách của người dùng (Nút "Add to Reading")
        Task<bool> AddToUserListAsync(int userId, int bookId, BookStatus status);

        // Cập nhật trạng thái (Ví dụ: Từ Reading sang Finished)
        Task<bool> ChangeBookStatusAsync(int userId, int bookId, BookStatus newStatus);

        // Đếm số lượng sách đã đọc xong (Để hiện con số 12 trên Flutter)
        Task<int> GetReadCountAsync(int userId);

        // Xóa sách khỏi danh sách hoạt động
        Task<bool> RemoveFromListAsync(int userId, int bookId);
        Task<bool> IsExistUserBook(int userID, int bookId);
        Task<UserBook?> GetUserBookById(int userId, int bookId);
        public Task<bool> UpdateUserBookAsync(UserBook userBook);
        //lấy tất cả sách theo userId
        Task<IEnumerable<BookDto>> GetUserLibrary(int userId);

    }
}
