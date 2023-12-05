using B2bApi.Shared.Exceptions;

namespace B2bApi.Shared.ValueObjects;

public record SortField
{
	public string Value { get; }
	private readonly string[] _allowedOrderBy = { "name", "price" };
	
	public SortField(string value)
	{
		if(string.IsNullOrEmpty(value))
		{
			Value = string.Empty;
			return;
		}
		if(string.IsNullOrEmpty(value) is false && _allowedOrderBy.Contains(value.ToLower()) is false)
		{
			throw new InvalidInputException($"Sort field must be one of {string.Join(';' + Environment.NewLine, _allowedOrderBy)}");
		}
		Value = value;
	}
	
	public static implicit operator string(SortField field) => field.Value;
	public static implicit operator SortField(string field) => new(field);
	public override string ToString() => Value;
}