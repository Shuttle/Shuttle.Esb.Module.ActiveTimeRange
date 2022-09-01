using System;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.Module.ActiveTimeRange
{
    public class ActiveTimeRangeModule
    {
        private readonly ActiveTimeRange _activeTimeRange;
        private readonly Type _shutdownPipelineType = typeof(ShutdownPipeline);
        private readonly Type _startupPipelineType = typeof(StartupPipeline);

        public ActiveTimeRangeModule(IOptions<ActiveTimeRangeOptions> activeTimeRangeOptions, IPipelineFactory pipelineFactory)
        {
            Guard.AgainstNull(activeTimeRangeOptions, nameof(activeTimeRangeOptions));
            Guard.AgainstNull(activeTimeRangeOptions.Value, nameof(activeTimeRangeOptions.Value));
            Guard.AgainstNull(pipelineFactory, nameof(pipelineFactory));

            _activeTimeRange = new ActiveTimeRange(activeTimeRangeOptions.Value.ActiveFromTime, activeTimeRangeOptions.Value.ActiveToTime);

            pipelineFactory.PipelineCreated += PipelineCreated;
        }

        private void PipelineCreated(object sender, PipelineEventArgs e)
        {
            var pipelineType = e.Pipeline.GetType();

            if (pipelineType == _startupPipelineType
                ||
                pipelineType == _shutdownPipelineType)
            {
                return;
            }

            e.Pipeline.RegisterObserver(new ActiveTimeRangeObserver(_activeTimeRange));
        }
    }
}