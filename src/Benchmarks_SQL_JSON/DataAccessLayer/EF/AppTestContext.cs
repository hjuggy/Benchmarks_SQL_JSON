using Benchmarks_SQL_JSON.Models;
using Microsoft.EntityFrameworkCore;

namespace Benchmarks_SQL_JSON.DataAccessLayer.EF
{
    public class AppTestContext : DbContext
    {
        private readonly string DbConnectionStringName = "Server=localhost;Port=5432;Username=postgres;Database=SqlJson;Password=password123;SSLMode=Prefer";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(DbConnectionStringName);

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Subscription>().ToTable("Subscription");
            modelBuilder.Entity<Order>().ToTable("Order");

            modelBuilder.Entity<User>()
                    .Property(s => s.Id)
                    .HasColumnName("User_id")
                    .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(s => s.Id)
                .HasColumnName("Order_Id")
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasOne<User>(u => u.User)
                .WithMany(g => g.Orders)
                .HasForeignKey(s => s.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                .Property(s => s.Id)
                .HasColumnName("Subscription_Id")
                .IsRequired();

            modelBuilder.Entity<Subscription>()
                .HasOne<User>(u => u.User)
                .WithMany(g => g.Subscriptions)
                .HasForeignKey(s => s.User_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
