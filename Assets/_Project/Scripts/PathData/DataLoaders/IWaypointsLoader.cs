using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Interface representing loader of waypoints.
    /// </summary>
    public interface IWaypointsLoader
    {
        IList<Waypoint> LoadWaypoints();
    }
}