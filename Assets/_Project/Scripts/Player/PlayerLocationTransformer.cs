using Mapbox.Unity.Map;
using UnityEngine;
using PolSl.UrbanHealthPath.Map;


namespace PolSl.UrbanHealthPath.Player
{
    public class PlayerLocationTransformer : MonoBehaviour
    {
        [SerializeField] private AbstractMap _map;

        private ILocationProvider _locationProvider;

        private bool _mapInitliazed;

        private bool _initialized;

        public void Initialize(ILocationProvider locationProvider)
        {
            _map.OnInitialized += () => _mapInitliazed = true;
            _locationProvider = locationProvider;
            _locationProvider.LocationUpdated += LocationProviderLocationUpdated;
            _initialized = true;
        }

        void LocationProviderLocationUpdated(Mapbox.Unity.Location.Location location)
        {
            if (_mapInitliazed&&_initialized)
            {
                transform.position = _map.GeoToWorldPosition(location.LatitudeLongitude);
            }
        }
    }
}
