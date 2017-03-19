using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.ActiveTimeRange
{
    public class ActiveTimeRangeModule : IDisposable, IThreadState
    {
	    private readonly ActiveTimeRange _activeTimeRange;
        private readonly string _startupPipelineName = typeof (StartupPipeline).FullName;
        private volatile bool _active;

        public ActiveTimeRangeModule(IPipelineFactory pipelineFactory, IActiveTimeRangeConfiguration activeTimeRangeConfiguration )
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

        public bool Active
        {
            get { return _active; }
        }

        private void PipelineCreated(object sender, PipelineEventArgs e)
        {
            if (e.Pipeline.GetType().FullName.Equals(_startupPipelineName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            e.Pipeline.RegisterObserver(new ActiveTimeRangeObserver(this, _activeTimeRange));
        }
    }
}