using Microsoft.EntityFrameworkCore;
using test_sample.Data.Models;

namespace test_sample.Data.Contexts
{
  public class TranslationsDbContext : DbContext
  {
    public TranslationsDbContext(DbContextOptions<TranslationsDbContext> options)
      : base(options) { }

    public DbSet<Translation> Translations { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>(entity => entity.HasIndex(e => e.Email).IsUnique());
    }
  }
}