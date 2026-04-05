using BackendAPIASP.DTOs.Authentication;
using BackendAPIASP.Interfaces.Repository;
using BackendAPIASP.Interfaces.Services;
using BackendAPIASP.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity.Data;

namespace BackendAPIASP.Services
{
    public class AccountService : IAccountService
    {
        public IAccountRepository _repo;
        public ITokenService _tokenService;
        public AccountService(IAccountRepository repo, ITokenService tokenService) { this._repo = repo; this._tokenService = tokenService; }
        public async Task<AuthResponseDto?> AuthenticateAsync(LoginRequestDto loginRequest)
        {
            try
            {
                Account? account = await _repo.GetByUsernameAsync(loginRequest.Username);
                if (account == null || !account.IsActive) return null;
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, account.PasswordHash);
                if (!isPasswordValid)
                {
                    return null;
                }
                string token = await _tokenService.CreateToken(account);
                return new AuthResponseDto
                {
                    Token = token,
                    Username = account.Username,
                    FullName = account.User.FullName ?? "Member",
                    UserId = account.User.UserId.ToString()
                };

            }
            catch (Exception e)
            {
                throw;

            }


        }

        public async Task<bool> RegisterUserAsync(RegisterRequestDto registerRequest)
        {
            try
            {
                if (await _repo.IsUsernameExistsAsync(registerRequest.Username)) return false;
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
                var newAccount = new Account
                {
                    Username = registerRequest.Username,
                    PasswordHash = hashedPassword,
                    IsActive = true,
                    User = new User
                    {
                        FullName = registerRequest.FullName,
                        Email = registerRequest.Email
                    }
                };
                return await _repo.AddAsync(newAccount);

            }
            catch(Exception e)
            {
                throw;
            }


        }
    }

}
