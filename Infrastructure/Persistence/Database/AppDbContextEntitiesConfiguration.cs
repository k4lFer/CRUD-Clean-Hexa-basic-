using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public partial class AppDbContext
    {
        private void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TCustomer>().ToTable("customers");
            modelBuilder.Entity<TOwner>().ToTable("owners");
            modelBuilder.Entity<TProduct>().ToTable("products");
            modelBuilder.Entity<TOrder>().ToTable("orders");
            modelBuilder.Entity<TOrderDetail>().ToTable("order_details");
            ConfigureEntityRelationships(modelBuilder);
        }
        public DbSet<TCustomer> Customers { get; set; }
        public DbSet<TOwner> Owners { get; set; }
        public DbSet<TProduct> Products { get; set; }
        public DbSet<TOrder> Orders { get; set; }
        public DbSet<TOrderDetail> OrderDetails { get; set; }
    }
}
