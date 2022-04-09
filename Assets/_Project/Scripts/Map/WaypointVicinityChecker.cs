using System;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.Utils.DistanceCalculator;

namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Checks whether the user is in the vicinity of the waypoint
    /// </summary>
    public class WaypointVicinityChecker : IWaypointVicinityChecker
    {
        public event Action EnteringVicinity;
        public event Action LeavingVicinity;

        private readonly IDistanceCalculator _distanceCalculator;
        private Waypoint _waypoint;
        private Distance _vicinityRange;
        private bool _wasInVicinity;

        public WaypointVicinityChecker(IDistanceCalculator distanceCalculator, Waypoint waypoint, Distance vicinityRange)
        {
            _distanceCalculator = distanceCalculator;
            _waypoint = waypoint;
            _vicinityRange = vicinityRange;
        }

        public void SetReferenceWaypoint(Waypoint waypoint)
        {
            _waypoint = waypoint;
            _wasInVicinity = false;
        }

        public bool CheckIfInWaypointVicinity(Coordinates coordinates)
        {
            IDistance distance = _distanceCalculator.CalculateDistance(_waypoint.Coordinates,
                coordinates);

            bool isInVicinity = distance.LessThan(distance);

            if (isInVicinity && !_wasInVicinity)
            {
                EnteringVicinity?.Invoke();
            }

            if (!isInVicinity && _wasInVicinity)
            {
                LeavingVicinity?.Invoke();
            }

            return isInVicinity;
        }
    }
}