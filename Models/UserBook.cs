using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BackendAPIASP.Models
{
    public class UserBook
    {
        [Key]
        public int UserBookId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public BookStatus Status { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public UserBook(int userId, int bookId, BookStatus status)
        {
            this.UserId = userId;
            this.BookId = bookId;
            this.Status = status;
        }
    }
}
