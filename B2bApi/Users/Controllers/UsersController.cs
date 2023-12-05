using B2bApi.Shared.Security.Abstractions;
using B2bApi.Users.Commands.Login;
using B2bApi.Users.Commands.Password;
using B2bApi.Users.Commands.Register;
using B2bApi.Users.Entities;
using B2bApi.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2bApi.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
	private readonly ISender _sender;
	private readonly IUserContext _userContext;
	
	public UsersController(ISender sender, IUserContext userContext)
	{
		_sender = sender;
		_userContext = userContext;
	}
	
	/// <summary>
	/// Retrieves information about the authenticated user based on their email address.
	/// </summary>
	/// <returns>
	/// The user's details if the request is successful (HTTP 200 OK).
	/// </returns>
	/// <response code="200">Returns the user's details.</response>
	/// <response code="401">If the request is not authorized or the authorization token is invalid.</response>
	[HttpGet("me")]
	[Authorize]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Me(CancellationToken cancellationToken)
	{
		var query = new GetUserByEmailQuery(_userContext.Email);
		var user = await _sender.Send(query, cancellationToken);
		
		return Ok(user);
	}
	
	/// <summary>
	/// Registers a new user with the provided registration information.
	/// </summary>
	/// <param name="command">The registration command containing user information.</param>
	/// <returns>
	/// If the registration is successful, returns a response with HTTP 201 Created status and the user's unique identifier.
	/// </returns>
	/// <response code="201">Returns the user's unique identifier upon successful registration.</response>
	/// <response code="400">If the registration request is invalid or incomplete.</response>
	[HttpPost("register")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Register(RegisterCommand command, CancellationToken cancellationToken)
	{
		var id = await _sender.Send(command, cancellationToken);
		
		return Created("api/users/me", new { id });
	}

	/// <summary>
	/// Authenticates a user with the provided login credentials and returns an authentication token upon successful login.
	/// </summary>
	/// <param name="command">The login command containing user credentials.</param>
	/// <returns>
	/// If the login is successful, returns an authentication token with HTTP 200 OK status.
	/// </returns>
	/// <response code="200">Returns the authentication token upon successful login.</response>
	/// <response code="400">If the login request is invalid or incomplete.</response>
	[HttpPost("login")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Login(LoginCommand command, CancellationToken cancellationToken)
	{
		var token = await _sender.Send(command, cancellationToken);
		
		return Ok(token);
	}
	
	/// <summary>
	/// Changes the password of the authenticated user.
	/// </summary>
	/// <param name="command">The command containing the new password.</param>
	/// <returns>
	/// If the password change is successful, returns HTTP 202 Accepted status.
	/// </returns>
	/// <response code="202">Returns HTTP 202 Accepted upon successful password change.</response>
	/// <response code="400">If the password change request is invalid or incomplete.</response>
	/// <response code="401">If the request is not authorized or the authorization token is invalid.</response>
	[HttpPatch("me/password")]
	[ProducesResponseType(StatusCodes.Status202Accepted)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[Authorize]
	public async Task<IActionResult> ChangePassword(ChangePasswordCommand command, CancellationToken cancellationToken)
	{
		command = command with { Email = _userContext.Email };
		await _sender.Send(command, cancellationToken);
		
		return Accepted();
	}
}