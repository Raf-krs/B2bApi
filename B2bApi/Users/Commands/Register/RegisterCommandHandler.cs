using B2bApi.Shared.Commands;
using B2bApi.Shared.Security.Abstractions;
using B2bApi.Users.Entities;
using B2bApi.Users.Events.UserCreate;
using B2bApi.Users.Exceptions;
using B2bApi.Users.Repositories;
using B2bApi.Users.ValueObjects;
using MediatR;

namespace B2bApi.Users.Commands.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
{
	private readonly IUserRepository _userRepository;
	private readonly IPasswordManager _passwordManager;
	private readonly IPublisher _publisher;
	
	public RegisterCommandHandler(IUserRepository userRepository, IPasswordManager passwordManager, 
								IPublisher publisher)
	{
		_userRepository = userRepository;
		_passwordManager = passwordManager;
		_publisher = publisher;
	}

	public async Task<Guid> Handle(RegisterCommand command, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
		if(user is not null)
		{
			throw new UserAlreadyExistsException(command.Email);
		}
		
		var newUser = User.Create(UserId.New(), command.Email, command.Username, 
								_passwordManager.Secure(command.Password), Role.User, command.Currency);
		await _userRepository.AddAsync(newUser, cancellationToken);
		_publisher.Publish(new UserCreateEvent { User = newUser }, cancellationToken);
		
		return newUser.Id;
	}
}