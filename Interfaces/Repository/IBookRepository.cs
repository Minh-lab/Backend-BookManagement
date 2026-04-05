using BackendAPIASP.DTOs.Book;
using BackendAPIASP.Models;

namespace BackendAPIASP.Interfaces.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int bookId);
        Task<IEnumerable<Book>> SearchBooksAsync(string query);
        Task<bool> AddBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int bookId);
    }
}
