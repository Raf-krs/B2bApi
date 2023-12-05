using System.Reflection;
using B2bApi;

namespace B2bApiTests.Utils.Architecture;

public abstract class BaseArchitectureTest
{
	protected static readonly Assembly ApiAssembly = typeof(IApiMarker).Assembly;
}