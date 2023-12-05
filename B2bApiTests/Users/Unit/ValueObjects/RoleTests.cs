using B2bApi.Shared.Exceptions;
using B2bApi.Users.ValueObjects;
using Shouldly;
using Xunit;

namespace B2bApiTests.Users.Unit.ValueObjects;

public class RoleTests
{
	[Fact]
	public void CreateRole_ShouldFail_WhenValueIsEmpty()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Role(string.Empty));
	}
	
	[Fact]
	public void CreateRole_ShouldFail_WhenValueDoesNotInAllowedValues()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Role("invalid"));
	}
	
	[Fact]
	public void CreateRole_ShouldPass_WhenValueIsInAllowedValues()
	{
		// Arrange
		const string role = "admin";
		
		// Act
		var sut = new Role(role);
		
		// Assert
		sut.Value.ShouldBe(role);
	}
}