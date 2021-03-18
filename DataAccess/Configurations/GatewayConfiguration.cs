using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TestMusala.DataAccess
{
    class GatewayConfiguration : IEntityTypeConfiguration<Gateway>
    {
        public void Configure(EntityTypeBuilder<Gateway> builder)
        {
            builder.Property(o => o.Id).HasDefaultValueSql("newsequentialid()");
            builder.HasIndex(o => new { o.SerialNumber }).IsUnique();
            builder.ToTable("Gateways");
        }
    }
}
