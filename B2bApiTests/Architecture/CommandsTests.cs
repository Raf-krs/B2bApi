using B2bApi.Shared.Commands;
using B2bApiTests.Utils.Architecture;
using NetArchTest.Rules;
using Shouldly;
using Xunit;

namespace B2bApiTests.Architecture;

public class CommandsTests : BaseArchitectureTest
{
	[Fact]
	public void Commands_Should_HaveCommandPostfix()
	{
		var result = Types
			.InAssembly(ApiAssembly)
			.That()
			.ImplementInterface(typeof(ICommand<>))
			.Should()
			.HaveNameEndingWith("Command")
			.GetResult();
		
		result.IsSuccessful.ShouldBeTrue();
	}
	
	[Fact]
	public void Commands_Should_BeSealed()
	{
		var result = Types
			.InAssembly(ApiAssembly)
			.That()
			.ImplementInterface(typeof(ICommand<>))
			.Should()
			.BeSealed()
			.GetResult();
		
		result.IsSuccessful.ShouldBeTrue();
	}
	
	[Fact]
	public void CommandsHandlers_Should_HaveCommandHandlerPostfix()
	{
		var result = Types
			.InAssembly(ApiAssembly)
			.That()
			.ImplementInterface(typeof(ICommandHandler<,>))
			.Should()
			.HaveNameEndingWith("CommandHandler")
			.GetResult();
		
		result.IsSuccessful.ShouldBeTrue();
	}
}