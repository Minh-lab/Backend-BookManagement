using BackendAPIASP.DTOs.Book;
using BackendAPIASP.Interfaces.Repository;
using BackendAPIASP.Interfaces.Services;
using BackendAPIASP.Models;

namespace BackendAPIASP.Services
{
    public class BookService : IBookService
    {
        public readonly IBookRepository _repo;
        public BookService(IBookRepository repo) { this._repo = repo; }

        public Task<IEnumerable<BookDto>> GetAllBookAsync()
        {
            return _repo.GetAllBooksAsync();
        }

        public async Task<BookDetailDto?> GetBookDetailAsync(int bookId, int userId)
        {
            try
            {
                Book? book = await _repo.GetBookByIdAsync(bookId);
                if (book == null) return null;
                
                return new BookDetailDto
                {
                    Author = book.Author,
                    BookId = book.BookId,
                    CoverImage = book.imageUrl,
                    Title = book.Title,
                    Year = book.Year,
                    Synopsis = book.Synopsis,
                    IsSaved = book.UserBooks?.Any(e => e.Status == BookStatus.Saved && e.UserId == userId) ?? false
                };
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BookDto>> GetLibraryAsync()
        {
            try
            {
                var books =  await _repo.GetAllBooksAsync();
                return books.Select(b => new BookDto
                {
                    Author = b.Author,
                    BookId = b.BookId,
                    CoverImageUrl = b.CoverImageUrl,
                    Title = b.Title,
                    
                }).ToList();
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public async Task<bool> RemoveBookAsync(int id)
        {
            try
            {
                if (await _repo.GetBookByIdAsync(id) == null) return false;
                return await _repo.DeleteBookAsync(id);
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
