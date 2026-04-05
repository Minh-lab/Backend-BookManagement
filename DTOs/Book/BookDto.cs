namespace BackendAPIASP.DTOs.Book
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; }
        public int Year { get; set; }
        //public BookDto()
    }
}
