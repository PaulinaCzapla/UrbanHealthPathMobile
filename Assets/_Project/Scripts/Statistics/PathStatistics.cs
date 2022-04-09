using System;

namespace PolSl.UrbanHealthPath.Statistics
{
    /// <summary>
    /// Class that holds statistics for a finished path.
    /// </summary>
    public class PathStatistics
    {
        public string PathId { get; }
        public DateTime FinishedAt { get; }
        public int VisitedPointsCount { get; }
        public int EstimatedDistance { get; }

        public PathStatistics(string pathId, DateTime finishedAt, int visitedPointsCount, int estimatedDistance)
        {
            PathId = pathId;
            FinishedAt = finishedAt;
            VisitedPointsCount = visitedPointsCount;
            EstimatedDistance = estimatedDistance;
        }
    }
}