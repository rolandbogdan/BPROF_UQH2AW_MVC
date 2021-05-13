using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PCStoreContext : IdentityDbContext<IdentityUser>
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
                    // UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\PCStore.mdf;integrated security=True;MultipleActiveResultSets=True");
                    UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebshopDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            // todo admin user db seed

            modelbuilder.Entity<Product>(entity =>
            {
                entity
                .HasOne(product => product.Customer)
                .WithMany(customer => customer.Products)
                .HasForeignKey(product => product.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelbuilder.Entity<Customer>(entity =>
            {
                entity
                .HasOne(customer => customer.Order)
                .WithMany(order => order.Customers)
                .HasForeignKey(customer => customer.OrderID)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
