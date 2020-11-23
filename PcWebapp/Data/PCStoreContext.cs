using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class PCStoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public PCStoreContext(DbContextOptions<PCStoreContext> opt) : base(opt)
        {

        }
        public PCStoreContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\PCStore.mdf;integrated security=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Product>(entity =>
            {
                entity
                .HasOne(product => product.Customer)
                .WithMany(customer => customer.Products)
                .HasForeignKey(product => product.CustomerID);
            });

            modelbuilder.Entity<Customer>(entity =>
            {
                entity
                .HasOne(customer => customer.Order)
                .WithMany(order => order.Customers)
                .HasForeignKey(customer => customer.OrderID);
            });
        }
    }
}
