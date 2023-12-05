using B2bApi.ExchangeRates.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2bApi.ExchangeRates.Db;

public class RateConfiguration : IEntityTypeConfiguration<Rate>
{
	public void Configure(EntityTypeBuilder<Rate> builder)
	{
		builder.ToTable("rates");

		builder.Property(r => r.Id)
			.HasColumnType("uniqueidentifier")
			.HasColumnName("id")
			.IsRequired();
		builder.HasKey(r => r.Id);
		
		builder.Property(r => r.Code)
			.HasColumnType("char(3)")
			.HasColumnName("code")
			.IsRequired();
		
		builder.Property(r => r.Name)
			.HasColumnType("varchar(50)")
			.HasColumnName("name")
			.IsRequired();
		
		builder.Property(r => r.Mid)
			.HasColumnType("numeric(6, 4)")
			.HasColumnName("mid")
			.IsRequired();
		
		builder.Property(r => r.Bid)
			.HasColumnType("numeric(6, 4)")
			.HasColumnName("bid")
			.IsRequired();
		
		builder.Property(r => r.Ask)
			.HasColumnType("numeric(6, 4)")
			.HasColumnName("ask")
			.IsRequired();
		
		builder.HasOne(r => r.ExchangeRate)
			.WithMany(e => e.Rates)
			.HasForeignKey("exchange_rate_no")
			.IsRequired();
	}
}