namespace Shuttle.Esb.Module.ActiveTimeRange
{
	public interface IActiveTimeRangeConfiguration
	{
		string ActiveFromTime { get; }
		string ActiveToTime { get; }

		ActiveTimeRange CreateActiveTimeRange();
	}
}