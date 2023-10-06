using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BookStoreAPI.DbContext
{
    public class BookStoreDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<AuthorBook>(
                 l => l.HasOne<Author>(e => e.Author).WithMany(e => e.AuthorBooks),
                 r => r.HasOne<Book>(e => e.Book).WithMany(e => e.AuthorBooks)
                );      



            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);
                
            
            modelBuilder.Entity<Genre>()                
                .HasMany(g => g.Books)
                .WithOne(b => b.Genre);
                
        }


    }

}
