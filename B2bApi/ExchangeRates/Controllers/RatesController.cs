using B2bApi.ExchangeRates.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace B2bApi.ExchangeRates.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatesController : ControllerBase
{
	private readonly ISender _sender;
	
	public RatesController(ISender sender)
	{
		_sender = sender;
	}
	
	[HttpGet("tables")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> GetExchangeRatesTable(CancellationToken cancellationToken)
	{
		var query = new GetRatesTableQuery();
		var result = await _sender.Send(query, cancellationToken);

		return Ok(result);
	}

	[HttpGet("code/{code}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> GetRatesByCode(string code, CancellationToken cancellationToken)
	{
		var query = new GetRatesByCodeQuery(code);
		var result = await _sender.Send(query, cancellationToken);

		return Ok(result);
	}
}