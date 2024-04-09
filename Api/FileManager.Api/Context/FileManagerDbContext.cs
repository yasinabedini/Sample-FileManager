using FileManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Api.Context
{
    public class FileManagerDbContext : DbContext
    {
        public FileManagerDbContext(DbContextOptions<FileManagerDbContext> options) : base(options)
        {

        }

        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasIndex(t => t.Id).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=YasiAbdn\\ABDN;initial catalog=FileManagerDb;Integrated Security=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
