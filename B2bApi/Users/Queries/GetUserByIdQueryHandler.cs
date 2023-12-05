using B2bApi.Shared.Queries;
using B2bApi.Users.Entities;
using B2bApi.Users.Repositories;

namespace B2bApi.Users.Queries;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByEmailQuery, User>
{
	private readonly IUserRepository _userRepository;
	
	public GetUserByIdQueryHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken) 
		=> await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
}