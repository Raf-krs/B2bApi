using B2bApi.Shared.Repositories;

namespace B2bApi.Carts.Repositories.Item;

public interface ICartItemRepository : IRepository
{
	Task<Entities.Items> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task AddAsync(Entities.Items items, CancellationToken cancellationToken);
	Task UpdateAsync(Entities.Items items, CancellationToken cancellationToken);
	Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}