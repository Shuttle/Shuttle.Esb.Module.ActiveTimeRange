using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;
using Shuttle.Core.Threading;

namespace Shuttle.Esb.Module.ActiveTimeRange
{
	internal class ActiveTimeRangeObserver : IPipelineObserver<OnPipelineStarting>
	{
		private readonly IThreadState _state;

		private readonly ActiveTimeRange _activeTimeRange;

		public ActiveTimeRangeObserver(IThreadState state, ActiveTimeRange activeTimeRange)
		{
			Guard.AgainstNull(state, "state");

			_state = state;
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

			ThreadSleep.While(sleep, _state);
		}
	}
}