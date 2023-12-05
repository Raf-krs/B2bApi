using B2bApi.Availabilities.Commands;
using B2bApi.Availabilities.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2bApi.Availabilities.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "admin")]
public class AvailabilitiesController : ControllerBase
{
	private readonly ISender _sender;
	
	public AvailabilitiesController(ISender sender)
	{
		_sender = sender;
	}
	
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var result = await _sender.Send(new GetAllQuery(), cancellationToken);
		return Ok(result);
	}
	
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> AddAsync(AddCommand command, CancellationToken cancellationToken = default)
	{
		var result = await _sender.Send(command, cancellationToken);
		return result.IsFailed ? NotFound(result.Errors) : Created();
	}
}