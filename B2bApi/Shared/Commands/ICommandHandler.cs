using MediatR;

namespace B2bApi.Shared.Commands;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult> 
	where TCommand : ICommand<TResult>;