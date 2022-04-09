namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Interface containing methods for creating all loaders.
    /// </summary>
    public interface ILoadersFactory
    {
        IMediaFilesLoader CreateMediaFilesLoader();
        IExercisesLoader CreateExercisesLoader();
        IWaypointsLoader CreateWaypointsLoader();
        IUrbanPathsLoader CreateUrbanPathsLoader();
        ITestLoader CreateTestLoader();
    }
}