using B2bApi.Carts.Repositories.Cart;
using B2bApi.Shared.Clock;
using B2bApi.Shared.Commands;
using B2bApi.Shared.Security.Abstractions;

namespace B2bApi.Carts.Commands.Cart.Create;

public class CreateCartCommandHandler : ICommandHandler<CreateCartCommand, Guid>
{
	private readonly ICartRepository _cartRepository;
	private readonly IUserContext _userContext;
	private readonly IClock _clock;
	
	public CreateCartCommandHandler(ICartRepository cartRepository, IUserContext userContext, IClock clock)
	{
		_cartRepository = cartRepository;
		_userContext = userContext;
		_clock = clock;
	}

	public async Task<Guid> Handle(CreateCartCommand command, CancellationToken cancellationToken)
	{
		// INFO -> This is how we can get the user id from the token
		var userId = Guid.Parse(_userContext.Identity);
		var cart = Entities.Cart.Create(userId, null, command.Name, _userContext.Currency, 
										string.Empty, _clock.Current());
		await _cartRepository.AddAsync(cart, cancellationToken);
		
		return cart.Id;
	}
}