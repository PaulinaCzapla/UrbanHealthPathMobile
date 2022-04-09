using System;
using Mapbox.Unity.Location;


namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Interface which implementations provide location of the player.
    /// </summary>
    public interface ILocationProvider
    {
        public Location GetLocation();
    }
}
