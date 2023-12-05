using B2bApi.Orders.Entities;
using B2bApi.Shared.Repositories;

namespace B2bApi.Orders.Repositories;

public interface IOrderRepository : IRepository
{
	Task AddAsync(Order order, CancellationToken cancellationToken);
	Task<IEnumerable<Order>> GetByUserId(Guid id, CancellationToken cancellationToken);
}