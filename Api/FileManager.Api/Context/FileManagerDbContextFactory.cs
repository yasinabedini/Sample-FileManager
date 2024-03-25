using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyUploader.Context
{
    public class FileManagerDbContextFactory : IDesignTimeDbContextFactory<FileManagerDbContext>
    {
        public FileManagerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FileManagerDbContext>();
            optionsBuilder.UseSqlServer("Server=YasiAbdn\\ABDN;initial catalog=UploaderDb;Integrated Security=true");

            return new FileManagerDbContext(optionsBuilder.Options);
        }
    }
}
