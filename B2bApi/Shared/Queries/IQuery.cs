using MediatR;

namespace B2bApi.Shared.Queries;

public interface IQuery<out TResult> : IRequest<TResult>;