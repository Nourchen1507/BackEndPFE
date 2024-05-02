using App.ApplicationCore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Infrastructure.Persistance
{


    public class ApplicationDbContext : DbContext
    {

        private readonly IConfiguration _configuration;


        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }




        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<SoldeCarte > SoldeCarte { get; set; }
        public DbSet<Transaction> Transaction { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {





            modelBuilder.Entity<User>(entity =>
            {




                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.FirstName).IsRequired();
                entity.Property(u => u.LastName).IsRequired();
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.PasswordHash).IsRequired();
                // Pour les Enums, utilisez HasConversion pour convertir entre les types Enum C# et les chaînes
                entity.Property(u => u.Role).HasConversion<string>();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasIndex(c => c.Name).IsUnique();
                // entity.Property(c => c.Image).IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.OrderStatus).HasConversion<string>();
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(orderItem => new { orderItem.OrderId, orderItem.ProductId });

                entity.HasOne(orderItem => orderItem.Order)
                    .WithMany(order => order.OrderItems)
                    .HasForeignKey(orderItem => orderItem.OrderId);

                entity.HasOne(orderItem => orderItem.Product)
                    .WithMany()
                    .HasForeignKey(orderItem => orderItem.ProductId);
            });



            modelBuilder.Entity<Facture>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.HasOne(e => e.Order) // Définition de la relation avec la commande
                        .WithMany() // Il est probable que vous ayez une relation "Many-to-One" ici
                        .HasForeignKey(e => e.OrderId)
                        .OnDelete(DeleteBehavior.Cascade); // Assurez-vous de spécifier le comportement de suppression approprié
                });
        }
    }
}