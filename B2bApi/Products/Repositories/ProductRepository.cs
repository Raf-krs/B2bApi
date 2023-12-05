using B2bApi.Products.Dtos.Requests;
using B2bApi.Products.Dtos.Responses;
using B2bApi.Products.Entities;
using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.Db;
using B2bApi.Shared.Db.Data;
using B2bApi.Shared.Dtos;
using Dapper;

namespace B2bApi.Products.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly AppDbContext _dbContext;
	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	public ProductRepository(AppDbContext dbContext, ISqlConnectionFactory sqlConnectionFactory)
	{
		_dbContext = dbContext;
		_sqlConnectionFactory = sqlConnectionFactory;
	}

	public async Task AddAsync(Product product, CancellationToken cancellationToken)
	{
		await _dbContext.Products.AddAsync(product, cancellationToken);
	}

	public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var connection = _sqlConnectionFactory.Create();
		const string query = "SELECT * FROM products WHERE id = @Id";
		var command = new CommandDefinition(query, new { Id = id}, cancellationToken: cancellationToken);
		var result = await connection.QuerySingleAsync<ProductDto>(command);
		
		return result?.ToEntity();
	}

	public async Task<PagedResponse<ProductDto>> GetAllAsync(
		ProductsRequestOptions options, 
		CancellationToken cancellationToken)
	{
		var whereClause = GetWhereClause(options);
		var orderByClause = GetOrderByClause(options);
		var whereParameters = new { Name = $"%{options.Name}%" };
		var totalCount = await GetTotalCount(whereClause, whereParameters, cancellationToken);
		var query = $"""
					SELECT p.id, p.name, p.description, s.stock, pr.price 
					FROM products as p 
					CROSS APPLY f_stock(p.id) as s 
					CROSS APPLY f_price_calculate(p.id, '{options.Currency}') as pr 
					{whereClause} 
					{orderByClause} 
					OFFSET {(options.Page.Value - 1) * options.PageSize} ROWS 
					FETCH NEXT {options.PageSize} ROWS ONLY;
					""";
		var command = new CommandDefinition(query, whereParameters, cancellationToken: cancellationToken);
		var connection = _sqlConnectionFactory.Create();
		var result = await connection.QueryAsync<ProductDto>(command);
		
		return new PagedResponse<ProductDto>
		{
			Items = result,
			TotalCount = totalCount,
			HasNextPage = options.Page.Value * options.PageSize < totalCount,
		};
	}
	
	private async Task<int> GetTotalCount(string whereClause, object parameters, CancellationToken cancellationToken)
	{
		var connection = _sqlConnectionFactory.Create();
		var query = $"""
					SELECT COUNT(*) as result
					FROM products as p 
					{whereClause}
					""";
		var command = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
		var result = await connection.ExecuteScalarAsync<int>(command);
		
		return result;
	}
	
	private string GetWhereClause(ProductsRequestOptions options)
	{
		var whereClause = "WHERE 1=1";
		if (!string.IsNullOrEmpty(options.Name))
		{
			whereClause = "WHERE p.name LIKE @Name";
		}

		return whereClause;
	}
	
	private string GetOrderByClause(ProductsRequestOptions options)
	{
		var orderByClause = "ORDER BY p.id ASC";
		if(!string.IsNullOrEmpty(options.SortField))
		{
			var sortDirection = options.SortDirection switch
			{
				SortDirection.Descending => "DESC",
				_ => "ASC"
			};
			orderByClause = $"ORDER BY {options.SortField} {sortDirection}";
		}

		return orderByClause;
	}
}