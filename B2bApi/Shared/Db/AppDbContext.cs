using B2bApi.Carts.Entities;
using B2bApi.Discounts.Entities;
using B2bApi.ExchangeRates.Entities;
using B2bApi.Products.Entities;
using B2bApi.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace B2bApi.Shared.Db;

public class AppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<ExchangeRate> ExchangeRates { get; set; }
	public DbSet<Rate> Rates { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Availabilities.Entities.Availabilities> Availabilities { get; set; }
	public DbSet<Discount> Discounts { get; set; }
	public DbSet<Cart> Carts { get; set; }
	public DbSet<Items> CartItems { get; set; }
	public DbSet<Orders.Entities.Order> Orders { get; set; }
	public DbSet<Points.Entities.Points> Points { get; set; }
    

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(IApiMarker).Assembly);
		base.OnModelCreating(modelBuilder);
	}
}