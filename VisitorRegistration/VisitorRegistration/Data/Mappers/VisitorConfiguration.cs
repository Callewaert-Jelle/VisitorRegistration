using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitorRegistration.Models.Domain;

namespace VisitorRegistration.Data.Mappers
{
    public class VisitorConfiguration : IEntityTypeConfiguration<Visitor>
    {
        public void Configure(EntityTypeBuilder<Visitor> builder)
        {
            builder.ToTable("Visitor");

            builder.HasKey(v => v.VisitorId);

            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(v => v.LastName)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(v => v.VisitorType)
                .IsRequired();
            builder.Property(v => v.Entered)
                .IsRequired();
            builder.Property(v => v.Left);
            builder.Property(v => v.Company);
            builder.Property(v => v.LicensePlate);
        }
    }
}
