using FreshUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreshUp.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryHistory> InventoryHistories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderList> OrderLists { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region FluentApi
        //One to many realition Category and Product
        modelBuilder.Entity<Category>()
            .HasMany(x => x.Products)
            .WithOne(x => x.Category);

        modelBuilder.Entity<Product>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        //One to many relition Order and OrderList
        modelBuilder.Entity<Order>()
            .HasMany(x => x.OrderLists)
            .WithOne(x => x.Order);

        modelBuilder.Entity<OrderList>()
             .HasOne(x => x.Order)
             .WithMany(x => x.OrderLists)
             .HasForeignKey(x => x.OrderId)
             .OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}
