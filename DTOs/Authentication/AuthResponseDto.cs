namespace BackendAPIASP.DTOs.Authentication
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty; // JWT Token\
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}
