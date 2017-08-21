using datamodel.Models;
using Microsoft.EntityFrameworkCore;

namespace dataModel
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext()
        {
        }

        public DbSet<Url> Urls { get; set; }
        public DbSet<UrlHistory> UrlHistories { get; set; }
        public DbSet<UrlError> UrlErrors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            UrlContext.Build(builder);
        }
    }
}