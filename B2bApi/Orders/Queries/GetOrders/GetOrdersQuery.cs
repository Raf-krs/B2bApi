using B2bApi.Orders.Entities;
using B2bApi.Shared.Queries;

namespace B2bApi.Orders.Queries.GetOrders;

public sealed record GetOrdersQuery(Guid UserId) : IQuery<IEnumerable<Order>>;