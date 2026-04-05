using Bogus;
using BackendAPIASP.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BackendAPIASP.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // 1. CƠ CHẾ RESET: Xóa toàn bộ dữ liệu cũ để làm sạch Database
            // Lưu ý: Phải xóa theo thứ tự ngược lại của quan hệ (Bảng phụ xóa trước, bảng chính xóa sau)
            if (context.UserBooks.Any() || context.Books.Any() || context.Users.Any())
            {
                // 1. CƠ CHẾ RESET ID VỀ 1
                if (context.Accounts.Any() || context.Books.Any())
                {
                    Console.WriteLine("--> Đang dọn dẹp và reset ID...");

                    // Xóa dữ liệu (Thứ tự bảng phụ trước, bảng chính sau)
                    context.Database.ExecuteSqlRaw("DELETE FROM UserBooks");
                    context.Database.ExecuteSqlRaw("DELETE FROM Books");
                    context.Database.ExecuteSqlRaw("DELETE FROM Users");
                    context.Database.ExecuteSqlRaw("DELETE FROM Accounts");

                    // Reset Identity counter về 0 (Bản ghi tiếp theo sẽ là 1)
                    // Lưu ý: Tên bảng trong ngoặc đơn phải khớp chính xác với tên bảng trong DB
                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('UserBooks', RESEED, 0)");
                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Books', RESEED, 0)");
                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Users', RESEED, 0)");
                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Accounts', RESEED, 0)");

                    context.SaveChanges();
                }
            }

            Console.WriteLine("--> Đang khởi tạo dữ liệu mới...");

            // 2. Tạo dữ liệu giả cho Account & User (Dùng Bogus chuyên nghiệp)
            var accountFaker = new Faker<Account>()
                .RuleFor(a => a.Username, f => f.Internet.UserName())
                .RuleFor(a => a.PasswordHash, f => BCrypt.Net.BCrypt.HashPassword("Password123"))
                .RuleFor(a => a.IsActive, true);

            var userFaker = new Faker<User>()
                .RuleFor(u => u.FullName, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.MemberSince, f => f.Date.Past(2))
// Nếu Avatar của bạn là byte[], ta lưu URL dạng string rồi GetBytes
                .RuleFor(u => u.Avatar, f => f.Internet.Avatar());
            var accounts = accountFaker.Generate(20);
            context.Accounts.AddRange(accounts);
            context.SaveChanges(); // Lưu Account trước để có AccountId

            var users = new List<User>();
            foreach (var acc in accounts)
            {
                var user = userFaker.Generate();
                user.AccountId = acc.AccountId; // Gán thủ công để chắc chắn 1-1
                users.Add(user);
            }
            context.Users.AddRange(users);
            context.SaveChanges();

            // 3. Tạo dữ liệu giả cho Book (Lưu URL ảnh dưới dạng STRING)
            var bookFaker = new Faker<Book>()
                .RuleFor(b => b.Title, f => f.Commerce.ProductName())
                .RuleFor(b => b.Author, f => f.Name.FullName())
                .RuleFor(b => b.Year, f => f.Date.Past(10).Year)
                .RuleFor(b => b.Synopsis, f => f.Lorem.Paragraph())
                // FIX: Gán thẳng string URL, không GetBytes nữa!
                .RuleFor(b => b.imageUrl, f => f.Image.PicsumUrl());

            var books = bookFaker.Generate(10);
            context.Books.AddRange(books);
            context.SaveChanges();

            // 4. Tạo dữ liệu giả cho UserBook (Bảng trung gian)
            var userBooks = new List<UserBook>();
            var random = new Random();

            foreach (var user in users)
            {
                // Mỗi user chọn ngẫu nhiên từ 2-5 cuốn sách để cho vào thư viện
                var selectedBooks = books.OrderBy(x => Guid.NewGuid()).Take(random.Next(2, 6)).ToList();

                foreach (var book in selectedBooks)
                {
                    // Random trạng thái: Reading = 0, Finished = 1, Saved = 2
                    var status = (BookStatus)random.Next(0, 3);

                    var userBook = new UserBook(user.UserId, book.BookId, status)
                    {
                        AddedAt = DateTime.UtcNow.AddDays(-random.Next(1, 30))
                    };
                    userBooks.Add(userBook);
                }
            }

            // Đảm bảo không trùng lặp cặp (UserId, BookId) để tránh lỗi Primary Key
            var distinctUserBooks = userBooks
                .GroupBy(ub => new { ub.UserId, ub.BookId })
                .Select(g => g.First())
                .ToList();

            context.UserBooks.AddRange(distinctUserBooks);
            context.SaveChanges();

            Console.WriteLine("--> SEED DỮ LIỆU THÀNH CÔNG! Đã nạp 20 Users và 50 Books (URL ảnh xịn).");
        }
    }
}