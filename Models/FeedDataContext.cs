using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class FeedDataContext : DbContext
{
    public DbSet<FeedItem> Items{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(! System.IO.Directory.Exists("./db")){
            System.IO.Directory.CreateDirectory("./db");
        }
        optionsBuilder.UseSqlite("Filename=./db/feed.db"); 
    }
}