namespace To_Do_List_API.Entities;

using Microsoft.EntityFrameworkCore;

public class ChatAppContext : DbContext
{
    public ChatAppContext(DbContextOptions<ChatAppContext> options) 
        : base(options) {}

    public DbSet<TblUser> TblUsers { get; set; }
}
