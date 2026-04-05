using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackendAPIASP.Models

{

    public class User
    {
        [Key]
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; }
        public String FullName { get; set; } = String.Empty;
        public String Email { get; set; } = String.Empty;
        public string? Avatar { get; set; }
        public DateTime MemberSince { get; set; } = DateTime.UtcNow;
        public ICollection<UserBook>? UserBooks { get; set; }
    }
}
