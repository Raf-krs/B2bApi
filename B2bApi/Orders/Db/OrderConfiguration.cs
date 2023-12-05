using B2bApi.Orders.Entities;
using B2bApi.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2bApi.Orders.Db;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.ToTable("orders");

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

		builder.Property(x => x.TotalAmount)
			.HasColumnType("numeric(18, 2)")
			.HasColumnName("total_amount")
			.IsRequired();
		
		builder.Property(x => x.CreatedAt)
			.HasColumnType("datetime")
			.HasColumnName("created_at")
			.ValueGeneratedOnAdd()
			.IsRequired();
	}
}