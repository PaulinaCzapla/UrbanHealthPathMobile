using System;
using Mapbox.Unity.Map;
using UnityEngine;
using PolSl.UrbanHealthPath.Map;


namespace PolSl.UrbanHealthPath.Player
{
    /// <summary>
    /// Updates the location of the player to the position which gets converted from the Updated Location latitute and
    /// longitude.
    /// </summary>
    public class PlayerLocationTransformer : MonoBehaviour
    {
        private AbstractMap _map;

        private ILocationUpdater _locationUpdater;

        private bool _mapInitliazed;

        private bool _initialized;

        public void Initialize(AbstractMap map, ILocationUpdater locationUpdater)
        {
            _map = map;
            _map.OnInitialized += () => _mapInitliazed = true;
            _locationUpdater = locationUpdater;
            _locationUpdater.LocationUpdated += UpdatePosition;
            _initialized = true;
        }

        private void OnDestroy()
        {
            if (_initialized)
            {
                _locationUpdater.LocationUpdated -= UpdatePosition;
            }
        }

        void UpdatePosition(LocationUpdatedArgs args)
        {
            if (_mapInitliazed && _initialized)
            {
                transform.position = _map.GeoToWorldPosition(args.Location.LatitudeLongitude);
            }
        }
    }
}
