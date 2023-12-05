using B2bApi.Discounts.Commands.Create;
using B2bApi.Discounts.Commands.Delete;
using B2bApi.Discounts.Dtos.Requests;
using B2bApi.Discounts.Queries.GetDiscount;
using B2bApi.Discounts.Queries.GetDiscounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2bApi.Discounts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "admin")]
public class DiscountsController : ControllerBase
{
	private readonly ISender _sender;
	
	public DiscountsController(ISender sender)
	{
		_sender = sender;
	}
	
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> Get([FromQuery] GetDiscountsRequest request, CancellationToken cancellationToken)
	{
		var query = new GetDiscountsQuery(request);
		var discounts = await _sender.Send(query, cancellationToken);
		
		return Ok(discounts);
	}
	
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
	{
		var result = await _sender.Send(new GetDiscountQuery(id), cancellationToken);
		
		return result.IsFailed ? NotFound() : Ok(result);
	}
	
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Post(CreateDiscountCommand command, CancellationToken cancellationToken)
	{
		var result = await _sender.Send(command, cancellationToken);
		
		return result.IsFailed ? BadRequest(result.Errors) : Created();
	}
	
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
	{
		await _sender.Send(new DeleteDiscountCommand(id), cancellationToken);
		
		return NoContent();
	}
}