
using Microsoft.EntityFrameworkCore;
using Security.Domain;


namespace Security.Data
{
    public class SecurityContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FilePackage> Files { get; set; }
        public DbSet<Keys> Keys { get; set; }

        public SecurityContext(DbContextOptions options) : base(options)
        {
        }
    }
}