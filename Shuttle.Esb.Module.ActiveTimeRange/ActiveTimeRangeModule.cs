using System;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;
using Shuttle.Core.Threading;

namespace Shuttle.Esb.Module.ActiveTimeRange
{
    public class ActiveTimeRangeModule : IDisposable, IThreadState
    {
        private readonly ActiveTimeRange _activeTimeRange;
        private readonly string _shutdownPipelineName = typeof(ShutdownPipeline).FullName;
        private readonly string _startupPipelineName = typeof(StartupPipeline).FullName;
        private volatile bool _active;

        public ActiveTimeRangeModule(IPipelineFactory pipelineFactory,
            IActiveTimeRangeConfiguration activeTimeRangeConfiguration)
        {
            Guard.AgainstNull(pipelineFactory, "pipelineFactory");
            Guard.AgainstNull(activeTimeRangeConfiguration, "activeTimeRangeConfiguration");

            _activeTimeRange = activeTimeRangeConfiguration.CreateActiveTimeRange();

            pipelineFactory.PipelineCreated += PipelineCreated;
        }

        public void Dispose()
        {
            _active = false;
        }

        public bool Active => _active;

        private void PipelineCreated(object sender, PipelineEventArgs e)
        {
            var pipelineName = e.Pipeline.GetType().FullName ?? string.Empty;

            if (pipelineName.Equals(_startupPipelineName, StringComparison.InvariantCultureIgnoreCase)
                ||
                pipelineName.Equals(_shutdownPipelineName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            e.Pipeline.RegisterObserver(new ActiveTimeRangeObserver(this, _activeTimeRange));
        }
    }
}