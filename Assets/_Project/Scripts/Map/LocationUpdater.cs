using System;
using System.Collections;
using Mapbox.Unity.Location;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Updates the location of user.
    /// </summary>
    public class LocationUpdater : ILocationUpdater
    {
        public event Action<LocationUpdatedArgs> LocationUpdated;
        
        private readonly ILocationProvider _locationProvider;

        private readonly float _delayBetweenUpdates;
        private bool _stopUpdate = false;

        public LocationUpdater(ILocationProvider locationProvider, float delayBetweenUpdates = 0.5f)
        {
            _locationProvider = locationProvider;
            _delayBetweenUpdates = delayBetweenUpdates;
        }

        public IEnumerator UpdateLocation()
        {
            while (!_stopUpdate)
            {
                ProcessUpdate();
                yield return new WaitForSecondsRealtime(_delayBetweenUpdates);
            }
        }

        protected virtual void ProcessUpdate()
        {
            Location location = _locationProvider.GetLocation();
            LocationUpdated?.Invoke(new LocationUpdatedArgs(location));
        }
    }
}