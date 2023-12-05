using B2bApi.Shared.Exceptions;
using B2bApi.Users.ValueObjects;
using Shouldly;
using Xunit;

namespace B2bApiTests.Users.Unit.ValueObjects;

public class UsernameTests
{
	[Fact]
	public void Create_ShouldFail_WhenUsernameIsEmpty()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Username(string.Empty));
	}
	
	[Fact]
	public void  Create_ShouldPass_WhenUsernameIsValid()
	{
		// Arrange 
		const string user = "TestUsername";
		var sut = new Username(user);
		
		// Act
		var result = sut.Value;
		
		// Assert
		result.ShouldBe(user);
	}
}