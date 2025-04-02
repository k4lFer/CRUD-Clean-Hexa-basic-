using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence
{
    public partial class AppDbContext
    {
        private void ConfigureEntityRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TOwner>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.role)
                    .IsRequired()
                    .HasConversion(v => v.ToString(), v => (RoleEnum)Enum.Parse(typeof(RoleEnum), v))
                    .HasColumnType("enum('Admin','Manager')");
            });

            modelBuilder.Entity<TCustomer>(entity =>
            {
                entity.HasKey(e => e.id);
            });

            modelBuilder.Entity<TProduct>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.HasMany(p => p.OrderDetails)
                    .WithOne(od => od.Product)
                    .HasForeignKey(od => od.productId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TOrder>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.status)
                    .IsRequired()
                    .HasConversion(v => v.ToString(), v => (OrderEnum)Enum.Parse(typeof(OrderEnum), v))
                    .HasColumnType("enum('Pending','Processing','Completed','Cancelled')");

                entity.HasMany(o => o.OrderDetails)
                    .WithOne(od => od.Order)
                    .HasForeignKey(od => od.orderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TOrderDetail>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.productId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.orderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
