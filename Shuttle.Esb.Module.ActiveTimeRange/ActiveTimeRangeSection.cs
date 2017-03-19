using System.Configuration;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.ActiveTimeRange
{
	public class ActiveTimeRangeSection : ConfigurationSection
	{
		[ConfigurationProperty("from", IsRequired = false, DefaultValue = "*")]
		public string From
		{
			get { return (string)this["from"]; }
		}

		[ConfigurationProperty("to", IsRequired = false, DefaultValue = "*")]
		public string To
		{
			get { return (string)this["to"]; }
		}

		public static IActiveTimeRangeConfiguration Configuration()
		{
			var section = ConfigurationSectionProvider.Open<ActiveTimeRangeSection>("shuttle", "activeTimeRange");
			var configuration = new ActiveTimeRangeConfiguration();

			if (section != null)
			{
				configuration.ActiveFromTime = section.From;
				configuration.ActiveToTime = section.To;
			}

			return configuration;
		}
	}
}