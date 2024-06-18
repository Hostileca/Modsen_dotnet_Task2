using DataAccessLayer.Models;
﻿using DataAccessLayer.Data.Configs;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>(); 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var categoryConfig = new CategoryConfig();
            var orderConfig = new OrderConfig();
            var orderItemConfig = new OrderItemConfig();
            var productConfig = new ProductConfig();
            var roleConfig = new RoleConfig();
            var userConfig = new UserConfig();

            modelBuilder.ApplyConfiguration(categoryConfig);
            modelBuilder.ApplyConfiguration(orderConfig);
            modelBuilder.ApplyConfiguration(orderItemConfig);
            modelBuilder.ApplyConfiguration(productConfig);
            modelBuilder.ApplyConfiguration(roleConfig);
            modelBuilder.ApplyConfiguration(userConfig);

            base.OnModelCreating(modelBuilder);
        }
    }
}
