namespace Shuttle.Esb.Module.ActiveTimeRange
{
	public class ActiveTimeRangeConfiguration : IActiveTimeRangeConfiguration
	{
		public string ActiveFromTime { get; set; }
		public string ActiveToTime { get; set; }

		public ActiveTimeRange CreateActiveTimeRange()
		{
			return new ActiveTimeRange(ActiveFromTime, ActiveToTime);
		}
	}
}