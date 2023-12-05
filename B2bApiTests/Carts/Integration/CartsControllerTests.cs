using System.Net;
using System.Net.Http.Json;
using B2bApi.Carts.Commands.Cart.Create;
using B2bApi.Carts.Commands.Item.AddItem;
using B2bApi.Carts.Entities;
using B2bApi.Products.Entities;
using B2bApi.Shared.ValueObjects;
using B2bApi.Users.Entities;
using B2bApi.Users.ValueObjects;
using B2bApiTests.Utils.Integration;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace B2bApiTests.Carts.Integration;

public class CartsControllerTests : TestApi
{
	[Fact]
	public async Task CreateCart_ShouldReturnCreated()
	{
		// Arrange
		var user = await SaveUser();
		var command = new CreateCartCommand("Cart One");
		Authorize(user);
		
		// Act
		var response = await Client.PostAsJsonAsync("api/carts", command);
		
		// Assert
		response.StatusCode.ShouldBe(HttpStatusCode.Created);
		var cartId = Guid.Parse(response.Headers.Location?.Segments.Last()!);
		var cartInDb = await TestDbContext.Carts.Where(x => x.Id == cartId).FirstOrDefaultAsync();
		cartInDb.ShouldNotBeNull();
	}
	
	[Fact]
	public async Task DeleteCart_ShouldReturnNoContent()
	{
		// Arrange
		var user = await SaveUser();
		var cart = await SaveCart(user);
		Authorize(user);
		
		// Act
		var response = await Client.DeleteAsync($"api/carts/{cart.Id}");
		
		// Assert
		response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
	}
	
	[Fact]
	public async Task AddItem_ShouldReturnCreated()
	{
		// Arrange
		var user = await SaveUser();
		var product = await SaveProduct();
		var cart = await SaveCart(user);
		var addItemCommand = new AddItemCommand(cart.Id, product.Id, 1, product.Price.Amount);
		Authorize(user);
		
		// Act
		var response = await Client.PostAsJsonAsync($"api/carts/{cart.Id}/item", addItemCommand);
		
		// Assert
		// INFO -> Microsoft OMG, response NoContent when Created return nothing in body [sic!], maybe return Accepted?
		response.StatusCode.ShouldBe(HttpStatusCode.NoContent); 
	}

	private async Task<User> SaveUser()
	{
		var user = User.Create(Guid.NewGuid(), "test@mail.com", "Test", "Test123$%", 
								Role.User, Currency.Usd.ToString());
		await AddEntityAsync(user);

		return user;
	}
	
	private async Task<Product> SaveProduct()
	{
		var product = Product.Create(Guid.NewGuid(), "Product One", "Desc", 10.00m);
		await AddEntityAsync(product);

		return product;
	}
	
	private async Task<Cart> SaveCart(User user)
	{
		var cart = Cart.Create(user.Id, null, "Cart One", user.Currency.Code, 
								"Just test", DateTime.UtcNow);
		await AddEntityAsync(cart);

		return cart;
	}
}