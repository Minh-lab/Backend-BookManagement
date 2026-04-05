namespace BackendAPIASP.DTOs.Book
{
    public class BookDetailDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public string? Synopsis { get; set; }
        public string? CoverImage { get; set; } // Base64 của ảnh
        public bool IsSaved { get; set; } // Trạng thái đã lưu hay chưa
    }
}
