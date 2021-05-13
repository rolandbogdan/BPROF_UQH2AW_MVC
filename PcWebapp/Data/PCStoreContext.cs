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

            modelbuilder.Entity<IdentityRole>().HasData(
                new { Id = "12d2ff91-0ea7-415f-90df-ef859d3442a3", Name = "Admin", NormalizedName = "ADMIN" },
                new { Id = "9cea17a5-8122-487f-b1b5-59ec930f0055", Name = "Customer", NormalizedName = "CUSTOMER" }
                );

            var appUser = new IdentityUser
            {
                Id = "257ae463-93dc-48ed-95f8-000c4fdb3629",
                Email = "bogdanroland07@gmail.com",
                NormalizedEmail = "bogdanroland07@gmail.com",
                EmailConfirmed = true,
                UserName = "bogdanroland07@gmail.com",
                NormalizedUserName = "bogdanroland07@gmail.com",
                SecurityStamp = string.Empty
            };
            appUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Sajtvagyok123");

            var appUser2 = new IdentityUser
            {
                Id = "f07f59d8-6a55-4f08-9fb8-40d12808197e",
                Email = "peldabela@gmail.com",
                NormalizedEmail = "peldabela@gmail.com",
                EmailConfirmed = true,
                UserName = "peldabela@gmail.com",
                NormalizedUserName = "peldabela@gmail.com",
                SecurityStamp = string.Empty
            };
            appUser2.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Sajtvagyok123");

            // admin
            modelbuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "12d2ff91-0ea7-415f-90df-ef859d3442a3",
                UserId = "257ae463-93dc-48ed-95f8-000c4fdb3629"
            });

            // customer
            modelbuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "9cea17a5-8122-487f-b1b5-59ec930f0055",
                UserId = "f07f59d8-6a55-4f08-9fb8-40d12808197e"
            });

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
