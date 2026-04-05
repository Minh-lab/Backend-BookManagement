

using BackendAPIASP.Data;
using BackendAPIASP.Interfaces.Repository;
using BackendAPIASP.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPIASP.Repositores;

public class AccountRepository : IAccountRepository
{
    private AppDbContext _db;
    public AccountRepository(AppDbContext db)
    {
        this._db = db;
    }
    public async Task<bool> AddAsync(Account account)
    {
        try
        {
            await _db.Accounts.AddAsync(account);
            int res = await _db.SaveChangesAsync();
            return res > 0;
        }
        catch (Exception e)
        {
            throw;
        }



    }

    public async Task<Account?> GetByIdAsync(int accountId)
    {
        try
        {
            return await _db.Accounts.FirstOrDefaultAsync((e) => e.AccountId == accountId);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<Account?> GetByUsernameAsync(string username)
    {
        try
        {
            return await _db.Accounts.Include(e => e.User).FirstOrDefaultAsync(e => e.Username == username);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<bool> IsUsernameExistsAsync(string username)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            return (await _db.Accounts.AnyAsync(e => e.Username == username));
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<bool> UpdateStatusAsync(int accountId, bool isActive)
    {
        try
        {
            Account? account= await _db.Accounts.FindAsync(accountId);
            if (account == null) return false;
            account.IsActive = isActive;
            int res = await _db.SaveChangesAsync();
            return res > 0;
        }
        catch(Exception e)
        {
            throw;
        }
    }
}
