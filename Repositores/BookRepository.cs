using BackendAPIASP.Data;
using BackendAPIASP.DTOs.Book;
using BackendAPIASP.Interfaces.Repository;
using BackendAPIASP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BackendAPIASP.Repositores
{
    public class BookRepository : IBookRepository
    {
        public readonly AppDbContext _db;
        public BookRepository(AppDbContext db)
        {
            this._db = db;
        }
        public async Task<bool> AddBookAsync(Book book)
        {
            try
            {
                await _db.Books.AddAsync(book);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                return false;

            }
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            try
            {
                Book? book = await _db.Books.FindAsync(bookId);
                if (book == null) return false;
                _db.Books.Remove(book);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //Lay tat ca sach trong database
        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            try
            {
                var listBook = _db.Books;
                return await listBook.Select(b => new BookDto // 3. Ánh xạ sang Dto để gửi cho Flutter
                {
                    BookId = b.BookId,
                    Author = b.Author,
                    Title = b.Title,
                    CoverImageUrl = b.imageUrl,
                    Year = b.Year
                }).ToListAsync();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return [];

            }
        }

        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            try
            {
                Book? book = await _db.Books.Include(e => e.UserBooks).FirstOrDefaultAsync(e => e.BookId == bookId);
                return book;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                    return await _db.Books.AsNoTracking().Include(e => e.UserBooks).ToListAsync();
                return await _db.Books.AsNoTracking().Include(e => e.UserBooks).Where(e => e.Author.Contains(query) || e.Title.Contains(query)).ToListAsync();
            }
            catch (Exception e)
            {
                return Enumerable.Empty<Book>();
            }
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            try
            {
                Book? bookDb = await _db.Books.Include(e => e.UserBooks).FirstOrDefaultAsync(e => e.BookId == book.BookId);
                if (bookDb == null) return false;
                _db.Entry(bookDb).CurrentValues.SetValues(book);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
