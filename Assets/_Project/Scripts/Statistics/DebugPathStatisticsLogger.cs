using PolSl.UrbanHealthPath.Tools.TextLogger;

namespace PolSl.UrbanHealthPath.Statistics
{
    /// <summary>
    /// Path statistics logger that prints the data using text logger.
    /// </summary>
    public class DebugPathStatisticsLogger : IPathStatisticsLogger
    {
        private readonly ITextLogger _logger;

        public DebugPathStatisticsLogger(ITextLogger logger)
        {
            _logger = logger;
        }

        public void LogCompletedPathStatistics(PathStatistics pathStatistics)
        {
            LogMessage(pathStatistics, true);
        }

        public void LogCancelledPathStatistics(PathStatistics pathStatistics)
        {
            LogMessage(pathStatistics, false);
        }

        private void LogMessage(PathStatistics pathStatistics, bool wasPathCompleted)
        {
            _logger.Log(LogVerbosity.Debug,
                $"Path statistics - Completed: {wasPathCompleted.ToString()}, PathId: {pathStatistics.PathId}, FinishedAt: {pathStatistics.FinishedAt}, " +
                $"Visited points: {pathStatistics.VisitedPointsCount}, Estimated distance: {pathStatistics.EstimatedDistance}");
        }
    }
}