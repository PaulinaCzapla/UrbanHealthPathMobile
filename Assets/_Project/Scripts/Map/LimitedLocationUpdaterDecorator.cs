using System;

namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Modifies ILocationUpdater behaviour to update location only when given predicate evaluates to true.
    /// </summary>
    public class LimitedLocationUpdaterDecorator : LocationUpdater
    {
        private readonly Func<bool> _canUpdate;

        public LimitedLocationUpdaterDecorator(ILocationProvider locationProvider, Func<bool> canUpdate, float delayBetweenUpdates = 1f) :
            base(locationProvider, delayBetweenUpdates)
        {
            _canUpdate = canUpdate;
        }

        protected override void ProcessUpdate()
        {
            if (_canUpdate.Invoke())
            {
                base.ProcessUpdate();
            }
        }
    }
}