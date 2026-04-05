using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackendAPIASP.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        [MaxLength(100)]
        public String Username { get; set; } = String.Empty;
        [Required]
        public String PasswordHash { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
        //1 - 1 User
        public User? User { get; set; }


    }
}
