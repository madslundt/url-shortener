using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace datamodel.Models
{
    public class UrlContext
    {
        public static void Build(ModelBuilder builder)
        {
            builder.Entity<Url>(b =>
            {
                b.Property(p => p.Id)
                    .IsRequired();

                b.Property(p => p.Created)
                    .ValueGeneratedOnAdd();

                b.HasMany(r => r.UrlHistories)
                    .WithOne(r => r.Url)
                    .HasForeignKey(r => r.UrlId);

                b.HasIndex(idx => idx.Id)
                    .IsUnique();
                b.HasIndex(idx => idx.ShortId)
                    .IsUnique();
                b.HasIndex(idx => idx.RedirectUrl)
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.ToTable("Urls");
            });

            builder.Entity<UrlHistory>(b =>
            {
                b.Property(p => p.Id)
                    .IsRequired();

                b.Property(p => p.Redirected)
                    .ValueGeneratedOnAdd();

                b.HasKey(k => k.Id);

                b.ToTable("UrlRedirects");
            });
        }
    }
}