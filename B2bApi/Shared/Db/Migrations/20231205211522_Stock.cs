using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace B2bApi.Shared.Db.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20231205175855_Stock")]
public partial class Stock : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		var query = @"
					CREATE FUNCTION [dbo].[f_stock] (@Product UNIQUEIDENTIFIER)
					RETURNS @Stock TABLE 
						(product_id UNIQUEIDENTIFIER, stock INT)
					AS
					BEGIN
					    INSERT @Stock VALUES (@Product, (SELECT ISNULL(SUM(quantity), 0) FROM availabilities WHERE product_id = @Product))
					    RETURN
					END
					";
		migrationBuilder.Sql(query);
	}
	
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.Sql("DROP FUNCTION f_stock");
	}
}