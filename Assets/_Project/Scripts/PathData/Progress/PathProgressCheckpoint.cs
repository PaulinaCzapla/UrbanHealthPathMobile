using System;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    /// <summary>
    /// Class representing single path checkpoint - waypoint reached at given time.
    /// </summary>
    public class PathProgressCheckpoint
    {
        public string WaypointId { get; }
        public DateTime ReachedAt { get; }

        public PathProgressCheckpoint(string waypointId, DateTime reachedAt)
        {
            WaypointId = waypointId;
            ReachedAt = reachedAt;
        }
    }
}