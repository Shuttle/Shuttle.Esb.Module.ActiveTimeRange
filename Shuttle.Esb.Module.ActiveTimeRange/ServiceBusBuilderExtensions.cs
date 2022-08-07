using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

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

            serviceBusBuilder.Services.TryAddSingleton<ActiveTimeRangeObserver, ActiveTimeRangeObserver>();
            
            serviceBusBuilder.Services.AddOptions<ActiveTimeRangeOptions>().Configure(options =>
            {
                options.ActiveFromTime = activeTimeRangeBuilder.Options.ActiveFromTime;
                options.ActiveToTime = activeTimeRangeBuilder.Options.ActiveToTime;
            });

            serviceBusBuilder.Services.AddPipelineModule<ActiveTimeRangeModule>();

            return serviceBusBuilder;
        }
    }
}