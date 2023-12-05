using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2bApi.Availabilities.Db;

public class AvailabilityConfiguration : IEntityTypeConfiguration<Entities.Availabilities>
{
	public void Configure(EntityTypeBuilder<Entities.Availabilities> builder)
	{
		builder.ToTable("availabilities");
		
		builder.Property(x => x.Id)
			.HasColumnType("uniqueidentifier")
			.HasColumnName("id")
			.IsRequired();
		builder.HasKey(x => x.Id);
		
		builder.HasOne(x => x.Product)
			.WithMany()
			.HasForeignKey("product_id")
			.IsRequired();
		
		builder.Property(x => x.Quantity)
			.HasColumnType("int")
			.HasColumnName("quantity")
			.IsRequired();
		
	}
}