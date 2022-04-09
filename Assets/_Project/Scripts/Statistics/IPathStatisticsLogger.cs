namespace PolSl.UrbanHealthPath.Statistics
{
    /// <summary>
    /// Interface defining functionality for path statistics logging.
    /// </summary>
    public interface IPathStatisticsLogger
    {
        void LogCompletedPathStatistics(PathStatistics pathStatistics);
        void LogCancelledPathStatistics(PathStatistics pathStatistics);
    }
}