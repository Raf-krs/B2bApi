using B2bApi.Shared.Commands;

namespace B2bApi.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(string Name, decimal Price, string Description) : ICommand<Guid>;