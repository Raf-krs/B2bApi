using System.Reflection;
using B2bApi.Shared.Abstractions.Data;
using B2bApiTests.Utils.Architecture;
using NetArchTest.Rules;
using Shouldly;
using Xunit;

namespace B2bApiTests.Architecture;

public class EntitiesTests : BaseArchitectureTest
{
	[Fact]
	public void Entities_Should_BeSealed()
	{
		var result = Types
			.InAssembly(ApiAssembly)
			.That()
			.ImplementInterface(typeof(IEntity))
			.Should()
			.BeSealed()
			.GetResult();
		
		result.IsSuccessful.ShouldBeTrue();
	}
	
	[Fact]
	public void Entities_Should_HavePrivateParameterlessConstructor()
	{
		var entities = Types
			.InAssembly(ApiAssembly)
			.That()
			.ImplementInterface(typeof(IEntity))
			.GetTypes();
		var failedTypes = new List<Type>();

		foreach(var entity in entities)
		{
			var constructors = entity.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
			if(!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
			{
				failedTypes.Add(entity);
			}
		}
		
		failedTypes.ShouldBeEmpty("Entity should have private parameterless constructor");
	}
	
	[Fact]
	public void Entities_Should_HavePrivateSetters()
	{
		var entities = Types
			.InAssembly(ApiAssembly)
			.That()
			.ImplementInterface(typeof(IEntity))
			.GetTypes();
		var failedTypes = new List<Type>();

		foreach (var entity in entities)
		{
			var properties = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (properties.Any(p => p.SetMethod != null && !p.SetMethod.IsPrivate))
			{
				failedTypes.Add(entity);
			}
		}

		failedTypes.ShouldBeEmpty("All properties should have private setters or no setter at all");
	}
}