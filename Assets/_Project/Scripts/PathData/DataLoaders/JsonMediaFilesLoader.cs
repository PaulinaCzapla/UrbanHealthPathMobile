using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Loader of media files from JSON format.
    /// </summary>
    public class JsonMediaFilesLoader : JsonDataLoader<MediaFile>, IMediaFilesLoader
    {
        public JsonMediaFilesLoader(JToken json, IParser<JObject, MediaFile> mediaFileParser) : base(json, mediaFileParser)
        {
        }
        
        public IList<MediaFile> LoadMediaFiles()
        {
            return LoadJsonData();
        }
    }
}