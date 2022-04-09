namespace PolSl.UrbanHealthPath.PathData.Progress
{
    /// <summary>
    /// Interface defining functionality for storage and retrieval of path progress.
    /// </summary>
    public interface IPathProgressPersistor
    {
        PathProgress LoadPathProgress();
        bool SavePathProgress(PathProgress progress);
    }
}