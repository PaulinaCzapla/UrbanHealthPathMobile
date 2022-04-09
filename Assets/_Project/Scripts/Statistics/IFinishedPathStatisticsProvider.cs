using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;

namespace PolSl.UrbanHealthPath.Statistics
{
    /// <summary>
    /// Interface defining method for retrieving statistics for a completed path.
    /// </summary>
    public interface IFinishedPathStatisticsProvider
    {
        PathStatistics GetFinishedPathStatistics(IPathProgressManager progressManager, UrbanPath path);
    }
}