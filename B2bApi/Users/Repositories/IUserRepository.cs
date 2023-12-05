using B2bApi.Shared.Repositories;
using B2bApi.Users.Entities;

namespace B2bApi.Users.Repositories;

public interface IUserRepository : IRepository
{
	Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
	Task AddAsync(User user, CancellationToken cancellationToken = default);
	Task UpdateAsync(User user, CancellationToken cancellationToken = default);
	Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}