using Shuttle.Core.Container;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.Module.ActiveTimeRange
{
    public static class ComponentRegistryExtensions 
    {
        public static void RegisterActiveTimeRange(this IComponentRegistry registry)
        {
            if (!registry.IsRegistered<IActiveTimeRangeConfiguration>())
            {
                registry.AttemptRegisterInstance(ActiveTimeRangeSection.Configuration());
            }

            registry.AttemptRegister<ActiveTimeRangeModule>();
        }
    }
}