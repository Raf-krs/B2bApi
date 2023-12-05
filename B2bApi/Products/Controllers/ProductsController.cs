using B2bApi.Products.Commands.CreateProduct;
using B2bApi.Products.Dtos.Requests;
using B2bApi.Products.Queries.GetProduct;
using B2bApi.Products.Queries.GetProducts;
using B2bApi.Shared.Security.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2bApi.Products.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly ISender _sender;
	private readonly IUserContext _userContext;
	
	public ProductsController(ISender sender, IUserContext userContext)
	{
		_sender = sender;
		_userContext = userContext;
	}
	
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> Get([FromQuery] ProductsRequest request, CancellationToken cancellationToken)
	{
		var userId = Guid.Parse(_userContext.Identity ?? Guid.Empty.ToString());
		var products = await _sender.Send(new GetProductsQuery(request, userId), cancellationToken);
		
		return Ok(products);
	}
	
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
	{
		var product = await _sender.Send(new GetProductQuery(id), cancellationToken);
		
		return product is null ? NotFound() : Ok(product);
	}
	
	[HttpPost]
	[Authorize(Policy = "admin")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> Post(CreateProductCommand command, CancellationToken cancellationToken)
	{
		var productId = await _sender.Send(command, cancellationToken);
		
		return CreatedAtAction(nameof(GetById), new { id = productId }, default);
	}
}