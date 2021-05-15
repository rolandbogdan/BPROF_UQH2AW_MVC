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
                    UseSqlServer("Server=tcp:broland.database.windows.net,1433;Initial Catalog=pcstoredb;Persist Security Info=False;User ID=broli27;Password=Roliqwer270123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                    // UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\PCStore.mdf;integrated security=True;MultipleActiveResultSets=True");
                    // UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebshopDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<IdentityRole>().HasData(
                new { Id = "341743f0-asd2–42de-afbf-59kmkkmk72cf6", Name = "Admin", NormalizedName = "ADMIN" },
                new { Id = "341743f0-dee2–42de-bbbb-59kmkkmk72cf6", Name = "Customer", NormalizedName = "CUSTOMER" });

            var appUser = new IdentityUser
            {
                Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                Email = "bogdanroland07@gmail.com",
                NormalizedEmail = "bogdanroland07@gmail.com",
                EmailConfirmed = true,
                UserName = "bogdanroland07@gmail.com",
                NormalizedUserName = "bogdanroland07@gmail.com",
                SecurityStamp = string.Empty
            };

            var appUser2 = new IdentityUser
            {
                Id = "e2174cf0–9412–4cfe-afbf-59f706d72cf6",
                Email = "bob@gmail.com",
                NormalizedEmail = "bob@gmail.com",
                EmailConfirmed = true,
                UserName = "bob@gmail.com",
                NormalizedUserName = "bob@gmail.com",
                SecurityStamp = string.Empty
            };

            appUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Sajtvagyok123");
            appUser2.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Bobvagyok123");


            modelbuilder.Entity<IdentityUser>().HasData(appUser);
            modelbuilder.Entity<IdentityUser>().HasData(appUser2);

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
