using B2bApi.Orders.Commands;
using B2bApi.Orders.Queries.GetOrders;
using B2bApi.Shared.Security.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2bApi.Orders.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
	private readonly ISender _sender;
	private readonly IUserContext _userContext;
	
	public OrdersController(ISender sender, IUserContext userContext)
	{
		_sender = sender;
		_userContext = userContext;
	}
	
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Get(CancellationToken cancellationToken)
	{
		var userId = Guid.Parse(_userContext.Identity);
		var result = await _sender.Send(new GetOrdersQuery(userId), cancellationToken);
		
		return Ok(result);
	}

	[HttpPost("create/{cartId}")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> CreateOrder(Guid cartId, CancellationToken cancellationToken)
	{
		var result = await _sender.Send(new CreateOrderCommand(cartId), cancellationToken);

		return result.IsFailed ? BadRequest(result.Errors) : Created();
	}
}