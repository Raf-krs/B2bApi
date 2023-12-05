using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace B2bApi.Shared.Db.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20231205212032_Price")]
public partial class Price : Migration 
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		var query = @"
					CREATE FUNCTION [dbo].[f_price_calculate](@Product uniqueidentifier, @Currency CHAR(3))
					RETURNS @Calc_price TABLE (product UNIQUEIDENTIFIER, price NUMERIC(18, 6))
					AS 
					BEGIN 
					    DECLARE @Price NUMERIC(18, 6)
					    DECLARE @DiscountedPrice NUMERIC(18, 6)
					    DECLARE @Rate NUMERIC(6, 4)
					    SET @Price = (SELECT price FROM products WHERE id = @Product)
					    SET @DiscountedPrice = (SELECT ISNULL(price, 0) 
					                           FROM discounts 
					                           WHERE product_id = @Product 
					                               AND GETDATE() BETWEEN start_date AND end_date)
					    IF @DiscountedPrice > 0
					    BEGIN
					        SET @Price = @DiscountedPrice
					    END
					    SET @Rate = 1
					    IF UPPER(@Currency) <> 'PLN'
					    BEGIN
					        SET @Rate = (SELECT TOP 1 r.mid FROM rates AS r
					                     LEFT JOIN exchange_rates AS er ON (r.exchange_rate_no = er.no)
					                     WHERE r.code = @Currency
					                     ORDER BY er.date DESC)
					    END
					    INSERT @Calc_price VALUES (@Product, @Price * @Rate)
					    RETURN
					END
					";
		migrationBuilder.Sql(query);
	}
	
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.Sql("DROP FUNCTION f_price_calculate");
	}
}