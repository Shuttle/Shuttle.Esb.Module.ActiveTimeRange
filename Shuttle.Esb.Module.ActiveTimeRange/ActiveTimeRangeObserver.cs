using Shuttle.Core.Pipelines;
using Shuttle.Core.Threading;

namespace Shuttle.Esb.Module.ActiveTimeRange
{
	internal class ActiveTimeRangeObserver : IPipelineObserver<OnPipelineStarting>
	{
		private readonly ActiveTimeRange _activeTimeRange;

		public ActiveTimeRangeObserver(ActiveTimeRange activeTimeRange)
		{
			_activeTimeRange = activeTimeRange;
		}

		public void Execute(OnPipelineStarting pipelineEvent)
		{
			const int sleep = 15000;

			if (_activeTimeRange.Active())
			{
				return;
			}

			pipelineEvent.Pipeline.Abort();

            ThreadSleep.While(sleep, pipelineEvent.Pipeline.State.GetActiveState());
		}
	}
}