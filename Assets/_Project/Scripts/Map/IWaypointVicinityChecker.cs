using System;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Interface which implementations check whether the user's location is in the vicinity of a waypoint
    /// </summary>
    public interface IWaypointVicinityChecker
    {
        event Action EnteringVicinity;
        event Action LeavingVicinity;

        void SetReferenceWaypoint(Waypoint waypoint);
        bool CheckIfInWaypointVicinity(Coordinates coordinates);
    }
}