using B2bApi.Shared.Commands;
using B2bApi.Shared.Security.Abstractions;
using B2bApi.Users.Entities;
using B2bApi.Users.Repositories;
using MediatR;

namespace B2bApi.Users.Commands.Password;

public sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, Unit>
{
	private readonly IUserRepository _userRepository;
	private readonly IPasswordManager _passwordManager;
	
	public ChangePasswordCommandHandler(IUserRepository userRepository, IPasswordManager passwordManager)
	{
		_userRepository = userRepository;
		_passwordManager = passwordManager;
	}

	public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
		var updatedUser = User.UpdatePassword(user, _passwordManager.Secure(request.Password));
		await _userRepository.UpdateAsync(updatedUser, cancellationToken);
		
		return Unit.Value;
	}
}