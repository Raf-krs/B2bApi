using B2bApi.Products.Entities;
using B2bApi.Products.ValueObjects;
using B2bApi.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2bApi.Products.Db;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable("products");
		
		builder.Property(x => x.Id)
			.HasColumnName("id");
		builder.HasKey(x => x.Id);
		
		builder.Property(x => x.Name)
			.HasColumnName("name")
			.HasConversion(x => x.Value, 
							x => new Name(x))
			.IsRequired()
			.HasMaxLength(100);
		
		builder.Property(x => x.Description)
			.HasConversion(x => x.Value, 
							x => new Description(x))
			.IsRequired()
			.HasMaxLength(500);
		
		builder.Property(x => x.Price)
			.HasConversion(x => x.Amount, 
							x => new Money(x, Currency.Pln))
			.IsRequired()
			.HasColumnType("decimal(18, 4)");
	}
}