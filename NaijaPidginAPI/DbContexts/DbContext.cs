using Microsoft.EntityFrameworkCore;
using NaijaPidginAPI.Entities;

namespace NaijaPidginAPI.DbContexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Word> Words { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<WordClass> WordClasses { get; set; }

    }
}