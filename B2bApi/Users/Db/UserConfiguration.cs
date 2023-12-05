using B2bApi.Shared.ValueObjects;
using B2bApi.Users.Entities;
using B2bApi.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2bApi.Users.Db;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("users");
		
		builder.Property(u => u.Id)
			.HasColumnType("uniqueidentifier")
			.HasColumnName("id");
		builder.HasKey(u => u.Id);

		builder.Property(u => u.Email)
			.HasConversion(e => e.Value, 
							e => new Email(e))
			.HasColumnType("varchar(200)")
			.HasColumnName("email")
			.IsRequired();
		builder.HasIndex(u => u.Email).IsUnique();
		
		builder.Property(u => u.Username)
			.HasConversion(u => u.Value, 
							u => new Username(u))
			.HasColumnType("varchar(200)")
			.HasColumnName("username")
			.IsRequired();
		
		builder.Property(u => u.Password)
			.HasConversion(p => p.Value, 
							p => new Password(p))
			.HasColumnType("varchar(200)")
			.HasColumnName("password")
			.IsRequired();

		builder.Property(u => u.Role)
			.HasConversion(r => r.Value, 
							r => new Role(r))
			.HasColumnType("varchar(20)")
			.HasColumnName("role")
			.IsRequired();
		
		builder.Property(u => u.Currency)
			.HasConversion(c => c.Code, 
							c => Currency.FromCode(c))
			.HasColumnType("char(3)")
			.HasColumnName("currency")
			.IsRequired();
	}
}