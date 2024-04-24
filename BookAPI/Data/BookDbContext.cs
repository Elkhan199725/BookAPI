using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BookAPI.Data;

public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
