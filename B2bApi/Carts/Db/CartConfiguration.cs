using B2bApi.Carts.Entities;
using B2bApi.Orders.Entities;
using B2bApi.Shared.ValueObjects;
using B2bApi.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2bApi.Carts.Db;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
	public void Configure(EntityTypeBuilder<Cart> builder)
	{
		builder.ToTable("carts");

		builder.Property(x => x.Id)
			.HasColumnType("uniqueidentifier")
			.HasColumnName("id")
			.IsRequired();
		builder.HasKey(x => x.Id);
		
		builder.Property(x => x.UserId)
			.HasColumnType("uniqueidentifier")
			.HasColumnName("user_id")
			.IsRequired();
		builder.HasOne<User>()
			.WithMany()
			.HasForeignKey(x => x.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Property(x => x.OrderId)
			.HasColumnType("uniqueidentifier")
			.HasColumnName("order_id");
		builder.HasOne<Order>()
			.WithMany()
			.HasForeignKey(x => x.OrderId)
			.OnDelete(DeleteBehavior.SetNull);

		builder.Property(x => x.Name)
			.HasConversion(x => x.Value, 
							x => new Name(x))
			.HasColumnType("varchar(100)")
			.HasColumnName("name")
			.IsRequired();
		
		builder.Property(x => x.Currency)
			.HasConversion(x => x.Code, 
							x => Currency.FromCode(x))
			.HasColumnType("char(3)")
			.HasColumnName("currency")
			.IsRequired();

		builder.Property(x => x.Comment)
			.HasColumnType("varchar(500)")
			.HasColumnName("comment");
		
		builder.Property(x => x.CreatedAt)
			.HasColumnType("datetime")
			.HasColumnName("created_at")
			.ValueGeneratedOnAdd()
			.IsRequired();
	}
}