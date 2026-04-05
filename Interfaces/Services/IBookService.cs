using BackendAPIASP.DTOs.Book;

namespace BackendAPIASP.Interfaces.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBookAsync();
        Task<BookDetailDto?> GetBookDetailAsync(int bookId, int userId);
        // Logic: Kiểm tra xem sách có tồn tại trước khi xóa
        Task<bool> RemoveBookAsync(int id);
    }
}
