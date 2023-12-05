using B2bApi.Shared.Exceptions;
using B2bApi.Users.ValueObjects;
using Shouldly;
using Xunit;

namespace B2bApiTests.Users.Unit.ValueObjects;

public class EmailTests
{
	[Fact]
	public void Create_ShouldFail_WhenEmailIsEmpty()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Email(string.Empty));
	}
	
	[Fact]
	public void  Create_ShouldFail_WhenEmailIsInvalid()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Email("test"));
	}
	
	[Fact]
	public void  Create_ShouldPass_WhenEmailIsValid()
	{
		// Arrange 
		const string email = "test@mail.com";
		var sut = new Email(email);
		
		// Act
		var result = sut.Value;
		
		// Assert
		result.ShouldBe(email);
	}
}