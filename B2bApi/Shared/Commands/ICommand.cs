using MediatR;

namespace B2bApi.Shared.Commands;

public interface ICommand<out TResult> : IRequest<TResult>;
