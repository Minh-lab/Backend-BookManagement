namespace BackendAPIASP.DTOs.Account_Activity
{
    public class UserBookActivityDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Reading, Finished, Saved
        public DateTime AddedAt { get; set; }
    }
}
