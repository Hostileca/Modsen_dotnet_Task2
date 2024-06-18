using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Data.Configs
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Guid);

            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserGuid)
                   .IsRequired(); 

            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey(oi => oi.OrderGuid)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
