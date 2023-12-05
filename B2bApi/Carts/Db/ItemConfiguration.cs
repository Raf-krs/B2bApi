using B2bApi.Carts.Entities;
using B2bApi.Carts.ValueObjects;
using B2bApi.Products.Entities;
using B2bApi.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2bApi.Carts.Db;

public class ItemConfiguration : IEntityTypeConfiguration<Items>
{
	public void Configure(EntityTypeBuilder<Items> builder)
	{
		builder.ToTable("cart_items");
		
		builder.Property(x => x.Id)
			.HasColumnType("uniqueidentifier")
			.HasColumnName("id")
			.IsRequired();
		builder.HasKey(x => x.Id);
		
		builder.Property(x => x.CartId)
			.HasColumnType("uniqueidentifier")
			.HasColumnName("cart_id")
			.IsRequired();
		builder.HasOne<Cart>()
			.WithMany()
			.HasForeignKey(x => x.CartId)
			.OnDelete(DeleteBehavior.Cascade);
		
		builder.Property(x => x.ProductId)
			.HasColumnType("uniqueidentifier")
			.HasColumnName("product_id")
			.IsRequired();
		builder.HasOne<Product>()
			.WithMany()
			.HasForeignKey(x => x.ProductId)
			.OnDelete(DeleteBehavior.Cascade);
		
		builder.Property(x => x.Quantity)
			.HasConversion(x => x.Value, 
							x => new Quantity(x))
			.HasColumnType("int")
			.HasColumnName("quantity")
			.IsRequired();
		
		builder.Property(x => x.Price)
			.HasConversion(x => x.Amount, 
							x => new Money(x, Currency.None))
			.HasColumnType("numeric(18,2)")
			.HasColumnName("price")
			.IsRequired();
	}
}