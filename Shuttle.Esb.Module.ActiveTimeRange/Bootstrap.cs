using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.ActiveTimeRange
{
	public class Bootstrap:
		IComponentRegistryBootstrap,
		IComponentResolverBootstrap
	{
		private static bool _registryBootstrapCalled;
		private static bool _resolverBootstrapCalled;

		public void Register(IComponentRegistry registry)
		{
			Guard.AgainstNull(registry, "registry");

			if (_registryBootstrapCalled)
			{
				return;
			}

			registry.AttemptRegister(ActiveTimeRangeSection.Configuration());
			registry.AttemptRegister<ActiveTimeRangeModule>();

			_registryBootstrapCalled = true;
		}

		public void Resolve(IComponentResolver resolver)
		{
			Guard.AgainstNull(resolver, "resolver");

			if (_resolverBootstrapCalled)
			{
				return;
			}

			resolver.Resolve<ActiveTimeRangeModule>();

			_resolverBootstrapCalled = true;
		}
	}
}