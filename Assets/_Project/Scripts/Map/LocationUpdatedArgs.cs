using Mapbox.Unity.Location;

namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Arguments of Location Update
    /// </summary>
    public class LocationUpdatedArgs
    {
        public Location Location { get; }
        
        public LocationUpdatedArgs(Location location)
        {
            Location = location;
        }
    }
}