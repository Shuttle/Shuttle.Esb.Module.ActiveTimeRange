using System;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.Module.ActiveTimeRange
{
    public class ActiveTimeRangeModule
    {
        private readonly ActiveTimeRange _activeTimeRange;
        private readonly string _shutdownPipelineName = typeof(ShutdownPipeline).FullName;
        private readonly string _startupPipelineName = typeof(StartupPipeline).FullName;

        public ActiveTimeRangeModule(IPipelineFactory pipelineFactory,
            IActiveTimeRangeConfiguration activeTimeRangeConfiguration)
        {
            Guard.AgainstNull(pipelineFactory, nameof(pipelineFactory));
            Guard.AgainstNull(activeTimeRangeConfiguration, nameof(activeTimeRangeConfiguration));

            _activeTimeRange = activeTimeRangeConfiguration.CreateActiveTimeRange();

            pipelineFactory.PipelineCreated += PipelineCreated;
        }

        private void PipelineCreated(object sender, PipelineEventArgs e)
        {
            var pipelineName = e.Pipeline.GetType().FullName ?? string.Empty;

            if (pipelineName.Equals(_startupPipelineName, StringComparison.InvariantCultureIgnoreCase)
                ||
                pipelineName.Equals(_shutdownPipelineName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            e.Pipeline.RegisterObserver(new ActiveTimeRangeObserver(_activeTimeRange));
        }
    }
}