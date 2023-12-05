using B2bApi.Shared.Exceptions;
using B2bApi.Users.ValueObjects;
using Shouldly;
using Xunit;

namespace B2bApiTests.Users.Unit.ValueObjects;

public class PasswordTests
{
	[Fact]
	public void CreatePassword_ShouldFail_WhenPasswordIsEmpty()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Password(string.Empty));
	}
	
	[Fact]
	public void CreatePassword_ShouldFail_WhenPasswordIsLessThan8Characters()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Password("aA1!"));
	}
	
	[Fact]
	public void CreatePassword_ShouldFail_WhenPasswordContainSpace()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Password("a A1!aaaasdsd"));
	}
	
	[Fact]
	public void CreatePassword_ShouldFail_WhenPasswordDoesNotContainUppercaseLetter()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Password("a1!aaaaaaa2"));
	}
	
	[Fact]
	public void CreatePassword_ShouldFail_WhenPasswordDoesNotContainLowercaseLetter()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Password("A1!AAAAAAA2"));
	}
	
	[Fact]
	public void CreatePassword_ShouldFail_WhenPasswordDoesNotContainNumber()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Password("aA!aaaaaaaa"));
	}
	
	[Fact]
	public void CreatePassword_ShouldFail_WhenPasswordDoesNotContainSpecialCharacter()
	{
		// Arrange & Act & Assert
		Should.Throw<InvalidInputException>(() => new Password("aA1aaaaaaaa"));
	}
	
	[Fact]
	public void CreatePassword_ShouldPass_WhenPasswordIsValid()
	{
		// Arrange & Act
		const string password = "aA1!aaaaaaaa";
		var sut = new Password(password);
		
		// Assert
		sut.Value.ShouldBe(password);
	}
}