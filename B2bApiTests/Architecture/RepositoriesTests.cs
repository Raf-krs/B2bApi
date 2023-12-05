using B2bApi.Shared.Repositories;
using B2bApiTests.Utils.Architecture;
using NetArchTest.Rules;
using Shouldly;
using Xunit;

namespace B2bApiTests.Architecture;

public class RepositoriesTests : BaseArchitectureTest
{
	[Fact]
	public void Repositories_Should_HaveRepositoryPostfix()
	{
		var result = Types
			.InAssembly(ApiAssembly)
			.That()
			.ImplementInterface(typeof(IRepository))
			.Should()
			.HaveNameEndingWith("Repository")
			.GetResult();
		
		result.IsSuccessful.ShouldBeTrue();
	}
}