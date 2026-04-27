using Microsoft.EntityFrameworkCore;
using PIZZAORDERING.Models;

namespace PIZZAORDERING.Data;

public class AppDbContext : DbContext
{
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItems> CartItems { get; set; }
    public DbSet<Coupans> Coupans { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<LoyaltyTransaction> LoyaltyTransactions { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<ProductBrands> ProductBrands { get; set; }
    public DbSet<ProductCatogries> ProductCatogries { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCoupanUsages> UserCoupanUsages { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}