using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace datamodel.Models
{
    public class UrlErrorContext
    {
        public static void Build(ModelBuilder builder)
        {
            builder.Entity<UrlError>(b =>
            {
                b.Property(p => p.Id)
                    .IsRequired();

                b.Property(p => p.Created)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.StatusCode)
                    .IsRequired();


                b.HasKey(k => k.Id);

                b.ToTable("UrlErrors");
            });
        }

    }
}