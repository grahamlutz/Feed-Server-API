using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class FeedDataContext : DbContext
{
    public DbSet<FeedItem> Items{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=./feed.db");
    }
}