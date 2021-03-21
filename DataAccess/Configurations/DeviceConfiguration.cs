using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TestMusala.DataAccess
{
   public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasIndex(o => new { o.UId }).IsUnique();
            builder.ToTable("Devices");
        }
    }
}
