using BackendAPIASP.DTOs.Account_Activity;
using BackendAPIASP.DTOs.Book;
using BackendAPIASP.Interfaces.Repository;
using BackendAPIASP.Interfaces.Services;
using BackendAPIASP.Models;

namespace BackendAPIASP.Services
{
    public class UserBookService : IUserBookService
    {

        public readonly IUserBookRepository _repo;
        public UserBookService(IUserBookRepository repo) { this._repo = repo; }
        public async Task<bool> AddBookToUserAsync(int userId, int bookId)
        {
            try
            {
                if (await _repo.IsExistUserBook(userId, bookId)) return false;
                return await _repo.AddToUserListAsync(userId, bookId, BookStatus.Saved);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserBookActivityDto>> GetActivityAsync(int userId, string status)
        {
            try
            {
                if (!Enum.TryParse<BookStatus>(status, true, out var bookStatusEnum))
                {
                    // Nếu status truyền vào không khớp (ví dụ "abc"), trả về danh sách rỗng
                    return Enumerable.Empty<UserBookActivityDto>();
                }
                var listUserBook = await _repo.GetUserBooksByStatusAsync(userId, bookStatusEnum);
                return listUserBook.Select(e => new UserBookActivityDto
                {
                    Title = e.Book?.Title ?? "Temp",
                    Author = e.Book?.Author ?? "Temp",
                    Status =  e.Status.ToString() ?? "Temp",
                    AddedAt = e.AddedAt,
                    BookId = e.BookId

                }).ToList();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BookDto>> GetUserLibrary(int userId)
        {
            return await _repo.GetUserLibrary(userId);
        }

        public async Task<bool> MarkAsFinishedAsync(int userId, int bookId)
        {
            try
            {
                UserBook? userBook = await _repo.GetUserBookById(userId, bookId);
                if (userBook == null) return false;
                userBook.Status = BookStatus.Finished;
                return await _repo.UpdateUserBookAsync(userBook);
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
