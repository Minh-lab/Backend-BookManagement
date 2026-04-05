namespace BackendAPIASP.Data;

using BackendAPIASP.Models;
using Microsoft.EntityFrameworkCore;

public class    AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    //public DbSet<Category> Categories { get; set; }// mỗi DbSet là 1 bảng trong CSDL
    //public DbSet<TodoTask> TodoTasks { get; set; }//1 bảng trong CSDL
    //public DbSet<TodoTask> TodoTasks { get; set; }//1 bảng trong CSDL
    //public DbSet<TodoTask> TodoTasks { get; set; }//1 bảng trong CSDL
    //public DbSet<TodoTask> TodoTasks { get; set; }//1 bảng trong CSDL
    public DbSet<Book> Books { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserBook> UserBooks { get; set; }
}


