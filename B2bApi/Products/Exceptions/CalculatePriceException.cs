using B2bApi.Shared.Exceptions;

namespace B2bApi.Products.Exceptions;

public class CalculatePriceException : CustomException
{
	public CalculatePriceException() : base("Price is null for some products") { }
}