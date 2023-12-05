using B2bApi.Shared.Security.Abstractions;
using B2bApi.Shared.ValueObjects;
using B2bApi.Users.Entities;
using B2bApi.Users.ValueObjects;

namespace B2bApi.Shared.Db.Data.Seed;

public class AdminSeed
{
	private readonly AppDbContext _dbContext;
	private readonly IPasswordManager _passwordManager;
	
	public AdminSeed(AppDbContext dbContext, IPasswordManager passwordManager)
	{
		_dbContext = dbContext;
		_passwordManager = passwordManager;
	}

	public async Task Seed(CancellationToken cancellationToken)
	{
		var isAdmin = _dbContext.Users.Any(u => u.Role == Role.Admin);
		if(isAdmin is false)
		{
			var securePassword = _passwordManager.Secure("Strong_password_123!");
			var admin = User.Create(UserId.New(), "admin@mail.com", "Admin", securePassword, 
									Role.Admin, Currency.Pln.ToString());
			await _dbContext.Users.AddAsync(admin, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}