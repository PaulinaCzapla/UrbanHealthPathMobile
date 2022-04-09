namespace PolSl.UrbanHealthPath.Statistics
{
    /// <summary>
    /// Interface of path statistics logger factory that creates appropriate path statistics logger.
    /// </summary>
    public interface IPathStatisticsLoggerFactory
    {
        IPathStatisticsLogger GetLogger(bool debugMode = false);
    }
}