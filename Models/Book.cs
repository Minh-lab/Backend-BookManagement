using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackendAPIASP.Models
{
    public class Book
    {

        [Key]
        public int BookId { get; set; }
        [Required]
        [MaxLength(500)]
        public String Title { get; set; } = string.Empty;
        [Required]
        public String Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public String? Synopsis { get; set; }
        public String? imageUrl { get; set; }
        public ICollection<UserBook>? UserBooks { get; set; }


    }
}
