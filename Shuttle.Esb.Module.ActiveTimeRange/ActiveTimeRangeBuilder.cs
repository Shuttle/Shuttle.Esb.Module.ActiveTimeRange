using System;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.Module.ActiveTimeRange
{
    public class ActiveTimeRangeBuilder
    {
        private ActiveTimeRangeOptions _activeTimeRangeOptions = new ActiveTimeRangeOptions();

        public IServiceCollection Services { get; }

        public ActiveTimeRangeBuilder(IServiceCollection services)
        {
            Guard.AgainstNull(services, nameof(services));

            Services = services;
        }

        public ActiveTimeRangeOptions Options
        {
            get => _activeTimeRangeOptions;
            set => _activeTimeRangeOptions = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}