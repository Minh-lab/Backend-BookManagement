using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendAPIASP.Interfaces.Services;
using BackendAPIASP.Models;
using Microsoft.IdentityModel.Tokens;

namespace BackendAPIASP.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            // Lấy Key từ cấu hình và mã hóa nó thành dạng byte
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        }

        public async Task<string> CreateToken(Account account)
        {
            // 1. Tạo các "Claims" (Thông tin định danh người dùng bên trong Token)
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, account.AccountId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, account.Username)
            };

            // 2. Chọn thuật toán ký (HmacSha512 là cực kỳ an toàn)
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // 3. Mô tả các thông tin của Token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // Token có hiệu lực trong 7 ngày
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            // 4. Khởi tạo và tạo chuỗi Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}