using B2bApi.Discounts.Entities;
using B2bApi.Products.Entities;
using B2bApi.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2bApi.Discounts.Db;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
	public void Configure(EntityTypeBuilder<Discount> builder)
	{
		builder.ToTable("discounts");
		
		builder.Property(x => x.Id)
			.HasColumnType("uniqueidentifier")
			.HasColumnName("id")
			.IsRequired();
		builder.HasKey(x => x.Id);
		
		builder.Property(x => x.ProductId)
			.HasColumnType("uniqueidentifier")
			.HasColumnName("product_id")
			.IsRequired();
		builder.HasOne<Product>()
			.WithMany()
			.HasForeignKey(x => x.ProductId)
			.OnDelete(DeleteBehavior.Cascade);
		
		builder.Property(x => x.Price)
			.HasConversion(x => x.Amount, 
							x => new Money(x, Currency.Pln))
			.HasColumnType("numeric(18, 4)")
			.HasColumnName("price")
			.IsRequired();
		
		builder.Property(x => x.StartDate)
			.HasColumnType("date")
			.HasColumnName("start_date")
			.IsRequired();
		
		builder.Property(x => x.EndDate)
			.HasColumnType("date")
			.HasColumnName("end_date")
			.IsRequired();
	}
}