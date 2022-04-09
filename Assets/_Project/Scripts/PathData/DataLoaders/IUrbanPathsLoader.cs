using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Interface representing loader of urban paths.
    /// </summary>
    public interface IUrbanPathsLoader
    {
        IList<UrbanPath> LoadUrbanPaths();
    }
}