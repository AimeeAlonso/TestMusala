using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TestMusala.DataAccess
{
    class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.Property(o => o.Id).HasDefaultValueSql("newsequentialid()");
            builder.HasIndex(o => new { o.GatewayId }).IsUnique();
            builder.ToTable("Devices");
        }
    }
}
