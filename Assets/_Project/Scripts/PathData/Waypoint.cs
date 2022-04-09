using System;

namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Class that holds data for a waypoint - any point on path represented by coordinates.
    /// </summary>
    public abstract class Waypoint
    {
        public string WaypointId { get; }
        public Coordinates Coordinates { get; }
        public string ZoneName { get; }

        protected Waypoint(string waypointId, Coordinates coordinates, string zoneName)
        {
            WaypointId = waypointId;
            Coordinates = coordinates;
            ZoneName = zoneName;
        }
    }
}