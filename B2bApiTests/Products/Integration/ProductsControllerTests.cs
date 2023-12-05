using System.Net;
using B2bApi.Products.Dtos.Requests;
using B2bApi.Products.Dtos.Responses;
using B2bApi.Products.Entities;
using B2bApi.Shared.Dtos;
using B2bApiTests.Utils.Integration;
using Bogus;
using Shouldly;
using Xunit;

namespace B2bApiTests.Products.Integration;

public class ProductsControllerTests : TestApi
{
	private readonly Faker _faker = new();
	
	[Fact]
	public async Task Get_ShouldReturnList_WhenPageIsValid()
	{
		// Arrange
		var products = CreateProducts(6).ToList();
		await AddEntitiesAsync(products);
		var request = new ProductsRequest
		{
			OrderBy = "price"
		};
		var requestUri = $"api/products?page={request.Page}&pageSize={request.PageSize}&orderBy={request.OrderBy}";
		
		// Act
		var response = await Client.GetAsync(requestUri);

		// Assert
		var result = await DeserializeObject<PagedResponse<ProductDto>>(response);
		response.StatusCode.ShouldBe(HttpStatusCode.OK);
		result.ShouldNotBeNull();
		result.Items.Count().ShouldBe(6);
		result.TotalCount.ShouldBe(6);
		result.HasNextPage.ShouldBeFalse();
	}
	
	[Fact]
	public async Task Get_ShouldReturnList_WhenHasProductWithSoapName()
	{
		// Arrange
		var products = new List<Product>
		{
			Product.Create(Guid.NewGuid(), "Soap", _faker.Commerce.ProductDescription(), _faker.Random.Decimal(1, 1000)),
			Product.Create(Guid.NewGuid(), "Soap", _faker.Commerce.ProductDescription(), _faker.Random.Decimal(1, 1000)),
			Product.Create(Guid.NewGuid(), "Milk", _faker.Commerce.ProductDescription(), _faker.Random.Decimal(1, 1000))
		};
		await AddEntitiesAsync(products);
		var request = new ProductsRequest
		{
			Name = "soap"
		};
		var requestUri = $"api/products?name={request.Name}&page={request.Page}&pageSize={request.PageSize}";
		
		// Act
		var response = await Client.GetAsync(requestUri);

		// Assert
		var result = await DeserializeObject<PagedResponse<ProductDto>>(response);
		response.StatusCode.ShouldBe(HttpStatusCode.OK);
		result.ShouldNotBeNull();
		result.Items.Count().ShouldBe(2);
		result.TotalCount.ShouldBe(2);
		result.HasNextPage.ShouldBeFalse();
	}
	
	private IEnumerable<Product> CreateProducts(int count)
	{
		var products = new List<Product>();
		for (var i = 0; i < count; i++)
		{
			products.Add(Product.Create(Guid.NewGuid(), _faker.Commerce.Product(), _faker.Commerce.ProductDescription(),
										_faker.Random.Decimal(1, 1000)));
		}

		return products;
	}
	
	// ... rest of the tests
}