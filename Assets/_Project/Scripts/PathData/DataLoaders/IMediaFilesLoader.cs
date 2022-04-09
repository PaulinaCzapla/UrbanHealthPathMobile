using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Interface representing loader of media files.
    /// </summary>
    public interface IMediaFilesLoader
    {
        IList<MediaFile> LoadMediaFiles();
    }
}