using System;
using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;

namespace PolSl.UrbanHealthPath.Statistics
{
    /// <summary>
    /// Implementation of path statistics provider that uses path progress manager.
    /// </summary>
    public class FinishedPathStatisticsProvider : IFinishedPathStatisticsProvider
    {
        public PathStatistics GetFinishedPathStatistics(IPathProgressManager progressManager, UrbanPath path)
        {
            if (progressManager.IsPathInProgress)
            {
                throw new ArgumentException("Path is still in progress!", nameof(progressManager));
            }
            
            IList<Station> stations = path.GetWaypointsOfType<Station>();

            DateTime finishedAt = GetFinishedAt(progressManager);
            int visitedPointCount = GetVisitedPointCount(progressManager, stations);
            int estimatedDistance = GetEstimatedDistance(path.ApproximateDistanceInMeters, visitedPointCount, stations.Count);

            return new PathStatistics(path.PathId, finishedAt, visitedPointCount, estimatedDistance);
        }

        private DateTime GetFinishedAt(IPathProgressManager progressManager)
        {
            return progressManager.LastCheckpoint?.ReachedAt ?? DateTime.Now;
        }

        private int GetEstimatedDistance(int fullPathApproximateDistanceInMeters, int visitedPointCount, int stationCount)
        {
            return fullPathApproximateDistanceInMeters * visitedPointCount / stationCount;
        }

        private int GetVisitedPointCount(IPathProgressManager progressManager, IList<Station> stations)
        {
            if (progressManager.LastCheckpoint is null)
            {
                return 0;
            }

            Station lastStation = stations.First(x => x.WaypointId == progressManager.LastCheckpoint.WaypointId);
            
            return stations.IndexOf(lastStation) + 1;
        }
    }
}