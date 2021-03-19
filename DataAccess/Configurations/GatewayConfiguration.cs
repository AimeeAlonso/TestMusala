using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TestMusala.DataAccess
{
   public class GatewayConfiguration : IEntityTypeConfiguration<Gateway>
    {
        public void Configure(EntityTypeBuilder<Gateway> builder)
        {
            builder.HasIndex(o => new { o.SerialNumber }).IsUnique();
            builder.ToTable("Gateways");
        }
    }
}
