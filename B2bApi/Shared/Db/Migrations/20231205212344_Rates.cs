using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace B2bApi.Shared.Db.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20231205212344_Rates")]
public partial class Rates : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		var query = @"
					CREATE VIEW [dbo].[v_rates]
					AS
					SELECT 
						er.no
						, er.date
						, r.name
						, r.code
						, r.mid as value
					FROM rates as r
					LEFT JOIN exchange_rates as er on (er.no = r.exchange_rate_no)
					WHERE er.[table] = 'A' or er.[table] = 'B'
					";
		migrationBuilder.Sql(query);
	}
	
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.Sql("DROP VIEW v_rates");
	}
}