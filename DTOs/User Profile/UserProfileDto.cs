namespace BackendAPIASP.DTOs.User_Profile
{
    public class UserProfileDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Avatar { get; set; } // Base64
        public string MemberSince { get; set; } = string.Empty; // Ví dụ: "Jan 2024"

        // Các con số thống kê cho màn hình Profile
        public int BooksReadCount { get; set; }
        public int SavedBooksCount { get; set; }
    }
}
