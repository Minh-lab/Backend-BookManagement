using BackendAPIASP.Data;
using BackendAPIASP.DTOs.Book;
using BackendAPIASP.Interfaces.Repository;
using BackendAPIASP.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPIASP.Repositores
{
    public class UserBookRepository : IUserBookRepository
    {
        public readonly AppDbContext _db;
        public UserBookRepository(AppDbContext db) { this._db = db; }
        public async Task<bool> AddToUserListAsync(int userId, int bookId, BookStatus status)
        {
            try
            {
                UserBook userBook = new UserBook(userId, bookId, status);
                await _db.UserBooks.AddAsync(userBook);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Task<bool> ChangeBookStatusAsync(int userId, int bookId, BookStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetReadCountAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserBook?> GetUserBookById(int userId, int bookId)
        {
            try
            {
                return await _db.UserBooks.FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Task<IEnumerable<UserBook>> GetUserBooksByStatusAsync(int userId, BookStatus status)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookDto>> GetUserLibrary(int userId)
        {
            var listBook = await _db.UserBooks
                .Include(ub => ub.Book) // 1. Tải thông tin sách liên quan
                .Where(ub => ub.UserId == userId) // 2. Lọc đúng sách của User này
                .Select(ub => new BookDto // 3. Ánh xạ sang Dto để gửi cho Flutter
                {
                    BookId = ub.BookId,
                    Title = ub.Book!.Title,
                    Author = ub.Book.Author,
                    CoverImageUrl = ub.Book.imageUrl
                })
                .ToListAsync(); // 4. Thực thi và trả về list

            return listBook;
        }

        public async Task<bool> IsExistUserBook(int userId, int bookId)
        {
            try
            {
                //var user = await _db.Users.Include(u => u.UserBooks).FirstOrDefaultAsync(u => u.UserId == userId);
                //if (user == null) return false;
                //var listBookUser = user.UserBooks;
                //if (listBookUser == null) return false;
                //if (listBookUser.Any(b => b.BookId == bookId)) return true;
                //return false;
                return await _db.UserBooks.AnyAsync(ub => ub.BookId == bookId && ub.UserId == userId);
            }
            catch (Exception e)
            {
                throw;

            }
        }
        public Task<bool> RemoveFromListAsync(int userId, int bookId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUserBookAsync(UserBook userBook)
        {
            try
            {
                _db.UserBooks.Update(userBook);
                int rows = await _db.SaveChangesAsync();
                return rows > 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
