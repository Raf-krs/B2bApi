using B2bApi.Carts.Dtos;
using B2bApi.Shared.Repositories;

namespace B2bApi.Carts.Repositories.Cart;

public interface ICartRepository : IRepository
{
	Task<CartItemsResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task AddAsync(Entities.Cart cart, CancellationToken cancellationToken);
	Task UpdateAsync(Entities.Cart cart, CancellationToken cancellationToken);
	Task<Entities.Cart> GetByEfCoreIdAsync(Guid id, CancellationToken cancellationToken);
	Task DeleteAsync(Entities.Cart cart, CancellationToken cancellationToken);
}