using B2bApi.Shared.Commands;
using B2bApi.Shared.Security.Abstractions;
using B2bApi.Shared.Security.Dtos;
using B2bApi.Users.Exceptions;
using B2bApi.Users.Repositories;

namespace B2bApi.Users.Commands.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, JwtDto>
{
	private readonly IUserRepository _userRepository;
	private readonly IPasswordManager _passwordManager;
	private readonly IAuthenticator _authenticator;
	
	public LoginCommandHandler(IUserRepository userRepository, IPasswordManager passwordManager, 
								IAuthenticator authenticator)
	{
		_userRepository = userRepository;
		_passwordManager = passwordManager;
		_authenticator = authenticator;
	}
	
	public async Task<JwtDto> Handle(LoginCommand command, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
		if(user is null)
		{
			throw new InvalidCredentialsException(command.Email);
		}
		if(!_passwordManager.Verify(command.Password, user.Password))
		{
			throw new InvalidCredentialsException(command.Email);
		}

		var result = _authenticator.CreateToken(user.Id, user.Email, user.Role, user.Currency.Code);
		return result;
	}
}