using B2bApi.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2bApi.Points.Db;

public class PointsConfiguration : IEntityTypeConfiguration<Entities.Points>
{
	public void Configure(EntityTypeBuilder<Entities.Points> builder)
	{
		builder.ToTable("points");

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
		
		builder.Property(x => x.Amount)
			.HasColumnType("int")
			.HasColumnName("amount")
			.IsRequired();
	}
}