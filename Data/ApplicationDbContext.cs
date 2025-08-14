using ST10090394_PROG7311_POE_Part2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ST10090394_PROG7311_POE_Part2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        // Represents the Farmers table in the database
        public DbSet<Farmers> Farmers { get; set; }

        // Represents the Products table in the database
        public DbSet<Products> Products { get; set; }

        // Represents the Employees table in the database
        public DbSet<Employees> Employees { get; set; }

        // Override the OnModelCreating method to configure entity relationships or other configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employees>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Farmers>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Products>()
                .HasOne(p => p.Farmer)
                .WithMany()
                .HasForeignKey(p => p.FarmerId);

            modelBuilder.Entity<Farmers>()
                .HasKey(f => f.FarmerId);
            // Add any additional configurations here if needed
        }
    }
}