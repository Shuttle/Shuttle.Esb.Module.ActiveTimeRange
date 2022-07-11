using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.Module.ActiveTimeRange
{
    public static class ServiceBusBuilderExtensions
    {
        public static ServiceBusBuilder AddActiveTimeRangeModule(this ServiceBusBuilder serviceBusBuilder,
            Action<ActiveTimeRangeBuilder> builder = null)
        {
            Guard.AgainstNull(serviceBusBuilder, nameof(serviceBusBuilder));

            var activeTimeRangeBuilder = new ActiveTimeRangeBuilder(serviceBusBuilder.Services);

            builder?.Invoke(activeTimeRangeBuilder);

            serviceBusBuilder.Services.TryAddSingleton<ActiveTimeRangeModule, ActiveTimeRangeModule>();

            serviceBusBuilder.Services.AddOptions<ActiveTimeRangeOptions>().Configure(options =>
            {
                options.ActiveFromTime = activeTimeRangeBuilder.Options.ActiveFromTime;
                options.ActiveToTime = activeTimeRangeBuilder.Options.ActiveToTime;
            });

            serviceBusBuilder.AddModule<ActiveTimeRangeModule>();

            return serviceBusBuilder;
        }
    }
}