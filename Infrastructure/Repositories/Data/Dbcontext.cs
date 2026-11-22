using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data;

public class LibraryDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    // Cria ou conecta ao banco de dados SQLite local
    optionsBuilder.UseSqlite("Data Source=library.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    modelBuilder.Entity<Book>(entity =>
    {
    });

    modelBuilder.Entity<Author>(entity => entity.HasKey(e => e.Id));

    modelBuilder.Entity<Genre>(entity => entity.HasKey(e => e.Id));

    modelBuilder.Entity<Publisher>(entity => entity.HasKey(e => e.Id));
    }
}