using B2bApi.Points.Queries;
using B2bApi.Shared.Security.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2bApi.Points.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PointsController : ControllerBase
{
	private readonly ISender _sender;
	private readonly IUserContext _userContext;
	
	public PointsController(ISender sender, IUserContext userContext)
	{
		_sender = sender;
		_userContext = userContext;
	}
	
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetPoints(CancellationToken cancellationToken)
	{
		var userId = Guid.Parse(_userContext.Identity);
		var result = await _sender.Send(new GetPointsQuery(userId), cancellationToken);
		
		return Ok(result);
	}
}