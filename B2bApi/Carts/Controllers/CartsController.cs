using B2bApi.Carts.Commands.Cart.Create;
using B2bApi.Carts.Commands.Cart.Delete;
using B2bApi.Carts.Commands.Cart.Update;
using B2bApi.Carts.Commands.Item.AddItem;
using B2bApi.Carts.Commands.Item.DeleteItem;
using B2bApi.Carts.Commands.Item.UpdateItem;
using B2bApi.Carts.Queries;
using B2bApi.Shared.Security.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2bApi.Carts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartsController : ControllerBase
{
	private readonly ISender _sender;
	private readonly IUserContext _userContext;
	
	public CartsController(ISender sender, IUserContext userContext)
	{
		_sender = sender;
		_userContext = userContext;
	}
	
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
	{
		var result = await _sender.Send(new GetCartByIdQuery(id), cancellationToken);
		
		return result.IsFailed ? NotFound(result.Errors) : Ok(result.Value);
	}
	
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(CreateCartCommand command, CancellationToken cancellationToken)
	{
		var result = await _sender.Send(command, cancellationToken);
		
		return CreatedAtAction(nameof(GetById), new { id = result }, result);
	}
	
	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status202Accepted)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(Guid id, UpdateCartCommand command, CancellationToken cancellationToken)
	{
		var result = await _sender.Send(command with { Id = id }, cancellationToken);
		
		return result.IsFailed ? BadRequest(result.Errors) : Accepted();
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
	{
		var command = new DeleteCartCommand(id);
		var result = await _sender.Send(command, cancellationToken);
		
		return result.IsFailed ? BadRequest(result.Errors) : NoContent();
	}
	
	[HttpPost("{id}/item")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> AddItem(Guid id, AddItemCommand command, CancellationToken cancellationToken)
	{
		var result = await _sender.Send(command with { CartId = id }, cancellationToken);
		
		return result.IsFailed ? NotFound(result.Errors) : Created();
	}
	
	[HttpPatch("item/{id}")]
	[ProducesResponseType(StatusCodes.Status202Accepted)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateItem(Guid id, UpdateItemCommand command, CancellationToken cancellationToken)
	{
		var result = await _sender.Send(command with { ItemId = id }, cancellationToken);
		
		return result.IsFailed ? NotFound(result.Errors) : Accepted();
	}
	
	[HttpDelete("item/{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> DeleteItem(Guid id, CancellationToken cancellationToken)
	{
		await _sender.Send(new DeleteItemCommand(id), cancellationToken);
		
		return NoContent();
	}
}