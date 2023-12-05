using B2bApi.ExchangeRates.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B2bApi.ExchangeRates.Db;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
	public void Configure(EntityTypeBuilder<ExchangeRate> builder)
	{
		builder.ToTable("exchange_rates");
		builder.Property(x => x.No)
			.HasColumnType("varchar(20)")
			.HasColumnName("no")
			.IsRequired();
		builder.HasKey(x => x.No);

		builder.Property(x => x.Table)
			.HasColumnType("char(1)")
			.HasColumnName("table")
			.IsRequired();
		
		builder.Property(x => x.Date)
			.HasColumnType("date")
			.HasColumnName("date")
			.IsRequired();
	}
}