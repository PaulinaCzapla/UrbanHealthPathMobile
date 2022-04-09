using Mapbox.Unity.Map;

namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Initializes the map when the location is updated for the first time.
    /// </summary>
    public class DelayedMapInitializer
    {
        private readonly AbstractMap _map;
        private readonly ILocationUpdater _locationUpdater;
        
        public DelayedMapInitializer(AbstractMap map, ILocationUpdater locationUpdater)
        {
            _map = map;
            _locationUpdater = locationUpdater;
            _locationUpdater.LocationUpdated += InitializeMapAfterFirstLocationUpdate;
        }
        
        private void InitializeMapAfterFirstLocationUpdate(LocationUpdatedArgs args)
        {
            _locationUpdater.LocationUpdated -= InitializeMapAfterFirstLocationUpdate;
            _map.Initialize(args.Location.LatitudeLongitude, _map.AbsoluteZoom);
        }
    }
}
