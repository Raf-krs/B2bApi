using MediatR;

namespace B2bApi.Users.Events.UserCreate;

public class UserCreateEventHandler : INotificationHandler<UserCreateEvent>
{
	private readonly ILogger<UserCreateEventHandler> _logger;
	private const string EventName = "UserCreate";
	
	public UserCreateEventHandler(ILogger<UserCreateEventHandler> logger)
	{
		_logger = logger;
	}
	
	public async Task Handle(UserCreateEvent @event, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Start processing {EventName} with mail: {Email} ", EventName, @event.User.Email);
		
		await Task.Delay(5000, cancellationToken); // simulate event processing
		
		_logger.LogInformation("Finished processing {EventName} with mail: {Email} ", EventName, @event.User.Email);
	}
}