using B2bApi.Discounts.Entities;
using B2bApi.Shared.Repositories;

namespace B2bApi.Discounts.Repositories;

public interface IDiscountRepository : IRepository
{
	Task<IEnumerable<Discount>> GetAllAsync(CancellationToken cancellationToken);
	Task<Discount> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<bool> ExistsDiscountInDateRangeAsync(Guid productId, DateOnly start, CancellationToken cancellationToken);
	Task AddAsync(Discount discount, CancellationToken cancellationToken);
	Task DeleteAsync(Discount discount, CancellationToken cancellationToken);
}