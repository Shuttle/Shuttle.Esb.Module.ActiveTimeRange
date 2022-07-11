namespace Shuttle.Esb.Module.ActiveTimeRange
{
	public class ActiveTimeRangeOptions
	{
		public const string SectionName = "Shuttle:ServiceBus:Modules:ActiveTimeRange";

		public string ActiveFromTime { get; set; }
		public string ActiveToTime { get; set; }
	}
}