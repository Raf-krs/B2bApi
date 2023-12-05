using B2bApi.Shared.Commands;
using B2bApi.Shared.Db;
using Humanizer;
using MediatR;

namespace B2bApi.Shared.Behaviors;

public class UnitOfWorkBehavior<TCommand, TResponse> 
	: IPipelineBehavior<TCommand, TResponse> where TCommand : class, ICommand<TResponse>
{
	private readonly AppDbContext _dbContext;
	private readonly ILogger<UnitOfWorkBehavior<TCommand, TResponse>> _logger;
	
	public UnitOfWorkBehavior(AppDbContext dbContext, ILogger<UnitOfWorkBehavior<TCommand, TResponse>> logger)
	{
		_dbContext = dbContext;
		_logger = logger;
	}

	public async Task<TResponse> Handle(TCommand command, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		var commandName = command.GetType().Name.Underscore();
		
		_logger.LogInformation("Processing command: {CommandName}", commandName);
		TResponse response;
		await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
		try
		{
			response = await next();
			await _dbContext.SaveChangesAsync(cancellationToken);
			await transaction.CommitAsync(cancellationToken);
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Error while processing command: {CommandName} with error: {ErrorMessage}", 
							commandName, e.Message);
			await transaction.RollbackAsync(cancellationToken);
			throw;
		}
		_logger.LogInformation("Completed command: {CommandName}", commandName);

		return response;
	}
}